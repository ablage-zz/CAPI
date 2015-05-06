#region Using
using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

using DSELib.Audio.Wave.Enums;
#endregion

namespace DSELib.Audio.Wave
{
    public delegate void SendingEventHandler(object sender, byte[] lbData);

	internal class WaveOutBuffer : IDisposable
	{
        #region Vars
        public WaveOutBuffer NextBuffer = null;

        private AutoResetEvent m_PlayEvent = new AutoResetEvent(false);
        private IntPtr m_DeviceHandle = IntPtr.Zero;

        private Win32.WAVEHDR m_Header;
        private byte[] m_HeaderData;
        private GCHandle m_HeaderHandle;
        private GCHandle m_HeaderDataHandle;

        private bool m_Playing;
        #endregion
        
        #region Win32-CallBack-Funktion
        internal static void WaveOutProc(IntPtr hdrvr, int uMsg, int dwUser, ref Win32.WAVEHDR wavhdr, int dwParam2)
        {
            if (uMsg == (int)Win32.Enum_MM_MessageOutput.MM_WOM_DONE)
            {
                try
                {
                    WaveOutBuffer loBuffer = (WaveOutBuffer)(((GCHandle)wavhdr.ClientInfo).Target);
                    loBuffer.OnDataSent();
                }
                catch
                {
                }
            }
        }
        #endregion

        #region Constructor / Deconstructor
        public WaveOutBuffer(IntPtr loDeviceHandle, int liSize)
		{
            m_DeviceHandle = loDeviceHandle;

            // Header auf App linken
            m_HeaderHandle = GCHandle.Alloc(m_Header, GCHandleType.Pinned);
            m_Header.ClientInfo = (IntPtr)GCHandle.Alloc(this);

            // Datenbereich Vorbereiten
            m_HeaderData = new byte[liSize];
            m_HeaderDataHandle = GCHandle.Alloc(m_HeaderData, GCHandleType.Pinned);
            m_Header.Buffer = m_HeaderDataHandle.AddrOfPinnedObject();
            m_Header.BufferLength = liSize;
            CheckErrorCode(Win32.MM_Win32.waveOutPrepareHeader(m_DeviceHandle, ref m_Header, Marshal.SizeOf(m_Header)));
		}
        ~WaveOutBuffer()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (m_Header.Buffer != IntPtr.Zero)
            {
                Win32.MM_Win32.waveOutUnprepareHeader(m_DeviceHandle, ref m_Header, Marshal.SizeOf(m_Header));
                m_HeaderHandle.Free();
                m_Header.Buffer = IntPtr.Zero;
            }

            m_PlayEvent.Close();
            
            if (m_HeaderDataHandle.IsAllocated)
                m_HeaderDataHandle.Free();
            
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ErrorCheck
        private static void CheckErrorCode(int liErrNmbr)
        {
            if (liErrNmbr != Win32.MM_Win32.MMSYSERR_NOERROR)
            {
                throw new Exception("Error occurred: " + liErrNmbr.ToString());
            }
        }
        #endregion

        #region Properties
        public int Size
        {
            get { return m_Header.BufferLength; }
        }
        public IntPtr Data
        {
            get { return m_Header.Buffer; }
        }
        #endregion

        #region Ereignis
        public bool RequestForSending()
        {
            lock(this)
            {
                m_PlayEvent.Reset();
                m_Playing = (Win32.MM_Win32.waveOutWrite(m_DeviceHandle, ref m_Header, Marshal.SizeOf(m_Header)) == Win32.MM_Win32.MMSYSERR_NOERROR);
                return m_Playing;
            }
        }
        public void WaitForData()
        {
            if (m_Playing)
            {
                m_Playing = m_PlayEvent.WaitOne();
            }
            else
            {
                Thread.Sleep(0);
            }
        }
        public void OnDataSent()
        {
            m_PlayEvent.Set();
            m_Playing = false;
        }
        #endregion
    }

    public class WaveOutDevice : IDisposable
    {
        #region Vars
        private IntPtr m_DeviceHandle = IntPtr.Zero;

