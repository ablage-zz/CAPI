#region Using
using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

using DSELib.Audio.Wave.Enums;
#endregion

namespace DSELib.Audio.Wave
{
	public delegate void ProcessedEventHandler(object sender, byte[] lbData);

	internal class WaveInBuffer : IDisposable
    {
        #region Vars
        public WaveInBuffer NextBuffer = null;

        private AutoResetEvent m_RecordEvent = new AutoResetEvent(false);
        private IntPtr m_DeviceHandle = IntPtr.Zero;

        private Win32.WAVEHDR m_Header; // Datenstruktur für Win32-API
		private byte[] m_HeaderData;    // Headerdaten als Byte-Array

		private GCHandle m_HeaderHandle; // Handle für den gesamten Header
		private GCHandle m_HeaderDataHandle; // Handle für Daten des Headers

		private bool m_Recording; // Wird noch auf Daten gewartet?
        #endregion

        #region Win32-CallBack-Funktion
        internal static void WaveInProc(IntPtr hdrvr, int uMsg, int dwUser, ref Win32.WAVEHDR wavhdr, int dwParam2)
		{
            if (uMsg == (int)Win32.Enum_MM_MessageInput.MM_WIM_DATA)
			{
				try
				{
                    WaveInBuffer loBuffer = (WaveInBuffer)(((GCHandle)wavhdr.ClientInfo).Target); // Buffer-Instanz ermitteln
                    loBuffer.OnDataReceived(); // An Buffer: Daten erhalten
				}
				catch
				{
				}
			}
        }
        #endregion

        #region Constructor / Deconstructor
        public WaveInBuffer(IntPtr loDeviceHandle, int liSize)
		{
            m_DeviceHandle = loDeviceHandle;

            // Header auf App linken
			m_HeaderHandle = GCHandle.Alloc(m_Header, GCHandleType.Pinned);
			m_Header.ClientInfo = (IntPtr)GCHandle.Alloc(this);

            // Datenbereich vorbereiten
            m_HeaderData = new byte[liSize];
			m_HeaderDataHandle = GCHandle.Alloc(m_HeaderData, GCHandleType.Pinned);
			m_Header.Buffer = m_HeaderDataHandle.AddrOfPinnedObject();
            m_Header.BufferLength = liSize;
            CheckErrorCode(Win32.MM_Win32.waveInPrepareHeader(m_DeviceHandle, ref m_Header, Marshal.SizeOf(m_Header)));
		}
		~WaveInBuffer()
		{
			Dispose();
		}
		public void Dispose()
		{
			if (m_Header.Buffer != IntPtr.Zero)
			{
                Win32.MM_Win32.waveInUnprepareHeader(m_DeviceHandle, ref m_Header, Marshal.SizeOf(m_Header));
				m_HeaderHandle.Free();
				m_Header.Buffer = IntPtr.Zero;
			}

			m_RecordEvent.Close();
			
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
        public bool Recording
        {
            get { return m_Recording; }
        }
        #endregion

        #region Ereignis
        public bool RequestData()
		{
			lock(this)
			{
				m_RecordEvent.Reset();
                // Fordert neue Daten an
                m_Recording = (Win32.MM_Win32.waveInAddBuffer(m_DeviceHandle, ref m_Header, Marshal.SizeOf(m_Header)) == Win32.MM_Win32.MMSYSERR_NOERROR);
				return m_Recording;
			}
		}
		public void WaitForData()
		{
            // Wartet bis ein Ereignis ausgelöst wird (siehe OnDataReceived)
			if (m_Recording)
				m_Recording = m_RecordEvent.WaitOne();
			else
				Thread.Sleep(0);
		}

		private void OnDataReceived()
		{
            // Daten erhalten
			m_RecordEvent.Set();
			m_Recording = false;
		}
        #endregion
	}

	public class WaveInDevice : IDisposable
    {
        #region Variables
        private IntPtr m_DeviceHandle = IntPtr.Zero; // Handle zum geöffneten Aufnahme-Gerät

        private WaveInBuffer m_Buffer = null;
        private WaveInBuffer m_CurrentBuffer = null;
        private int m_BufferSize = 0; // Einstellung der Buffer-Größe

        private System.IO.Stream m_DataStream = null;
        
        private Thread m_Thread = null; // Listen-Thread
        private bool m_ThreadEnd = false; // Gibt das Ende des Threads an

        private byte[] lbTempBuffer = null; // Buffer von Eingabegeät

        // Win32-Callback
        private Win32.MM_Win32.WaveDelegate m_BufferProc = new Win32.MM_Win32.WaveDelegate(WaveInBuffer.WaveInProc);
        #endregion

