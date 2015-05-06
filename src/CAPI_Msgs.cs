#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
#endregion

namespace DSELib.CAPI
{
    public interface ICapiStruct
    {
        string ToString();

        int DataSize { get; }
        int StructSize { get; }
        object Parent { get; }

        byte[] AsByteArray();
        byte[] AsByteArray(byte[] lbData);
        byte[] AsByteArray(byte[] lbData, int liOffset);

        int SetData(byte[] lbData, int liOffset);
    }

    #region Selection-Interfaces
    public interface IFacilitySelector
    {
        FacilitySelector_Enum FacilitySelector { get; }
    }
    public interface IFunctionSelector
    {
        ushort Function { get; }
    }
    public interface IInfoSelector
    {
        InfoDataType DataType { get; }
        InfoChargeType ChargeType { get; }
    }
    public interface IB1ProtocolSelector
    {
        B1Protocol_Enum B1Protocol { get; }
    }
    public interface IB2ProtocolSelector
    {
        B2Protocol_Enum B2Protocol { get; }
    }
    public interface IB3ProtocolSelector
    {
        B3Protocol_Enum B3Protocol { get; }
    }
    #endregion

    public abstract class CapiStruct_Base : ICapiStruct, IDisposable
    {
        #region Vars
        protected object m_Parent = null;
        #endregion

        #region Constructor
        public CapiStruct_Base(object loParent)
        {
            m_Parent = loParent;
        }
        public CapiStruct_Base(object loParent, byte[] lbData)
        {
            m_Parent = loParent;
            SetData(lbData, 0);
        }
        public CapiStruct_Base(object loParent, byte[] lbData, int liOffset)
        {
            m_Parent = loParent;
            liOffset = SetData(lbData, liOffset);
        }
        #endregion

        #region Size
        public abstract int DataSize { get; }

        public int StructSize
        {
            get
            {
                return GetStructSize(DataSize);

            }
        }
        private int GetStructSize(int liDataSize)
        {
            if (liDataSize >= 255)
            {
                return liDataSize + 3;
            }
            else
            {
                return liDataSize + 1;
            }
        }

        protected int CheckStructSize(int liLength, ref int liDataSize)
        {
            liDataSize = DataSize;
            int liStructSize = GetStructSize(liDataSize);

            if (liLength < liStructSize)
            {
                throw new Exception("Byte-array smaller than needed.");
            }

            return liStructSize;
        }
        #endregion

        #region StructHeader-Functions
        protected byte[] WriteStructHeader()
        {
            int liSize = DataSize;

            if (liSize >= 255)
            {
                byte[] lbHeader = new byte[liSize + 3];
                lbHeader[0] = 255;
                Array.Copy(BitConverter.GetBytes(liSize), 0, lbHeader, 1, 2);
                return lbHeader;
            }
            else
            {
                byte[] lbHeader = new byte[liSize + 1];
                lbHeader[0] = (byte)liSize;
                return lbHeader;
            }
        }

        protected int ReadStructHeader(byte[] lbData, ref int liOffset)
        {
            int liWholeLength = lbData.Length - liOffset;

            if (liWholeLength != 0)
            {
                byte lbLen = lbData[liOffset];
                liOffset++;

                if (lbLen == 255) // Daten sind größer als 255
                {
                    if (liWholeLength > 1)
                    {
                        ushort liLen = BitConverter.ToUInt16(lbData, liOffset);
                        liOffset += 2;

                        if (liWholeLength - 3 < liLen)
                        {
                            throw new Exception("Extended length of Capi-structure is greater than the real size.");
                        }
                        else
                        {
                            return liLen;
                        }
                    }
                    else
                    {
                        throw new Exception("Extended length of Capi-structure is not available.");
                    }
                }
                else if (lbLen == 0)
                {
                    return 0;
                }
                else
                {
                    if (liWholeLength - 1 < lbLen)
                    {
                        throw new Exception("Simple length of Capi-Structure is greater than the real size.");
                    }
                    else
                    {
                        return lbLen;
                    }
                }
            }
            else
            {
                throw new Exception("Capi-structure doesn't have any length.");
            }
        }
        #endregion

        #region Parent
        public object Parent
        {
            get { return m_Parent; }
        }
        #endregion