        private WaveOutBuffer m_Buffer = null;
        private WaveOutBuffer m_CurrentBuffer = null;
        private int m_BufferSize = 0;

        private System.IO.Stream m_OutData;
        private byte[] m_TempBytes = null;

        private Thread m_Thread; 
        private bool m_ThreadEnd; // Wird Thread angehalten?
		
        private byte m_ZeroBase = 0; // Nullpunkt bei aktueller Bitrate

        private Win32.MM_Win32.WaveDelegate m_BufferProc = new Win32.MM_Win32.WaveDelegate(WaveOutBuffer.WaveOutProc);
        #endregion

        #region Constructor / Deconstructor
        public WaveOutDevice(int liBufferSize)
        {
            Win32.WaveFormat loFormat = new DSELib.Audio.Wave.Win32.WaveFormat(Enum_SampleRates.SampleRates_44_1_kHz, Enum_BitRates.BitRates_16bit, Enum_Channels.Channels_Mono);
            m_ZeroBase = (loFormat.BitRate == 8 ? (byte)128 : (byte)0);
            m_BufferSize = liBufferSize;

            // CallBack einrichten
            int liReplyCode = Win32.MM_Win32.waveOutOpen(out m_DeviceHandle, -1, loFormat, m_BufferProc, 0, Win32.MM_Win32.CALLBACK_FUNCTION);
            CheckErrorCode(liReplyCode);
        }
        public WaveOutDevice(Win32.WaveFormat loFormat, int liBufferSize)
        {
            m_ZeroBase = (loFormat.BitRate == 8 ? (byte)128 : (byte)0);
            m_BufferSize = liBufferSize;

            // CallBack einrichten
            int liReplyCode = Win32.MM_Win32.waveOutOpen(out m_DeviceHandle, -1, loFormat, m_BufferProc, 0, Win32.MM_Win32.CALLBACK_FUNCTION);
            CheckErrorCode(liReplyCode);
        }
        public WaveOutDevice(int liDevice, Win32.WaveFormat loFormat, int liBufferSize)
        {
            m_ZeroBase = (loFormat.BitRate == 8 ? (byte)128 : (byte)0);
            m_BufferSize = liBufferSize;

            // CallBack einrichten
            int liReplyCode = Win32.MM_Win32.waveOutOpen(out m_DeviceHandle, liDevice, loFormat, m_BufferProc, 0, Win32.MM_Win32.CALLBACK_FUNCTION);
            CheckErrorCode(liReplyCode);
        }
        ~WaveOutDevice()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (m_Thread != null)
            {
                try
                {
                    Active = false;

                    if (m_DeviceHandle != IntPtr.Zero)
                        Win32.MM_Win32.waveOutClose(m_DeviceHandle);
                }
                finally
                {
                    m_DeviceHandle = IntPtr.Zero;
                }
            }
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Properties
        public Stream DataStream
        {
            get
            {
                return m_OutData;
            }
            set
            {
                if (value.CanRead == false)
                {
                    throw new ArgumentException("Cannot read from stream.");
                }
                else
                {
                    m_OutData = value;
                }
            }
        }
        public bool Active
        {
            get
            {
                return (m_Thread != null);
            }
            set
            {
                if (value == false)
                {
                    if (Active == true)
                    {
                        try
                        {
                            m_ThreadEnd = true;
                            if (m_DeviceHandle != IntPtr.Zero)
                            {
                                Win32.MM_Win32.waveOutReset(m_DeviceHandle);
                            }
                            else
                            {
                                throw new Exception("Device is not initialised.");
                            }

                            m_Thread.Join();

                            ClearBuffer();
                        }
                        finally
                        {
                            m_Thread = null;
                        }
                    }
                }
                else
                {
                    if (Active == false)
                    {
                        CreateBuffer(m_BufferSize);

                        //m_CurrentBuffer.RequestForSending();

                        // Thread starten
                        m_Thread = new Thread(new ThreadStart(ThreadProc));
                        m_Thread.Start();
                    }
                }
            }
        }