        #region Events
        public ProcessedEventHandler OnProcessed;
        protected void onProcessed(IntPtr loData, int liSize)
        {
            if (((OnProcessed != null) || (m_DataStream != null)) && (liSize > 0)) {

                if (lbTempBuffer == null || lbTempBuffer.Length < liSize)
                    lbTempBuffer = new byte[liSize];

                Marshal.Copy(loData, lbTempBuffer, 0, liSize);

                if (m_DataStream != null) 
                {
                    m_DataStream.Write(lbTempBuffer, 0, lbTempBuffer.Length);
                }
                else
                {
                    OnProcessed(this, lbTempBuffer);
                }
            }
        }
        #endregion

        #region Constructor / Deconstructor
        public WaveInDevice(int liBufferSize)
        {
            Win32.WaveFormat loFormat = new DSELib.Audio.Wave.Win32.WaveFormat(Enum_SampleRates.SampleRates_44_1_kHz, Enum_BitRates.BitRates_16bit, Enum_Channels.Channels_Mono);
            m_BufferSize = liBufferSize;

            // CallBack einrichten
            int liReplyCode = Win32.MM_Win32.waveInOpen(out m_DeviceHandle, -1, loFormat, m_BufferProc, 0, Win32.MM_Win32.CALLBACK_FUNCTION);
            CheckErrorCode(liReplyCode);
        }
        public WaveInDevice(Win32.WaveFormat loFormat, int liBufferSize)
        {
            m_BufferSize = liBufferSize;

            // CallBack einrichten
            int liReplyCode = Win32.MM_Win32.waveInOpen(out m_DeviceHandle, -1, loFormat, m_BufferProc, 0, Win32.MM_Win32.CALLBACK_FUNCTION);
            CheckErrorCode(liReplyCode);
        }
        public WaveInDevice(int liDevice, Win32.WaveFormat loFormat, int liBufferSize)
        {
            m_BufferSize = liBufferSize;

            // CallBack einrichten
            int liReplyCode = Win32.MM_Win32.waveInOpen(out m_DeviceHandle, liDevice, loFormat, m_BufferProc, 0, Win32.MM_Win32.CALLBACK_FUNCTION);
            CheckErrorCode(liReplyCode);
        }
        ~WaveInDevice()
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
                        Win32.MM_Win32.waveInClose(m_DeviceHandle);
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
                return m_DataStream;
            }
            set
            {
                if (value.CanWrite == false)
                {
                    throw new ArgumentException("Cannot write to stream.");
                }
                else
                {
                    m_DataStream = value;
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
                                Win32.MM_Win32.waveInReset(m_DeviceHandle);
                            }
                            else
                            {
                                throw new Exception("Device is not initialised.");
                            }

                            // Warten bis der Verbindungsabbau durchgeführt ist
                            WaitTilAllDone();
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

                        RequestDataFromAll();

                        // Empfang starten
                        int liReplyCode = Win32.MM_Win32.waveInStart(m_DeviceHandle);
                        CheckErrorCode(liReplyCode);

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
                return Win32.MM_Win32.waveInGetNumDevs();
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
        
        #region Listen-Thread
        private void ThreadProc()
		{
            while (!m_ThreadEnd)
			{
                SwitchBuffer();
                m_CurrentBuffer.WaitForData();

                SwitchBuffer();

                if (!m_ThreadEnd)
                {
                    onProcessed(m_CurrentBuffer.Data, m_CurrentBuffer.Size);
                    m_CurrentBuffer.RequestData();
                }
            }
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
            m_Buffer = new WaveInBuffer(m_DeviceHandle, liBufferSize);
            WaveInBuffer loPreviousBuffer = m_Buffer;
            try
            {
                for (int i = 1; i < 3; i++)
                {
                    WaveInBuffer loLocalBuffer = new WaveInBuffer(m_DeviceHandle, liBufferSize);
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
                WaveInBuffer loCurrentBuffer = m_Buffer.NextBuffer;
                do
                {
                    WaveInBuffer loNextBuffer = loCurrentBuffer.NextBuffer;
                    loCurrentBuffer.Dispose();
                    loCurrentBuffer = loNextBuffer;
                } while (loCurrentBuffer != m_Buffer);

                m_Buffer.Dispose();
                m_Buffer = null;
            }
        }
        private void RequestDataFromAll()
        {
            WaveInBuffer loCurrentBuffer = m_Buffer.NextBuffer;
            do
            {
                WaveInBuffer loNextBuffer = loCurrentBuffer.NextBuffer;
                loCurrentBuffer.RequestData();
                loCurrentBuffer = loNextBuffer;
            } while (loCurrentBuffer != m_Buffer);
            m_Buffer.RequestData();
        }
        private void WaitTilAllDone()
        {
            if (m_Buffer.Recording) m_Buffer.WaitForData();
            WaveInBuffer loCurrentBuffer = m_Buffer.NextBuffer;
            do
            {
                WaveInBuffer loNextBuffer = loCurrentBuffer.NextBuffer;
                if (loCurrentBuffer.Recording) loCurrentBuffer.WaitForData();
                loCurrentBuffer = loNextBuffer;
            } while (loCurrentBuffer != m_Buffer);
        }
        #endregion
    }
}
