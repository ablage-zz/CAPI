#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
#endregion

namespace DSELib.CAPI
{
    public class CONNECT_REQ : CapiOutMessage
    {
        protected ControllerClass m_Controller = new ControllerClass();
        protected ushort m_CIPValue = 0;
        protected CapiStruct_CalledNumber m_CalledPartyNumber = null;
        protected CapiStruct_CallingNumber m_CallingPartyNumber = null;
        protected CapiStruct_SubAddress m_CalledPartySubaddress = null;
        protected CapiStruct_SubAddress m_CallingPartySubaddress = null;
        protected CapiStruct_BProtocol m_BProtocol = null;
        protected CapiStruct_Data m_BearerCapability = null;
        protected CapiStruct_Data m_LowLayerCompatibility = null;
        protected CapiStruct_Data m_HighLayerCompatibility = null;
        protected CapiStruct_AdditionalInfo m_AdditionalInfo = null;

        public ControllerClass Controller
        {
            get { return m_Controller; }
            set { m_Controller = value; }
        }
        public ushort CIPValue
        {
            get { return m_CIPValue; }
            set { m_CIPValue = value; }
        }
        public CapiStruct_CalledNumber CalledPartyNumber
        {
            get { return m_CalledPartyNumber; }
            set { m_CalledPartyNumber = value; }
        }
        public CapiStruct_CallingNumber CallingPartyNumber
        {
            get { return m_CallingPartyNumber; }
            set { m_CallingPartyNumber = value; }
        }
        public CapiStruct_SubAddress CalledPartySubaddress
        {
            get { return m_CalledPartySubaddress; }
            set { m_CalledPartySubaddress = value; }
        }
        public CapiStruct_SubAddress CallingPartySubaddress
        {
            get { return m_CallingPartySubaddress; }
            set { m_CallingPartySubaddress = value; }
        }
        public CapiStruct_BProtocol BProtocol
        {
            get { return m_BProtocol; }
            set { m_BProtocol = value; }
        }
        public CapiStruct_Data BearerCapability
        {
            get { return m_BearerCapability; }
            set { m_BearerCapability = value; }
        }
        public CapiStruct_Data LowLayerCompatibility
        {
            get { return m_LowLayerCompatibility; }
            set { m_LowLayerCompatibility = value; }
        }
        public CapiStruct_Data HighLayerCompatibility
        {
            get { return m_HighLayerCompatibility; }
            set { m_HighLayerCompatibility = value; }
        }
        public CapiStruct_AdditionalInfo AdditionalInfo
        {
            get { return m_AdditionalInfo; }
            set { m_AdditionalInfo = value; }
        }

        public CONNECT_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.CONNECT_REQ);

