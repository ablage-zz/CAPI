using System;
using System.Runtime.InteropServices;
using System.IO;

namespace DSELib.Audio.Wave.Win32
{
    internal enum Enum_MM_MessageInput
    {
        MM_WIM_OPEN  = 0x3BE, // Eingabe-Gerät wurde geöffnet (wParam = device-handle)
        MM_WIM_CLOSE = 0x3BF, // Eingabe-Gerät wurde geschlossen (wParam = device-handle)
        MM_WIM_DATA  = 0x3C0  // Eingabe-Gerät hat Arbeit verrichtet (wParam = device-handle, lParam = Pointer zum Puffer)
    }
    internal enum Enum_MM_MessageOutput
    {
        MM_WOM_OPEN  = 0x3BB, // Ausgabe-Gerät wurde geöffnet (wParam = device-handle)
        MM_WOM_CLOSE = 0x3BC, // Ausgabe-Gerät wurde geschlossen (wParam = device-handle)
        MM_WOM_DONE  = 0x3BD  // Ausgabe-Gerät hat Arbeit verrichtet (wParam = device-handle, lParam = Pointer zum Puffer)
    }

    [StructLayout(LayoutKind.Sequential)]
    public class WaveFormat
    {
        public short AudioFormat = (short)Enums.Enum_WaveFormats.WAVE_FORMAT_PCM; // Codec für Sound
        public short Channels = (short)Enums.Enum_Channels.Channels_Stereo;

        public int SampleRate = (int)Enums.Enum_SampleRates.SampleRates_44_1_kHz; // Sample-Rate

        public int BytesPerSecond = 0; // Durchschnittliche Datenrate
        public short BlockSize = 0; // Größe pro Block

        public short BitRate = (short)Enums.Enum_BitRates.BitRates_16bit; // Bit-Rate

        public short ExtendedSize = 0; // Keine weiteren Informationen

        public WaveFormat()
        {
            CalcValues();
        }
        public WaveFormat(BinaryReader loReader, bool lbReadSize)
        {
            ReadFormat(loReader, true);
            CalcValues();
        }
        public WaveFormat(Enums.Enum_SampleRates loSampleRate, Enums.Enum_BitRates loBitRate, Enums.Enum_Channels loChannels)
        {
            this.Channels = (short)loChannels;
            this.SampleRate = (int)loSampleRate;
            this.BitRate = (short)loBitRate;
            CalcValues();
        }

        public bool ReadFormat(BinaryReader loReader, bool lbReadSize) {
            if (lbReadSize) loReader.ReadUInt16();
            AudioFormat = loReader.ReadInt16();
            Channels = loReader.ReadInt16();
            SampleRate = loReader.ReadInt32();
            BytesPerSecond = loReader.ReadInt32();
            BlockSize = loReader.ReadInt16();
            BitRate = loReader.ReadInt16();
            loReader.ReadInt16(); // BlockSize
            return true;
        }
        public bool WriteFormat(BinaryWriter loWriter, bool lbWriteSize)
        {
            if (lbWriteSize) loWriter.Write((uint)18);
            loWriter.Write(AudioFormat);
            loWriter.Write(Channels);
            loWriter.Write((uint)SampleRate);
            loWriter.Write((uint)BytesPerSecond);
            loWriter.Write(BlockSize);
            loWriter.Write(BitRate);
            loWriter.Write(BlockSize);
            return true;
        }
        private void CalcValues()
        {
            this.BlockSize = (short)(this.Channels * (this.BitRate / 8));
            this.BytesPerSecond = this.SampleRate * this.BlockSize;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WAVEHDR
    {
        public IntPtr Buffer; // Pointer zum Puffer
        public int BufferLength; // Länge des Puffers
        public int BytesRecorded; // [in] Anzahl Bytes, die aufgenommen wurden
        public IntPtr ClientInfo; // Daten für Client (nicht genutzt)
        public int Flags; // Flags (siehe Enum_BufferInfo)
        public int Loops; // [out] Anzahl der Wiederholungen
        public IntPtr ReservedPtr; // Reserviert
        public int Reserved; // Reserviert
    }

	internal class MM_Win32
    {
        #region DLL-Importe
        // Wave-Ausgabe
        [DllImport("winmm.dll")]
        public static extern int waveOutGetNumDevs();
        [DllImport("winmm.dll")]
        public static extern int waveOutPrepareHeader(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);
        [DllImport("winmm.dll")]
        public static extern int waveOutUnprepareHeader(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);
        [DllImport("winmm.dll")]
        public static extern int waveOutWrite(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);
        [DllImport("winmm.dll")]
        public static extern int waveOutOpen(out IntPtr hWaveOut, int uDeviceID, WaveFormat lpFormat, WaveDelegate dwCallback, int dwInstance, int dwFlags);
        [DllImport("winmm.dll")]
        public static extern int waveOutReset(IntPtr hWaveOut);
        [DllImport("winmm.dll")]
        public static extern int waveOutClose(IntPtr hWaveOut);
        [DllImport("winmm.dll")]
        public static extern int waveOutPause(IntPtr hWaveOut);
        [DllImport("winmm.dll")]
        public static extern int waveOutRestart(IntPtr hWaveOut);
        [DllImport("winmm.dll")]
        public static extern int waveOutGetPosition(IntPtr hWaveOut, out int lpInfo, int uSize);
        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hWaveOut, int dwVolume);
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hWaveOut, out int dwVolume);

        // Wave-Aufnahme
        [DllImport("winmm.dll")]
        public static extern int waveInGetNumDevs();
        [DllImport("winmm.dll")]
        public static extern int waveInAddBuffer(IntPtr hwi, ref WAVEHDR pwh, int cbwh);
        [DllImport("winmm.dll")]
        public static extern int waveInClose(IntPtr hwi);
        [DllImport("winmm.dll")]
        public static extern int waveInOpen(out IntPtr phwi, int uDeviceID, WaveFormat lpFormat, WaveDelegate dwCallback, int dwInstance, int dwFlags);
        [DllImport("winmm.dll")]
        public static extern int waveInPrepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm.dll")]
        public static extern int waveInUnprepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm.dll")]
        public static extern int waveInReset(IntPtr hwi);
        [DllImport("winmm.dll")]
        public static extern int waveInStart(IntPtr hwi);
        [DllImport("winmm.dll")]
        public static extern int waveInStop(IntPtr hwi);
        #endregion

        #region Konstanten
        public const int MMSYSERR_NOERROR = 0; // Kein Fehler
        public const int CALLBACK_FUNCTION = 0x00030000; // CallBack-Funktion wird angegeben [Status]
        #endregion

        // callbacks
        public delegate void WaveDelegate(IntPtr hdrvr, int uMsg, int dwUser, ref WAVEHDR wavhdr, int dwParam2);
	}
}