        #region AsByteArray
        public byte[] AsByteArray()
        {
            return AsByteArray(new byte[StructSize], 0);
        }
        public byte[] AsByteArray(byte[] lbData)
        {
            return AsByteArray(lbData, 0);
        }
        public abstract byte[] AsByteArray(byte[] lbData, int liOffset);
        #endregion

        public virtual void Dispose()
        {
        }

        public abstract int SetData(byte[] lbData, int liOffset);
    }
    public class CapiStruct_Data : CapiStruct_Base
    {
        #region Vars
        protected byte[] m_Data = null;
        #endregion

        #region Constructor
        public CapiStruct_Data(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_Data(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_Data(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return ASCIIEncoding.ASCII.GetString(m_Data);
        }
        #endregion

        #region DataSize
        public override int DataSize
        {
            get
            {
                if (m_Data == null)
                {
                    return 0;
                }
                else
                {
                    return m_Data.Length;
                }
            }
        }
        #endregion

        #region AsByteArray
        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            int liDataSize = 0;
            int liStructSize = CheckStructSize(lbData.Length - liOffset, ref liDataSize);

            byte[] lbHeader = WriteStructHeader();
            Array.Copy(lbHeader, 0, lbData, liOffset, lbHeader.Length); liOffset += lbHeader.Length;

            if (liDataSize > 0)
            {
                Array.Copy(m_Data, 0, lbData, liOffset, liDataSize);
            }

            return lbData;
        }
        #endregion

        #region SetData
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_Data = new byte[liLen];
                Array.Copy(lbData, liOffset, m_Data, 0, liLen);
                liOffset += liLen;
            }
            else
            {
                m_Data = null;
            }

