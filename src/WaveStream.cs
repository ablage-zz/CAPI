#region Using
using System;
using System.IO;
#endregion

namespace DSELib.Audio.Wave
{
	public class WaveStreamReader : Stream, IDisposable
    {
        #region Vars
        private Stream m_Stream; // Genutzter Stream (Base-Stream wird nicht genutzt)
		private long m_DataPos;  // Index der Daten im Stream
		private long m_Length;   // Länge der Daten

		private Win32.WaveFormat m_Format;
        #endregion

        #region Header-lesen
        private string ReadChunk(BinaryReader loReader)
		{
			byte[] lbData = new byte[4];

            loReader.Read(lbData, 0, lbData.Length);
            return System.Text.Encoding.ASCII.GetString(lbData);
		}

		private void ReadHeader()
		{
			BinaryReader loReader = new BinaryReader(m_Stream);

            string lsFormat = ReadChunk(loReader); // Ersten Block lesen
            if (lsFormat != "RIFF") throw new Exception("Invalid file format. '" + lsFormat + "' not supported.");

            loReader.ReadInt32(); // Länge des Headers

            lsFormat = ReadChunk(loReader); // Ersten Block lesen
            if (lsFormat != "WAVE") throw new Exception("Invalid file format. '" + lsFormat + "' not supported.");

            lsFormat = ReadChunk(loReader); // Ersten Block lesen
            if (lsFormat != "fmt ") throw new Exception("Invalid file format. '" + lsFormat + "' not supported.");

            m_Format = new Win32.WaveFormat(loReader, true); // Mit Daten aus Stream initialisieren

			// Position in Datei ausrichten
			while((m_Stream.Position < m_Stream.Length) && (loReader.ReadByte() != (byte)'d'));

			if (m_Stream.Position >= m_Stream.Length) throw new Exception("Invalid file format. End of file.");

            loReader.ReadInt32(); // Länge der Extension
            
            m_Length = m_Stream.Length - m_Stream.Position;
            m_DataPos = m_Stream.Position; // Position der Daten setzen

			Position = 0;
        }
        #endregion

        #region Constructor / Deconstructor
        public WaveStreamReader(string lsFile)
		{
            m_Stream = (Stream)(new FileStream(lsFile, FileMode.Open, FileAccess.Read, FileShare.Read));
            ReadHeader();
        }
		public WaveStreamReader(Stream loStream)
		{
            m_Stream = loStream;
			ReadHeader();
		}
        ~WaveStreamReader()
		{
			Dispose();
		}
		public new void Dispose()
		{
			if (m_Stream != null) m_Stream.Close(); // Wenn Stream noch geöffnet, dann schliessen
			GC.SuppressFinalize(this); // Garbage-Collector unterdrücken
        }
        #endregion

        #region Properties (Festgelegt)
        public override bool CanRead
		{
			get { return true; }
		}
		public override bool CanSeek
		{
			get { return true; }
		}
		public override bool CanWrite
		{
			get { return false; }
        }
        #endregion

        #region Properties
        public Win32.WaveFormat Format
        {
            get { return m_Format; }
        }
        public override long Length
		{
			get { return m_Length; }
		}
		public override long Position
		{
			get { return m_Stream.Position - m_DataPos; }
			set { Seek(value + m_DataPos, SeekOrigin.Begin); }
        }
        #endregion

        #region Methoden
        public override void Close()
		{
			Dispose();
		}
		public override void Flush()
		{
		}

        // Seek
		public override long Seek(long llPosition, SeekOrigin o)
		{
			switch(o)
			{
				case SeekOrigin.Begin:
                    m_Stream.Position = llPosition + m_DataPos;
					break;
				case SeekOrigin.Current:
                    m_Stream.Seek(llPosition, SeekOrigin.Current);
					break;
				case SeekOrigin.End:
                    m_Stream.Position = m_DataPos + m_Length - llPosition;
					break;
			}
			return this.Position;
		}

        // Lesen
		public override int Read(byte[] lbBuffer, int liOffset, int liCount)
		{
            // Sicherstellen, das nicht OutOfRange-Exception auftritt
            int liNeedToRead = (int)Math.Min(liCount, m_Length - Position);

            return m_Stream.Read(lbBuffer, liOffset, liNeedToRead);
        }
        #endregion

        #region NotSupported
        public override void SetLength(long len)
        {
            throw new InvalidOperationException();
        }
        public override void Write(byte[] buf, int ofs, int count)
		{
			throw new InvalidOperationException();
        }
        #endregion
    }
    public class WaveStreamWriter : Stream, IDisposable
    {
        #region Vars
        private Stream m_Stream = null; // Genutzter Stream (Base-Stream wird nicht genutzt)
        private long m_DataPos = 0;     // Index der Daten im Stream
        private long m_Length = 0;      // Länge der Daten

        private Win32.WaveFormat m_Format = null;
        #endregion

        #region Header-schreiben
        private void WriteHeader(Win32.WaveFormat loFormat)
        {
            BinaryWriter loWriter = new BinaryWriter(m_Stream);

            loWriter.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
            loWriter.Write((uint)38);
            loWriter.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));
            loWriter.Write(System.Text.Encoding.ASCII.GetBytes("fmt "));

            m_Format = new Win32.WaveFormat(); // Mit Daten aus Stream initialisieren
            m_Format.WriteFormat(loWriter, true);

            loWriter.Write(System.Text.Encoding.ASCII.GetBytes("data"));
            loWriter.Write((uint)0);

            m_Length = 0;
            m_DataPos = m_Stream.Position; // Position der Daten setzen

            Position = 0;
        }
        #endregion

        #region Constructor / Deconstructor
        public WaveStreamWriter(string lsFile, Win32.WaveFormat loFormat)
        {
            m_Stream = (Stream)(new FileStream(lsFile, FileMode.Create, FileAccess.Write, FileShare.Write));
            WriteHeader(loFormat);
        }
        public WaveStreamWriter(Stream loStream, Win32.WaveFormat loFormat)
        {
            m_Stream = loStream;
            WriteHeader(loFormat);
        }
        ~WaveStreamWriter()
        {
            Dispose();
        }
        public new void Dispose()
        {
            if (m_Stream != null) m_Stream.Close(); // Wenn Stream noch geöffnet, dann schliessen
            GC.SuppressFinalize(this); // Garbage-Collector unterdrücken
        }
        #endregion

        #region Properties (Festgelegt)
        public override bool CanRead
        {
            get { return false; }
        }
        public override bool CanSeek
        {
            get { return false; }
        }
        public override bool CanWrite
        {
            get { return true; }
        }
        #endregion

        #region Properties
        public Win32.WaveFormat Format
        {
            get { return m_Format; }
        }
        public override long Length
        {
            get { return m_Length; }
        }
        public override long Position
        {
            get { return m_Stream.Position - m_DataPos; }
            set { m_Stream.Position = value; }
        }
        #endregion

        #region Methoden
        public override void Close()
        {
            Dispose();
        }
        public override void Flush()
        {
        }


        // Lesen
        public override void Write(byte[] lbBuffer, int liOffset, int liCount)
        {
            m_Length += liCount;
            throw new InvalidOperationException();
        }
        #endregion

        #region NotSupported
        public override long Seek(long llPosition, SeekOrigin o)
        {
            throw new InvalidOperationException();
        }
        public override int Read(byte[] lbBuffer, int liOffset, int liCount)
        {
            throw new InvalidOperationException();
        }
        public override void SetLength(long len)
        {
            throw new InvalidOperationException();
        }
        #endregion
    }
}
