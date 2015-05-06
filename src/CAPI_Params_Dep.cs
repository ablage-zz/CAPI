#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace DSELib.CAPI
{
    public class CapiStruct_BProtocol : CapiStruct_Base, IB1ProtocolSelector, IB2ProtocolSelector, IB3ProtocolSelector
    {
        #region Vars
        protected B1Protocol_Enum m_B1Protocol = B1Protocol_Enum.Kbits64WithHDLCFraming;
        protected B2Protocol_Enum m_B2Protocol = B2Protocol_Enum.ISO7776_X76SLP;
        protected B3Protocol_Enum m_B3Protocol = B3Protocol_Enum.Transparent;
        protected CapiStruct_B1Configuration m_B1Configuration = null;
        protected CapiStruct_B2Configuration m_B2Configuration = null;
        protected CapiStruct_B3Configuration m_B3Configuration = null;
        protected CapiStruct_GlobalConfiguration m_GlobalConfiguration = null;
        #endregion

        #region Constructor
        public CapiStruct_BProtocol(object loParent) 
            : base(loParent)
        {
            m_B1Configuration = new CapiStruct_B1Configuration(this);
            m_B2Configuration = new CapiStruct_B2Configuration(this);
            m_B3Configuration = new CapiStruct_B3Configuration(this);
            m_GlobalConfiguration = new CapiStruct_GlobalConfiguration(this);
        }
        public CapiStruct_BProtocol(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_B1Configuration = new CapiStruct_B1Configuration(this);
            m_B2Configuration = new CapiStruct_B2Configuration(this);
            m_B3Configuration = new CapiStruct_B3Configuration(this);
            m_GlobalConfiguration = new CapiStruct_GlobalConfiguration(this);
        }
        public CapiStruct_BProtocol(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_B1Configuration = new CapiStruct_B1Configuration(this);
            m_B2Configuration = new CapiStruct_B2Configuration(this);
            m_B3Configuration = new CapiStruct_B3Configuration(this);
            m_GlobalConfiguration = new CapiStruct_GlobalConfiguration(this);
        }
        #endregion

        #region Properties
        public B1Protocol_Enum B1Protocol
        {
            get { return m_B1Protocol; }
            set { m_B1Protocol = value; }
        }
        public B2Protocol_Enum B2Protocol
        {
            get { return m_B2Protocol; }
            set { m_B2Protocol = value; }
        }
        public B3Protocol_Enum B3Protocol
        {
            get { return m_B3Protocol; }
            set { m_B3Protocol = value; }
        }

        public CapiStruct_B1Configuration B1Configuration
        {
            get { return m_B1Configuration; }
            set { m_B1Configuration = value; }
        }
        public CapiStruct_B2Configuration B2Configuration
        {
            get { return m_B2Configuration; }
            set { m_B2Configuration = value; }
        }
        public CapiStruct_B3Configuration B3Configuration
        {
            get { return m_B3Configuration; }
            set { m_B3Configuration = value; }
        }
        public CapiStruct_GlobalConfiguration GlobalConfiguration
        {
            get { return m_GlobalConfiguration; }
            set { m_GlobalConfiguration = value; }
        }

        public override int DataSize
        {
            get
            {
                return m_B1Configuration.StructSize + m_B2Configuration.StructSize + m_B3Configuration.StructSize + 6 + m_GlobalConfiguration.StructSize;
            }
        }
        #endregion

        #region Methods
        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            int liDataSize = 0;
            int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

            byte[] lbHeader = WriteStructHeader();
            Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

            if (liDataSize > 0)
            {
                BitConverter.GetBytes((ushort)m_B1Protocol).CopyTo(lbData, liOffset); liOffset += 2;
                BitConverter.GetBytes((ushort)m_B2Protocol).CopyTo(lbData, liOffset); liOffset += 2;
                BitConverter.GetBytes((ushort)m_B3Protocol).CopyTo(lbData, liOffset); liOffset += 2;
                lbData = m_B1Configuration.AsByteArray(lbData, liOffset); liOffset += m_B1Configuration.StructSize;
                lbData = m_B2Configuration.AsByteArray(lbData, liOffset); liOffset += m_B2Configuration.StructSize;
                lbData = m_B3Configuration.AsByteArray(lbData, liOffset); liOffset += m_B3Configuration.StructSize;
                lbData = m_GlobalConfiguration.AsByteArray(lbData, liOffset); liOffset += m_GlobalConfiguration.StructSize;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                B1Protocol = (B1Protocol_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                B2Protocol = (B2Protocol_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                B3Protocol = (B3Protocol_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                liOffset = m_B1Configuration.SetData(lbData, liOffset);
                liOffset = m_B2Configuration.SetData(lbData, liOffset);
                liOffset = m_B3Configuration.SetData(lbData, liOffset);
                liOffset = m_GlobalConfiguration.SetData(lbData, liOffset);
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_B1Configuration : CapiStruct_Base
    {
        #region Vars
        protected CapiStruct_Data m_Protocol0 = null;
        protected CapiStruct_Data m_Protocol1 = null;
        protected CapiStruct_Protocol2 m_Protocol2 = null;
        protected CapiStruct_Protocol3 m_Protocol3 = null;
        protected CapiStruct_Protocol4 m_Protocol4 = null;
        protected CapiStruct_Data m_Protocol5 = null;
        protected CapiStruct_Data m_Protocol6 = null;
        protected CapiStruct_ProtocolX m_Protocol7 = null;
        protected CapiStruct_ProtocolX m_Protocol8 = null;
        protected CapiStruct_ProtocolX m_Protocol9 = null;
        #endregion

        #region Constructor
        public CapiStruct_B1Configuration(object loParent) 
            : base(loParent)
        {
            m_Protocol0 = new CapiStruct_Data(this);
            m_Protocol1 = new CapiStruct_Data(this);
            m_Protocol2 = new CapiStruct_Protocol2(this);
            m_Protocol3 = new CapiStruct_Protocol3(this);
            m_Protocol4 = new CapiStruct_Protocol4(this);
            m_Protocol5 = new CapiStruct_Data(this);
            m_Protocol6 = new CapiStruct_Data(this);
            m_Protocol7 = new CapiStruct_ProtocolX(this);
            m_Protocol8 = new CapiStruct_ProtocolX(this);
            m_Protocol9 = new CapiStruct_ProtocolX(this);
        }
        public CapiStruct_B1Configuration(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_Protocol0 = new CapiStruct_Data(this);
            m_Protocol1 = new CapiStruct_Data(this);
            m_Protocol2 = new CapiStruct_Protocol2(this);
            m_Protocol3 = new CapiStruct_Protocol3(this);
            m_Protocol4 = new CapiStruct_Protocol4(this);
            m_Protocol5 = new CapiStruct_Data(this);
            m_Protocol6 = new CapiStruct_Data(this);
            m_Protocol7 = new CapiStruct_ProtocolX(this);
            m_Protocol8 = new CapiStruct_ProtocolX(this);
            m_Protocol9 = new CapiStruct_ProtocolX(this);
        }
        public CapiStruct_B1Configuration(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_Protocol0 = new CapiStruct_Data(this);
            m_Protocol1 = new CapiStruct_Data(this);
            m_Protocol2 = new CapiStruct_Protocol2(this);
            m_Protocol3 = new CapiStruct_Protocol3(this);
            m_Protocol4 = new CapiStruct_Protocol4(this);
            m_Protocol5 = new CapiStruct_Data(this);
            m_Protocol6 = new CapiStruct_Data(this);
            m_Protocol7 = new CapiStruct_ProtocolX(this);
            m_Protocol8 = new CapiStruct_ProtocolX(this);
            m_Protocol9 = new CapiStruct_ProtocolX(this);
        }
        #endregion

        #region Internal Classes
        public class CapiStruct_Protocol2 : CapiStruct_Base
        {
            #region Vars
            protected ushort m_MaximumBitRate = 0;
            protected ushort m_BitsPerCharacter = 8;
            protected Parity_Enum m_Parity = Parity_Enum.None;
            protected StopBits_Enum m_StopBits = StopBits_Enum.StopBits_1;
            #endregion

            #region Properties
            public ushort MaximumBitRate
            {
                get { return m_MaximumBitRate; }
                set { m_MaximumBitRate = value; }
            }
            public ushort BitsPerCharacter
            {
                get { return m_BitsPerCharacter; }
                set { m_BitsPerCharacter = value; }
            }
            public Parity_Enum Parity
            {
                get { return m_Parity; }
                set { m_Parity = value; }
            }
            public StopBits_Enum StopBits
            {
                get { return m_StopBits; }
                set { m_StopBits = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 8;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol2(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_Protocol2(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_Protocol2(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    BitConverter.GetBytes(m_MaximumBitRate).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_BitsPerCharacter).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes((ushort)m_Parity).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes((ushort)m_StopBits).CopyTo(lbData, liOffset); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_MaximumBitRate = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_BitsPerCharacter = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Parity = (Parity_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_StopBits = (StopBits_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol3 : CapiStruct_Base
        {
            #region Vars
            protected ushort m_MaximumBitRate = 56;
            protected ushort m_BitsPerCharacter = 0;
            protected Parity_Enum m_Parity = Parity_Enum.None;
            protected StopBits_Enum m_StopBits = StopBits_Enum.StopBits_1;
            #endregion

            #region Properties
            public ushort MaximumBitRate
            {
                get { return m_MaximumBitRate; }
                set { m_MaximumBitRate = value; }
            }
            public ushort BitsPerCharacter
            {
                get { return m_BitsPerCharacter; }
                set { m_BitsPerCharacter = value; }
            }
            public Parity_Enum Parity
            {
                get { return m_Parity; }
                set { m_Parity = value; }
            }
            public StopBits_Enum StopBits
            {
                get { return m_StopBits; }
                set { m_StopBits = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 8;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol3(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_Protocol3(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_Protocol3(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    BitConverter.GetBytes(m_MaximumBitRate).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_BitsPerCharacter).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes((ushort)m_Parity).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes((ushort)m_StopBits).CopyTo(lbData, liOffset); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_MaximumBitRate = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_BitsPerCharacter = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Parity = (Parity_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_StopBits = (StopBits_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol4 : CapiStruct_Base
        {
            #region Vars
            protected ushort m_MaximumBitRate = 0;
            protected short m_TransmitLevelinDb = 8;
            protected ushort m_Reserved1 = 0;
            protected ushort m_Reserved2 = 0;
            #endregion

            #region Properties
            public ushort MaximumBitRate
            {
                get { return m_MaximumBitRate; }
                set { m_MaximumBitRate = value; }
            }
            public short TransmitLevelinDb
            {
                get { return m_TransmitLevelinDb; }
                set { m_TransmitLevelinDb = value; }
            }
            public ushort Reserved1
            {
                get { return m_Reserved1; }
                set { m_Reserved1 = value; }
            }
            public ushort Reserved2
            {
                get { return m_Reserved2; }
                set { m_Reserved2 = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 8;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol4(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_Protocol4(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_Protocol4(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    BitConverter.GetBytes(m_MaximumBitRate).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_TransmitLevelinDb).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_Reserved1).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_Reserved2).CopyTo(lbData, liOffset); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_MaximumBitRate = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_TransmitLevelinDb = BitConverter.ToInt16(lbData, liOffset); liOffset += 2;
                    m_Reserved1 = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Reserved2 = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_ProtocolX : CapiStruct_Base
        {
            #region Vars
            protected ushort m_MaximumBitRate = 0;
            protected ushort m_BitsPerCharacter = 8;
            protected Parity_Enum m_Parity = Parity_Enum.None;
            protected StopBits_Enum m_StopBits = StopBits_Enum.StopBits_1;

            protected ushort m_Options = 0;
            protected SpeedNegotiation_Enum m_SpeedNegotiation = SpeedNegotiation_Enum.V8;
            #endregion

            #region Properties
            public ushort MaximumBitRate
            {
                get { return m_MaximumBitRate; }
                set { m_MaximumBitRate = value; }
            }
            public ushort BitsPerCharacter
            {
                get { return m_BitsPerCharacter; }
                set { m_BitsPerCharacter = value; }
            }
            public Parity_Enum Parity
            {
                get { return m_Parity; }
                set { m_Parity = value; }
            }
            public StopBits_Enum StopBits
            {
                get { return m_StopBits; }
                set { m_StopBits = value; }
            }
            public SpeedNegotiation_Enum SpeedNegotiation
            {
                get { return m_SpeedNegotiation; }
                set { m_SpeedNegotiation = value; }
            }

            public ushort Options
            {
                get { return m_Options; }
                set { m_Options = value; }
            }
            public bool Op_DisableRetain
            {
                get { return ((m_Options & 1) == 1); }
                set
                {
                    if (value)
                    {
                        m_Options |= 1;
                    }
                    else
                    {
                        m_Options ^= 1;
                    }
                }
            }
            public bool Op_DisableRingTone
            {
                get { return ((m_Options & 2) == 2); }
                set
                {
                    if (value)
                    {
                        m_Options |= 2;
                    }
                    else
                    {
                        m_Options ^= 2;
                    }
                }
            }
            public GuardTone_Enum Op_GuardTone
            {
                get { return (GuardTone_Enum)((m_Options & 12) >> 2); }
                set
                {
                    m_Options &= 243; // Clear Flags
                    m_Options |= (ushort)((int)value << 2);
                }
            }
            public Loudspeaker_Enum Op_Loudspeaker
            {
                get { return (Loudspeaker_Enum)((m_Options & 48) >> 4); }
                set
                {
                    m_Options &= 207; // Clear Flags
                    m_Options |= (ushort)((int)value << 4);
                }
            }
            public LoudspeakerVolume_Enum Op_LoudspeakerVolume
            {
                get { return (LoudspeakerVolume_Enum)((m_Options & 192) >> 6); }
                set
                {
                    m_Options &= 63; // Clear Flags
                    m_Options |= (ushort)((int)value << 6);
                }
            }


            public override int DataSize
            {
                get
                {
                    return 12;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_ProtocolX(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_ProtocolX(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_ProtocolX(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    BitConverter.GetBytes(m_MaximumBitRate).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_BitsPerCharacter).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes((ushort)m_Parity).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes((ushort)m_StopBits).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_Options).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes((ushort)m_SpeedNegotiation).CopyTo(lbData, liOffset); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_MaximumBitRate = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_BitsPerCharacter = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Parity = (Parity_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_StopBits = (StopBits_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Options = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_SpeedNegotiation = (SpeedNegotiation_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        #endregion

        #region Properties
        private B1Protocol_Enum m_Protocol
        {
            get {
                if (m_Parent != null)
                {
                    return ((IB1ProtocolSelector)m_Parent).B1Protocol;
                }
                else
                {
                    return B1Protocol_Enum.Kbits64WithHDLCFraming;
                }
            }
        }

        public CapiStruct_Data Protocol0
        {
            get { return m_Protocol0; }
            set { m_Protocol0 = value; }
        }
        public CapiStruct_Data Protocol1
        {
            get { return m_Protocol1; }
            set { m_Protocol1 = value; }
        }
        public CapiStruct_Protocol2 Protocol2
        {
            get { return m_Protocol2; }
            set { m_Protocol2 = value; }
        }
        public CapiStruct_Protocol3 Protocol3
        {
            get { return m_Protocol3; }
            set { m_Protocol3 = value; }
        }
        public CapiStruct_Protocol4 Protocol4
        {
            get { return m_Protocol4; }
            set { m_Protocol4 = value; }
        }
        public CapiStruct_Data Protocol5
        {
            get { return m_Protocol5; }
            set { m_Protocol5 = value; }
        }
        public CapiStruct_Data Protocol6
        {
            get { return m_Protocol6; }
            set { m_Protocol6 = value; }
        }
        public CapiStruct_ProtocolX Protocol7
        {
            get { return m_Protocol7; }
            set { m_Protocol7 = value; }
        }
        public CapiStruct_ProtocolX Protocol8
        {
            get { return m_Protocol8; }
            set { m_Protocol8 = value; }
        }
        public CapiStruct_ProtocolX Protocol9
        {
            get { return m_Protocol9; }
            set { m_Protocol9 = value; }
        }

        public override int DataSize
        {
            get
            {
                if ((ushort)m_Protocol == 0)
                {
                    return m_Protocol0.StructSize;
                }
                else if ((ushort)m_Protocol == 1)
                {
                    return m_Protocol1.StructSize;
                }
                else if ((ushort)m_Protocol == 2)
                {
                    return m_Protocol2.StructSize;
                }
                else if ((ushort)m_Protocol == 3)
                {
                    return m_Protocol3.StructSize;
                }
                else if ((ushort)m_Protocol == 4)
                {
                    return m_Protocol4.StructSize;
                }
                else if ((ushort)m_Protocol == 5)
                {
                    return m_Protocol5.StructSize;
                }
                else if ((ushort)m_Protocol == 6)
                {
                    return m_Protocol6.StructSize;
                }
                else if ((ushort)m_Protocol == 7)
                {
                    return m_Protocol7.StructSize;
                }
                else if ((ushort)m_Protocol == 8)
                {
                    return m_Protocol8.StructSize;
                }
                else 
                {
                    return m_Protocol9.StructSize;
                }
            }
        }
        #endregion

        #region Methods
        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            int liDataSize = 0;
            int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

            byte[] lbHeader = WriteStructHeader();
            Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

            if (liDataSize > 0)
            {
                if ((ushort)m_Protocol == 0)
                {
                    return m_Protocol0.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 1)
                {
                    return m_Protocol1.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 2)
                {
                    return m_Protocol2.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 3)
                {
                    return m_Protocol3.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 4)
                {
                    return m_Protocol4.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 5)
                {
                    return m_Protocol5.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 6)
                {
                    return m_Protocol6.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 7)
                {
                    return m_Protocol7.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 8)
                {
                    return m_Protocol8.AsByteArray(lbData, liOffset);
                }
                else
                {
                    return m_Protocol9.AsByteArray(lbData, liOffset);
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if ((ushort)m_Protocol == 0)
                {
                    return m_Protocol0.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 1)
                {
                    return m_Protocol1.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 2)
                {
                    return m_Protocol2.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 3)
                {
                    return m_Protocol3.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 4)
                {
                    return m_Protocol4.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 5)
                {
                    return m_Protocol5.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 6)
                {
                    return m_Protocol6.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 7)
                {
                    return m_Protocol7.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 8)
                {
                    return m_Protocol8.SetData(lbData, liOffset);
                }
                else
                {
                    return m_Protocol9.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }
    public class CapiStruct_B2Configuration : CapiStruct_Base
    {
        #region Vars
        protected CapiStruct_Protocol0 m_Protocol0 = null;
        protected CapiStruct_Data m_Protocol1 = null;
        protected CapiStruct_Protocol2 m_Protocol2 = null;
        protected CapiStruct_Protocol3 m_Protocol3 = null;
        protected CapiStruct_Data m_Protocol4 = null;
        protected CapiStruct_Data m_Protocol5 = null;
        protected CapiStruct_Data m_Protocol6 = null;
        protected CapiStruct_Protocol7 m_Protocol7 = null;
        protected CapiStruct_Protocol8 m_Protocol8 = null;
        protected CapiStruct_ProtocolX m_Protocol9 = null;
        protected CapiStruct_Protocol10 m_Protocol10 = null;
        protected CapiStruct_ProtocolX m_Protocol11 = null;
        protected CapiStruct_Protocol12 m_Protocol12 = null;
        #endregion

        #region Constructor
        public CapiStruct_B2Configuration(object loParent) 
            : base(loParent)
        {
            m_Protocol0 = new CapiStruct_Protocol0(this);
            m_Protocol1 = new CapiStruct_Data(this);
            m_Protocol2 = new CapiStruct_Protocol2(this);
            m_Protocol3 = new CapiStruct_Protocol3(this);
            m_Protocol4 = new CapiStruct_Data(this);
            m_Protocol5 = new CapiStruct_Data(this);
            m_Protocol6 = new CapiStruct_Data(this);
            m_Protocol7 = new CapiStruct_Protocol7(this);
            m_Protocol8 = new CapiStruct_Protocol8(this);
            m_Protocol9 = new CapiStruct_ProtocolX(this);
            m_Protocol10 = new CapiStruct_Protocol10(this);
            m_Protocol11 = new CapiStruct_ProtocolX(this);
            m_Protocol12 = new CapiStruct_Protocol12(this);
        }
        public CapiStruct_B2Configuration(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_Protocol0 = new CapiStruct_Protocol0(this);
            m_Protocol1 = new CapiStruct_Data(this);
            m_Protocol2 = new CapiStruct_Protocol2(this);
            m_Protocol3 = new CapiStruct_Protocol3(this);
            m_Protocol4 = new CapiStruct_Data(this);
            m_Protocol5 = new CapiStruct_Data(this);
            m_Protocol6 = new CapiStruct_Data(this);
            m_Protocol7 = new CapiStruct_Protocol7(this);
            m_Protocol8 = new CapiStruct_Protocol8(this);
            m_Protocol9 = new CapiStruct_ProtocolX(this);
            m_Protocol10 = new CapiStruct_Protocol10(this);
            m_Protocol11 = new CapiStruct_ProtocolX(this);
            m_Protocol12 = new CapiStruct_Protocol12(this);
        }
        public CapiStruct_B2Configuration(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_Protocol0 = new CapiStruct_Protocol0(this);
            m_Protocol1 = new CapiStruct_Data(this);
            m_Protocol2 = new CapiStruct_Protocol2(this);
            m_Protocol3 = new CapiStruct_Protocol3(this);
            m_Protocol4 = new CapiStruct_Data(this);
            m_Protocol5 = new CapiStruct_Data(this);
            m_Protocol6 = new CapiStruct_Data(this);
            m_Protocol7 = new CapiStruct_Protocol7(this);
            m_Protocol8 = new CapiStruct_Protocol8(this);
            m_Protocol9 = new CapiStruct_ProtocolX(this);
            m_Protocol10 = new CapiStruct_Protocol10(this);
            m_Protocol11 = new CapiStruct_ProtocolX(this);
            m_Protocol12 = new CapiStruct_Protocol12(this);
        }
        #endregion

        #region Internal Classes
        public class CapiStruct_Protocol0 : CapiStruct_Base
        {
            #region Vars
            protected byte m_AddressA = 0x03;
            protected byte m_AddressB = 0x01;
            protected ModuloMode_Enum m_ModuloMode = ModuloMode_Enum.NormalOperation;
            protected byte m_WindowSize = 7;
            protected CapiStruct_Data m_XID = null;
            #endregion

            #region Properties
            public byte AddressA
            {
                get { return m_AddressA; }
                set { m_AddressA = value; }
            }
            public byte AddressB
            {
                get { return m_AddressB; }
                set { m_AddressB = value; }
            }
            public ModuloMode_Enum ModuloMode
            {
                get { return m_ModuloMode; }
                set { m_ModuloMode = value; }
            }
            public byte WindowSize
            {
                get { return m_WindowSize; }
                set { m_WindowSize = value; }
            }
            public CapiStruct_Data XID
            {
                get { return m_XID; }
                set { m_XID = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4 + m_XID.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol0(object loParent)
                : base(loParent)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_Protocol0(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_Protocol0(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_XID = new CapiStruct_Data(this);
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    lbData[liOffset] = m_AddressA; liOffset++;
                    lbData[liOffset] = m_AddressB; liOffset++;
                    lbData[liOffset] = (byte)m_ModuloMode; liOffset++;
                    lbData[liOffset] = m_WindowSize; liOffset++;
                    lbData = m_XID.AsByteArray(lbData, liOffset); liOffset += m_XID.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_AddressA = lbData[liOffset]; liOffset++;
                    m_AddressB = lbData[liOffset]; liOffset++;
                    m_ModuloMode = (ModuloMode_Enum)lbData[liOffset]; liOffset++;
                    m_WindowSize = lbData[liOffset]; liOffset++;
                    liOffset = m_XID.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol2 : CapiStruct_Base
        {
            #region Vars
            protected byte m_AddressA = 0xc1;
            protected byte m_AddressB = 0;
            protected ModuloMode_Enum m_ModuloMode = ModuloMode_Enum.NormalOperation;
            protected byte m_WindowSize = 7;
            protected CapiStruct_Data m_XID = null;
            #endregion

            #region Properties
            public byte AddressA
            {
                get { return m_AddressA; }
                set { m_AddressA = value; }
            }
            public byte AddressB
            {
                get { return m_AddressB; }
                set { m_AddressB = value; }
            }
            public ModuloMode_Enum ModuloMode
            {
                get { return m_ModuloMode; }
                set { m_ModuloMode = value; }
            }
            public byte WindowSize
            {
                get { return m_WindowSize; }
                set { m_WindowSize = value; }
            }
            public CapiStruct_Data XID
            {
                get { return m_XID; }
                set { m_XID = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4 + m_XID.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol2(object loParent)
                : base(loParent)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_Protocol2(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_Protocol2(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_XID = new CapiStruct_Data(this);
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    lbData[liOffset] = m_AddressA; liOffset++;
                    lbData[liOffset] = m_AddressB; liOffset++;
                    lbData[liOffset] = (byte)m_ModuloMode; liOffset++;
                    lbData[liOffset] = m_WindowSize; liOffset++;
                    lbData = m_XID.AsByteArray(lbData, liOffset); liOffset += m_XID.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_AddressA = lbData[liOffset]; liOffset++;
                    m_AddressB = lbData[liOffset]; liOffset++;
                    m_ModuloMode = (ModuloMode_Enum)lbData[liOffset]; liOffset++;
                    m_WindowSize = lbData[liOffset]; liOffset++;
                    liOffset = m_XID.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol3 : CapiStruct_Base
        {
            #region Vars
            protected TEI_Enum m_TEI = TEI_Enum.FixedTEI;
            protected byte m_TEIValue = 0;
            protected byte m_AddressB = 0;
            protected ModuloMode_Enum m_ModuloMode = ModuloMode_Enum.ExtendedOperation;
            protected byte m_WindowSize = 3;
            protected CapiStruct_Data m_XID = null;
            #endregion

            #region Properties
            public TEI_Enum TEI
            {
                get { return m_TEI; }
                set { m_TEI = value; }
            }
            public byte TEIValue
            {
                get { return m_TEIValue; }
                set { m_TEIValue = value; }
            }
            public byte AddressB
            {
                get { return m_AddressB; }
                set { m_AddressB = value; }
            }
            public ModuloMode_Enum ModuloMode
            {
                get { return m_ModuloMode; }
                set { m_ModuloMode = value; }
            }
            public byte WindowSize
            {
                get { return m_WindowSize; }
                set { m_WindowSize = value; }
            }
            public CapiStruct_Data XID
            {
                get { return m_XID; }
                set { m_XID = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4 + m_XID.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol3(object loParent)
                : base(loParent)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_Protocol3(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_Protocol3(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_XID = new CapiStruct_Data(this);
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    lbData[liOffset] = (byte)((m_TEIValue << 1) + (byte)m_TEI); liOffset++;
                    lbData[liOffset] = m_AddressB; liOffset++;
                    lbData[liOffset] = (byte)m_ModuloMode; liOffset++;
                    lbData[liOffset] = m_WindowSize; liOffset++;
                    lbData = m_XID.AsByteArray(lbData, liOffset); liOffset += m_XID.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_TEI = (TEI_Enum)(lbData[liOffset] & 1);
                    m_TEIValue = (byte)(lbData[liOffset] >> 1); liOffset++;

                    m_AddressB = lbData[liOffset]; liOffset++;
                    m_ModuloMode = (ModuloMode_Enum)lbData[liOffset]; liOffset++;
                    m_WindowSize = lbData[liOffset]; liOffset++;
                    liOffset = m_XID.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol7 : CapiStruct_Base
        {
            #region Vars
            protected B2ConfigurationProtocol7Options_Flags m_Options = 0;
            #endregion

            #region Properties
            public B2ConfigurationProtocol7Options_Flags Options
            {
                get { return m_Options; }
                set { m_Options = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 2;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol7(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_Protocol7(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_Protocol7(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_Options), 0, lbData, liOffset, 2); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_Options = (B2ConfigurationProtocol7Options_Flags)BitConverter.ToUInt16(lbData, liOffset);
                    liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol8 : CapiStruct_Base
        {
            #region Vars
            protected byte m_AddressA = 0x03;
            protected byte m_AddressB = 0x01;
            protected ModuloMode_Enum m_ModuloMode = ModuloMode_Enum.NormalOperation;
            protected byte m_WindowSize = 7;
            protected Direction_Enum m_Direction = Direction_Enum.AllDirections;
            protected ushort m_NumberOfCodeWords = 2048;
            protected ushort m_MaximumStringLength = 250;
            #endregion

            #region Properties
            public byte AddressA
            {
                get { return m_AddressA; }
                set { m_AddressA = value; }
            }
            public byte AddressB
            {
                get { return m_AddressB; }
                set { m_AddressB = value; }
            }
            public ModuloMode_Enum ModuloMode
            {
                get { return m_ModuloMode; }
                set { m_ModuloMode = value; }
            }
            public byte WindowSize
            {
                get { return m_WindowSize; }
                set { m_WindowSize = value; }
            }
            public Direction_Enum Direction
            {
                get { return m_Direction; }
                set { m_Direction = value; }
            }
            public ushort NumberOfCodeWords
            {
                get { return m_NumberOfCodeWords; }
                set { m_NumberOfCodeWords = value; }
            }
            public ushort MaximumStringLength
            {
                get { return m_MaximumStringLength; }
                set
                {
                    if (value < 6)
                    {
                        throw new Exception("Length should be higher or equal 6.");
                    }
                    else if (value > 250)
                    {
                        throw new Exception("Length should be smaller or equal 250.");
                    }
                    else
                    {
                        m_MaximumStringLength = value;
                    }
                }
            }

            public override int DataSize
            {
                get
                {
                    return 10;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol8(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_Protocol8(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_Protocol8(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    lbData[liOffset] = m_AddressA; liOffset++;
                    lbData[liOffset] = m_AddressB; liOffset++;
                    lbData[liOffset] = (byte)m_ModuloMode; liOffset++;
                    lbData[liOffset] = m_WindowSize; liOffset++;
                    BitConverter.GetBytes((ushort)m_Direction).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_NumberOfCodeWords).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_MaximumStringLength).CopyTo(lbData, liOffset); liOffset += 2;
                }
                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_AddressA = lbData[liOffset]; liOffset++;
                    m_AddressB = lbData[liOffset]; liOffset++;
                    m_ModuloMode = (ModuloMode_Enum)lbData[liOffset]; liOffset++;
                    m_WindowSize = lbData[liOffset]; liOffset++;
                    m_Direction = (Direction_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_NumberOfCodeWords = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_MaximumStringLength = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_ProtocolX : CapiStruct_Base
        {
            #region Vars
            protected byte m_AddressA = 0x00;
            protected byte m_AddressB = 0x01;
            protected ModuloMode_Enum m_ModuloMode = ModuloMode_Enum.ExtendedOperation;
            protected byte m_WindowSize = 7;
            protected CapiStruct_Data m_XID = null;
            #endregion

            #region Properties
            public byte AddressA
            {
                get { return m_AddressA; }
                set { m_AddressA = value; }
            }
            public byte AddressB
            {
                get { return m_AddressB; }
                set { m_AddressB = value; }
            }
            public ModuloMode_Enum ModuloMode
            {
                get { return m_ModuloMode; }
                set { m_ModuloMode = value; }
            }
            public byte WindowSize
            {
                get { return m_WindowSize; }
                set { m_WindowSize = value; }
            }
            public CapiStruct_Data XID
            {
                get { return m_XID; }
                set { m_XID = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4 + m_XID.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_ProtocolX(object loParent)
                : base(loParent)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_ProtocolX(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_ProtocolX(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_XID = new CapiStruct_Data(this);
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    lbData[liOffset] = m_AddressA; liOffset++;
                    lbData[liOffset] = m_AddressB; liOffset++;
                    lbData[liOffset] = (byte)m_ModuloMode; liOffset++;
                    lbData[liOffset] = m_WindowSize; liOffset++;
                    lbData = m_XID.AsByteArray(lbData, liOffset); liOffset += m_XID.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_AddressA = lbData[liOffset]; liOffset++;
                    m_AddressB = lbData[liOffset]; liOffset++;
                    m_ModuloMode = (ModuloMode_Enum)lbData[liOffset]; liOffset++;
                    m_WindowSize = lbData[liOffset]; liOffset++;
                    liOffset = m_XID.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol10 : CapiStruct_Base
        {
            #region Vars
            protected byte m_AddressA = 0x00;
            protected byte m_AddressB = 0x01;
            protected ModuloMode_Enum m_ModuloMode = ModuloMode_Enum.ExtendedOperation;
            protected byte m_WindowSize = 7;
            protected Direction_Enum m_Direction = Direction_Enum.AllDirections;
            protected ushort m_NumberOfCodeWords = 2048;
            protected ushort m_MaximumStringLength = 250;
            #endregion

            #region Properties
            public byte AddressA
            {
                get { return m_AddressA; }
                set { m_AddressA = value; }
            }
            public byte AddressB
            {
                get { return m_AddressB; }
                set { m_AddressB = value; }
            }
            public ModuloMode_Enum ModuloMode
            {
                get { return m_ModuloMode; }
                set { m_ModuloMode = value; }
            }
            public byte WindowSize
            {
                get { return m_WindowSize; }
                set { m_WindowSize = value; }
            }
            public Direction_Enum Direction
            {
                get { return m_Direction; }
                set { m_Direction = value; }
            }
            public ushort NumberOfCodeWords
            {
                get { return m_NumberOfCodeWords; }
                set { m_NumberOfCodeWords = value; }
            }
            public ushort MaximumStringLength
            {
                get { return m_MaximumStringLength; }
                set
                {
                    if (value < 6)
                    {
                        throw new Exception("Length should be higher or equal 6.");
                    }
                    else if (value > 250)
                    {
                        throw new Exception("Length should be smaller or equal 250.");
                    }
                    else
                    {
                        m_MaximumStringLength = value;
                    }
                }
            }

            public override int DataSize
            {
                get
                {
                    return 10;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol10(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_Protocol10(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_Protocol10(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    lbData[liOffset] = m_AddressA; liOffset++;
                    lbData[liOffset] = m_AddressB; liOffset++;
                    lbData[liOffset] = (byte)m_ModuloMode; liOffset++;
                    lbData[liOffset] = m_WindowSize; liOffset++;
                    BitConverter.GetBytes((ushort)m_Direction).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_NumberOfCodeWords).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_MaximumStringLength).CopyTo(lbData, liOffset); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_AddressA = lbData[liOffset]; liOffset++;
                    m_AddressB = lbData[liOffset]; liOffset++;
                    m_ModuloMode = (ModuloMode_Enum)lbData[liOffset]; liOffset++;
                    m_WindowSize = lbData[liOffset]; liOffset++;
                    m_Direction = (Direction_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_NumberOfCodeWords = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_MaximumStringLength = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol12 : CapiStruct_Base
        {
            #region Vars
            protected TEI_Enum m_TEI = TEI_Enum.FixedTEI;
            protected byte m_TEIValue = 0;
            protected byte m_SAPI = 0;
            protected ModuloMode_Enum m_ModuloMode = ModuloMode_Enum.ExtendedOperation;
            protected byte m_WindowSize = 1;
            protected CapiStruct_Data m_XID = null;
            #endregion

            #region Properties
            public TEI_Enum TEI
            {
                get { return m_TEI; }
                set { m_TEI = value; }
            }
            public byte TEIValue
            {
                get { return m_TEIValue; }
                set { m_TEIValue = value; }
            }
            public byte SAPI
            {
                get { return m_SAPI; }
                set
                {
                    if (value > 63)
                    {
                        throw new Exception("SAPI should be smaller or equal 63.");
                    }
                    else
                    {
                        m_SAPI = value;
                    }
                }
            }
            public ModuloMode_Enum ModuloMode
            {
                get { return m_ModuloMode; }
                set { m_ModuloMode = value; }
            }
            public byte WindowSize
            {
                get { return m_WindowSize; }
                set { m_WindowSize = value; }
            }
            public CapiStruct_Data XID
            {
                get { return m_XID; }
                set { m_XID = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4 + m_XID.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol12(object loParent)
                : base(loParent)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_Protocol12(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_XID = new CapiStruct_Data(this);
            }
            public CapiStruct_Protocol12(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_XID = new CapiStruct_Data(this);
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    lbData[liOffset] = (byte)((m_TEIValue << 1) + (byte)m_TEI); liOffset++;
                    lbData[liOffset] = (byte)(m_SAPI << 2); liOffset++;
                    lbData[liOffset] = (byte)m_ModuloMode; liOffset++;
                    lbData[liOffset] = m_WindowSize; liOffset++;
                    lbData = m_XID.AsByteArray(lbData, liOffset); liOffset += m_XID.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_TEI = (TEI_Enum)(lbData[liOffset] & 1);
                    m_TEIValue = (byte)(lbData[liOffset] >> 1); liOffset++;

                    m_SAPI = (byte)(lbData[liOffset] >> 2); liOffset++;
                    m_ModuloMode = (ModuloMode_Enum)lbData[liOffset]; liOffset++;
                    m_WindowSize = lbData[liOffset]; liOffset++;
                    liOffset = m_XID.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        #endregion

        #region Properties
        private B2Protocol_Enum m_Protocol
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IB2ProtocolSelector)m_Parent).B2Protocol;
                }
                else
                {
                    return B2Protocol_Enum.ISO7776_X76SLP;
                }
            }
        }

        public CapiStruct_Protocol0 Protocol0
        {
            get { return m_Protocol0; }
            set { m_Protocol0 = value; }
        }
        public CapiStruct_Data Protocol1
        {
            get { return m_Protocol1; }
            set { m_Protocol1 = value; }
        }
        public CapiStruct_Protocol2 Protocol2
        {
            get { return m_Protocol2; }
            set { m_Protocol2 = value; }
        }
        public CapiStruct_Protocol3 Protocol3
        {
            get { return m_Protocol3; }
            set { m_Protocol3 = value; }
        }
        public CapiStruct_Data Protocol4
        {
            get { return m_Protocol4; }
            set { m_Protocol4 = value; }
        }
        public CapiStruct_Data Protocol5
        {
            get { return m_Protocol5; }
            set { m_Protocol5 = value; }
        }
        public CapiStruct_Data Protocol6
        {
            get { return m_Protocol6; }
            set { m_Protocol6 = value; }
        }
        public CapiStruct_Protocol7 Protocol7
        {
            get { return m_Protocol7; }
            set { m_Protocol7 = value; }
        }
        public CapiStruct_Protocol8 Protocol8
        {
            get { return m_Protocol8; }
            set { m_Protocol8 = value; }
        }
        public CapiStruct_ProtocolX Protocol9
        {
            get { return m_Protocol9; }
            set { m_Protocol9 = value; }
        }
        public CapiStruct_Protocol10 Protocol10
        {
            get { return m_Protocol10; }
            set { m_Protocol10 = value; }
        }
        public CapiStruct_ProtocolX Protocol11
        {
            get { return m_Protocol11; }
            set { m_Protocol11 = value; }
        }
        public CapiStruct_Protocol12 Protocol12
        {
            get { return m_Protocol12; }
            set { m_Protocol12 = value; }
        }

        public override int DataSize
        {
            get
            {
                if ((ushort)m_Protocol == 0)
                {
                    return m_Protocol0.StructSize;
                }
                else if ((ushort)m_Protocol == 1)
                {
                    return m_Protocol1.StructSize;
                }
                else if ((ushort)m_Protocol == 2)
                {
                    return m_Protocol2.StructSize;
                }
                else if ((ushort)m_Protocol == 3)
                {
                    return m_Protocol3.StructSize;
                }
                else if ((ushort)m_Protocol == 4)
                {
                    return m_Protocol4.StructSize;
                }
                else if ((ushort)m_Protocol == 5)
                {
                    return m_Protocol5.StructSize;
                }
                else if ((ushort)m_Protocol == 6)
                {
                    return m_Protocol6.StructSize;
                }
                else if ((ushort)m_Protocol == 7)
                {
                    return m_Protocol7.StructSize;
                }
                else if ((ushort)m_Protocol == 8)
                {
                    return m_Protocol8.StructSize;
                }
                else if ((ushort)m_Protocol == 9)
                {
                    return m_Protocol9.StructSize;
                }
                else if ((ushort)m_Protocol == 10)
                {
                    return m_Protocol10.StructSize;
                }
                else if ((ushort)m_Protocol == 11)
                {
                    return m_Protocol11.StructSize;
                }
                else
                {
                    return m_Protocol12.StructSize;
                }
            }
        }
        #endregion

        #region Methods
        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            int liDataSize = 0;
            int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

            byte[] lbHeader = WriteStructHeader();
            Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

            if (liDataSize > 0)
            {
                if ((ushort)m_Protocol == 0)
                {
                    return m_Protocol0.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 1)
                {
                    return m_Protocol1.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 2)
                {
                    return m_Protocol2.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 3)
                {
                    return m_Protocol3.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 4)
                {
                    return m_Protocol4.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 5)
                {
                    return m_Protocol5.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 6)
                {
                    return m_Protocol6.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 7)
                {
                    return m_Protocol7.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 8)
                {
                    return m_Protocol8.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 9)
                {
                    return m_Protocol9.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 10)
                {
                    return m_Protocol10.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 11)
                {
                    return m_Protocol11.AsByteArray(lbData, liOffset);
                }
                else
                {
                    return m_Protocol12.AsByteArray(lbData, liOffset);
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if ((ushort)m_Protocol == 0)
                {
                    return m_Protocol0.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 1)
                {
                    return m_Protocol1.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 2)
                {
                    return m_Protocol2.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 3)
                {
                    return m_Protocol3.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 4)
                {
                    return m_Protocol4.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 5)
                {
                    return m_Protocol5.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 6)
                {
                    return m_Protocol6.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 7)
                {
                    return m_Protocol7.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 8)
                {
                    return m_Protocol8.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 9)
                {
                    return m_Protocol9.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 10)
                {
                    return m_Protocol10.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 11)
                {
                    return m_Protocol11.SetData(lbData, liOffset);
                }
                else
                {
                    return m_Protocol12.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }
    public class CapiStruct_B3Configuration : CapiStruct_Base
    {
        #region Vars
        protected CapiStruct_Data m_Protocol0 = null;
        protected CapiStruct_ProtocolX m_Protocol1 = null;
        protected CapiStruct_ProtocolX m_Protocol2 = null;
        protected CapiStruct_ProtocolX m_Protocol3 = null;
        protected CapiStruct_Protocol4 m_Protocol4 = null;
        protected CapiStruct_Protocol5 m_Protocol5 = null;
        protected CapiStruct_Data m_Protocol7 = null;
        #endregion

        #region Constructor
        public CapiStruct_B3Configuration(object loParent) 
            : base(loParent)
        {
            m_Protocol0 = new CapiStruct_Data(this);
            m_Protocol1 = new CapiStruct_ProtocolX(this);
            m_Protocol2 = new CapiStruct_ProtocolX(this);
            m_Protocol3 = new CapiStruct_ProtocolX(this);
            m_Protocol4 = new CapiStruct_Protocol4(this);
            m_Protocol5 = new CapiStruct_Protocol5(this);
            m_Protocol7 = new CapiStruct_Data(this);
        }
        public CapiStruct_B3Configuration(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_Protocol0 = new CapiStruct_Data(this);
            m_Protocol1 = new CapiStruct_ProtocolX(this);
            m_Protocol2 = new CapiStruct_ProtocolX(this);
            m_Protocol3 = new CapiStruct_ProtocolX(this);
            m_Protocol4 = new CapiStruct_Protocol4(this);
            m_Protocol5 = new CapiStruct_Protocol5(this);
            m_Protocol7 = new CapiStruct_Data(this);
        }
        public CapiStruct_B3Configuration(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_Protocol0 = new CapiStruct_Data(this);
            m_Protocol1 = new CapiStruct_ProtocolX(this);
            m_Protocol2 = new CapiStruct_ProtocolX(this);
            m_Protocol3 = new CapiStruct_ProtocolX(this);
            m_Protocol4 = new CapiStruct_Protocol4(this);
            m_Protocol5 = new CapiStruct_Protocol5(this);
            m_Protocol7 = new CapiStruct_Data(this);
        }
        #endregion

        #region Internal Classes
        public class CapiStruct_ProtocolX : CapiStruct_Base
        {
            #region Vars
            protected ushort m_LowestIncomingChannel = 0;
            protected ushort m_HighestIncomingChannel = 0;
            protected ushort m_LowestTwoWayChannel = 1;
            protected ushort m_HighestTwoWayChannel = 1;
            protected ushort m_LowestOutgoingChannel = 0;
            protected ushort m_HighestOutgoingChannel = 0;
            protected ModuloMode_Enum m_ModuloMode = ModuloMode_Enum.NormalOperation;
            protected ushort m_WindowSize = 2;
            #endregion

            #region Properties
            public ushort LowestIncomingChannel
            {
                get { return m_LowestIncomingChannel; }
                set { m_LowestIncomingChannel = value; }
            }
            public ushort HighestIncomingChannel
            {
                get { return m_HighestIncomingChannel; }
                set { m_HighestIncomingChannel = value; }
            }
            public ushort LowestTwoWayChannel
            {
                get { return m_LowestTwoWayChannel; }
                set { m_LowestTwoWayChannel = value; }
            }
            public ushort HighestTwoWayChannel
            {
                get { return m_HighestTwoWayChannel; }
                set { m_HighestTwoWayChannel = value; }
            }
            public ushort LowestOutgoingChannel
            {
                get { return m_LowestOutgoingChannel; }
                set { m_LowestOutgoingChannel = value; }
            }
            public ushort HighestOutgoingChannel
            {
                get { return m_HighestOutgoingChannel; }
                set { m_HighestOutgoingChannel = value; }
            }
            public ModuloMode_Enum ModuloMode
            {
                get { return m_ModuloMode; }
                set { m_ModuloMode = value; }
            }
            public ushort WindowSize
            {
                get { return m_WindowSize; }
                set { m_WindowSize = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 16;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_ProtocolX(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_ProtocolX(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_ProtocolX(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    BitConverter.GetBytes(m_LowestIncomingChannel).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_HighestIncomingChannel).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_LowestTwoWayChannel).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_HighestTwoWayChannel).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_LowestOutgoingChannel).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes(m_HighestOutgoingChannel).CopyTo(lbData, liOffset); liOffset += 2;
                    lbData[liOffset] = (byte)m_ModuloMode; liOffset++;
                    BitConverter.GetBytes(m_WindowSize).CopyTo(lbData, liOffset); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_LowestIncomingChannel = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_HighestIncomingChannel = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_LowestTwoWayChannel = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_HighestTwoWayChannel = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_LowestOutgoingChannel = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_HighestOutgoingChannel = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_ModuloMode = (ModuloMode_Enum)lbData[liOffset]; liOffset++;
                    m_WindowSize = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol4 : CapiStruct_Base
        {
            #region Vars
            protected Resolution_Enum m_Resolution = Resolution_Enum.Standard;
            protected Format_Enum m_Format = Format_Enum.SFF;
            protected CapiStruct_IA5 m_StationID = null;
            protected CapiStruct_ISO8859_1 m_HeadLine = null;
            #endregion

            #region Properties
            public Resolution_Enum Resolution
            {
                get { return m_Resolution; }
                set { m_Resolution = value; }
            }
            public Format_Enum Format
            {
                get { return m_Format; }
                set { m_Format = value; }
            }
            public CapiStruct_IA5 StationID
            {
                get { return m_StationID; }
                set { m_StationID = value; }
            }
            public CapiStruct_ISO8859_1 HeadLine
            {
                get { return m_HeadLine; }
                set { m_HeadLine = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4 + m_StationID.StructSize + m_HeadLine.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol4(object loParent)
                : base(loParent)
            {
                m_StationID = new CapiStruct_IA5(this);
                m_HeadLine = new CapiStruct_ISO8859_1(this);
            }
            public CapiStruct_Protocol4(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_StationID = new CapiStruct_IA5(this);
                m_HeadLine = new CapiStruct_ISO8859_1(this);
            }
            public CapiStruct_Protocol4(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_StationID = new CapiStruct_IA5(this);
                m_HeadLine = new CapiStruct_ISO8859_1(this);
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    BitConverter.GetBytes((ushort)m_Resolution).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes((ushort)m_Format).CopyTo(lbData, liOffset); liOffset += 2;
                    lbData = m_StationID.AsByteArray(lbData, liOffset); liOffset += m_StationID.StructSize;
                    lbData = m_HeadLine.AsByteArray(lbData, liOffset); liOffset += m_HeadLine.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_Resolution = (Resolution_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Format = (Format_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_StationID.SetData(lbData, liOffset);
                    liOffset = m_HeadLine.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_Protocol5 : CapiStruct_Base
        {
            #region Vars
            protected B3ConfigurationProtocol5Options_Flags m_Options = 0;

            protected Format_Enum m_Format = Format_Enum.SFF;
            protected CapiStruct_IA5 m_StationID = null;
            protected CapiStruct_ISO8859_1 m_HeadLine = null;
            #endregion

            #region Properties
            public B3ConfigurationProtocol5Options_Flags Options
            {
                get { return m_Options; }
                set { m_Options = value; }
            }

            public Format_Enum Format
            {
                get { return m_Format; }
                set { m_Format = value; }
            }
            public CapiStruct_IA5 StationID
            {
                get { return m_StationID; }
                set { m_StationID = value; }
            }
            public CapiStruct_ISO8859_1 HeadLine
            {
                get { return m_HeadLine; }
                set { m_HeadLine = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4 + m_StationID.StructSize + m_HeadLine.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Protocol5(object loParent)
                : base(loParent)
            {
                m_StationID = new CapiStruct_IA5(this);
                m_HeadLine = new CapiStruct_ISO8859_1(this);
            }
            public CapiStruct_Protocol5(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_StationID = new CapiStruct_IA5(this);
                m_HeadLine = new CapiStruct_ISO8859_1(this);
            }
            public CapiStruct_Protocol5(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_StationID = new CapiStruct_IA5(this);
                m_HeadLine = new CapiStruct_ISO8859_1(this);
            }
            #endregion

            #region Methods
            public override byte[] AsByteArray(byte[] lbData, int liOffset)
            {
                int liDataSize = 0;
                int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

                byte[] lbHeader = WriteStructHeader();
                Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

                if (liDataSize > 0)
                {
                    BitConverter.GetBytes((ushort)m_Options).CopyTo(lbData, liOffset); liOffset += 2;
                    BitConverter.GetBytes((ushort)m_Format).CopyTo(lbData, liOffset); liOffset += 2;
                    lbData = m_StationID.AsByteArray(lbData, liOffset); liOffset += m_StationID.StructSize;
                    lbData = m_HeadLine.AsByteArray(lbData, liOffset); liOffset += m_HeadLine.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_Options = (B3ConfigurationProtocol5Options_Flags)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Format = (Format_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_StationID.SetData(lbData, liOffset);
                    liOffset = m_HeadLine.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        #endregion

        #region Properties
        private B3Protocol_Enum m_Protocol
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IB3ProtocolSelector)m_Parent).B3Protocol;
                }
                else
                {
                    return B3Protocol_Enum.Transparent;
                }
            }
        }

        public CapiStruct_Data Protocol0
        {
            get { return m_Protocol0; }
            set { m_Protocol0 = value; }
        }
        public CapiStruct_ProtocolX Protocol1
        {
            get { return m_Protocol1; }
            set { m_Protocol1 = value; }
        }
        public CapiStruct_ProtocolX Protocol2
        {
            get { return m_Protocol2; }
            set { m_Protocol2 = value; }
        }
        public CapiStruct_ProtocolX Protocol3
        {
            get { return m_Protocol3; }
            set { m_Protocol3 = value; }
        }
        public CapiStruct_Protocol4 Protocol4
        {
            get { return m_Protocol4; }
            set { m_Protocol4 = value; }
        }
        public CapiStruct_Protocol5 Protocol5
        {
            get { return m_Protocol5; }
            set { m_Protocol5 = value; }
        }
        public CapiStruct_Data Protocol7
        {
            get { return m_Protocol7; }
            set { m_Protocol7 = value; }
        }

        public override int DataSize
        {
            get
            {
                if ((ushort)m_Protocol == 0)
                {
                    return m_Protocol0.StructSize;
                }
                else if ((ushort)m_Protocol == 1)
                {
                    return m_Protocol1.StructSize;
                }
                else if ((ushort)m_Protocol == 2)
                {
                    return m_Protocol2.StructSize;
                }
                else if ((ushort)m_Protocol == 3)
                {
                    return m_Protocol3.StructSize;
                }
                else if ((ushort)m_Protocol == 4)
                {
                    return m_Protocol4.StructSize;
                }
                else if ((ushort)m_Protocol == 5)
                {
                    return m_Protocol5.StructSize;
                }
                else
                {
                    return m_Protocol7.StructSize;
                }
            }
        }
        #endregion

        #region Methods
        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            int liDataSize = 0;
            int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

            byte[] lbHeader = WriteStructHeader();
            Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

            if (liDataSize > 0)
            {
                if ((ushort)m_Protocol == 0)
                {
                    return m_Protocol0.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 1)
                {
                    return m_Protocol1.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 2)
                {
                    return m_Protocol2.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 3)
                {
                    return m_Protocol3.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 4)
                {
                    return m_Protocol4.AsByteArray(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 5)
                {
                    return m_Protocol5.AsByteArray(lbData, liOffset);
                }
                else
                {
                    return m_Protocol7.AsByteArray(lbData, liOffset);
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if ((ushort)m_Protocol == 0)
                {
                    return m_Protocol0.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 1)
                {
                    return m_Protocol1.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 2)
                {
                    return m_Protocol2.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 3)
                {
                    return m_Protocol3.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 4)
                {
                    return m_Protocol4.SetData(lbData, liOffset);
                }
                else if ((ushort)m_Protocol == 5)
                {
                    return m_Protocol5.SetData(lbData, liOffset);
                }
                else
                {
                    return m_Protocol7.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }
    public class CapiStruct_GlobalConfiguration : CapiStruct_Base
    {
        #region Vars
        protected BChannel_Operation_Enum m_BChannelOperation = BChannel_Operation_Enum.Default;
        #endregion

        #region Constructor
        public CapiStruct_GlobalConfiguration(object loParent) 
            : base(loParent)
        {
        }
        public CapiStruct_GlobalConfiguration(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_GlobalConfiguration(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
        }
        #endregion

        #region Properties
        public BChannel_Operation_Enum BChannelOperation
        {
            get { return m_BChannelOperation;  }
            set { m_BChannelOperation = value; }
        }

        public override int DataSize
        {
            get
            {
                return 2;
            }
        }
        #endregion

        #region Methods
        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            int liDataSize = 0;
            int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

            byte[] lbHeader = WriteStructHeader();
            Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

            if (liDataSize > 0)
            {
                BitConverter.GetBytes((ushort)m_BChannelOperation).CopyTo(lbData, liOffset); liOffset += 2;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_BChannelOperation = (BChannel_Operation_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
            }

            return liOffset;
        }
        #endregion
    }
}