            return liOffset;
        }
        #endregion
    }

    public abstract class CapiInMessage : IDisposable
    {
        #region Vars / Properties
        protected CapiMessageHeader m_Header = null;
        public CapiMessageHeader Header { get { return m_Header; } set { m_Header = value; } }
        #endregion

        #region Constructor
        public CapiInMessage()
        {
            m_Header = new CapiMessageHeader();
        }
        public CapiInMessage(byte[] lbData)
        {
            m_Header = new CapiMessageHeader(lbData, 0);
        }
        public CapiInMessage(byte[] lbData, int liOffset)
        {
            m_Header = new CapiMessageHeader(lbData, liOffset);
        }
        #endregion

        public virtual void Dispose()
        {
        }
    }
    public abstract class CapiOutMessage : IDisposable
    {
        #region Vars / Properties
        protected CapiMessageHeader m_Header = null;
        public CapiMessageHeader Header { get { return m_Header; } }

        public abstract int Size { get; }
        #endregion

        #region Constructor
        public CapiOutMessage()
        {
            m_Header = new CapiMessageHeader();
        }
        #endregion

        #region AsByteArray
        public byte[] AsByteArray()
        {
            return AsByteArray(new byte[Size], 0);
        }
        public byte[] AsByteArray(byte[] lbData)
        {
            return AsByteArray(lbData, 0);
        }
        public abstract byte[] AsByteArray(byte[] lbData, int liOffset);
        #endregion

        public virtual void Dispose()
        {
        }
    }

    public class CapiMessageHeader : IDisposable
    {
        #region Vars
        protected ushort m_Length;
        protected ushort m_AppID;
        protected CapiMessages_Commands_Enum m_Command;
        protected CapiMessages_SubCommands_Enum m_SubCommand;
        protected ushort m_MessageNumber;
        #endregion

        #region Properties
        public ushort Length { get { return m_Length; } }
        public ushort AppID { get { return m_AppID; } set { m_AppID = value; } }
        public CapiMessages_Commands_Enum Command { get { return m_Command; } set { m_Command = value; } }
        public CapiMessages_SubCommands_Enum SubCommand { get { return m_SubCommand; } set { m_SubCommand = value; } }
        public ushort MessageNumber { get { return m_MessageNumber; } set { m_MessageNumber = value; } }
        #endregion

        #region Command-Functions
        public CapiMessages_Enum GetCommand()
        {
            return (CapiMessages_Enum)(((byte)m_Command * 0x100) + (byte)m_SubCommand);
        }
        public void SetCommand(CapiMessages_Enum loCommand)
        {
            m_Command = (CapiMessages_Commands_Enum)((int)loCommand / 0x100);
            m_SubCommand = (CapiMessages_SubCommands_Enum)((int)loCommand % 0x100);
        }
        #endregion

        #region Size
        public int Size
        {
            get
            {
                return 8;
            }
        }
        #endregion

        #region AsByteArray
        public byte[] AsByteArray()
        {
            return AsByteArray(new byte[Size], 0);
        }
        public byte[] AsByteArray(byte[] lbData)
        {
            return AsByteArray(lbData, 0);
        }
        public byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                BitConverter.GetBytes(m_Length).CopyTo(lbData, liOffset);
                BitConverter.GetBytes(m_AppID).CopyTo(lbData, liOffset + 2);
                lbData[liOffset + 4] = (byte)m_Command;
                lbData[liOffset + 5] = (byte)m_SubCommand;
                BitConverter.GetBytes(m_MessageNumber).CopyTo(lbData, liOffset + 6);
            }
            return lbData;
        }
        #endregion

        #region SetData
        public void SetData(byte[] lbData, int liOffset)
        {
            m_Length = BitConverter.ToUInt16(lbData, liOffset);
            m_AppID = BitConverter.ToUInt16(lbData, 2 + liOffset);
            m_Command = (CapiMessages_Commands_Enum)lbData[4 + liOffset];
            m_SubCommand = (CapiMessages_SubCommands_Enum)lbData[5 + liOffset];
            m_MessageNumber = BitConverter.ToUInt16(lbData, 6 + liOffset);
        }
        #endregion

        #region Constructor
        public CapiMessageHeader()
        {
        }
        public CapiMessageHeader(byte[] lbData)
        {
            SetData(lbData, 0);
        }
        public CapiMessageHeader(byte[] lbData, int liOffset)
        {
            SetData(lbData, liOffset);
        }
        #endregion

        public virtual void Dispose()
        {
        }
    }

    public class CapiProfile : IDisposable
    {
        #region Vars
        protected ushort m_NumberOfController = 0;
        protected ushort m_MaxBChannels = 0;
        protected uint m_GlobalOption = 0;
        protected uint m_B1Protocol = 0;
        protected uint m_B2Protocol = 0;
        protected uint m_B3Protocol = 0;
        protected byte[] m_CapiInfo = new byte[24];
        protected byte[] m_Manufacturer = new byte[20];
        #endregion

        #region Properties
        public ushort NumberOfController
        {
            get { return m_NumberOfController; }
        }
        public ushort MaxBChannels
        {
            get { return m_MaxBChannels; }
        }
        public uint GlobalOptionFlags
        {
            get { return m_GlobalOption; }
        }
        public uint B1ProtocolFlags
        {
            get { return m_B1Protocol; }
        }
        public uint B2ProtocolFlags
        {
            get { return m_B2Protocol; }
        }
        public uint B3ProtocolFlags
        {
            get { return m_B3Protocol; }
        }
        public byte[] CapiInfo
        {
            get { return m_CapiInfo; }
        }
        public byte[] Manufacturer
        {
            get { return m_Manufacturer; }
        }

        public bool GO_InternalControllerSupported
        {
            get { return ((m_GlobalOption & 1) == 1); }
        }
        public bool GO_ExternalEquipmentSupported
        {
            get { return ((m_GlobalOption & 2) == 2); }
        }
        public bool GO_HandsetSupported
        {
            get { return ((m_GlobalOption & 4) == 4); }
        }
        public bool GO_DTMFSupported
        {
            get { return ((m_GlobalOption & 8) == 8); }
        }
        public bool GO_SupplementaryServices
        {
            get { return ((m_GlobalOption & 16) == 16); }
        }
        public bool GO_ChannelAllocationSupported
        {
            get { return ((m_GlobalOption & 32) == 32); }
        }
        public bool GO_ParamBChannelOperationSupported
        {
            get { return ((m_GlobalOption & 64) == 64); }
        }
        public bool GO_LineInterconnectSupported
        {
            get { return ((m_GlobalOption & 128) == 128); }
        }

        public bool B1_Kbits64WithHDLCFraming
        {
            get { return ((m_B1Protocol & 1) == 1); }
        }
        public bool B1_Kbits64BitTransparent
        {
            get { return ((m_B1Protocol & 2) == 2); }
        }
        public bool B1_V110Asynchronous
        {
            get { return ((m_B1Protocol & 4) == 4); }
        }
        public bool B1_V110Synchronous
        {
            get { return ((m_B1Protocol & 8) == 8); }
        }
        public bool B1_T30ModemForGroup3Fax
        {
            get { return ((m_B1Protocol & 16) == 16); }
        }
        public bool B1_Kbits64BitinvertedWithHDLCFraming
        {
            get { return ((m_B1Protocol & 32) == 32); }
        }
        public bool B1_Kbits56BitTransparent
        {
            get { return ((m_B1Protocol & 64) == 64); }
        }
        public bool B1_ModemWithAllNegotiations
        {
            get { return ((m_B1Protocol & 128) == 128); }
        }
        public bool B1_ModemAsynchronous
        {
            get { return ((m_B1Protocol & 256) == 256); }
        }
        public bool B1_ModemSynchronous
        {
            get { return ((m_B1Protocol & 512) == 512); }
        }

        public bool B2_ISO7776
        {
            get { return ((m_B2Protocol & 1) == 1); }
        }
        public bool B2_Transparent
        {
            get { return ((m_B2Protocol & 2) == 2); }
        }
        public bool B2_SDLC
        {
            get { return ((m_B2Protocol & 4) == 4); }
        }
        public bool B2_LAPD
        {
            get { return ((m_B2Protocol & 8) == 8); }
        }
        public bool B2_T30ForGroup3Fax
        {
            get { return ((m_B2Protocol & 16) == 16); }
        }
        public bool B2_PointToPointProtocol
        {
            get { return ((m_B2Protocol & 32) == 32); }
        }
        public bool B2_Transparent_IgnoringFramingError
        {
            get { return ((m_B2Protocol & 64) == 64); }
        }
        public bool B2_ModemErrorConnectionAndCompression
        {
            get { return ((m_B2Protocol & 128) == 128); }
        }
        public bool B2_ISO7776_ModifiedSupporting
        {
            get { return ((m_B2Protocol & 256) == 256); }
        }
        public bool B2_V120AsynchronousMode
        {
            get { return ((m_B2Protocol & 512) == 512); }
        }
        public bool B2_V120AsynchronousModeSupportingV42Bis
        {
            get { return ((m_B2Protocol & 1024) == 1024); }
        }
        public bool B2_V120BitTransparentMode
        {
            get { return ((m_B2Protocol & 2048) == 2048); }
        }
        public bool B2_LAPD_IncludingFreeSAPI
        {
            get { return ((m_B2Protocol & 4096) == 4096); }
        }

        public bool B3_Transparent
        {
            get { return ((m_B3Protocol & 1) == 1); }
        }
        public bool B3_T90NL
        {
            get { return ((m_B3Protocol & 2) == 2); }
        }
        public bool B3_ISO8208
        {
            get { return ((m_B3Protocol & 4) == 4); }
        }
        public bool B3_X25DCE
        {
            get { return ((m_B3Protocol & 8) == 8); }
        }
        public bool B3_T30ForGroup3Fax
        {
            get { return ((m_B3Protocol & 16) == 16); }
        }
        public bool B3_T30ForGroup3FaxWithExtensions
        {
            get { return ((m_B3Protocol & 32) == 32); }
        }
        public bool B3_Modem
        {
            get { return ((m_B3Protocol & 128) == 128); }
        }
        #endregion

        #region Constructors
        public CapiProfile(byte[] lbData)
        {
            SetData(lbData, 0);
        }
        public CapiProfile(byte[] lbData, int liOffset)
        {
            SetData(lbData, liOffset);
        }
        #endregion

        #region SetData
        public int SetData(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < 64)
            {
                throw new ArgumentException("Byte-array smaller than needed.", "lbData");
            }
            else
            {
                m_NumberOfController = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                m_MaxBChannels = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                m_GlobalOption = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                m_B1Protocol = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                m_B2Protocol = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                m_B3Protocol = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                Array.Copy(m_CapiInfo, 0, lbData, liOffset, 24); liOffset += 24;
                Array.Copy(m_Manufacturer, 0, lbData, liOffset, 20); liOffset += 20;
            }

            return liOffset;
        }
        #endregion

        public virtual void Dispose()
        {
        }
    }
}