            m_CalledPartyNumber = new CapiStruct_CalledNumber(this);
            m_CallingPartyNumber = new CapiStruct_CallingNumber(this);
            m_CalledPartySubaddress = new CapiStruct_SubAddress(this);
            m_CallingPartySubaddress = new CapiStruct_SubAddress(this);
            m_BProtocol = new CapiStruct_BProtocol(this);
            m_BearerCapability = new CapiStruct_Data(this);
            m_LowLayerCompatibility = new CapiStruct_Data(this);
            m_HighLayerCompatibility = new CapiStruct_Data(this);
            m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_Controller.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes(m_CIPValue).CopyTo(lbData, liOffset); liOffset += 2;
                m_CalledPartyNumber.AsByteArray(lbData, liOffset); liOffset += m_CalledPartyNumber.StructSize;
                m_CallingPartyNumber.AsByteArray(lbData, liOffset); liOffset += m_CallingPartyNumber.StructSize;
                m_CalledPartySubaddress.AsByteArray(lbData, liOffset); liOffset += m_CalledPartySubaddress.StructSize;
                m_CallingPartySubaddress.AsByteArray(lbData, liOffset); liOffset += m_CallingPartySubaddress.StructSize;
                m_BProtocol.AsByteArray(lbData, liOffset); liOffset += m_BProtocol.StructSize;
                m_BearerCapability.AsByteArray(lbData, liOffset); liOffset += m_BearerCapability.StructSize;
                m_LowLayerCompatibility.AsByteArray(lbData, liOffset); liOffset += m_LowLayerCompatibility.StructSize;
                m_HighLayerCompatibility.AsByteArray(lbData, liOffset); liOffset += m_HighLayerCompatibility.StructSize;
                m_AdditionalInfo.AsByteArray(lbData, liOffset); liOffset += m_AdditionalInfo.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 6 + m_CalledPartyNumber.StructSize + m_CallingPartyNumber.StructSize +
                       m_CalledPartySubaddress.StructSize + m_CallingPartySubaddress.StructSize +
                       m_BProtocol.StructSize + m_BearerCapability.StructSize + m_LowLayerCompatibility.StructSize +
                       m_HighLayerCompatibility.StructSize + m_AdditionalInfo.StructSize;
            }
        }
    }

    public class CONNECT_RESP : CapiOutMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();
        protected Reject_Enum m_Reject = Reject_Enum.AcceptCall;
        protected byte m_RejectNetwork = 0;
        protected CapiStruct_BProtocol m_BProtocol = null;
        protected CapiStruct_ConnectedNumber m_ConnectedNumber = null;
        protected CapiStruct_SubAddress m_ConnectedSubaddress = null;
        protected CapiStruct_Data m_LowLayerCompatibility = null;
        protected CapiStruct_AdditionalInfo m_AdditionalInfo = null;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public Reject_Enum Reject
        {
            get { return m_Reject; }
            set { m_Reject = value; }
        }
        public byte RejectNetwork
        {
            get { return m_RejectNetwork; }
            set { m_RejectNetwork = value; }
        }
        public CapiStruct_BProtocol BProtocol
        {
            get { return m_BProtocol; }
            set { m_BProtocol = value; }
        }
        public CapiStruct_ConnectedNumber ConnectedNumber
        {
            get { return m_ConnectedNumber; }
            set { m_ConnectedNumber = value; }
        }
        public CapiStruct_SubAddress ConnectedSubaddress
        {
            get { return m_ConnectedSubaddress; }
            set { m_ConnectedSubaddress = value; }
        }
        public CapiStruct_Data LowLayerCompatibility
        {
            get { return m_LowLayerCompatibility; }
            set { m_LowLayerCompatibility = value; }
        }
        public CapiStruct_AdditionalInfo AdditionalInfo
        {
            get { return m_AdditionalInfo; }
            set { m_AdditionalInfo = value; }
        }

        public CONNECT_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.CONNECT_RESP);

            m_BProtocol = new CapiStruct_BProtocol(this);
            m_ConnectedNumber = new CapiStruct_ConnectedNumber(this);
            m_ConnectedSubaddress = new CapiStruct_SubAddress(this);
            m_LowLayerCompatibility = new CapiStruct_Data(this);
            m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_PLCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                if (m_Reject == Reject_Enum.Cause_from_network)
                {
                    BitConverter.GetBytes((ushort)(m_RejectNetwork + Reject_Enum.Cause_from_network)).CopyTo(lbData, liOffset); liOffset += 2;
                }
                else
                {
                    BitConverter.GetBytes((ushort)m_Reject).CopyTo(lbData, liOffset); liOffset += 2;
                }
                m_BProtocol.AsByteArray(lbData, liOffset); liOffset += m_BProtocol.StructSize;
                m_ConnectedNumber.AsByteArray(lbData, liOffset); liOffset += m_ConnectedNumber.StructSize;
                m_ConnectedSubaddress.AsByteArray(lbData, liOffset); liOffset += m_ConnectedSubaddress.StructSize;
                m_LowLayerCompatibility.AsByteArray(lbData, liOffset); liOffset += m_LowLayerCompatibility.StructSize;
                m_AdditionalInfo.AsByteArray(lbData, liOffset); liOffset += m_AdditionalInfo.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 6 + m_BProtocol.StructSize + m_ConnectedNumber.StructSize + m_ConnectedSubaddress.StructSize +
                       m_LowLayerCompatibility.StructSize + m_AdditionalInfo.StructSize;
            }
        }
    }

    public class CONNECT_ACTIVE_RESP : CapiOutMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }

        public CONNECT_ACTIVE_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.CONNECT_ACTIVE_RESP); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_PLCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4;
            }
        }
    }

    public class DISCONNECT_REQ : CapiOutMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();
        protected CapiStruct_AdditionalInfo m_AdditionalInfo = null;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public CapiStruct_AdditionalInfo AdditionalInfo
        {
            get { return m_AdditionalInfo; }
            set { m_AdditionalInfo = value; }
        }

        public DISCONNECT_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.DISCONNECT_REQ); 

            m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_PLCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                m_AdditionalInfo.AsByteArray(lbData, liOffset); liOffset += m_AdditionalInfo.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4 + m_AdditionalInfo.StructSize;
            }
        }
    }

    public class DISCONNECT_RESP : CapiOutMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }

        public DISCONNECT_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.DISCONNECT_RESP); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_PLCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4;
            }
        }
    }

    public class ALERT_REQ : CapiOutMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();
        protected CapiStruct_AdditionalInfo m_AdditionalInfo = null;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public CapiStruct_AdditionalInfo AdditionalInfo
        {
            get { return m_AdditionalInfo; }
            set { m_AdditionalInfo = value; }
        }

        public ALERT_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.ALERT_REQ); 

            m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_PLCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                m_AdditionalInfo.AsByteArray(lbData, liOffset); liOffset += m_AdditionalInfo.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4 + m_AdditionalInfo.StructSize;
            }
        }
    }

    public class INFO_REQ : CapiOutMessage
    {
        protected NCCIClass m_Controller_PLCI_NCCI = new NCCIClass();
        protected CapiStruct_CalledNumber m_CalledPartyNumber = null;
        protected CapiStruct_AdditionalInfo m_AdditionalInfo = null;

        public NCCIClass Controller_PLCI_NCCI
        {
            get { return m_Controller_PLCI_NCCI; }
            set { m_Controller_PLCI_NCCI = value; }
        }
        public CapiStruct_CalledNumber CalledPartyNumber
        {
            get { return m_CalledPartyNumber; }
            set { m_CalledPartyNumber = value; }
        }
        public CapiStruct_AdditionalInfo AdditionalInfo
        {
            get { return m_AdditionalInfo; }
            set { m_AdditionalInfo = value; }
        }

        public INFO_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.INFO_REQ); 

            m_CalledPartyNumber = new CapiStruct_CalledNumber(this);
            m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_Controller_PLCI_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                m_CalledPartyNumber.AsByteArray(lbData, liOffset); liOffset += m_CalledPartyNumber.StructSize;
                m_AdditionalInfo.AsByteArray(lbData, liOffset); liOffset += m_AdditionalInfo.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4 + m_CalledPartyNumber.StructSize + m_AdditionalInfo.StructSize;
            }
        }
    }

    public class INFO_RESP : CapiOutMessage
    {
        protected NCCIClass m_Controller_PLCI_NCCI = new NCCIClass();

        public NCCIClass Controller_PLCI_NCCI
        {
            get { return m_Controller_PLCI_NCCI; }
            set { m_Controller_PLCI_NCCI = value; }
        }

        public INFO_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.INFO_RESP); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_Controller_PLCI_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4;
            }
        }
    }

    public class CONNECT_B3_REQ : CapiOutMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();
        protected CapiStruct_Data m_NetworkControlProtocolInformation = null;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public CapiStruct_Data NetworkControlProtocolInformation
        {
            get { return m_NetworkControlProtocolInformation; }
            set { m_NetworkControlProtocolInformation = value; }
        }

        public CONNECT_B3_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.CONNECT_B3_REQ);
            m_NetworkControlProtocolInformation = new CapiStruct_Data(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_PLCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                m_NetworkControlProtocolInformation.AsByteArray(lbData, liOffset); liOffset += m_NetworkControlProtocolInformation.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4 + m_NetworkControlProtocolInformation.StructSize;
            }
        }
    }

    public class CONNECT_B3_RESP : CapiOutMessage
    {
        protected NCCIClass m_NCCI = new NCCIClass();
        protected Reject_Enum m_Reject = Reject_Enum.AcceptCall;
        protected CapiStruct_Data m_NetworkControlProtocolInformation = null;

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }
        public Reject_Enum Reject
        {
            get { return m_Reject; }
            set { m_Reject = value; }
        }
        public CapiStruct_Data NetworkControlProtocolInformation
        {
            get { return m_NetworkControlProtocolInformation; }
            set { m_NetworkControlProtocolInformation = value; }
        }

        public CONNECT_B3_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.CONNECT_B3_RESP);

            m_NetworkControlProtocolInformation = new CapiStruct_Data(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes((ushort)m_Reject).CopyTo(lbData, liOffset); liOffset += 2;
                m_NetworkControlProtocolInformation.AsByteArray(lbData, liOffset); liOffset += m_NetworkControlProtocolInformation.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 6 + m_NetworkControlProtocolInformation.StructSize;
            }
        }
    }

    public class CONNECT_B3_ACTIVE_RESP : CapiOutMessage
    {
        protected NCCIClass m_NCCI = new NCCIClass();

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }

        public CONNECT_B3_ACTIVE_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.CONNECT_B3_ACTIVE_RESP); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4;
            }
        }
    }

    public class CONNECT_B3_T90_ACTIVE_RESP : CapiOutMessage
    {
        protected NCCIClass m_NCCI = new NCCIClass();

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }

        public CONNECT_B3_T90_ACTIVE_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.CONNECT_B3_T90_ACTIVE_RESP); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4;
            }
        }
    }

    public class DISCONNECT_B3_REQ : CapiOutMessage
    {
        protected NCCIClass m_NCCI = new NCCIClass();
        protected CapiStruct_Data m_NetworkControlProtocolInformation = null;

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }
        public CapiStruct_Data NetworkControlProtocolInformation
        {
            get { return m_NetworkControlProtocolInformation; }
            set { m_NetworkControlProtocolInformation = value; }
        }

        public DISCONNECT_B3_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.DISCONNECT_B3_REQ);

            m_NetworkControlProtocolInformation = new CapiStruct_Data(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                m_NetworkControlProtocolInformation.AsByteArray(lbData, liOffset); liOffset += m_NetworkControlProtocolInformation.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4 + m_NetworkControlProtocolInformation.StructSize;
            }
        }
    }

    public class DISCONNECT_B3_RESP : CapiOutMessage
    {
        protected NCCIClass m_NCCI = new NCCIClass();

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }

        public DISCONNECT_B3_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.DISCONNECT_B3_RESP); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4;
            }
        }
    }

    public class DATA_B3_REQ : CapiOutMessage
    {
        private IntPtr m_Data = IntPtr.Zero;
        private ushort m_DataLength = 0;
        private ulong m_Data64 = 0;

        protected NCCIClass m_NCCI = new NCCIClass();
        protected byte[] m_DataBuffer = null;
        protected ushort m_DataHandle = 0;
        protected DataB3_Flags m_Flags = 0;


        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }
        public byte[] Buffer
        {
            get { return m_DataBuffer; }
            set { m_DataBuffer = value; }
        }
        public ushort Handle
        {
            get { return m_DataHandle; }
            set { m_DataHandle = value; }
        }
        public DataB3_Flags Flags
        {
            get { return m_Flags; }
            set { m_Flags = value; }
        }


        public DATA_B3_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.DATA_B3_REQ); 
        }
        public override void Dispose() 
        {
            FreeBuffer();
        }

        private void AllocateBuffer()
        {
            FreeBuffer();
            if (m_DataBuffer == null)
            {
                m_DataLength = (ushort)m_DataBuffer.Length;
                m_Data = Marshal.AllocHGlobal(m_DataLength);
                Marshal.Copy(m_DataBuffer, 0, m_Data, m_DataLength);
            }
        }
        private void FreeBuffer()
        {
            if (m_Data != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(m_Data);
            }
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                AllocateBuffer();
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes(m_Data.ToInt32()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes(m_DataLength).CopyTo(lbData, liOffset); liOffset += 2;
                BitConverter.GetBytes(m_DataHandle).CopyTo(lbData, liOffset); liOffset += 2;
                BitConverter.GetBytes((ushort)m_Flags).CopyTo(lbData, liOffset); liOffset += 2;
                BitConverter.GetBytes(m_Data64).CopyTo(lbData, liOffset); liOffset += 8;

                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 22;
            }
        }
    }

    public class DATA_B3_RESP : CapiOutMessage
    {
        protected NCCIClass m_NCCI = new NCCIClass();
        protected ushort m_DataHandle = 0;

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }
        public ushort Handle
        {
            get { return m_DataHandle; }
            set { m_DataHandle = value; }
        }

        public DATA_B3_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.DATA_B3_RESP); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes(m_DataHandle).CopyTo(lbData, liOffset); liOffset += 2;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 6;
            }
        }
    }

    public class RESET_B3_REQ : CapiOutMessage
    {
        protected NCCIClass m_NCCI = new NCCIClass();
        protected CapiStruct_Data m_NetworkControlProtocolInformation = null;

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }
        public CapiStruct_Data NetworkControlProtocolInformation
        {
            get { return m_NetworkControlProtocolInformation; }
            set { m_NetworkControlProtocolInformation = value; }
        }

        public RESET_B3_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.RESET_B3_REQ);

            m_NetworkControlProtocolInformation = new CapiStruct_Data(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                m_NetworkControlProtocolInformation.AsByteArray(lbData, liOffset); liOffset += m_NetworkControlProtocolInformation.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4 + m_NetworkControlProtocolInformation.StructSize;
            }
        }
    }

    public class RESET_B3_RESP : CapiOutMessage
    {
        protected NCCIClass m_NCCI = new NCCIClass();

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }

        public RESET_B3_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.RESET_B3_RESP); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4;
            }
        }
    }

    public class LISTEN_REQ : CapiOutMessage
    {
        protected ControllerClass m_Controller = new ControllerClass();
        protected InfoMask_Flags m_InfoMask = 0;
        protected CIPMask_Flags m_CIPMask = 0;
        protected uint m_CIPMask2 = 0;
        protected CapiStruct_CallingNumber m_CallingPartyNumber = null;
        protected CapiStruct_SubAddress m_CallingPartySubaddress = null;

        public ControllerClass Controller
        {
            get { return m_Controller; }
            set { m_Controller = value; }
        }
        public InfoMask_Flags InfoMask
        {
            get { return m_InfoMask; }
            set { m_InfoMask = value; }
        }
        public CIPMask_Flags CIPMask
        {
            get { return m_CIPMask; }
            set { m_CIPMask = value; }
        }
        public uint CIPMask2
        {
            get { return m_CIPMask2; }
            set { m_CIPMask2 = value; }
        }
        public CapiStruct_CallingNumber CallingPartyNumber
        {
            get { return m_CallingPartyNumber; }
            set { m_CallingPartyNumber = value; }
        }
        public CapiStruct_SubAddress CallingPartySubaddress
        {
            get { return m_CallingPartySubaddress; }
            set { m_CallingPartySubaddress = value; }
        }



        public LISTEN_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.LISTEN_REQ); 
        
            m_CallingPartyNumber = new CapiStruct_CallingNumber(this);
            m_CallingPartySubaddress = new CapiStruct_SubAddress(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_Controller.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes((uint)m_InfoMask).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes((uint)m_CIPMask).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes(m_CIPMask2).CopyTo(lbData, liOffset); liOffset += 4;

                lbData = m_CallingPartyNumber.AsByteArray(lbData, liOffset); liOffset += m_CallingPartyNumber.StructSize;
                lbData = m_CallingPartySubaddress.AsByteArray(lbData, liOffset); liOffset += m_CallingPartySubaddress.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 16 + m_CallingPartyNumber.StructSize + m_CallingPartySubaddress.StructSize;
            }
        }
    }

    public class FACILITY_REQ : CapiOutMessage, IFacilitySelector
    {
        protected NCCIClass m_Controller_PLCI_NCCI = new NCCIClass();
        protected FacilitySelector_Enum m_FacilitySelector = 0;
        protected CapiStruct_FacilityRequestParameter m_FacilityRequestParameter = null;

        public NCCIClass Controller_PLCI_NCCI
        {
            get { return m_Controller_PLCI_NCCI; }
            set { m_Controller_PLCI_NCCI = value; }
        }
        public FacilitySelector_Enum FacilitySelector
        {
            get { return m_FacilitySelector; }
            set { m_FacilitySelector = value; }
        }
        public CapiStruct_FacilityRequestParameter FacilityRequestParameter
        {
            get { return m_FacilityRequestParameter; }
            set { m_FacilityRequestParameter = value; }
        }

        public FACILITY_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.FACILITY_REQ); 

            m_FacilityRequestParameter = new CapiStruct_FacilityRequestParameter(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_Controller_PLCI_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes((ushort)m_FacilitySelector).CopyTo(lbData, liOffset); liOffset += 2;
                m_FacilityRequestParameter.AsByteArray(lbData, liOffset); liOffset += m_FacilityRequestParameter.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 6 + m_FacilityRequestParameter.StructSize;
            }
        }
    }

    public class FACILITY_RESP : CapiOutMessage, IFacilitySelector
    {
        protected NCCIClass m_Controller_PLCI_NCCI = new NCCIClass();
        protected FacilitySelector_Enum m_FacilitySelector = 0;
        protected CapiStruct_FacilityResponseParameter m_FacilityResponseParameter = null;

        public NCCIClass Controller_PLCI_NCCI
        {
            get { return m_Controller_PLCI_NCCI; }
            set { m_Controller_PLCI_NCCI = value; }
        }
        public FacilitySelector_Enum FacilitySelector
        {
            get { return m_FacilitySelector; }
            set { m_FacilitySelector = value; }
        }
        public CapiStruct_FacilityResponseParameter FacilityResponseParameter
        {
            get { return m_FacilityResponseParameter; }
            set { m_FacilityResponseParameter = value; }
        }

        public FACILITY_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.FACILITY_RESP); 

            m_FacilityResponseParameter = new CapiStruct_FacilityResponseParameter(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_Controller_PLCI_NCCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes((ushort)m_FacilitySelector).CopyTo(lbData, liOffset); liOffset += 2;
                m_FacilityResponseParameter.AsByteArray(lbData, liOffset); liOffset += m_FacilityResponseParameter.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 6 + m_FacilityResponseParameter.StructSize;
            }
        }
    }

    public class SELECT_B_PROTOCOL_REQ : CapiOutMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();
        protected CapiStruct_BProtocol m_BProtocol = null;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public CapiStruct_BProtocol BProtocol
        {
            get { return m_BProtocol; }
            set { m_BProtocol = value; }
        }

        public SELECT_B_PROTOCOL_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.SELECT_B_PROTOCOL_REQ); 

            m_BProtocol = new CapiStruct_BProtocol(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes(m_PLCI.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                m_BProtocol.AsByteArray(lbData, liOffset); liOffset += m_BProtocol.StructSize;
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                return Header.Size + 4 + m_BProtocol.StructSize;
            }
        }
    }

    public class MANUFACTURER_REQ : CapiOutMessage
    {
        protected ControllerClass m_Controller = new ControllerClass();
        protected uint m_ManuID = 0;
        protected byte[] m_SpecificData = null;

        public ControllerClass Controller
        {
            get { return m_Controller; }
            set { m_Controller = value; }
        }
        public uint ManuID
        {
            get { return m_ManuID; }
            set { m_ManuID = value; }
        }
        public byte[] SpecificData
        {
            get { return m_SpecificData; }
            set { m_SpecificData = value; }
        }

        public MANUFACTURER_REQ() : base() { 
            Header.SetCommand(CapiMessages_Enum.MANUFACTURER_REQ); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;

                BitConverter.GetBytes(m_Controller.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes(m_ManuID).CopyTo(lbData, liOffset); liOffset += 4;

                if (m_SpecificData != null)
                {
                    Array.Copy(m_SpecificData, 0, lbData, liOffset, m_SpecificData.Length);
                }

                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                if (m_SpecificData == null)
                {
                    return Header.Size + 8;
                }
                else
                {
                    return Header.Size + 8 + m_SpecificData.Length;
                }
            }
        }
    }

    public class MANUFACTURER_RESP : CapiOutMessage
    {
        protected ControllerClass m_Controller = new ControllerClass();
        protected uint m_ManuID = 0;
        protected byte[] m_SpecificData = null;

        public ControllerClass Controller
        {
            get { return m_Controller; }
            set { m_Controller = value; }
        }
        public uint ManuID
        {
            get { return m_ManuID; }
            set { m_ManuID = value; }
        }

        public MANUFACTURER_RESP() : base() { 
            Header.SetCommand(CapiMessages_Enum.MANUFACTURER_RESP); 
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;

                BitConverter.GetBytes(m_Controller.GetValue()).CopyTo(lbData, liOffset); liOffset += 4;
                BitConverter.GetBytes(m_ManuID).CopyTo(lbData, liOffset); liOffset += 4;

                if (m_SpecificData != null)
                {
                    Array.Copy(m_SpecificData, 0, lbData, liOffset, m_SpecificData.Length);
                }
                
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                if (m_SpecificData == null)
                {
                    return Header.Size + 8;
                }
                else
                {
                    return Header.Size + 8 + m_SpecificData.Length;
                }
            }
        }
    }

    public class INTEROPERABILITY_REQ : CapiOutMessage
    {
        protected InteroperabilitySelector_Enum m_InteroperabilitySelector = 0;
        protected CapiStruct_BluetoothInteroperabilityRequestParameter m_BluetoothInteroperabilityRequestParameter = null;
        protected CapiStruct_USBInteroperabilityRequestParameter m_USBInteroperabilityRequestParameter = null;

        public InteroperabilitySelector_Enum InteroperabilitySelector
        {
            get { return m_InteroperabilitySelector; }
            set { m_InteroperabilitySelector = value; }
        }
        public CapiStruct_USBInteroperabilityRequestParameter USBInteroperabilityRequestParameter
        {
            get { return m_USBInteroperabilityRequestParameter; }
            set { m_USBInteroperabilityRequestParameter = value; }
        }
        public CapiStruct_BluetoothInteroperabilityRequestParameter BluetoothInteroperabilityRequestParameter
        {
            get { return m_BluetoothInteroperabilityRequestParameter; }
            set { m_BluetoothInteroperabilityRequestParameter = value; }
        }

        public INTEROPERABILITY_REQ()
            : base()
        {
            Header.SetCommand(CapiMessages_Enum.INTEROPERABILITY_REQ);

            m_USBInteroperabilityRequestParameter = new CapiStruct_USBInteroperabilityRequestParameter(this);
            m_BluetoothInteroperabilityRequestParameter = new CapiStruct_BluetoothInteroperabilityRequestParameter(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes((ushort)m_InteroperabilitySelector).CopyTo(lbData, liOffset); liOffset += 2;
                if (m_InteroperabilitySelector == InteroperabilitySelector_Enum.USBDeviceMenagement)
                {
                    m_USBInteroperabilityRequestParameter.AsByteArray(lbData, liOffset); liOffset += m_USBInteroperabilityRequestParameter.StructSize;
                }
                else
                {
                    m_BluetoothInteroperabilityRequestParameter.AsByteArray(lbData, liOffset); liOffset += m_BluetoothInteroperabilityRequestParameter.StructSize;
                }
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                if (m_InteroperabilitySelector == InteroperabilitySelector_Enum.USBDeviceMenagement)
                {
                    return Header.Size + 2 + m_USBInteroperabilityRequestParameter.StructSize;
                }
                else
                {
                    return Header.Size + 2 + m_BluetoothInteroperabilityRequestParameter.StructSize;
                }
            }
        }
    }

    public class INTEROPERABILITY_RESP : CapiOutMessage
    {
        protected InteroperabilitySelector_Enum m_InteroperabilitySelector = 0;
        protected CapiStruct_Data m_USBInteroperabilityResponseParameter = null;
        protected CapiStruct_Data m_BluetoothInteroperabilityResponseParameter = null;

        public InteroperabilitySelector_Enum InteroperabilitySelector
        {
            get { return m_InteroperabilitySelector; }
            set { m_InteroperabilitySelector = value; }
        }
        public CapiStruct_Data USBInteroperabilityResponseParameter
        {
            get { return m_USBInteroperabilityResponseParameter; }
            set { m_USBInteroperabilityResponseParameter = value; }
        }
        public CapiStruct_Data BluetoothInteroperabilityResponseParameter
        {
            get { return m_BluetoothInteroperabilityResponseParameter; }
            set { m_BluetoothInteroperabilityResponseParameter = value; }
        }

        public INTEROPERABILITY_RESP()
            : base()
        {
            Header.SetCommand(CapiMessages_Enum.INTEROPERABILITY_RESP);

            m_USBInteroperabilityResponseParameter = new CapiStruct_Data(this);
            m_BluetoothInteroperabilityResponseParameter = new CapiStruct_Data(this);
        }

        public override byte[] AsByteArray(byte[] lbData, int liOffset)
        {
            if (lbData.Length - liOffset < Size)
            {
                throw new Exception("Byte-array smaller than needed.");
            }
            else
            {
                Header.AsByteArray(lbData, liOffset); liOffset += Header.Size;
                BitConverter.GetBytes((ushort)m_InteroperabilitySelector).CopyTo(lbData, liOffset); liOffset += 2;
                if (m_InteroperabilitySelector == InteroperabilitySelector_Enum.USBDeviceMenagement)
                {
                    m_USBInteroperabilityResponseParameter.AsByteArray(lbData, liOffset); liOffset += m_USBInteroperabilityResponseParameter.StructSize;
                }
                else
                {
                    m_BluetoothInteroperabilityResponseParameter.AsByteArray(lbData, liOffset); liOffset += m_BluetoothInteroperabilityResponseParameter.StructSize;
                }
                return lbData;
            }
        }

        public override int Size
        {
            get
            {
                if (m_InteroperabilitySelector == InteroperabilitySelector_Enum.USBDeviceMenagement)
                {
                    return Header.Size + 2 + m_USBInteroperabilityResponseParameter.StructSize;
                }
                else
                {
                    return Header.Size + 2 + m_BluetoothInteroperabilityResponseParameter.StructSize;
                }
            }
        }
    }
}
