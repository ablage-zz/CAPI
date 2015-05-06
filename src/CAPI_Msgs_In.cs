#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
#endregion

namespace DSELib.CAPI
{
    public class CONNECT_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Connect_initiated = 0,
            Illegal_Controller = 0x2002,
            No_PLCI_available = 0x2003,
            Illegal_Message_Parameter_Coding = 0x2007,
            B1_protocol_not_supported = 0x3001,
            B2_protocol_not_supported = 0x3002,
            B3_protocol_not_supported = 0x3003,
            B1_protocol_parameter_not_supported = 0x3004,
            B2_protocol_parameter_not_supported = 0x3005,
            B3_protocol_parameter_not_supported = 0x3006,
            B_protocol_combination_not_supported = 0x3007,
            CIP_Value_unknown = 0x3009
        }
        protected PLCIClass m_PLCI = new PLCIClass();
        protected Info_Enum m_Info = Info_Enum.Connect_initiated;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public CONNECT_CONF(byte[] lbData, int liOffset)
        {
            m_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
        }
    }

    public class CONNECT_IND : CapiInMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();
        protected ushort m_CIPValue = 0;
        protected CapiStruct_CalledNumber m_CalledPartyNumber = null;
        protected CapiStruct_CallingNumber m_CallingPartyNumber = null;
        protected CapiStruct_SubAddress m_CalledPartySubaddress = null;
        protected CapiStruct_SubAddress m_CallingPartySubaddress = null;
        protected CapiStruct_Data m_BearerCapability = null;
        protected CapiStruct_Data m_LowLayerCopatibility = null;
        protected CapiStruct_Data m_HighLayerCopatibility = null;
        protected CapiStruct_AdditionalInfo m_AdditionalInformationElements = null;
        protected CapiStruct_CallingNumber m_SecondCallingPartyNumber = null;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
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
        public CapiStruct_Data BearerCapability
        {
            get { return m_BearerCapability; }
            set { m_BearerCapability = value; }
        }
        public CapiStruct_Data LowLayerCopatibility
        {
            get { return m_LowLayerCopatibility; }
            set { m_LowLayerCopatibility = value; }
        }
        public CapiStruct_Data HighLayerCopatibility
        {
            get { return m_HighLayerCopatibility; }
            set { m_HighLayerCopatibility = value; }
        }
        public CapiStruct_AdditionalInfo AdditionalInformationElements
        {
            get { return m_AdditionalInformationElements; }
            set { m_AdditionalInformationElements = value; }
        }
        public CapiStruct_CallingNumber SecondCallingPartyNumber
        {
            get { return m_SecondCallingPartyNumber; }
            set { m_SecondCallingPartyNumber = value; }
        }

        public CONNECT_IND(byte[] lbData, int liOffset)
        {
            m_CalledPartyNumber = new CapiStruct_CalledNumber(this);
            m_CallingPartyNumber = new CapiStruct_CallingNumber(this);
            m_CalledPartySubaddress = new CapiStruct_SubAddress(this);
            m_CallingPartySubaddress = new CapiStruct_SubAddress(this);
            m_BearerCapability = new CapiStruct_Data(this);
            m_LowLayerCopatibility = new CapiStruct_Data(this);
            m_HighLayerCopatibility = new CapiStruct_Data(this);
            m_AdditionalInformationElements = new CapiStruct_AdditionalInfo(this);
            m_SecondCallingPartyNumber = new CapiStruct_CallingNumber(this);

            m_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_CIPValue = BitConverter.ToUInt16(lbData, liOffset + 4);
            liOffset = m_CalledPartyNumber.SetData(lbData, liOffset + 6);
            liOffset = m_CallingPartyNumber.SetData(lbData, liOffset);
            liOffset = m_CalledPartySubaddress.SetData(lbData, liOffset);
            liOffset = m_CallingPartySubaddress.SetData(lbData, liOffset);
            liOffset = m_BearerCapability.SetData(lbData, liOffset);
            liOffset = m_LowLayerCopatibility.SetData(lbData, liOffset);
            liOffset = m_HighLayerCopatibility.SetData(lbData, liOffset);
            liOffset = m_AdditionalInformationElements.SetData(lbData, liOffset);
            liOffset = m_SecondCallingPartyNumber.SetData(lbData, liOffset);
        }
    }

    public class CONNECT_ACTIVE_IND : CapiInMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();
        protected CapiStruct_ConnectedNumber m_ConnectedNumber = null;
        protected CapiStruct_SubAddress m_ConnectedSubaddress = null;
        protected CapiStruct_Data m_LowLayerCompatibility = null;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
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

        public CONNECT_ACTIVE_IND(byte[] lbData, int liOffset)
        {
            m_ConnectedNumber = new CapiStruct_ConnectedNumber(this);
            m_ConnectedSubaddress = new CapiStruct_SubAddress(this);
            m_LowLayerCompatibility = new CapiStruct_Data(this);

            m_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            liOffset = m_ConnectedNumber.SetData(lbData, liOffset + 4);
            liOffset = m_ConnectedSubaddress.SetData(lbData, liOffset);
            liOffset = m_LowLayerCompatibility.SetData(lbData, liOffset);
        }
    }

    public class DISCONNECT_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Disconnect_initiated = 0,
            Message_not_supported_in_current_state = 0x2001,
            Illegal_PLCI = 0x2002,
            Illegal_Message_Parameter_Coding = 0x2007
        }
        protected PLCIClass m_PLCI = new PLCIClass();
        protected Info_Enum m_Info = Info_Enum.Disconnect_initiated;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public DISCONNECT_CONF(byte[] lbData, int liOffset)
        {
            m_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
        }
    }

    public class DISCONNECT_IND : CapiInMessage
    {
        protected PLCIClass m_PLCI = new PLCIClass();
        protected Reason_Enum m_Reason = Reason_Enum.No_Cause_available;
        protected byte m_ReasonNetwork = 0;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public Reason_Enum Reason
        {
            get { return m_Reason; }
            set { m_Reason = value; }
        }
        public byte ReasonNetwork
        {
            get { return m_ReasonNetwork; }
            set { m_ReasonNetwork = value; }
        }

        public DISCONNECT_IND(byte[] lbData, int liOffset)
        {
            m_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            if (BitConverter.ToUInt16(lbData, liOffset + 4) >= 0x3400)
            {
                m_Reason = Reason_Enum.Disconnect_cause_from_network;
                m_ReasonNetwork = lbData[liOffset + 6];
            }
            else
            {
                m_Reason = (Reason_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
                m_ReasonNetwork = 0;
            }
        }
    }

    public class ALERT_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Alert_initiated = 0,
            Alert_already_sent_by_another_application = 0x0003,
            Message_not_supported_in_current_state = 0x2001,
            Illegal_PLCI = 0x2002,
            Illegal_message_parameter_coding = 0x2007
        }
        protected PLCIClass m_PLCI = new PLCIClass();
        protected Info_Enum m_Info = Info_Enum.Alert_initiated;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public ALERT_CONF(byte[] lbData, int liOffset)
        {
            m_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
        }
    }

    public class INFO_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Connect_initiated = 0,
            Message_not_supported_in_current_state = 0x2001,
            Illegal_Controller_PLCI = 0x2002,
            No_PLCI_available = 0x2003,
            Illegal_Message_Parameter_Coding = 0x2007
        }
        protected PLCIClass m_PLCI = new PLCIClass();
        protected Info_Enum m_Info = Info_Enum.Connect_initiated;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public INFO_CONF(byte[] lbData, int liOffset)
        {
            m_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
        }
    }

    public class INFO_IND : CapiInMessage, IInfoSelector
    {
        protected PLCIClass m_Controller_PLCI = new PLCIClass();
        protected byte m_InfoNumber = 0;
        protected CapiStruct_Data m_EmptyStruct = null;
        protected CapiStruct_InfoElement m_InfoElement = null;

        protected InfoDataType m_DataType = InfoDataType.MessageType; // Bit 15 of InfoNumber
        protected InfoChargeType m_ChargeType = InfoChargeType.ChargeUnits; // Bit 14 of InfoNumber

        public InfoDataType DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }
        public InfoChargeType ChargeType
        {
            get { return m_ChargeType; }
            set { m_ChargeType = value; }
        }

        public PLCIClass ControllerPLCI
        {
            get { return m_Controller_PLCI; }
            set { m_Controller_PLCI = value; }
        }
        public byte InfoNumber
        {
            get { return m_InfoNumber; }
            set { m_InfoNumber = value; }
        }
        public CapiStruct_InfoElement InfoElement
        {
            get { return m_InfoElement; }
            set { m_InfoElement = value; }
        }

        public INFO_IND(byte[] lbData, int liOffset)
        {
            m_EmptyStruct = new CapiStruct_Data(this);
            m_InfoElement = new CapiStruct_InfoElement(this);

            m_Controller_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));

            if ((BitConverter.ToUInt16(lbData, liOffset + 4) & 0x4000) == 0x4000)
            {
                m_DataType = InfoDataType.MessageType;
            }
            else
            {
                m_DataType = InfoDataType.ElementType;
            }

            if ((BitConverter.ToUInt16(lbData, liOffset + 4) & 0x2000) == 0x2000)
            {
                m_ChargeType = InfoChargeType.NationalCurrency;
            }
            else
            {
                m_ChargeType = InfoChargeType.ChargeUnits;
            }

            m_InfoNumber = (byte)(BitConverter.ToUInt16(lbData, liOffset + 4) & 0xFF);

            if (m_DataType == InfoDataType.MessageType)
            {
                liOffset = m_EmptyStruct.SetData(lbData, liOffset + 6);
            }
            else
            {
                liOffset = m_InfoElement.SetData(lbData, liOffset + 6);
            }
        }
    }

    public class CONNECT_B3_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Connect_initiated = 0,
            NCPI_not_supported_by_current_protocol_NCPI_ignored = 0x0001,
            Message_not_supported_in_current_state = 0x2001,
            Illegal_PLCI = 0x2002,
            No_NCCI_available = 0x2004,
            NCPI_not_supported = 0x3008
        }
        protected NCCIClass m_NCCI = new NCCIClass();
        protected Info_Enum m_Info = Info_Enum.Connect_initiated;

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public CONNECT_B3_CONF(byte[] lbData, int liOffset)
        {
            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
        }
    }

    public class CONNECT_B3_IND : CapiInMessage
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

        public CONNECT_B3_IND(byte[] lbData, int liOffset)
        {
            m_NetworkControlProtocolInformation = new CapiStruct_Data(this);

            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            liOffset = m_NetworkControlProtocolInformation.SetData(lbData, liOffset + 4);
        }
    }

    public class CONNECT_B3_ACTIVE_IND : CapiInMessage
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

        public CONNECT_B3_ACTIVE_IND(byte[] lbData, int liOffset)
        {
            m_NetworkControlProtocolInformation = new CapiStruct_Data(this);

            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            liOffset = m_NetworkControlProtocolInformation.SetData(lbData, liOffset + 4);
        }
    }

    public class CONNECT_B3_T90_ACTIVE_IND : CapiInMessage
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

        public CONNECT_B3_T90_ACTIVE_IND(byte[] lbData, int liOffset)
        {
            m_NetworkControlProtocolInformation = new CapiStruct_Data(this);

            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            liOffset = m_NetworkControlProtocolInformation.SetData(lbData, liOffset + 4);
        }
    }

    public class DISCONNECT_B3_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Disconnect_initiated = 0,
            NCPI_not_supported_by_current_protocol_NCPI_ignored = 0x0001,
            Message_not_supported_in_current_state = 0x2001,
            Illegal_PLCI = 0x2002,
            Illegal_message_parameter_coding = 0x2007,
            NCPI_not_supported = 0x3008
        }
        protected NCCIClass m_NCCI = new NCCIClass();
        protected Info_Enum m_Info = Info_Enum.Disconnect_initiated;

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public DISCONNECT_B3_CONF(byte[] lbData, int liOffset)
        {
            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
        }
    }

    public class DISCONNECT_B3_IND : CapiInMessage
    {
        protected NCCIClass m_NCCI = new NCCIClass();
        protected ReasonB3_Enum m_ReasonB3 = ReasonB3_Enum.Clearance_in_accordance_with_protocol;
        protected CapiStruct_Data m_NetworkControlProtocolInformation = null;

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }
        public ReasonB3_Enum ReasonB3
        {
            get { return m_ReasonB3; }
            set { m_ReasonB3 = value; }
        }
        public CapiStruct_Data NetworkControlProtocolInformation
        {
            get { return m_NetworkControlProtocolInformation; }
            set { m_NetworkControlProtocolInformation = value; }
        }

        public DISCONNECT_B3_IND(byte[] lbData, int liOffset)
        {
            m_NetworkControlProtocolInformation = new CapiStruct_Data(this);

            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_ReasonB3 = (ReasonB3_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
            liOffset = m_NetworkControlProtocolInformation.SetData(lbData, liOffset + 6);
        }
    }

    public class DATA_B3_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Data_transmission_initiated = 0,
            Flags_not_supported_by_current_protocol_flags_ignored = 0x0002,
            Message_not_supported_in_current_state = 0x2001,
            Illegal_NCCI = 0x2002,
            Illegal_message_parameter_coding = 0x2007,
            Flags_not_supported = 0x300A,
            Data_length_not_supported_by_current_protocol = 0x300C
        }
        protected NCCIClass m_NCCI = new NCCIClass();
        protected ushort m_DataHandle = 0;
        protected Info_Enum m_Info = Info_Enum.Data_transmission_initiated;

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
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public DATA_B3_CONF(byte[] lbData, int liOffset)
        {
            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_DataHandle = BitConverter.ToUInt16(lbData, liOffset + 4);
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 6);
        }
    }

    public class DATA_B3_IND : CapiInMessage
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

        public DATA_B3_IND(byte[] lbData, int liOffset)
        {
            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Data = new IntPtr(BitConverter.ToUInt32(lbData, liOffset + 4));
            m_DataLength = BitConverter.ToUInt16(lbData, liOffset + 8);
            if (m_DataLength == 0)
            {
                m_DataBuffer = null;
            }
            else
            {
                m_DataBuffer = new byte[m_DataLength];
                Marshal.Copy(m_Data, m_DataBuffer, 0, m_DataLength);
            }
            m_DataHandle = BitConverter.ToUInt16(lbData, liOffset + 10);
            m_Flags = (DataB3_Flags)BitConverter.ToUInt16(lbData, liOffset + 12);
            m_Data64 = BitConverter.ToUInt64(lbData, liOffset + 14);
        }
    }

    public class RESET_B3_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Reset_initiated = 0,
            NCPI_not_supported_by_current_protocol_NCPI_ignored = 0x0001,
            Message_not_supported_in_current_state = 0x2001,
            Illegal_NCCI = 0x2002,
            Illegal_message_parameter_coding = 0x2007,
            NCPI_not_supported = 0x3008,
            Reset_procedure_not_supported_by_current_protocol = 0x300D
        }
        protected NCCIClass m_NCCI = new NCCIClass();
        protected Info_Enum m_Info = Info_Enum.Reset_initiated;

        public NCCIClass NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public RESET_B3_CONF(byte[] lbData, int liOffset)
        {
            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
        }
    }

    public class RESET_B3_IND : CapiInMessage
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

        public RESET_B3_IND(byte[] lbData, int liOffset)
        {
            m_NetworkControlProtocolInformation = new CapiStruct_Data(this);

            m_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            liOffset = m_NetworkControlProtocolInformation.SetData(lbData, liOffset + 4);
        }
    }

    public class LISTEN_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Listen_is_active = 0,
            Illegal_Controller = 0x2002,
            No_Listen_resources_available = 0x2005,
            Illegal_message_parameter_coding = 0x2007
        }
        protected ControllerClass m_Controller = new ControllerClass();
        protected Info_Enum m_Info = 0;

        public ControllerClass Controller
        {
            get { return m_Controller; }
            set { m_Controller = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public LISTEN_CONF(byte[] lbData, int liOffset)
        {
            m_Controller.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
        }
    }

    public class FACILITY_CONF : CapiInMessage, IFacilitySelector
    {
        public enum Info_Enum : ushort
        {
            Request_accepted = 0,
            Message_not_supported_in_current_state = 0x2001,
            IncorrectController_PLCI_NCCI = 0x2002,
            Illegal_message_parameter_coding = 0x2007,
            No_interconnection_resources_available = 0x2008,
            Facility_not_supported = 0x300B,
            Facility_specific_function_not_supported = 0x3011
        }

        protected NCCIClass m_Controller_PLCI_NCCI = new NCCIClass();
        protected Info_Enum m_Info = 0;
        protected FacilitySelector_Enum m_FacilitySelector = 0;
        protected CapiStruct_FacilityConfirmationParameter m_FacilityConfirmationParameter = null;

        public NCCIClass Controller_PLCI_NCCI
        {
            get { return m_Controller_PLCI_NCCI; }
            set { m_Controller_PLCI_NCCI = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }
        public FacilitySelector_Enum FacilitySelector
        {
            get { return m_FacilitySelector; }
            set { m_FacilitySelector = value; }
        }
        public CapiStruct_FacilityConfirmationParameter FacilityConfirmationParameter
        {
            get { return m_FacilityConfirmationParameter; }
            set { m_FacilityConfirmationParameter = value; }
        }

        public FACILITY_CONF(byte[] lbData, int liOffset)
        {
            m_FacilityConfirmationParameter = new CapiStruct_FacilityConfirmationParameter(this);

            m_Controller_PLCI_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
            m_FacilitySelector = (FacilitySelector_Enum)BitConverter.ToUInt16(lbData, liOffset + 6);
            liOffset = m_FacilityConfirmationParameter.SetData(lbData, liOffset + 8);
        }
    }

    public class FACILITY_IND : CapiInMessage, IFacilitySelector
    {
        protected NCCIClass m_Controller_PLCI_NCCI = new NCCIClass();
        protected FacilitySelector_Enum m_FacilitySelector = 0;
        protected CapiStruct_FacilityIndicationParameter m_FacilityIndicationParameter = null;

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
        public CapiStruct_FacilityIndicationParameter FacilityIndicationParameter
        {
            get { return m_FacilityIndicationParameter; }
            set { m_FacilityIndicationParameter = value; }
        }

        public FACILITY_IND(byte[] lbData, int liOffset)
        {
            m_FacilityIndicationParameter = new CapiStruct_FacilityIndicationParameter(this);

            m_Controller_PLCI_NCCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_FacilitySelector = (FacilitySelector_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
            liOffset = m_FacilityIndicationParameter.SetData(lbData, liOffset + 6);
        }
    }

    public class SELECT_B_PROTOCOL_CONF : CapiInMessage
    {
        public enum Info_Enum : ushort
        {
            Protocol_change_successful = 0,
            Message_not_supported_in_current_state = 0x2001,
            Illegal_PLCI = 0x2002,
            Illegal_Message_Parameter_Coding = 0x2007,
            B1_protocol_not_supported = 0x3001,
            B2_protocol_not_supported = 0x3002,
            B3_protocol_not_supported = 0x3003,
            B1_protocol_parameter_not_supported = 0x3004,
            B2_protocol_parameter_not_supported = 0x3005,
            B3_protocol_parameter_not_supported = 0x3006,
            B_protocol_combination_not_supported = 0x3007
        }
        protected PLCIClass m_PLCI = new PLCIClass();
        protected Info_Enum m_Info = Info_Enum.Protocol_change_successful;

        public PLCIClass PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }
        public Info_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        public SELECT_B_PROTOCOL_CONF(byte[] lbData, int liOffset)
        {
            m_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_Info = (Info_Enum)BitConverter.ToUInt16(lbData, liOffset + 4);
        }
    }

    public class MANUFACTURER_CONF : CapiInMessage
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

        public MANUFACTURER_CONF(byte[] lbData, int liOffset)
        {
            m_Controller.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_ManuID = BitConverter.ToUInt32(lbData, liOffset + 4);
            m_SpecificData = new byte[m_Header.Length - 16];
            Array.Copy(lbData, liOffset, m_SpecificData, 0, m_SpecificData.Length);
        }
    }

    public class MANUFACTURER_IND : CapiInMessage
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

        public MANUFACTURER_IND(byte[] lbData, int liOffset)
        {
            m_Controller.SetValue(BitConverter.ToUInt32(lbData, liOffset));
            m_ManuID = BitConverter.ToUInt32(lbData, liOffset + 4);
            m_SpecificData = new byte[m_Header.Length - 16];
            Array.Copy(lbData, liOffset, m_SpecificData, 0, m_SpecificData.Length);
        }
    }

    public class INTEROPERABILITY_CONF : CapiInMessage
    {
        protected GeneralInfo_Enum m_Info = GeneralInfo_Enum.RequestAccepted;
        protected InteroperabilitySelector_Enum m_InteroperabilitySelector = InteroperabilitySelector_Enum.USBDeviceMenagement;
        protected CapiStruct_USBInteroperabilityConfirmationParameter m_USBInteroperabilityConfirmationParameter = null;
        protected CapiStruct_BluetoothInteroperabilityConfirmationParameter m_BluetoothInteroperabilityConfirmationParameter = null;

        public GeneralInfo_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }
        public InteroperabilitySelector_Enum InteroperabilitySelector
        {
            get { return m_InteroperabilitySelector; }
            set { m_InteroperabilitySelector = value; }
        }
        public CapiStruct_USBInteroperabilityConfirmationParameter USBInteroperabilityConfirmationParameter
        {
            get { return m_USBInteroperabilityConfirmationParameter; }
            set { m_USBInteroperabilityConfirmationParameter = value; }
        }
        public CapiStruct_BluetoothInteroperabilityConfirmationParameter BluetoothInteroperabilityConfirmationParameter
        {
            get { return m_BluetoothInteroperabilityConfirmationParameter; }
            set { m_BluetoothInteroperabilityConfirmationParameter = value; }
        }

        public INTEROPERABILITY_CONF(byte[] lbData, int liOffset)
        {
            m_USBInteroperabilityConfirmationParameter = new CapiStruct_USBInteroperabilityConfirmationParameter(this);
            m_BluetoothInteroperabilityConfirmationParameter = new CapiStruct_BluetoothInteroperabilityConfirmationParameter(this);

            m_Info = (GeneralInfo_Enum)BitConverter.ToUInt16(lbData, liOffset);
            m_InteroperabilitySelector = (InteroperabilitySelector_Enum)BitConverter.ToUInt16(lbData, liOffset + 2);
            if (m_InteroperabilitySelector == InteroperabilitySelector_Enum.USBDeviceMenagement)
            {
                liOffset = m_USBInteroperabilityConfirmationParameter.SetData(lbData, liOffset + 4);
            }
            else
            {
                liOffset = m_BluetoothInteroperabilityConfirmationParameter.SetData(lbData, liOffset + 4);
            }
        }
    }

    public class INTEROPERABILITY_IND : CapiInMessage
    {
        protected InteroperabilitySelector_Enum m_InteroperabilitySelector = 0;
        protected CapiStruct_USBInteroperabilityIndicationParameter m_USBInteroperabilityIndicationParameter = null;
        protected CapiStruct_Data m_BluetoothInteroperabilityIndicationParameter = null;

        public InteroperabilitySelector_Enum InteroperabilitySelector
        {
            get { return m_InteroperabilitySelector; }
            set { m_InteroperabilitySelector = value; }
        }
        public CapiStruct_USBInteroperabilityIndicationParameter USBInteroperabilityIndicationParameter
        {
            get { return m_USBInteroperabilityIndicationParameter; }
            set { m_USBInteroperabilityIndicationParameter = value; }
        }
        public CapiStruct_Data BluetoothInteroperabilityIndicationParameter
        {
            get { return m_BluetoothInteroperabilityIndicationParameter; }
            set { m_BluetoothInteroperabilityIndicationParameter = value; }
        }

        public INTEROPERABILITY_IND(byte[] lbData, int liOffset)
        {
            m_USBInteroperabilityIndicationParameter = new CapiStruct_USBInteroperabilityIndicationParameter(this);
            m_BluetoothInteroperabilityIndicationParameter = new CapiStruct_Data(this);

            m_InteroperabilitySelector = (InteroperabilitySelector_Enum)BitConverter.ToUInt16(lbData, liOffset);
            if (m_InteroperabilitySelector == InteroperabilitySelector_Enum.USBDeviceMenagement)
            {
                liOffset = m_USBInteroperabilityIndicationParameter.SetData(lbData, liOffset + 2);
            }
            else
            {
                liOffset = m_BluetoothInteroperabilityIndicationParameter.SetData(lbData, liOffset + 2);
            }
        }
    }
}