        // Gibt Anzahl der Eingabe-Geräte zurück
        public static int DeviceCount
        {
            get
            {
                return Win32.MM_Win32.waveOutGetNumDevs();
            }
        }
        #endregion

        #region ErrorCheck
        // Überprüft den Fehlercode der API-Funktionen und erzeugt evtl. eine Exception
        private void CheckErrorCode(int liErrNmbr)
        {
            if (liErrNmbr != Win32.MM_Win32.MMSYSERR_NOERROR)
            {
                m_DeviceHandle = IntPtr.Zero;
                throw new Exception("Error occurred: " + liErrNmbr.ToString());
            }
        }
        #endregion

        #region Sending
        public void Send(byte[] lbOutData)
        {
            lock ("WaveOutSperre")
            {
//                m_OutData = lbOutData;
            }
        }
        #endregion

        #region Send-Thread
        private void ThreadProc()
        {
            while (!m_ThreadEnd)
            {
                SwitchBuffer();
                m_CurrentBuffer.WaitForData();

                if ((m_ThreadEnd) || (m_OutData == null) || (m_OutData.Length == 0))
                {
                    // Letzten Daten mit Nullen füllen
                    byte[] lbTemp = new byte[1];
                    lbTemp[0] = m_ZeroBase;
                    Marshal.Copy(lbTemp, 0, m_CurrentBuffer.Data, lbTemp.Length);
                }
                else
                {
                    if (m_OutData.Length > m_CurrentBuffer.Size)
                    {
                        if (m_TempBytes == null || m_TempBytes.Length < m_CurrentBuffer.Size)
                            m_TempBytes = new byte[m_CurrentBuffer.Size];
                        m_OutData.Read(m_TempBytes, 0, m_CurrentBuffer.Size);

                        System.Runtime.InteropServices.Marshal.Copy(m_TempBytes, 0, m_CurrentBuffer.Data, m_CurrentBuffer.Size);
                    }
                    else
                    {
                        if (m_TempBytes == null || m_TempBytes.Length < m_OutData.Length)
                            m_TempBytes = new byte[m_OutData.Length];

                        byte[] lbTemp = new byte[1];
                        lbTemp[0] = m_ZeroBase;
                        Marshal.Copy(lbTemp, 0, m_CurrentBuffer.Data, lbTemp.Length);

                        System.Runtime.InteropServices.Marshal.Copy(m_TempBytes, 0, m_CurrentBuffer.Data, (int)lbTemp.Length);
                    }
                }
                m_CurrentBuffer.RequestForSending();
            }
            m_CurrentBuffer.WaitForData();
        }
        #endregion

        #region Buffer-Handling
        private void SwitchBuffer()
        {
            m_CurrentBuffer = m_CurrentBuffer.NextBuffer;
        }
        private void CreateBuffer(int liBufferSize)
        {
            ClearBuffer();
            m_Buffer = new WaveOutBuffer(m_DeviceHandle, liBufferSize);
            WaveOutBuffer loPreviousBuffer = m_Buffer;
            try
            {
                for (int i = 1; i < 3; i++)
                {
                    WaveOutBuffer loLocalBuffer = new WaveOutBuffer(m_DeviceHandle, liBufferSize);
                    loPreviousBuffer.NextBuffer = loLocalBuffer;
                    loPreviousBuffer = loLocalBuffer;
                }
            }
            finally
            {
                loPreviousBuffer.NextBuffer = m_Buffer;
            }
            m_CurrentBuffer = m_Buffer;
        }
        private void ClearBuffer()
        {
            if (m_Buffer != null)
            {
                WaveOutBuffer loCurrentBuffer = m_Buffer.NextBuffer;
                do
                {
                    WaveOutBuffer loNextBuffer = loCurrentBuffer.NextBuffer;
                    loCurrentBuffer.Dispose();
                    loCurrentBuffer = loNextBuffer;
                } while (loCurrentBuffer != m_Buffer);

                m_Buffer.Dispose();
                m_Buffer = null;
            }
        }
        #endregion
    }
}
