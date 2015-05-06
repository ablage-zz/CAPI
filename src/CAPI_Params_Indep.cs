#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace DSELib.CAPI
{
    public class CapiStruct_AdditionalInfo : CapiStruct_Base
    {
        #region Vars
        protected CapiStruct_BChannelInformation m_BChannelInformation = null;
        protected CapiStruct_Data m_KeypadFacility = null;
        protected CapiStruct_Data m_UserUserData = null;
        protected CapiStruct_Data m_FacilityDataArray = null;
        protected CapiStruct_SendingComplete m_SendingComplete = null;
        #endregion

        #region Properties
        public CapiStruct_BChannelInformation BChannelInformation
        {
            get { return m_BChannelInformation; }
            set { m_BChannelInformation = value; }
        }
        public CapiStruct_Data KeypadFacility
        {
            get { return m_KeypadFacility; }
            set { m_KeypadFacility = value; }
        }
        public CapiStruct_Data UserUserData
        {
            get { return m_UserUserData; }
            set { m_UserUserData = value; }
        }
        public CapiStruct_Data FacilityDataArray
        {
            get { return m_FacilityDataArray; }
            set { m_FacilityDataArray = value; }
        }
        public CapiStruct_SendingComplete SendingComplete
        {
            get { return m_SendingComplete; }
            set { m_SendingComplete = value; }
        }

        public override int DataSize
        {
            get
            {
                return m_BChannelInformation.StructSize + m_KeypadFacility.StructSize + m_UserUserData.StructSize + m_FacilityDataArray.StructSize + m_SendingComplete.StructSize;
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_AdditionalInfo(object loParent)
            : base(loParent)
        {
            m_BChannelInformation = new CapiStruct_BChannelInformation(this);
            m_KeypadFacility = new CapiStruct_Data(this);
            m_UserUserData = new CapiStruct_Data(this);
            m_FacilityDataArray = new CapiStruct_Data(this);
            m_SendingComplete = new CapiStruct_SendingComplete(this);
        }
        public CapiStruct_AdditionalInfo(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_BChannelInformation = new CapiStruct_BChannelInformation(this);
            m_KeypadFacility = new CapiStruct_Data(this);
            m_UserUserData = new CapiStruct_Data(this);
            m_FacilityDataArray = new CapiStruct_Data(this);
            m_SendingComplete = new CapiStruct_SendingComplete(this);
        }
        public CapiStruct_AdditionalInfo(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_BChannelInformation = new CapiStruct_BChannelInformation(this);
            m_KeypadFacility = new CapiStruct_Data(this);
            m_UserUserData = new CapiStruct_Data(this);
            m_FacilityDataArray = new CapiStruct_Data(this);
            m_SendingComplete = new CapiStruct_SendingComplete(this);
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
                lbData = m_BChannelInformation.AsByteArray(lbData, liOffset); liOffset += m_BChannelInformation.StructSize;
                lbData = m_KeypadFacility.AsByteArray(lbData, liOffset); liOffset += m_KeypadFacility.StructSize;
                lbData = m_UserUserData.AsByteArray(lbData, liOffset); liOffset += m_UserUserData.StructSize;
                lbData = m_FacilityDataArray.AsByteArray(lbData, liOffset); liOffset += m_FacilityDataArray.StructSize;
                lbData = m_SendingComplete.AsByteArray(lbData, liOffset); liOffset += m_SendingComplete.StructSize;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                liOffset = m_BChannelInformation.SetData(lbData, liOffset);
                liOffset = m_KeypadFacility.SetData(lbData, liOffset);
                liOffset = m_UserUserData.SetData(lbData, liOffset);
                liOffset = m_FacilityDataArray.SetData(lbData, liOffset);
                liOffset = m_SendingComplete.SetData(lbData, liOffset);
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_BChannelInformation : CapiStruct_Base
    {
        #region Vars
        protected Channel_Enum m_Channel = Channel_Enum.ChannelB;
        protected Channel3_Operation_Enum m_Channel3_Operation = Channel3_Operation_Enum.DTEMode;
        protected byte[] m_Channel3_ChannelMaskArray = new byte[31];
        protected CapiStruct_Data m_Channel4_ChannelIdentification = null;
        #endregion

        #region Properties
        public Channel_Enum Channel
        {
            get { return m_Channel; }
            set { m_Channel = value; }
        }
        public Channel3_Operation_Enum Channel3_Operation
        {
            get { return m_Channel3_Operation; }
            set { m_Channel3_Operation = value; }
        }
        public byte[] Channel3_MaskArray
        {
            get { return m_Channel3_ChannelMaskArray; }
            set { m_Channel3_ChannelMaskArray = value; }
        }
        public CapiStruct_Data Channel4_Identification
        {
            get { return m_Channel4_ChannelIdentification; }
            set { m_Channel4_ChannelIdentification = value; }
        }

        public override int DataSize
        {
            get
            {
                if ((m_Channel == Channel_Enum.ChannelB) || (m_Channel == Channel_Enum.ChannelD) || (m_Channel == Channel_Enum.NeitherChannelBNorChannelD))
                {
                    return 2;
                }
                else if (m_Channel == Channel_Enum.ChannelAllocation)
                {
                    return 2 + 2 + 31;
                }
                else
                {
                    return 2 + m_Channel4_ChannelIdentification.StructSize;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_BChannelInformation(object loParent) 
            : base(loParent)
        {
            m_Channel4_ChannelIdentification = new CapiStruct_Data(this);
        }
        public CapiStruct_BChannelInformation(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_Channel4_ChannelIdentification = new CapiStruct_Data(this);
        }
        public CapiStruct_BChannelInformation(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_Channel4_ChannelIdentification = new CapiStruct_Data(this);
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
                Array.Copy(BitConverter.GetBytes((ushort)m_Channel), 0, lbData, liOffset, 2); liOffset += 2;
                if (m_Channel == Channel_Enum.ChannelAllocation)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_Channel3_Operation), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(m_Channel3_ChannelMaskArray, 0, lbData, liOffset, 31); liOffset += 31;
                }
                else if (m_Channel == Channel_Enum.ChannelIdentificationInformationElement)
                {
                    lbData = m_Channel4_ChannelIdentification.AsByteArray(lbData, liOffset); liOffset += m_Channel4_ChannelIdentification.StructSize;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_Channel = (Channel_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;

                if (m_Channel == Channel_Enum.ChannelAllocation)
                {
                    m_Channel3_Operation = (Channel3_Operation_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    Array.Copy(lbData, liOffset, m_Channel3_ChannelMaskArray, 0, 31); liOffset += 31;
                }
                else if (m_Channel == Channel_Enum.ChannelIdentificationInformationElement)
                {
                    m_Channel4_ChannelIdentification.SetData(lbData, liOffset);
                    liOffset += m_Channel4_ChannelIdentification.StructSize;
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_DTMFCharacteristics : CapiStruct_Base
    {
        #region Vars
        protected ushort m_DTMFSelectivity = 0;
        #endregion

        #region Properties
        public ushort DTMFSelectivity
        {
            get { return m_DTMFSelectivity; }
            set { m_DTMFSelectivity = value; }
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
        public CapiStruct_DTMFCharacteristics(object loParent) 
            : base(loParent)
        {
        }
        public CapiStruct_DTMFCharacteristics(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_DTMFCharacteristics(object loParent, byte[] lbData, int liOffset)
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
                Array.Copy(BitConverter.GetBytes(m_DTMFSelectivity), 0, lbData, liOffset, 2); liOffset += 2;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_DTMFSelectivity = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_FacilityAwakeRequestParameter : CapiStruct_Base
    {
        #region Vars
        protected CapiStruct_CalledNumber m_CalledPartyNumber = null;
        protected CIPMask_Enum m_CIPMask = CIPMask_Enum.Telephony_ISDNWithoutHLCInfo;
        #endregion

        #region Properties
        public CapiStruct_CalledNumber CalledPartyNumber
        {
            get { return m_CalledPartyNumber; }
            set { m_CalledPartyNumber = value; }
        }
        public CIPMask_Enum CIPMask
        {
            get { return m_CIPMask; }
            set { m_CIPMask = value; }
        }

        public override int DataSize
        {
            get
            {
                return m_CalledPartyNumber.StructSize + 4;
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_FacilityAwakeRequestParameter(object loParent) 
            : base(loParent)
        {
            m_CalledPartyNumber = new CapiStruct_CalledNumber(this);
        }
        public CapiStruct_FacilityAwakeRequestParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_CalledPartyNumber = new CapiStruct_CalledNumber(this);
        }
        public CapiStruct_FacilityAwakeRequestParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_CalledPartyNumber = new CapiStruct_CalledNumber(this);
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
                m_CalledPartyNumber.AsByteArray(lbData, liOffset); liOffset += m_CalledPartyNumber.StructSize;
                Array.Copy(BitConverter.GetBytes((uint)m_CIPMask), 0, lbData, liOffset, 4); liOffset += 4;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                liOffset = m_CalledPartyNumber.SetData(lbData, liOffset);
                m_CIPMask = (CIPMask_Enum)BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_FacilityRequestParameter : CapiStruct_Base, IFunctionSelector
    {
        #region Vars
        protected CapiStruct_Data m_EmptyStructure = null;

        protected ushort m_Function = 1;
        protected ushort m_ToneDuration = 40;
        protected ushort m_GapDuration = 40;
        protected CapiStruct_IA5 m_DTMFDigits = null;
        protected CapiStruct_DTMFCharacteristics m_DTMFCharacteristics = null;

        protected ushort m_NumberOfAwakeRequestParameters = 0;
        protected CapiStruct_FacilityAwakeRequestParameter m_FacilityAwakeRequestParameter = null;

        protected CapiStruct_LIRequestParameter m_LIRequestParameter = null;
        #endregion

        #region Properties
        private FacilitySelector_Enum m_FacilitySelector
        {
            get {
                if (m_Parent != null)
                {
                    return ((IFacilitySelector)m_Parent).FacilitySelector;
                }
                else
                {
                    return FacilitySelector_Enum.Handset;
                }
            }
        }

        public ushort Function
        {
            get { return m_Function; }
            set { m_Function = value; }
        }
        public ushort ToneDuration
        {
            get { return m_ToneDuration; }
            set { m_ToneDuration = value; }
        }
        public ushort GapDuration
        {
            get { return m_GapDuration; }
            set { m_GapDuration = value; }
        }
        public CapiStruct_IA5 DTMFDigits
        {
            get { return m_DTMFDigits; }
            set { m_DTMFDigits = value; }
        }
        public CapiStruct_DTMFCharacteristics DTMFCharacteristics
        {
            get { return m_DTMFCharacteristics; }
            set { m_DTMFCharacteristics = value; }
        }
        public ushort NumberOfAwakeRequestParameters
        {
            get { return m_NumberOfAwakeRequestParameters; }
            set { m_NumberOfAwakeRequestParameters = value; }
        }
        public CapiStruct_FacilityAwakeRequestParameter FacilityAwakeRequestParameter
        {
            get { return m_FacilityAwakeRequestParameter; }
            set { m_FacilityAwakeRequestParameter = value; }
        }
        public CapiStruct_LIRequestParameter LIRequestParameter
        {
            get { return m_LIRequestParameter; }
            set { m_LIRequestParameter = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    return m_EmptyStructure.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    return 6 + m_DTMFDigits.StructSize + m_DTMFCharacteristics.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    return 2;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                    return 0;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    return 2 + m_FacilityAwakeRequestParameter.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.LineInterconnect)
                {
                    return 2 + m_LIRequestParameter.StructSize;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_FacilityRequestParameter(object loParent)
            : base(loParent)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_DTMFDigits = new CapiStruct_IA5(this);
            m_DTMFCharacteristics = new CapiStruct_DTMFCharacteristics(this);
            m_FacilityAwakeRequestParameter = new CapiStruct_FacilityAwakeRequestParameter(this);
            m_LIRequestParameter = new CapiStruct_LIRequestParameter(this);
        }
        public CapiStruct_FacilityRequestParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_DTMFDigits = new CapiStruct_IA5(this);
            m_DTMFCharacteristics = new CapiStruct_DTMFCharacteristics(this);
            m_FacilityAwakeRequestParameter = new CapiStruct_FacilityAwakeRequestParameter(this);
            m_LIRequestParameter = new CapiStruct_LIRequestParameter(this);
        }
        public CapiStruct_FacilityRequestParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_DTMFDigits = new CapiStruct_IA5(this);
            m_DTMFCharacteristics = new CapiStruct_DTMFCharacteristics(this);
            m_FacilityAwakeRequestParameter = new CapiStruct_FacilityAwakeRequestParameter(this);
            m_LIRequestParameter = new CapiStruct_LIRequestParameter(this);
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
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    lbData = m_EmptyStructure.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    Array.Copy(BitConverter.GetBytes(m_Function), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_ToneDuration), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_GapDuration), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_DTMFDigits.AsByteArray(lbData, liOffset); liOffset += m_DTMFDigits.StructSize;
                    lbData = m_DTMFCharacteristics.AsByteArray(lbData, liOffset); liOffset += m_DTMFCharacteristics.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    Array.Copy(BitConverter.GetBytes(m_Function), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    Array.Copy(BitConverter.GetBytes(m_NumberOfAwakeRequestParameters), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_FacilityAwakeRequestParameter.AsByteArray(lbData, liOffset); liOffset += m_FacilityAwakeRequestParameter.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.LineInterconnect)
                {
                    Array.Copy(BitConverter.GetBytes(m_Function), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_LIRequestParameter.AsByteArray(lbData, liOffset); liOffset += m_LIRequestParameter.StructSize;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    m_Function = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_ToneDuration = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_GapDuration = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_DTMFDigits.SetData(lbData, liOffset);
                    liOffset = m_DTMFCharacteristics.SetData(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    m_Function = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    m_NumberOfAwakeRequestParameters = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_FacilityAwakeRequestParameter.SetData(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.LineInterconnect)
                {
                    m_Function = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_LIRequestParameter.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_FacilityConfirmationParameter : CapiStruct_Base, IFunctionSelector
    {
        #region Vars
        protected CapiStruct_Data m_EmptyStructure = null;

        protected DTMFInformation_Enum m_DTMFInformation = DTMFInformation_Enum.SendingOfDTMFSuccessfully;
        protected bool m_V42BisInformation = true;
        protected CompressionMode_Enum m_CompressionMode = CompressionMode_Enum.NoCompression;
        protected ushort m_NumberOfCodeWords = 0;
        protected ushort m_MaximumStringLength = 0;
        protected uint m_TxTotal = 0;
        protected uint m_TxCompressed = 0;
        protected uint m_RxTotal = 0;
        protected uint m_RxUncompressed = 0;

        protected ushort m_NumberOfAcceptedAwakeRequestParameters = 0;

        protected ushort m_Function = 0;
        protected CapiStruct_LIConfirmationParameter m_LIConfirmationParameter = null;
        #endregion

        #region Properties
        private FacilitySelector_Enum m_FacilitySelector
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IFacilitySelector)m_Parent).FacilitySelector;
                }
                else
                {
                    return FacilitySelector_Enum.Handset;
                }
            }
        }

        public DTMFInformation_Enum DTMFInformation
        {
            get { return m_DTMFInformation; }
            set { m_DTMFInformation = value; }
        }
        public bool V42BisInformation
        {
            get { return m_V42BisInformation; }
            set { m_V42BisInformation = value; }
        }
        public CompressionMode_Enum CompressionMode
        {
            get { return m_CompressionMode; }
            set { m_CompressionMode = value; }
        }
        public ushort NumberOfCodeWords
        {
            get { return m_NumberOfCodeWords; }
            set { m_NumberOfCodeWords = value; }
        }
        public ushort MaximumStringLength
        {
            get { return m_MaximumStringLength; }
            set { m_MaximumStringLength = value; }
        }
        public uint TxTotal
        {
            get { return m_TxTotal; }
            set { m_TxTotal = value; }
        }
        public uint TxCompressed
        {
            get { return m_TxCompressed; }
            set { m_TxCompressed = value; }
        }
        public uint RxTotal
        {
            get { return m_RxTotal; }
            set { m_RxTotal = value; }
        }
        public uint RxUncompressed
        {
            get { return m_RxUncompressed; }
            set { m_RxUncompressed = value; }
        }
        public ushort NumberOfAcceptedAwakeRequestParameters
        {
            get { return m_NumberOfAcceptedAwakeRequestParameters; }
            set { m_NumberOfAcceptedAwakeRequestParameters = value; }
        }
        public ushort Function
        {
            get { return m_Function; }
            set { m_Function = value; }
        }
        public CapiStruct_LIConfirmationParameter LIConfirmationParameter
        {
            get { return m_LIConfirmationParameter; }
            set { m_LIConfirmationParameter = value; }
        }
            
        public override int DataSize
        {
            get
            {
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    return m_EmptyStructure.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    return 2;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    return 24;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                    return 0;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    return 2;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.LineInterconnect)
                {
                    return 2 + m_LIConfirmationParameter.StructSize;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_FacilityConfirmationParameter(object loParent) 
            : base(loParent)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIConfirmationParameter = new CapiStruct_LIConfirmationParameter(this);
        }
        public CapiStruct_FacilityConfirmationParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIConfirmationParameter = new CapiStruct_LIConfirmationParameter(this);
        }
        public CapiStruct_FacilityConfirmationParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIConfirmationParameter = new CapiStruct_LIConfirmationParameter(this);
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
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    lbData = m_EmptyStructure.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_DTMFInformation), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    if (m_V42BisInformation)
                    {
                        Array.Copy(BitConverter.GetBytes((ushort)0), 0, lbData, liOffset, 2); liOffset += 2;
                    }
                    else
                    {
                        Array.Copy(BitConverter.GetBytes((ushort)1), 0, lbData, liOffset, 2); liOffset += 2;
                    }
                    Array.Copy(BitConverter.GetBytes((ushort)m_CompressionMode), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_NumberOfCodeWords), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_MaximumStringLength), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_TxTotal), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_TxCompressed), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_RxTotal), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_RxUncompressed), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    Array.Copy(BitConverter.GetBytes(m_NumberOfAcceptedAwakeRequestParameters), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.LineInterconnect)
                {
                    Array.Copy(BitConverter.GetBytes(m_Function), 0, lbData, liOffset, 2); liOffset += 2;
                    m_LIConfirmationParameter.AsByteArray(lbData, liOffset); liOffset += m_LIConfirmationParameter.StructSize;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    m_DTMFInformation = (DTMFInformation_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    m_V42BisInformation = (BitConverter.ToUInt16(lbData, liOffset) == 0); liOffset += 2;
                    m_CompressionMode = (CompressionMode_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_NumberOfCodeWords = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_MaximumStringLength = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_TxTotal = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_TxCompressed = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_RxTotal = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_RxUncompressed = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    m_NumberOfAcceptedAwakeRequestParameters = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.LineInterconnect)
                {
                    m_Function = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_LIConfirmationParameter.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_FacilityIndicationParameter : CapiStruct_Base, IFunctionSelector
    {
        #region Vars
        protected CapiStruct_Data m_EmptyStructure = null;

        protected ushort m_Function = 0x0001;
        protected CapiStruct_LIIndicationParameter m_LIIndicationParameter = null;

        protected string m_HandsetDigits = "";
        protected string m_DTMFDigits = "";
        #endregion

        #region Properties
        private FacilitySelector_Enum m_FacilitySelector
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IFacilitySelector)m_Parent).FacilitySelector;
                }
                else
                {
                    return FacilitySelector_Enum.Handset;
                }
            }
        }

        public string HandsetDigits
        {
            get { return m_HandsetDigits; }
            set { m_HandsetDigits = value; }
        }
        public string DTMFDigits
        {
            get { return m_DTMFDigits; }
            set { m_DTMFDigits = value; }
        }
        public ushort Function
        {
            get { return m_Function; }
            set { m_Function = value; }
        }
        public CapiStruct_LIIndicationParameter LIIndicationParameter
        {
            get { return m_LIIndicationParameter; }
            set { m_LIIndicationParameter = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    return m_HandsetDigits.Length;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    return m_DTMFDigits.Length;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    return m_EmptyStructure.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                    return 0;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    return m_EmptyStructure.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.LineInterconnect)
                {
                    return 2 + m_LIIndicationParameter.StructSize;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_FacilityIndicationParameter(object loParent)
            : base(loParent)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIIndicationParameter = new CapiStruct_LIIndicationParameter(this);
        }
        public CapiStruct_FacilityIndicationParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIIndicationParameter = new CapiStruct_LIIndicationParameter(this);
        }
        public CapiStruct_FacilityIndicationParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIIndicationParameter = new CapiStruct_LIIndicationParameter(this);
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
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    if (m_HandsetDigits.Length > 0)
                    {
                        Array.Copy(Encoding.GetEncoding(20105).GetBytes(m_HandsetDigits), 0, lbData, liOffset, m_HandsetDigits.Length);
                    }
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    if (m_DTMFDigits.Length > 0)
                    {
                        Array.Copy(Encoding.GetEncoding(20105).GetBytes(m_DTMFDigits), 0, lbData, liOffset, m_HandsetDigits.Length);
                    }
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    lbData = m_EmptyStructure.AsByteArray(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    lbData = m_EmptyStructure.AsByteArray(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.LineInterconnect)
                {
                    Array.Copy(BitConverter.GetBytes(m_Function), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_LIIndicationParameter.AsByteArray(lbData, liOffset);
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    m_HandsetDigits = Encoding.GetEncoding(20105).GetString(lbData, liOffset, liLen);
                    liOffset += liLen;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    m_DTMFDigits = Encoding.GetEncoding(20105).GetString(lbData, liOffset, liLen);
                    liOffset += liLen;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.LineInterconnect)
                {
                    m_Function = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_LIIndicationParameter.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_FacilityResponseParameter : CapiStruct_Base
    {
        #region Vars
        protected CapiStruct_Data m_EmptyStructure = null;

        protected string m_HandsetDigits = "";
        protected string m_DTMFDigits = "";
        #endregion

        #region Properties
        public string HandsetDigits
        {
            get { return m_HandsetDigits; }
            set { m_HandsetDigits = value; }
        }
        public string DTMFDigits
        {
            get { return m_DTMFDigits; }
            set { m_DTMFDigits = value; }
        }
        private FacilitySelector_Enum m_FacilitySelector
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IFacilitySelector)m_Parent).FacilitySelector;
                }
                else
                {
                    return FacilitySelector_Enum.Handset;
                }
            }
        }

        public override int DataSize
        {
            get
            {
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    return m_HandsetDigits.Length;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    return m_DTMFDigits.Length;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    return m_EmptyStructure.StructSize;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                    return 0;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    return m_EmptyStructure.StructSize;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_FacilityResponseParameter(object loParent)
            : base(loParent)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
        }
        public CapiStruct_FacilityResponseParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
        }
        public CapiStruct_FacilityResponseParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
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
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    if (m_HandsetDigits.Length > 0)
                    {
                        Array.Copy(Encoding.GetEncoding(20105).GetBytes(m_HandsetDigits), 0, lbData, liOffset, m_HandsetDigits.Length);
                    }
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    if (m_DTMFDigits.Length > 0)
                    {
                        Array.Copy(Encoding.GetEncoding(20105).GetBytes(m_DTMFDigits), 0, lbData, liOffset, m_HandsetDigits.Length);
                    }
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    lbData = m_EmptyStructure.AsByteArray(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    lbData = m_EmptyStructure.AsByteArray(lbData, liOffset);
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if (m_FacilitySelector == FacilitySelector_Enum.Handset)
                {
                    m_HandsetDigits = Encoding.GetEncoding(20105).GetString(lbData, liOffset, liLen);
                    liOffset += liLen;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.DTMF)
                {
                    m_DTMFDigits = Encoding.GetEncoding(20105).GetString(lbData, liOffset, liLen);
                    liOffset += liLen;
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.V42bisCompression)
                {
                    liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.SupplementaryServices)
                {
                    //// Part III of docs
                }
                else if (m_FacilitySelector == FacilitySelector_Enum.PowerManagementWakeup)
                {
                    liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                }
            }
            
            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_LIConnectRequestParticipant : CapiStruct_Base
    {
        #region Vars
        protected PLCIClass m_ParticipatingPLCI = new PLCIClass();
        protected ConnectRequestDataPath_Flags m_DataPath = 0;
        #endregion

        #region Properties
        public PLCIClass ParticipatingPLCI
        {
            get { return m_ParticipatingPLCI; }
            set { m_ParticipatingPLCI = value; }
        }
        public ConnectRequestDataPath_Flags DataPath 
        {
            get { return m_DataPath; }
            set { m_DataPath = value; }
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
        public CapiStruct_LIConnectRequestParticipant(object loParent) 
            : base(loParent)
        {
        }
        public CapiStruct_LIConnectRequestParticipant(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_LIConnectRequestParticipant(object loParent, byte[] lbData, int liOffset)
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
                Array.Copy(BitConverter.GetBytes(m_ParticipatingPLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                Array.Copy(BitConverter.GetBytes((uint)m_DataPath), 0, lbData, liOffset, 4); liOffset += 4;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_ParticipatingPLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                m_DataPath = (ConnectRequestDataPath_Flags)BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_LIConnectConfirmationParticipant : CapiStruct_Base
    {
        #region Vars
        protected PLCIClass m_ParticipatingPLCI = new PLCIClass();
        protected GeneralInfo_Enum m_ParticipatingInfo = GeneralInfo_Enum.RequestAccepted;
        #endregion

        #region Properties
        public PLCIClass ParticipatingPLCI
        {
            get { return m_ParticipatingPLCI; }
            set { m_ParticipatingPLCI = value; }
        }
        public GeneralInfo_Enum ParticipatingInfo
        {
            get { return m_ParticipatingInfo; }
            set { m_ParticipatingInfo = value; }
        }

        public override int DataSize
        {
            get
            {
                return 6;
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_LIConnectConfirmationParticipant(object loParent) 
            : base(loParent)
        {
        }
        public CapiStruct_LIConnectConfirmationParticipant(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_LIConnectConfirmationParticipant(object loParent, byte[] lbData, int liOffset)
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
                Array.Copy(BitConverter.GetBytes(m_ParticipatingPLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                Array.Copy(BitConverter.GetBytes((ushort)m_ParticipatingInfo), 0, lbData, liOffset, 2); liOffset += 2;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_ParticipatingPLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                m_ParticipatingInfo = (GeneralInfo_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_LIDisconnectRequestParticipant : CapiStruct_Base
    {
        #region Vars
        protected PLCIClass m_ParticipatingPLCI = new PLCIClass();
        #endregion

        #region Properties
        public PLCIClass ParticipatingPLCI
        {
            get { return m_ParticipatingPLCI; }
            set { m_ParticipatingPLCI = value; }
        }

        public override int DataSize
        {
            get
            {
                return 4;
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_LIDisconnectRequestParticipant(object loParent) 
            : base(loParent)
        {
        }
        public CapiStruct_LIDisconnectRequestParticipant(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_LIDisconnectRequestParticipant(object loParent, byte[] lbData, int liOffset)
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
                Array.Copy(BitConverter.GetBytes(m_ParticipatingPLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_ParticipatingPLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_LIDisconnectConfirmationParticipant : CapiStruct_Base
    {
        #region Vars
        protected PLCIClass m_ParticipatingPLCI = new PLCIClass();
        protected GeneralInfo_Enum m_ParticipatingInfo = GeneralInfo_Enum.RequestAccepted;
        #endregion

        #region Properties
        public PLCIClass ParticipatingPLCI
        {
            get { return m_ParticipatingPLCI; }
            set { m_ParticipatingPLCI = value; }
        }
        public GeneralInfo_Enum ParticipatingInfo
        {
            get { return m_ParticipatingInfo; }
            set { m_ParticipatingInfo = value; }
        }

        public override int DataSize
        {
            get
            {
                return 6;
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_LIDisconnectConfirmationParticipant(object loParent) 
            : base(loParent)
        {
        }
        public CapiStruct_LIDisconnectConfirmationParticipant(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_LIDisconnectConfirmationParticipant(object loParent, byte[] lbData, int liOffset)
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
                Array.Copy(BitConverter.GetBytes(m_ParticipatingPLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                Array.Copy(BitConverter.GetBytes((ushort)m_ParticipatingInfo), 0, lbData, liOffset, 2); liOffset += 2;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_ParticipatingPLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                m_ParticipatingInfo = (GeneralInfo_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_LIRequestParameter : CapiStruct_Base
    {
        #region Vars
        protected CapiStruct_Data m_EmptyStructure = null;

        protected RequestParameterDataPath_Flags m_DataPath = 0;
        protected CapiStruct_LIConnectRequestParticipant m_LIConnectRequestParticipant = null;
        protected CapiStruct_LIDisconnectRequestParticipant m_LIDisconnectRequestParticipant = null;
        #endregion

        #region Properties
        private ushort m_Function
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IFunctionSelector)m_Parent).Function;
                }
                else
                {
                    return 0;
                }
            }
        }

        public RequestParameterDataPath_Flags DataPath
        {
            get { return m_DataPath; }
            set { m_DataPath = value; }
        }
        public CapiStruct_LIConnectRequestParticipant LIConnectRequestParticipant
        {
            get { return m_LIConnectRequestParticipant; }
            set { m_LIConnectRequestParticipant = value; }
        }
        public CapiStruct_LIDisconnectRequestParticipant LIDisconnectRequestParticipant
        {
            get { return m_LIDisconnectRequestParticipant; }
            set { m_LIDisconnectRequestParticipant = value; }
        }

        public override int DataSize
        {
            get
            {
                return 4 + m_LIConnectRequestParticipant.StructSize;
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_LIRequestParameter(object loParent) 
            : base(loParent)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIConnectRequestParticipant = new CapiStruct_LIConnectRequestParticipant(this);
            m_LIDisconnectRequestParticipant = new CapiStruct_LIDisconnectRequestParticipant(this);
        }
        public CapiStruct_LIRequestParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIConnectRequestParticipant = new CapiStruct_LIConnectRequestParticipant(this);
            m_LIDisconnectRequestParticipant = new CapiStruct_LIDisconnectRequestParticipant(this);
        }
        public CapiStruct_LIRequestParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIConnectRequestParticipant = new CapiStruct_LIConnectRequestParticipant(this);
            m_LIDisconnectRequestParticipant = new CapiStruct_LIDisconnectRequestParticipant(this);
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
                if (m_Function == 0)
                {
                    lbData = m_EmptyStructure.AsByteArray(lbData, liOffset);
                }
                else if (m_Function == 1)
                {
                    Array.Copy(BitConverter.GetBytes((uint)m_DataPath), 0, lbData, liOffset, 4); liOffset += 4;
                    lbData = m_LIConnectRequestParticipant.AsByteArray(lbData, liOffset);
                }
                else if (m_Function == 2)
                {
                    lbData = m_LIDisconnectRequestParticipant.AsByteArray(lbData, liOffset);
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if (m_Function == 0)
                {
                    m_EmptyStructure.SetData(lbData, liOffset);
                }
                else if (m_Function == 1)
                {
                    m_DataPath = (RequestParameterDataPath_Flags)BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    liOffset = m_LIConnectRequestParticipant.SetData(lbData, liOffset);
                }
                else if (m_Function == 1)
                {
                    liOffset = m_LIDisconnectRequestParticipant.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_LIConfirmationParameter : CapiStruct_Base
    {
        #region Vars
        protected CapiStruct_Data m_EmptyStructure = null;

        protected GeneralInfo_Enum m_Info = GeneralInfo_Enum.RequestAccepted;
        protected SupportedServices_Flags m_SupportedServices = 0;
        protected uint m_SupportedInterconnectsThisController = 0;
        protected uint m_SupportedParticipantsThisController = 0;
        protected uint m_SupportedInterconnectsAllController = 0;
        protected uint m_SupportedParticipantsAllController = 0;

        protected CapiStruct_LIConnectConfirmationParticipant m_LIConnectConfirmationParticipant = null;
        protected CapiStruct_LIDisconnectConfirmationParticipant m_LIDisconnectConfirmationParticipant = null;
        #endregion

        #region Properties
        private ushort m_Function
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IFunctionSelector)m_Parent).Function;
                }
                else
                {
                    return 0;
                }
            }
        }

        public GeneralInfo_Enum Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }
        public SupportedServices_Flags SupportedServices
        {
            get { return m_SupportedServices; }
            set { m_SupportedServices = value; }
        }
        public uint SupportedInterconnectsThisController
        {
            get { return m_SupportedInterconnectsThisController; }
            set { m_SupportedInterconnectsThisController = value; }
        }
        public uint SupportedParticipantsThisController
        {
            get { return m_SupportedParticipantsThisController; }
            set { m_SupportedParticipantsThisController = value; }
        }
        public uint SupportedInterconnectsAllController
        {
            get { return m_SupportedInterconnectsAllController; }
            set { m_SupportedInterconnectsAllController = value; }
        }
        public uint SupportedParticipantsAllController
        {
            get { return m_SupportedParticipantsAllController; }
            set { m_SupportedParticipantsAllController = value; }
        }
        public CapiStruct_LIConnectConfirmationParticipant LIConnectConfirmationParticipant
        {
            get { return m_LIConnectConfirmationParticipant; }
            set { m_LIConnectConfirmationParticipant = value; }
        }
        public CapiStruct_LIDisconnectConfirmationParticipant LIDisconnectConfirmationParticipant
        {
            get { return m_LIDisconnectConfirmationParticipant; }
            set { m_LIDisconnectConfirmationParticipant = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Function == 0)
                {
                    return 22;
                }
                else if (m_Function == 1)
                {
                    return 2 + m_LIConnectConfirmationParticipant.StructSize;
                }
                else if (m_Function == 2)
                {
                    return 2 + m_LIDisconnectConfirmationParticipant.StructSize;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_LIConfirmationParameter(object loParent) 
            : base(loParent)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIConnectConfirmationParticipant = new CapiStruct_LIConnectConfirmationParticipant(this);
            m_LIDisconnectConfirmationParticipant = new CapiStruct_LIDisconnectConfirmationParticipant(this);
        }
        public CapiStruct_LIConfirmationParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIConnectConfirmationParticipant = new CapiStruct_LIConnectConfirmationParticipant(this);
            m_LIDisconnectConfirmationParticipant = new CapiStruct_LIDisconnectConfirmationParticipant(this);
        }
        public CapiStruct_LIConfirmationParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_LIConnectConfirmationParticipant = new CapiStruct_LIConnectConfirmationParticipant(this);
            m_LIDisconnectConfirmationParticipant = new CapiStruct_LIDisconnectConfirmationParticipant(this);
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
                if (m_Function == 0)
                {
                    m_Info = (GeneralInfo_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_SupportedServices = (SupportedServices_Flags)BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_SupportedInterconnectsThisController = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_SupportedParticipantsThisController = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_SupportedInterconnectsAllController = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_SupportedParticipantsAllController = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else if (m_Function == 1)
                {
                    m_Info = (GeneralInfo_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    lbData = m_LIConnectConfirmationParticipant.AsByteArray(lbData, liOffset); liOffset += m_LIConnectConfirmationParticipant.StructSize;
                }
                else if (m_Function == 2)
                {
                    m_Info = (GeneralInfo_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    lbData = m_LIDisconnectConfirmationParticipant.AsByteArray(lbData, liOffset); liOffset += m_LIDisconnectConfirmationParticipant.StructSize;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if (m_Function == 0)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_Info), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes((uint)m_SupportedServices), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_SupportedInterconnectsThisController), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_SupportedParticipantsThisController), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_SupportedInterconnectsAllController), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_SupportedParticipantsAllController), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_Function == 1)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_Info), 0, lbData, liOffset, 2); liOffset += 2;
                    liOffset = m_LIConnectConfirmationParticipant.SetData(lbData, liOffset);
                }
                else if (m_Function == 2)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_Info), 0, lbData, liOffset, 2); liOffset += 2;
                    liOffset = m_LIDisconnectConfirmationParticipant.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_LIIndicationParameter : CapiStruct_Base
    {
        #region Vars
        protected PLCIClass m_ParticipatingPLCI = new PLCIClass();
        protected ServiceReason_Enum m_ServiceReason = ServiceReason_Enum.UserInitiated;
        #endregion

        #region Properties
        private ushort m_Function
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IFunctionSelector)m_Parent).Function;
                }
                else
                {
                    return 0;
                }
            }
        }

        public PLCIClass ParticipatingPLCI
        {
            get { return m_ParticipatingPLCI; }
            set { m_ParticipatingPLCI = value; }
        }
        public ServiceReason_Enum ServiceReason
        {
            get { return m_ServiceReason; }
            set { m_ServiceReason = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Function == 1)
                {
                    return 4;
                }
                else if (m_Function == 2)
                {
                    return 6;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_LIIndicationParameter(object loParent) 
            : base(loParent)
        {
        }
        public CapiStruct_LIIndicationParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_LIIndicationParameter(object loParent, byte[] lbData, int liOffset)
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
                if (m_Function == 1)
                {
                    Array.Copy(BitConverter.GetBytes(m_ParticipatingPLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_Function == 2)
                {
                    Array.Copy(BitConverter.GetBytes(m_ParticipatingPLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes((ushort)m_ServiceReason), 0, lbData, liOffset, 2); liOffset += 2;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if (m_Function == 1)
                {
                    m_ParticipatingPLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                }
                else if (m_Function == 2)
                {
                    m_ParticipatingPLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                    m_ServiceReason = (ServiceReason_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
            }

            return liOffset;
        }
        #endregion
    }
    
    public class CapiStruct_SendingComplete : CapiStruct_Base
    {
        #region Vars
        protected ModeEnum m_Mode = ModeEnum.NotSendingInformation;
        #endregion

        #region Properties
        public ModeEnum Mode
        {
            get { return m_Mode; }
            set { m_Mode = value; }
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
        public CapiStruct_SendingComplete(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_SendingComplete(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_SendingComplete(object loParent, byte[] lbData, int liOffset)
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
                Array.Copy(BitConverter.GetBytes((ushort)m_Mode), 0, lbData, liOffset, 2); liOffset += 2;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_Mode = (ModeEnum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
            }

            return liOffset;
        }
        #endregion
    }


    public class CapiStruct_CallingNumber : CapiStruct_Base
    {
        #region Vars
        protected string m_Number = "";
        protected bool m_Suppressing = false;
        #endregion

        #region Properties
        public string Number
        {
            get { return m_Number; }
            set { m_Number = value; }
        }
        public bool Suppressing
        {
            get { return m_Suppressing; }
            set { m_Suppressing = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Number.Length == 0)
                {
                    return 0;
                }
                else
                {
                    return m_Number.Length + 2;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_CallingNumber(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_CallingNumber(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_CallingNumber(object loParent, byte[] lbData, int liOffset)
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

            if (liDataSize > 2)
            {
                lbData[liOffset] = 0x00; liOffset++;
                if (m_Suppressing)
                {
                    lbData[liOffset] = 0xA0;
                }
                else
                {
                    lbData[liOffset] = 0x80;
                }
                liOffset++;
                if (m_Number.Length != 0)
                {
                    Array.Copy(ASCIIEncoding.ASCII.GetBytes(m_Number), 0, lbData, liOffset, m_Number.Length); liOffset += m_Number.Length;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                liOffset++;
                m_Suppressing = (lbData[liOffset] == 0xA0); liOffset++;
                m_Number = ASCIIEncoding.ASCII.GetString(lbData, liOffset, liLen - 2); liOffset += (liLen - 2);
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_ConnectedNumber : CapiStruct_Base
    {
        #region Vars
        protected string m_Number = "";
        protected bool m_Suppressing = false;
        #endregion

        #region Properties
        public string Number
        {
            get { return m_Number; }
            set { m_Number = value; }
        }
        public bool Suppressing
        {
            get { return m_Suppressing; }
            set { m_Suppressing = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Number.Length == 0)
                {
                    return 0;
                }
                else
                {
                    return m_Number.Length + 2;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_ConnectedNumber(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_ConnectedNumber(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_ConnectedNumber(object loParent, byte[] lbData, int liOffset)
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

            if (liDataSize > 2)
            {
                lbData[liOffset] = 0x00; liOffset++;
                if (m_Suppressing)
                {
                    lbData[liOffset] = 0x00;
                }
                else
                {
                    lbData[liOffset] = 0x80;
                }
                liOffset++;
                if (m_Number.Length != 0)
                {
                    Array.Copy(ASCIIEncoding.ASCII.GetBytes(m_Number), 0, lbData, liOffset, m_Number.Length); liOffset += m_Number.Length;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                liOffset++;
                m_Suppressing = (lbData[liOffset] != 0x80); liOffset++;
                m_Number = ASCIIEncoding.ASCII.GetString(lbData, liOffset, liLen - 2); liOffset += (liLen - 2);
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_CalledNumber : CapiStruct_Base
    {
        #region Vars
        protected string m_Number = "";
        #endregion

        #region Properties
        public string Number
        {
            get { return m_Number; }
            set { m_Number = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Number.Length == 0)
                {
                    return 0;
                }
                else
                {
                    return m_Number.Length + 1;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_CalledNumber(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_CalledNumber(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_CalledNumber(object loParent, byte[] lbData, int liOffset)
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

            if (liDataSize > 1)
            {
                lbData[liOffset] = 0x80; liOffset++;
                if (m_Number.Length != 0)
                {
                    Array.Copy(ASCIIEncoding.ASCII.GetBytes(m_Number), 0, lbData, liOffset, m_Number.Length); liOffset += m_Number.Length;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                liOffset += 1;
                m_Number = ASCIIEncoding.ASCII.GetString(lbData, liOffset, liLen - 1); liOffset += (liLen - 1);
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_SubAddress : CapiStruct_Base
    {
        #region Vars
        protected string m_Number = "";
        #endregion

        #region Properties
        public string Number
        {
            get { return m_Number; }
            set { m_Number = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Number.Length == 0)
                {
                    return 0;
                }
                else
                {
                    return m_Number.Length + 1;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_SubAddress(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_SubAddress(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_SubAddress(object loParent, byte[] lbData, int liOffset)
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

            if (liDataSize > 1)
            {
                lbData[liOffset] = 0x80; liOffset++;
                if (m_Number.Length != 0)
                {
                    Array.Copy(ASCIIEncoding.ASCII.GetBytes(m_Number), 0, lbData, liOffset, m_Number.Length); liOffset += m_Number.Length;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                liOffset += 1;
                m_Number = ASCIIEncoding.ASCII.GetString(lbData, liOffset, liLen - 1); liOffset += (liLen - 1);
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_IA5 : CapiStruct_Base
    {
        #region Vars
        protected string m_Data = "";
        #endregion

        #region Properties
        public string Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Data.Length == 0)
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

        #region Constructor
        public CapiStruct_IA5(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_IA5(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_IA5(object loParent, byte[] lbData, int liOffset)
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
                if (m_Data.Length != 0)
                {
                    Array.Copy(Encoding.GetEncoding(20105).GetBytes(m_Data), 0, lbData, liOffset, m_Data.Length); liOffset += m_Data.Length;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                liOffset += 1;
                m_Data = Encoding.GetEncoding(20105).GetString(lbData, liOffset, liLen - 1); liOffset += (liLen - 1);
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_InfoElement : CapiStruct_Base
    {
        #region Vars
        protected uint m_Charges = 0;
        protected uint m_ExtendedCharges = 0;
        protected Multiplier_Enum m_Multiplier = Multiplier_Enum.M_1_of_1000;
        protected CapiStruct_IA5 m_CurrencySign = null;
        #endregion

        #region Properties
        private InfoDataType m_DataType
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IInfoSelector)m_Parent).DataType;
                }
                else
                {
                    return InfoDataType.MessageType;
                }
            }
        }
        private InfoChargeType m_ChargeType
        {
            get
            {
                if (m_Parent != null)
                {
                    return ((IInfoSelector)m_Parent).ChargeType;
                }
                else
                {
                    return InfoChargeType.ChargeUnits;
                }
            }
        }

        public uint Charges
        {
            get { return m_Charges; }
            set { m_Charges = value; }
        }
        public uint ExtendedCharges
        {
            get { return m_ExtendedCharges; }
            set { m_ExtendedCharges = value; }
        }
        public Multiplier_Enum Multiplier
        {
            get { return m_Multiplier; }
            set { m_Multiplier = value; }
        }
        public CapiStruct_IA5 CurrencySign
        {
            get { return m_CurrencySign; }
            set { m_CurrencySign = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_ChargeType == InfoChargeType.ChargeUnits) {
                    return 4;
                } else {
                    return 10 + m_CurrencySign.StructSize;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_InfoElement(object loParent)
            : base(loParent)
        {
            m_CurrencySign = new CapiStruct_IA5(this);
        }
        public CapiStruct_InfoElement(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_CurrencySign = new CapiStruct_IA5(this);
        }
        public CapiStruct_InfoElement(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_CurrencySign = new CapiStruct_IA5(this);
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
                if (m_ChargeType == InfoChargeType.ChargeUnits)
                {
                    Array.Copy(BitConverter.GetBytes(m_Charges), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else 
                {
                    Array.Copy(BitConverter.GetBytes(m_Charges), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_ExtendedCharges), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes((ushort)m_Multiplier), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_CurrencySign.AsByteArray(lbData, liOffset);
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                if (m_ChargeType == InfoChargeType.ChargeUnits)
                {
                    m_Charges = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else 
                {
                    m_Charges = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_ExtendedCharges = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Multiplier = (Multiplier_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_CurrencySign.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_ISO8859_1 : CapiStruct_Base
    {
        #region Vars
        protected string m_Data = "";
        #endregion

        #region Properties
        public string Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Data.Length == 0)
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

        #region Constructor
        public CapiStruct_ISO8859_1(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_ISO8859_1(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_ISO8859_1(object loParent, byte[] lbData, int liOffset)
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
                if (m_Data.Length != 0)
                {
                    Array.Copy(Encoding.GetEncoding(28591).GetBytes(m_Data), 0, lbData, liOffset, m_Data.Length); liOffset += m_Data.Length;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                liOffset += 1;
                m_Data = Encoding.GetEncoding(28591).GetString(lbData, liOffset, liLen - 1); liOffset += (liLen - 1);
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_ASCIIString : CapiStruct_Base
    {
        #region Vars
        protected string m_Data = "";
        #endregion

        #region Properties
        public string Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Data.Length == 0)
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

        #region Constructor
        public CapiStruct_ASCIIString(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_ASCIIString(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_ASCIIString(object loParent, byte[] lbData, int liOffset)
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
                if (m_Data.Length != 0)
                {
                    Array.Copy(Encoding.ASCII.GetBytes(m_Data), 0, lbData, liOffset, m_Data.Length); liOffset += m_Data.Length;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                liOffset += 1;
                m_Data = Encoding.ASCII.GetString(lbData, liOffset, liLen - 1); liOffset += (liLen - 1);
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_PLCI : CapiStruct_Base
        {
            #region Vars
            protected PLCIClass m_PLCI = new PLCIClass();
            #endregion

            #region Properties
            public PLCIClass PLCI
            {
                get { return m_PLCI; }
                set { m_PLCI = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_PLCI(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_PLCI(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_PLCI(object loParent, byte[] lbData, int liOffset)
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
                    Array.Copy(BitConverter.GetBytes(m_PLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                }

                return liOffset;
            }
            #endregion
        }
  
    public class CapiStruct_DWord : CapiStruct_Base
        {
            #region Vars
            protected uint m_Value = 0;
            #endregion

            #region Properties
            public uint Value
            {
                get { return m_Value; }
                set { m_Value = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_DWord(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_DWord(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_DWord(object loParent, byte[] lbData, int liOffset)
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
                    Array.Copy(BitConverter.GetBytes((uint)m_Value), 0, lbData, liOffset, 4); liOffset += 4;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                if (liLen > 0)
                {
                    m_Value = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }

                return liOffset;
            }
            #endregion
        }


    public class CapiStruct_USBInteroperabilityRequestParameter : CapiStruct_Base, IB1ProtocolSelector
    {
        #region Vars
        protected CapiStruct_Data m_EmptyStructure = null;

        protected USBFunction_Enum m_Function = USBFunction_Enum.Register;

        protected ushort m_Register_MaxLogicalConnections = 0;
        protected ushort m_Register_MaxBDataBlocks = 0;
        protected ushort m_Register_MaxBDataLength = 0;

        protected B1Protocol_Enum m_OpenInterface_B1Protocol = B1Protocol_Enum.Kbits64WithHDLCFraming;
        protected CapiStruct_B1Configuration m_OpenInterface_B1Configuration = null;
        protected CapiStruct_BChannelInformation m_OpenInerface_BChannelInformation = null;

        protected uint m_USBInterfaceID = 0;
        protected CapiStruct_Data m_Data = null;
        #endregion

        #region Properties
        public USBFunction_Enum Function
        {
            get { return m_Function; }
            set { m_Function = value; }
        }

        public ushort MaxLogicalConnections
        {
            get { return m_Register_MaxLogicalConnections; }
            set { m_Register_MaxLogicalConnections = value; }
        }
        public ushort MaxBDataBlocks
        {
            get { return m_Register_MaxBDataBlocks; }
            set { m_Register_MaxBDataBlocks = value; }
        }
        public ushort MaxBDataLength
        {
            get { return m_Register_MaxBDataLength; }
            set { m_Register_MaxBDataLength = value; }
        }

        public B1Protocol_Enum B1Protocol
        {
            get { return m_OpenInterface_B1Protocol; }
            set { m_OpenInterface_B1Protocol = value; }
        }
        public CapiStruct_B1Configuration B1Configuration
        {
            get { return m_OpenInterface_B1Configuration; }
            set { m_OpenInterface_B1Configuration = value; }
        }
        public CapiStruct_BChannelInformation BChannelInformation
        {
            get { return m_OpenInerface_BChannelInformation; }
            set { m_OpenInerface_BChannelInformation = value; }
        }

        public uint USBInterfaceID
        {
            get { return m_USBInterfaceID; }
            set { m_USBInterfaceID = value; }
        }
        public CapiStruct_Data Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Function == USBFunction_Enum.Register)
                {
                    return 8;
                }
                else if (m_Function == USBFunction_Enum.Release)
                {
                    return 2 + m_EmptyStructure.StructSize;
                }
                else if (m_Function == USBFunction_Enum.OpenInterface)
                {
                    return 4 + m_OpenInterface_B1Configuration.StructSize + m_OpenInerface_BChannelInformation.StructSize;
                }
                else if (m_Function == USBFunction_Enum.CloseInterface)
                {
                    return 6;
                }
                else if (m_Function == USBFunction_Enum.Write)
                {
                    return 6 + m_Data.StructSize;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_USBInteroperabilityRequestParameter(object loParent)
            : base(loParent)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_OpenInterface_B1Configuration = new CapiStruct_B1Configuration(this);
            m_OpenInerface_BChannelInformation = new CapiStruct_BChannelInformation(this);
            m_Data = new CapiStruct_Data(this);
        }
        public CapiStruct_USBInteroperabilityRequestParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_OpenInterface_B1Configuration = new CapiStruct_B1Configuration(this);
            m_OpenInerface_BChannelInformation = new CapiStruct_BChannelInformation(this);
            m_Data = new CapiStruct_Data(this);
        }
        public CapiStruct_USBInteroperabilityRequestParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_OpenInterface_B1Configuration = new CapiStruct_B1Configuration(this);
            m_OpenInerface_BChannelInformation = new CapiStruct_BChannelInformation(this);
            m_Data = new CapiStruct_Data(this);
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
                Array.Copy(BitConverter.GetBytes((ushort)m_Function), 0, lbData, liOffset, 2); liOffset += 2;

                if (m_Function == USBFunction_Enum.Register)
                {
                    Array.Copy(BitConverter.GetBytes(m_Register_MaxLogicalConnections), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_Register_MaxBDataBlocks), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_Register_MaxBDataLength), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_Function == USBFunction_Enum.Release)
                {
                    lbData = m_EmptyStructure.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                }
                else if (m_Function == USBFunction_Enum.OpenInterface)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_OpenInterface_B1Protocol), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_OpenInterface_B1Configuration.AsByteArray(lbData, liOffset); liOffset += m_OpenInterface_B1Configuration.StructSize;
                    lbData = m_OpenInerface_BChannelInformation.AsByteArray(lbData, liOffset); liOffset += m_OpenInerface_BChannelInformation.StructSize;
                }
                else if (m_Function == USBFunction_Enum.CloseInterface)
                {
                    Array.Copy(BitConverter.GetBytes(m_USBInterfaceID), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_Function == USBFunction_Enum.Write)
                {
                    Array.Copy(BitConverter.GetBytes(m_USBInterfaceID), 0, lbData, liOffset, 4); liOffset += 4;
                    lbData = m_Data.AsByteArray(lbData, liOffset); liOffset += m_Data.StructSize;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_Function = (USBFunction_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;

                if (m_Function == USBFunction_Enum.Register)
                {
                    m_Register_MaxLogicalConnections = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Register_MaxBDataBlocks = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Register_MaxBDataLength = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_Function == USBFunction_Enum.Release)
                {
                    liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                }
                else if (m_Function == USBFunction_Enum.OpenInterface)
                {
                    m_OpenInterface_B1Protocol = (B1Protocol_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_OpenInterface_B1Configuration.SetData(lbData, liOffset);
                    liOffset = m_OpenInerface_BChannelInformation.SetData(lbData, liOffset);
                }
                else if (m_Function == USBFunction_Enum.CloseInterface)
                {
                    m_USBInterfaceID = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else if (m_Function == USBFunction_Enum.Write)
                {
                    m_USBInterfaceID = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    liOffset = m_Data.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_USBInteroperabilityConfirmationParameter : CapiStruct_Base
    {
        #region Vars
        protected USBFunction_Enum m_Function = USBFunction_Enum.Register;

        protected CapiResults_Enum m_ResultInfo = CapiResults_Enum.NoError;
        protected InteroperabilityInfo_Enum m_InteropInfo = InteroperabilityInfo_Enum.NoError;
        protected USBDeviceMode_Enum m_DeviceMode = USBDeviceMode_Enum.Intelligent;
        protected uint m_USBInterfaceID = 0;
        #endregion

        #region Properties
        public USBFunction_Enum Function
        {
            get { return m_Function; }
            set { m_Function = value; }
        }

        public CapiResults_Enum ResultInfo
        {
            get { return m_ResultInfo; }
            set { m_ResultInfo = value; }
        }
        public InteroperabilityInfo_Enum InteropInfo
        {
            get { return m_InteropInfo; }
            set { m_InteropInfo = value; }
        }
        public USBDeviceMode_Enum DeviceMode
        {
            get { return m_DeviceMode; }
            set { m_DeviceMode = value; }
        }
        public uint USBInterfaceID
        {
            get { return m_USBInterfaceID; }
            set { m_USBInterfaceID = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Function == USBFunction_Enum.Register)
                {
                    return 6;
                }
                else if (m_Function == USBFunction_Enum.Release)
                {
                    return 4;
                }
                else if (m_Function == USBFunction_Enum.OpenInterface)
                {
                    return 8;
                }
                else if (m_Function == USBFunction_Enum.CloseInterface)
                {
                    return 4;
                }
                else if (m_Function == USBFunction_Enum.Write)
                {
                    return 4;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_USBInteroperabilityConfirmationParameter(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_USBInteroperabilityConfirmationParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_USBInteroperabilityConfirmationParameter(object loParent, byte[] lbData, int liOffset)
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
                Array.Copy(BitConverter.GetBytes((ushort)m_Function), 0, lbData, liOffset, 2); liOffset += 2;

                if (m_Function == USBFunction_Enum.Register)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_ResultInfo), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes((ushort)m_DeviceMode), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_Function == USBFunction_Enum.Release)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_ResultInfo), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_Function == USBFunction_Enum.OpenInterface)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_InteropInfo), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_USBInterfaceID), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_Function == USBFunction_Enum.CloseInterface)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_InteropInfo), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_Function == USBFunction_Enum.Write)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_InteropInfo), 0, lbData, liOffset, 2); liOffset += 2;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_Function = (USBFunction_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;

                if (m_Function == USBFunction_Enum.Register)
                {
                    m_ResultInfo = (CapiResults_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_DeviceMode = (USBDeviceMode_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_Function == USBFunction_Enum.Release)
                {
                    m_ResultInfo = (CapiResults_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_Function == USBFunction_Enum.OpenInterface)
                {
                    m_InteropInfo = (InteroperabilityInfo_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_USBInterfaceID = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else if (m_Function == USBFunction_Enum.CloseInterface)
                {
                    m_InteropInfo = (InteroperabilityInfo_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_Function == USBFunction_Enum.Write)
                {
                    m_InteropInfo = (InteroperabilityInfo_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_USBInteroperabilityIndicationParameter : CapiStruct_Base
    {
        #region Vars
        protected USBFunction_Enum m_Function = USBFunction_Enum.Read;

        protected uint m_USBInterfaceID = 0;
        protected CapiStruct_Data m_Data = null;
        #endregion

        #region Properties
        public USBFunction_Enum Function
        {
            get { return m_Function; }
            set { m_Function = value; }
        }

        public uint USBInterfaceID
        {
            get { return m_USBInterfaceID; }
            set { m_USBInterfaceID = value; }
        }
        public CapiStruct_Data Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Function == USBFunction_Enum.Read)
                {
                    return 6 + m_Data.StructSize;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_USBInteroperabilityIndicationParameter(object loParent)
            : base(loParent)
        {
            m_Data = new CapiStruct_Data(this);
        }
        public CapiStruct_USBInteroperabilityIndicationParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_Data = new CapiStruct_Data(this);
        }
        public CapiStruct_USBInteroperabilityIndicationParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_Data = new CapiStruct_Data(this);
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
                Array.Copy(BitConverter.GetBytes((ushort)m_Function), 0, lbData, liOffset, 2); liOffset += 2;

                if (m_Function == USBFunction_Enum.Read)
                {
                    Array.Copy(BitConverter.GetBytes(m_USBInterfaceID), 0, lbData, liOffset, 4); liOffset += 4;
                    lbData = m_Data.AsByteArray(lbData, liOffset); liOffset += m_Data.StructSize;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_Function = (USBFunction_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;

                if (m_Function == USBFunction_Enum.Read)
                {
                    m_USBInterfaceID = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    liOffset = m_Data.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_BluetoothInteroperabilityRequestParameter : CapiStruct_Base
    {
        #region Vars
        protected CapiStruct_Data m_EmptyStructure = null;

        protected BluetoothFunction_Enum m_Function = BluetoothFunction_Enum.Register;

        protected ushort m_Register_MaxLogicalConnections = 0;
        protected ushort m_Register_MaxBDataBlocks = 0;
        protected ushort m_Register_MaxBDataLength = 0;

        protected uint m_Controller = 0;

        protected CapiStruct_Data m_Manufacturer = null;
        #endregion

        #region Properties
        public BluetoothFunction_Enum Function
        {
            get { return m_Function; }
            set { m_Function = value; }
        }

        public ushort Register_MaxLogicalConnections
        {
            get { return m_Register_MaxLogicalConnections; }
            set { m_Register_MaxLogicalConnections = value; }
        }
        public ushort Register_MaxBDataBlocks
        {
            get { return m_Register_MaxBDataBlocks; }
            set { m_Register_MaxBDataBlocks = value; }
        }
        public ushort Register_MaxBDataLength
        {
            get { return m_Register_MaxBDataLength; }
            set { m_Register_MaxBDataLength = value; }
        }

        public uint Controller
        {
            get { return m_Controller; }
            set { m_Controller = value; }
        }

        public CapiStruct_Data Manufacturer
        {
            get { return m_Manufacturer; }
            set { m_Manufacturer = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Function == BluetoothFunction_Enum.Register)
                {
                    return 8;
                }
                else if (m_Function == BluetoothFunction_Enum.Release)
                {
                    return 2 + m_EmptyStructure.StructSize;
                }
                else if (m_Function == BluetoothFunction_Enum.GetProfile)
                {
                    return 6;
                }
                else if (m_Function == BluetoothFunction_Enum.GetManufacturer)
                {
                    return 6;
                }
                else if (m_Function == BluetoothFunction_Enum.GetVersion)
                {
                    return 6;
                }
                else if (m_Function == BluetoothFunction_Enum.GetSerialNumber)
                {
                    return 6;
                }
                else if (m_Function == BluetoothFunction_Enum.Manufacturer)
                {
                    return 2 + m_Manufacturer.StructSize;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_BluetoothInteroperabilityRequestParameter(object loParent)
            : base(loParent)
        {
            m_Manufacturer = new CapiStruct_Data(this);
        }
        public CapiStruct_BluetoothInteroperabilityRequestParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_Manufacturer = new CapiStruct_Data(this);
        }
        public CapiStruct_BluetoothInteroperabilityRequestParameter(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_Manufacturer = new CapiStruct_Data(this);
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
                Array.Copy(BitConverter.GetBytes((ushort)m_Function), 0, lbData, liOffset, 2); liOffset += 2;

                if (m_Function == BluetoothFunction_Enum.Register)
                {
                    Array.Copy(BitConverter.GetBytes(m_Register_MaxLogicalConnections), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_Register_MaxBDataBlocks), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_Register_MaxBDataLength), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_Function == BluetoothFunction_Enum.Release)
                {
                    lbData = m_EmptyStructure.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                }
                else if (m_Function == BluetoothFunction_Enum.GetProfile)
                {
                    Array.Copy(BitConverter.GetBytes(m_Controller), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.GetManufacturer)
                {
                    Array.Copy(BitConverter.GetBytes(m_Controller), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.GetVersion)
                {
                    Array.Copy(BitConverter.GetBytes(m_Controller), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.GetSerialNumber)
                {
                    Array.Copy(BitConverter.GetBytes(m_Controller), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.Manufacturer)
                {
                    lbData = m_Manufacturer.AsByteArray(lbData, liOffset); liOffset += m_Manufacturer.StructSize;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_Function = (BluetoothFunction_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;

                if (m_Function == BluetoothFunction_Enum.Register)
                {
                    m_Register_MaxLogicalConnections = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Register_MaxBDataBlocks = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Register_MaxBDataLength = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_Function == BluetoothFunction_Enum.Release)
                {
                    liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                }
                else if (m_Function == BluetoothFunction_Enum.GetProfile)
                {
                    m_Controller = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.GetManufacturer)
                {
                    m_Controller = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.GetVersion)
                {
                    m_Controller = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.GetSerialNumber)
                {
                    m_Controller = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.Manufacturer)
                {
                    liOffset = m_Manufacturer.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_BluetoothInteroperabilityConfirmationParameter : CapiStruct_Base
    {
        #region Vars
        protected BluetoothFunction_Enum m_Function = BluetoothFunction_Enum.Register;

        protected CapiResults_Enum m_ResultInfo = CapiResults_Enum.NoError;
        protected InteroperabilityInfo_Enum m_InteropInfo = InteroperabilityInfo_Enum.NoError;
        protected uint m_Controller = 0;
        protected CapiStruct_ASCIIString m_Text = null;
        protected CapiVersion m_Version = new CapiVersion(0, 0, 0, 0);
        protected uint m_ReturnValue = 0;
        protected CapiStruct_Data m_Manufacturer = null;
        #endregion

        #region Properties
        public BluetoothFunction_Enum Function
        {
            get { return m_Function; }
            set { m_Function = value; }
        }

        public CapiResults_Enum ResultInfo
        {
            get { return m_ResultInfo; }
            set { m_ResultInfo = value; }
        }
        public InteroperabilityInfo_Enum InteropInfo
        {
            get { return m_InteropInfo; }
            set { m_InteropInfo = value; }
        }
        public uint Controller
        {
            get { return m_Controller; }
            set { m_Controller = value; }
        }
        public CapiStruct_ASCIIString Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        public CapiVersion Version
        {
            get { return m_Version; }
            set { m_Version = value; }
        }
        public uint ReturnValue
        {
            get { return m_ReturnValue; }
            set { m_ReturnValue = value; }
        }
        public CapiStruct_Data Manufacturer
        {
            get { return m_Manufacturer; }
            set { m_Manufacturer = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Function == BluetoothFunction_Enum.Register)
                {
                    return 4;
                }
                else if (m_Function == BluetoothFunction_Enum.Release)
                {
                    return 4;
                }
                else if (m_Function == BluetoothFunction_Enum.GetProfile)
                {
                    return 4 + m_Text.StructSize;
                }
                else if (m_Function == BluetoothFunction_Enum.GetManufacturer)
                {
                    return 8 + m_Text.StructSize;
                }
                else if (m_Function == BluetoothFunction_Enum.GetVersion)
                {
                    return 26;
                }
                else if (m_Function == BluetoothFunction_Enum.GetSerialNumber)
                {
                    return 10 + m_Text.StructSize;
                }
                else if (m_Function == BluetoothFunction_Enum.Manufacturer)
                {
                    return m_Manufacturer.StructSize;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_BluetoothInteroperabilityConfirmationParameter(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_BluetoothInteroperabilityConfirmationParameter(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_BluetoothInteroperabilityConfirmationParameter(object loParent, byte[] lbData, int liOffset)
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
                Array.Copy(BitConverter.GetBytes((ushort)m_Function), 0, lbData, liOffset, 2); liOffset += 2;

                if (m_Function == BluetoothFunction_Enum.Register)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_ResultInfo), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_Function == BluetoothFunction_Enum.Release)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_ResultInfo), 0, lbData, liOffset, 2); liOffset += 2;
                }
                else if (m_Function == BluetoothFunction_Enum.GetProfile)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_ResultInfo), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_Text.AsByteArray(lbData, liOffset);
                }
                else if (m_Function == BluetoothFunction_Enum.GetManufacturer)
                {
                    Array.Copy(BitConverter.GetBytes((ushort)m_InteropInfo), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_Controller), 0, lbData, liOffset, 4); liOffset += 4;
                    lbData = m_Text.AsByteArray(lbData, liOffset);
                }
                else if (m_Function == BluetoothFunction_Enum.GetVersion)
                {
                    Array.Copy(BitConverter.GetBytes(m_ReturnValue), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Controller), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Version.CAPIMajor), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Version.CAPIMinor), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Version.ManufacturerMajor), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Version.ManufacturerMinor), 0, lbData, liOffset, 4); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.GetSerialNumber)
                {
                    Array.Copy(BitConverter.GetBytes(m_ReturnValue), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Controller), 0, lbData, liOffset, 4); liOffset += 4;
                    lbData = m_Text.AsByteArray(lbData, liOffset);
                }
                else if (m_Function == BluetoothFunction_Enum.Manufacturer)
                {
                    lbData = m_Manufacturer.AsByteArray(lbData, liOffset);
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            if (liLen > 0)
            {
                m_Function = (BluetoothFunction_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;

                if (m_Function == BluetoothFunction_Enum.Register)
                {
                    m_ResultInfo = (CapiResults_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_Function == BluetoothFunction_Enum.Release)
                {
                    m_ResultInfo = (CapiResults_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                }
                else if (m_Function == BluetoothFunction_Enum.GetProfile)
                {
                    m_ResultInfo = (CapiResults_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_Text.SetData(lbData, liOffset);
                }
                else if (m_Function == BluetoothFunction_Enum.GetManufacturer)
                {
                    m_InteropInfo = (InteroperabilityInfo_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Controller = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    liOffset = m_Text.SetData(lbData, liOffset);
                }
                else if (m_Function == BluetoothFunction_Enum.GetVersion)
                {
                    m_ReturnValue = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Controller = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;

                    m_Version.CAPIMajor = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Version.CAPIMinor = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Version.ManufacturerMajor = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Version.ManufacturerMinor = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }
                else if (m_Function == BluetoothFunction_Enum.GetSerialNumber)
                {
                    m_ReturnValue = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Controller = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    liOffset = m_Text.SetData(lbData, liOffset);
                }
                else if (m_Function == BluetoothFunction_Enum.Manufacturer)
                {
                    liOffset = m_Manufacturer.SetData(lbData, liOffset);
                }
            }

            return liOffset;
        }
        #endregion
    }
        

    public class CapiStruct_FacilityPartyNumber : CapiStruct_Base
    {
        #region Vars
        protected TypeOfPartyNumber_Enum m_Type1 = TypeOfPartyNumber_Enum.Unknown;
        protected TypeOfPartyNumber_Enum m_Type2 = TypeOfPartyNumber_Enum.Unknown;
        protected TypeOfPartyNumber_Enum m_Type3 = TypeOfPartyNumber_Enum.Unknown;
        protected byte[] m_Numbers = null;
        #endregion

        #region Properties
        public TypeOfPartyNumber_Enum Type1
        {
            get { return m_Type1; }
            set { m_Type1 = value; }
        }
        public TypeOfPartyNumber_Enum Type2
        {
            get { return m_Type2; }
            set { m_Type2 = value; }
        }
        public TypeOfPartyNumber_Enum Type3
        {
            get { return m_Type3; }
            set { m_Type3 = value; }
        }
        public byte[] Numbers
        {
            get { return m_Numbers; }
            set { m_Numbers = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Numbers == null) {
                    return 3;
                } else {
                    return 3 + m_Numbers.Length;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_FacilityPartyNumber(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_FacilityPartyNumber(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_FacilityPartyNumber(object loParent, byte[] lbData, int liOffset)
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
                lbData[liOffset] = (byte)m_Type1; liOffset++;
                lbData[liOffset] = (byte)m_Type2; liOffset++;
                lbData[liOffset] = (byte)m_Type3; liOffset++;

                if (m_Numbers != null)
                {
                    Array.Copy(m_Numbers, 0, lbData, liOffset, m_Numbers.Length); liOffset += m_Numbers.Length;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            m_Numbers = null;
            if (liLen > 0)
            {
                m_Type1 = (TypeOfPartyNumber_Enum)lbData[liOffset]; liOffset++;
                m_Type2 = (TypeOfPartyNumber_Enum)lbData[liOffset]; liOffset++;
                m_Type3 = (TypeOfPartyNumber_Enum)lbData[liOffset]; liOffset++;

                if (liLen > 3)
                {
                    m_Numbers = new byte[liLen - 3];
                    Array.Copy(lbData, liOffset, m_Numbers, 0, liLen - 3); liOffset += liLen - 3;
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_CTRedirectionNumber : CapiStruct_Base
    {
        #region Vars
        protected byte m_Type1 = 0;
        protected byte m_Type2 = 0;
        protected byte[] m_Numbers = null;
        #endregion

        #region Properties
        public byte Type1
        {
            get { return m_Type1; }
            set { m_Type1 = value; }
        }
        public byte Type2
        {
            get { return m_Type2; }
            set { m_Type2 = value; }
        }
        public byte[] Numbers
        {
            get { return m_Numbers; }
            set { m_Numbers = value; }
        }

        public override int DataSize
        {
            get
            {
                if (m_Numbers == null)
                {
                    return 2;
                }
                else
                {
                    return 2 + m_Numbers.Length;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_CTRedirectionNumber(object loParent)
            : base(loParent)
        {
        }
        public CapiStruct_CTRedirectionNumber(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
        }
        public CapiStruct_CTRedirectionNumber(object loParent, byte[] lbData, int liOffset)
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
                lbData[liOffset] = m_Type1; liOffset++;
                lbData[liOffset] = m_Type2; liOffset++;

                if (m_Numbers != null)
                {
                    Array.Copy(m_Numbers, 0, lbData, liOffset, m_Numbers.Length); liOffset += m_Numbers.Length;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            m_Numbers = null;
            if (liLen > 0)
            {
                m_Type1 = lbData[liOffset]; liOffset++;
                m_Type2 = lbData[liOffset]; liOffset++;

                if (liLen > 2)
                {
                    m_Numbers = new byte[liLen - 2];
                    Array.Copy(lbData, liOffset, m_Numbers, 0, liLen - 2); liOffset += liLen - 2;
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_CCBSInterrogateResponse : CapiStruct_Base
    {
        #region Vars
        protected CCBSReference m_Reference = new CCBSReference();
        protected CIPValue_Enum m_CIPValue = CIPValue_Enum.NoPredefinedProfile;
        protected CapiStruct_Data m_BC = null;
        protected CapiStruct_Data m_LLC = null;
        protected CapiStruct_Data m_HLC = null;
        protected CapiStruct_FacilityPartyNumber m_FacilityPartyNumber = null;
        protected CapiStruct_SubAddress m_FacilityPartySubAddress = null;
        protected CapiStruct_SubAddress m_InitiatorPartySubAddress = null;
        #endregion

        #region Properties
        public CCBSReference Reference
        {
            get { return m_Reference; }
            set { m_Reference = value; }
        }
        public CIPValue_Enum CIPValue
        {
            get { return m_CIPValue; }
            set { m_CIPValue = value; }
        }
        public CapiStruct_Data BC
        {
            get { return m_BC; }
            set { m_BC = value; }
        }
        public CapiStruct_Data LLC
        {
            get { return m_LLC; }
            set { m_LLC = value; }
        }
        public CapiStruct_Data HLC
        {
            get { return m_HLC; }
            set { m_HLC = value; }
        }
        public CapiStruct_FacilityPartyNumber FacilityPartyNumber
        {
            get { return m_FacilityPartyNumber; }
            set { m_FacilityPartyNumber = value; }
        }
        public CapiStruct_SubAddress FacilityPartySubAddress
        {
            get { return m_FacilityPartySubAddress; }
            set { m_FacilityPartySubAddress = value; }
        }
        public CapiStruct_SubAddress InitiatorPartySubAddress
        {
            get { return m_InitiatorPartySubAddress; }
            set { m_InitiatorPartySubAddress = value; }
        }

        public override int DataSize
        {
            get
            {
                return 4 + m_BC.StructSize + m_LLC.StructSize + m_HLC.StructSize + m_FacilityPartyNumber.StructSize + m_FacilityPartySubAddress.StructSize + m_InitiatorPartySubAddress.StructSize;
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_CCBSInterrogateResponse(object loParent)
            : base(loParent)
        {
            m_BC = new CapiStruct_Data(this);
            m_LLC = new CapiStruct_Data(this);
            m_HLC = new CapiStruct_Data(this);
            m_FacilityPartyNumber = new CapiStruct_FacilityPartyNumber(this);
            m_FacilityPartySubAddress = new CapiStruct_SubAddress(this);
            m_InitiatorPartySubAddress = new CapiStruct_SubAddress(this);
        }
        public CapiStruct_CCBSInterrogateResponse(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_BC = new CapiStruct_Data(this);
            m_LLC = new CapiStruct_Data(this);
            m_HLC = new CapiStruct_Data(this);
            m_FacilityPartyNumber = new CapiStruct_FacilityPartyNumber(this);
            m_FacilityPartySubAddress = new CapiStruct_SubAddress(this);
            m_InitiatorPartySubAddress = new CapiStruct_SubAddress(this);
        }
        public CapiStruct_CCBSInterrogateResponse(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_BC = new CapiStruct_Data(this);
            m_LLC = new CapiStruct_Data(this);
            m_HLC = new CapiStruct_Data(this);
            m_FacilityPartyNumber = new CapiStruct_FacilityPartyNumber(this);
            m_FacilityPartySubAddress = new CapiStruct_SubAddress(this);
            m_InitiatorPartySubAddress = new CapiStruct_SubAddress(this);
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
                Array.Copy(BitConverter.GetBytes(m_Reference.GetValue()), 0, lbData, liOffset, 2); liOffset += 2;
                Array.Copy(BitConverter.GetBytes((ushort)m_CIPValue), 0, lbData, liOffset, 2); liOffset += 2;
                lbData = m_BC.AsByteArray(lbData, liOffset); liOffset += m_BC.StructSize;
                lbData = m_LLC.AsByteArray(lbData, liOffset); liOffset += m_LLC.StructSize;
                lbData = m_HLC.AsByteArray(lbData, liOffset); liOffset += m_HLC.StructSize;
                lbData = m_FacilityPartyNumber.AsByteArray(lbData, liOffset); liOffset += m_FacilityPartyNumber.StructSize;
                lbData = m_FacilityPartySubAddress.AsByteArray(lbData, liOffset); liOffset += m_FacilityPartySubAddress.StructSize;
                lbData = m_InitiatorPartySubAddress.AsByteArray(lbData, liOffset); liOffset += m_InitiatorPartySubAddress.StructSize;
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            m_Numbers = null;
            if (liLen > 0)
            {
                m_Reference.SetValue(BitConverter.ToUInt16(lbData, liOffset)); liOffset += 2;
                m_CIPValue = (CIPValue_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                liOffset = m_BC.SetData(lbData, liOffset);
                liOffset = m_LLC.SetData(lbData, liOffset);
                liOffset = m_HLC.SetData(lbData, liOffset);
                liOffset = m_FacilityPartyNumber.SetData(lbData, liOffset);
                liOffset = m_FacilityPartySubAddress.SetData(lbData, liOffset);
                liOffset = m_InitiatorPartySubAddress.SetData(lbData, liOffset);
            }

            return liOffset;
        }
        #endregion
    }


    public class CapiStruct_FR_SupplementaryServices : CapiStruct_Base
    {
        #region Vars
        protected SupplementaryServicesFunction_Enum m_Function = SupplementaryServicesFunction_Enum.GetSupportedServices;

        protected CapiStruct_Data m_EmptyStructure = null;
        protected CapiStruct_Listen m_Listen = null; //
        protected CapiStruct_PLCI m_ECT = null; //
        protected CapiStruct_CFActivate m_CFActivate = null;
        protected CapiStruct_CFDeactivate m_CFDeactivate = null;
        protected CapiStruct_DWord m_InterrogateNumbers = null;
        protected CapiStruct_CD m_CD = null;
        protected CapiStruct_CCBSRequest m_CCBSRequest = null;
        protected CapiStruct_CCBSDeactivate m_CCBSDeactivate = null;
        protected CapiStruct_CCBSInterrogate m_CCBSInterrogate = null;
        protected CapiStruct_CCBSCall m_CCBSCall = null;
        protected CapiStruct_MWIActivate m_MWIActivate = null;
        protected CapiStruct_MWIDeactivate m_MWIDeactivate = null;
        protected CapiStruct_DWord m_CONFBegin = null;//
        protected CapiStruct_PLCI m_CONFAdd = null; //
        protected CapiStruct_DWord m_CONFSplit = null;//
        #endregion

        #region Properties
        public SupplementaryServicesFunction_Enum Function
        {
            get { return m_Function; }
            set { m_Function = value; }
        }

        public CapiStruct_Data GetSupportedServices
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_Listen Listen
        {
            get { return m_Listen; }
            set { m_Listen = value; }
        }
        public CapiStruct_Data Hold
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_Data Retrieve
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_Data Suspend
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_Data Resume
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_PLCI ECT
        {
            get { return m_ECT; }
            set { m_ECT = value; }
        }
        public CapiStruct_PLCI ThreePartyBegin
        {
            get { return m_ECT; }
            set { m_ECT = value; }
        }
        public CapiStruct_PLCI ThreePartyEnd
        {
            get { return m_ECT; }
            set { m_ECT = value; }
        }
        public CapiStruct_CFActivate CFActivate
        {
            get { return m_CFActivate; }
            set { m_CFActivate = value; }
        }
        public CapiStruct_CFDeactivate CFDeactivate
        {
            get { return m_CFDeactivate; }
            set { m_CFDeactivate = value; }
        }
        public CapiStruct_CFDeactivate CFInterrogateParameters
        {
            get { return m_CFDeactivate; }
            set { m_CFDeactivate = value; }
        }
        public CapiStruct_DWord CFInterrogateNumbers
        {
            get { return m_InterrogateNumbers; }
            set { m_InterrogateNumbers = value; }
        }
        public CapiStruct_CD CD
        {
            get { return m_CD; }
            set { m_CD = value; }
        }
        public CapiStruct_Data MCID
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_CCBSRequest CCBSRequest
        {
            get { return m_CCBSRequest; }
            set { m_CCBSRequest = value; }
        }
        public CapiStruct_CCBSDeactivate CCBSDeactivate
        {
            get { return m_CCBSDeactivate; }
            set { m_CCBSDeactivate = value; }
        }
        public CapiStruct_CCBSInterrogate CCBSInterrogate
        {
            get { return m_CCBSInterrogate; }
            set { m_CCBSInterrogate = value; }
        }
        public CapiStruct_CCBSCall CCBSCall
        {
            get { return m_CCBSCall; }
            set { m_CCBSCall = value; }
        }
        public CapiStruct_MWIActivate MWIActivate
        {
            get { return m_MWIActivate; }
            set { m_MWIActivate = value; }
        }
        public CapiStruct_MWIDeactivate MWIDeactivate
        {
            get { return m_MWIDeactivate; }
            set { m_MWIDeactivate = value; }
        }
        public CapiStruct_CCBSRequest CCNRRequest
        {
            get { return m_CCBSRequest; }
            set { m_CCBSRequest = value; }
        }
        public CapiStruct_CCBSInterrogate m_CCNRInterrogate
        {
            get { return m_CCBSInterrogate; }
            set { m_CCBSInterrogate = value; }
        }
        public CapiStruct_DWord CONFBegin
        {
            get { return m_CONFBegin; }
            set { m_CONFBegin = value; }
        }
        public CapiStruct_PLCI CONFAdd
        {
            get { return m_CONFAdd; }
            set { m_CONFAdd = value; }
        }
        public CapiStruct_DWord CONFSplit
        {
            get { return m_CONFSplit; }
            set { m_CONFSplit = value; }
        }
        public CapiStruct_DWord CONFDrop
        {
            get { return m_CONFSplit; }
            set { m_CONFSplit = value; }
        }
        public CapiStruct_DWord CONFIsolate
        {
            get { return m_CONFSplit; }
            set { m_CONFSplit = value; }
        }
        public CapiStruct_DWord CONFReattach
        {
            get { return m_CONFSplit; }
            set { m_CONFSplit = value; }
        }

        public override int DataSize
        {
            get
            {
                switch (m_Function)
                {
                    case SupplementaryServicesFunction_Enum.GetSupportedServices:
                    case SupplementaryServicesFunction_Enum.Hold:
                    case SupplementaryServicesFunction_Enum.Retrieve:
                    case SupplementaryServicesFunction_Enum.Suspend:
                    case SupplementaryServicesFunction_Enum.Resume:
                    case SupplementaryServicesFunction_Enum.MaliciousCallIdentification:
                        return 2;

                    case SupplementaryServicesFunction_Enum.Listen:
                        return 6;

                    case SupplementaryServicesFunction_Enum.ExplicitCallTransfer:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceBegin:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceEnd:
                        return 6;

                    case SupplementaryServicesFunction_Enum.CallForwardingActivate:
                        return 2 + m_CFActivate.StructSize;

                    case SupplementaryServicesFunction_Enum.CallForwardingDeactivate:
                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateParameters:
                        return 10 + m_CFDeactivate.StructSize;

                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateNumbers:
                        return 6;

                    case SupplementaryServicesFunction_Enum.CallDeflection:
                        return 4 + m_CD.StructSize;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubRequest:
                    case SupplementaryServicesFunction_Enum.CCNRRequest:
                        return 2 + m_CCBSRequest.StructSize;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubDeactivate:
                        return 2 + m_CCBSDeactivate.StructSize;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubInterrogate:
                    case SupplementaryServicesFunction_Enum.CCNRInterrogate:
                        return 2 + m_CCBSInterrogate.StructSize;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubCall:
                        return 2 + m_CCBSCall.StructSize;

                    case SupplementaryServicesFunction_Enum.MWIActivate:
                        return 2 + m_MWIActivate.StructSize;

                    case SupplementaryServicesFunction_Enum.MWIDeactivate:
                        return 2 + m_MWIDeactivate.StructSize;

                    case SupplementaryServicesFunction_Enum.CONFBegin:
                        return 6;

                    case SupplementaryServicesFunction_Enum.CONFAdd:
                        return 6;

                    case SupplementaryServicesFunction_Enum.CONFSplit:
                    case SupplementaryServicesFunction_Enum.CONFDrop:
                    case SupplementaryServicesFunction_Enum.CONFIsolate:
                    case SupplementaryServicesFunction_Enum.CONFReattach:
                        return 6;

                    default: return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_FR_SupplementaryServices(object loParent)
            : base(loParent)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_CFActivate = new CapiStruct_CFActivate(this);
            m_CFDeactivate = new CapiStruct_CFDeactivate(this);
            m_CD = new CapiStruct_CD(this);
            m_CCBSRequest = new CapiStruct_CCBSRequest(this);
            m_CCBSDeactivate = new CapiStruct_CCBSDeactivate(this);
            m_CCBSInterrogate = new CapiStruct_CCBSInterrogate(this);
            m_CCBSCall = new CapiStruct_CCBSCall(this);
            m_MWIActivate = new CapiStruct_MWIActivate(this);
            m_MWIDeactivate = new CapiStruct_MWIDeactivate(this);
            m_Listen = new CapiStruct_Listen(this);
            m_ECT = new CapiStruct_PLCI(this);
            m_CONFBegin = new CapiStruct_DWord(this);
            m_CONFAdd = new CapiStruct_PLCI(this);
            m_CONFSplit = new CapiStruct_DWord(this);
        }
        public CapiStruct_FR_SupplementaryServices(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_CFActivate = new CapiStruct_CFActivate(this);
            m_CFDeactivate = new CapiStruct_CFDeactivate(this);
            m_CD = new CapiStruct_CD(this);
            m_CCBSRequest = new CapiStruct_CCBSRequest(this);
            m_CCBSDeactivate = new CapiStruct_CCBSDeactivate(this);
            m_CCBSInterrogate = new CapiStruct_CCBSInterrogate(this);
            m_CCBSCall = new CapiStruct_CCBSCall(this);
            m_MWIActivate = new CapiStruct_MWIActivate(this);
            m_MWIDeactivate = new CapiStruct_MWIDeactivate(this);
            m_Listen = new CapiStruct_Listen(this);
            m_ECT = new CapiStruct_PLCI(this);
            m_CONFBegin = new CapiStruct_DWord(this);
            m_CONFAdd = new CapiStruct_PLCI(this);
            m_CONFSplit = new CapiStruct_DWord(this);
        }
        public CapiStruct_FR_SupplementaryServices(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_CFActivate = new CapiStruct_CFActivate(this);
            m_CFDeactivate = new CapiStruct_CFDeactivate(this);
            m_CD = new CapiStruct_CD(this);
            m_CCBSRequest = new CapiStruct_CCBSRequest(this);
            m_CCBSDeactivate = new CapiStruct_CCBSDeactivate(this);
            m_CCBSInterrogate = new CapiStruct_CCBSInterrogate(this);
            m_CCBSCall = new CapiStruct_CCBSCall(this);
            m_MWIActivate = new CapiStruct_MWIActivate(this);
            m_MWIDeactivate = new CapiStruct_MWIDeactivate(this);
            m_Listen = new CapiStruct_Listen(this);
            m_ECT = new CapiStruct_PLCI(this);
            m_CONFBegin = new CapiStruct_DWord(this);
            m_CONFAdd = new CapiStruct_PLCI(this);
            m_CONFSplit = new CapiStruct_DWord(this);
        }
        #endregion

        #region Internal Classes
        public class CapiStruct_Listen : CapiStruct_Base
        {
            #region Vars
            protected NotificationMask_Flags m_Mask = 0;
            #endregion

            #region Properties
            public NotificationMask_Flags Mask
            {
                get { return m_Mask; }
                set { m_Mask = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_Listen(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_Listen(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_Listen(object loParent, byte[] lbData, int liOffset)
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
                    Array.Copy(BitConverter.GetBytes((uint)m_Mask), 0, lbData, liOffset, 4); liOffset += 4;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Mask = (NotificationMask_Enum)BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CFActivate : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected TypeOfCallForwarding_Enum m_CFType = TypeOfCallForwarding_Enum.Unconditional;
            protected ushort m_BasicService = 0;
            protected CapiStruct_FacilityPartyNumber m_ServedUserNumber = null;
            protected CapiStruct_FacilityPartyNumber m_ForwardedToNumber = null;
            protected CapiStruct_SubAddress m_ForwardedToSubaddress = null;
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public TypeOfCallForwarding_Enum CFType
            {
                get { return m_CFType; }
                set { m_CFType = value; }
            }
            public ushort BasicService
            {
                get { return m_BasicService; }
                set { m_BasicService = value; }
            }
            public CapiStruct_FacilityPartyNumber ServedUserNumber
            {
                get { return m_ServedUserNumber; }
                set { m_ServedUserNumber = value; }
            }
            public CapiStruct_FacilityPartyNumber ForwardedToNumber
            {
                get { return m_ForwardedToNumber; }
                set { m_ForwardedToNumber = value; }
            }
            public CapiStruct_SubAddress ForwardedToSubaddress
            {
                get { return m_ForwardedToSubaddress; }
                set { m_ForwardedToSubaddress = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 8 + m_ServedUserNumber.StructSize + m_ForwardedToNumber.StructSize + m_ForwardedToSubaddress.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CFActivate(object loParent)
                : base(loParent)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToSubaddress = new CapiStruct_SubAddress(this);
            }
            public CapiStruct_CFActivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToSubaddress = new CapiStruct_SubAddress(this);
            }
            public CapiStruct_CFActivate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToSubaddress = new CapiStruct_SubAddress(this);
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes((ushort)m_CFType), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_BasicService), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_ServedUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ServedUserNumber.StructSize;
                    lbData = m_ForwardedToNumber.AsByteArray(lbData, liOffset); liOffset += m_ForwardedToNumber.StructSize;
                    lbData = m_ForwardedToSubaddress.AsByteArray(lbData, liOffset); liOffset += m_ForwardedToSubaddress.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_CFType = (TypeOfCallForwarding_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_BasicService = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_ServedUserNumber.SetData(lbData, liOffset);
                    liOffset = m_ForwardedToNumber.SetData(lbData, liOffset);
                    liOffset = m_ForwardedToSubaddress.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CFDeactivate : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected TypeOfCallForwarding_Enum m_CFType = TypeOfCallForwarding_Enum.Unconditional;
            protected ushort m_BasicService = 0;
            protected CapiStruct_FacilityPartyNumber m_ServedUserNumber = null;
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public TypeOfCallForwarding_Enum CFType
            {
                get { return m_CFType; }
                set { m_CFType = value; }
            }
            public ushort BasicService
            {
                get { return m_BasicService; }
                set { m_BasicService = value; }
            }
            public CapiStruct_FacilityPartyNumber ServedUserNumber
            {
                get { return m_ServedUserNumber; }
                set { m_ServedUserNumber = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 8 + m_ServedUserNumber.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CFDeactivate(object loParent)
                : base(loParent)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_CFDeactivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_CFDeactivate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes((ushort)m_CFType), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_BasicService), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_ServedUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ServedUserNumber.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_CFType = (TypeOfCallForwarding_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_BasicService = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_ServedUserNumber.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CD : CapiStruct_Base
        {
            #region Vars
            protected PresentationAllowed_Enum m_Presentation = 0;
            protected CapiStruct_FacilityPartyNumber m_DeflectedToNumber = null;
            protected CapiStruct_SubAddress m_DeflectedToSubaddress = null;
            #endregion

            #region Properties
            public PresentationAllowed_Enum Presentation
            {
                get { return m_Presentation; }
                set { m_Presentation = value; }
            }
            public CapiStruct_FacilityPartyNumber DeflectedToNumber
            {
                get { return m_DeflectedToNumber; }
                set { m_DeflectedToNumber = value; }
            }
            public CapiStruct_SubAddress DeflectedToSubaddress
            {
                get { return m_DeflectedToSubaddress; }
                set { m_DeflectedToSubaddress = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 2 + m_DeflectedToNumber.StructSize + m_DeflectedToSubaddress.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CD(object loParent)
                : base(loParent)
            {
                m_DeflectedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_DeflectedToSubaddress = new CapiStruct_SubAddress(this);
            }
            public CapiStruct_CD(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_DeflectedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_DeflectedToSubaddress = new CapiStruct_SubAddress(this);
            }
            public CapiStruct_CD(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_DeflectedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_DeflectedToSubaddress = new CapiStruct_SubAddress(this);
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
                    Array.Copy(BitConverter.GetBytes((ushort)m_Presentation), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_DeflectedToNumber.AsByteArray(lbData, liOffset); liOffset += m_DeflectedToNumber.StructSize;
                    lbData = m_DeflectedToSubaddress.AsByteArray(lbData, liOffset); liOffset += m_DeflectedToSubaddress.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Presentation = (PresentationAllowed_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_DeflectedToNumber.SetData(lbData, liOffset);
                    liOffset = m_DeflectedToSubaddress.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CCBSRequest : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected CCBSID m_LinkageID = new CCBSID();
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public CCBSID LinkageID
            {
                get { return m_LinkageID; }
                set { m_LinkageID = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 6;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CCBSRequest(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_CCBSRequest(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_CCBSRequest(object loParent, byte[] lbData, int liOffset)
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_LinkageID.GetValue()), 0, lbData, liOffset, 2); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_LinkageID.SetValue(BitConverter.ToUInt16(lbData, liOffset)); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CCBSDeactivate : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected CCBSReference m_Reference = new CCBSReference();
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public CCBSReference Reference
            {
                get { return m_Reference; }
                set { m_Reference = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 6;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CCBSDeactivate(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_CCBSDeactivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_CCBSDeactivate(object loParent, byte[] lbData, int liOffset)
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Reference.GetValue()), 0, lbData, liOffset, 2); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Reference.SetValue(BitConverter.ToUInt16(lbData, liOffset)); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CCBSInterrogate : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected CCBSReference m_Reference = new CCBSReference();
            protected CapiStruct_FacilityPartyNumber m_FacilityPartyNumber = null;
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public CCBSReference Reference
            {
                get { return m_Reference; }
                set { m_Reference = value; }
            }
            public CapiStruct_FacilityPartyNumber FacilityPartyNumber
            {
                get { return m_FacilityPartyNumber; }
                set { m_FacilityPartyNumber = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 6 + m_FacilityPartyNumber.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CCBSInterrogate(object loParent)
                : base(loParent)
            {
                m_FacilityPartyNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_CCBSInterrogate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_FacilityPartyNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_CCBSInterrogate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_FacilityPartyNumber = new CapiStruct_FacilityPartyNumber(this);
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Reference.GetValue()), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_FacilityPartyNumber.AsByteArray(lbData, liOffset); liOffset += m_FacilityPartyNumber.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Reference.SetValue(BitConverter.ToUInt16(lbData, liOffset)); liOffset += 2;
                    liOffset = m_FacilityPartyNumber.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CCBSCall : CapiStruct_Base
        {
            #region Vars
            protected CCBSReference m_Reference = new CCBSReference();
            protected CIPValue_Enum m_CIPValue = CIPValue_Enum.NoPredefinedProfile;
            protected ushort m_Reserved = 0;
            protected CapiStruct_BProtocol m_BProtocol = null;
            protected CapiStruct_Data m_BC = null;
            protected CapiStruct_Data m_LLC = null;
            protected CapiStruct_Data m_HLC = null;
            protected CapiStruct_AdditionalInfo m_AdditionalInfo = null;
            #endregion

            #region Properties
            public CCBSReference Reference
            {
                get { return m_Reference; }
                set { m_Reference = value; }
            }
            public CIPValue_Enum CIPValue
            {
                get { return m_CIPValue; }
                set { m_CIPValue = value; }
            }
            public ushort Reserved
            {
                get { return m_Reserved; }
                set { m_Reserved = value; }
            }
            public CapiStruct_BProtocol BProtocol
            {
                get { return m_BProtocol; }
                set { m_BProtocol = value; }
            }
            public CapiStruct_Data BC
            {
                get { return m_BC; }
                set { m_BC = value; }
            }
            public CapiStruct_Data LLC
            {
                get { return m_LLC; }
                set { m_LLC = value; }
            }
            public CapiStruct_Data HLC
            {
                get { return m_HLC; }
                set { m_HLC = value; }
            }
            public CapiStruct_AdditionalInfo AdditionalInfo
            {
                get { return m_AdditionalInfo; }
                set { m_AdditionalInfo = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 6 + m_BProtocol.StructSize + m_BC.StructSize + m_LLC.StructSize + m_HLC.StructSize + m_AdditionalInfo.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CCBSCall(object loParent)
                : base(loParent)
            {
                m_BProtocol = new CapiStruct_BProtocol(this);
                m_BC = new CapiStruct_Data(this);
                m_LLC = new CapiStruct_Data(this);
                m_HLC = new CapiStruct_Data(this);
                m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
            }
            public CapiStruct_CCBSCall(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_BProtocol = new CapiStruct_BProtocol(this);
                m_BC = new CapiStruct_Data(this);
                m_LLC = new CapiStruct_Data(this);
                m_HLC = new CapiStruct_Data(this);
                m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
            }
            public CapiStruct_CCBSCall(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_BProtocol = new CapiStruct_BProtocol(this);
                m_BC = new CapiStruct_Data(this);
                m_LLC = new CapiStruct_Data(this);
                m_HLC = new CapiStruct_Data(this);
                m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
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
                    Array.Copy(BitConverter.GetBytes(m_Reference.GetValue()), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes((ushort)m_CIPValue), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_Reserved), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_BProtocol.AsByteArray(lbData, liOffset); liOffset += m_BProtocol.StructSize;
                    lbData = m_BC.AsByteArray(lbData, liOffset); liOffset += m_BC.StructSize;
                    lbData = m_LLC.AsByteArray(lbData, liOffset); liOffset += m_LLC.StructSize;
                    lbData = m_HLC.AsByteArray(lbData, liOffset); liOffset += m_HLC.StructSize;
                    lbData = m_AdditionalInfo.AsByteArray(lbData, liOffset); liOffset += m_AdditionalInfo.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Reference.SetValue(BitConverter.ToUInt16(lbData, liOffset)); liOffset += 2;
                    m_CIPValue = (CIPValue_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Reserved = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_BProtocol.SetData(lbData, liOffset);
                    liOffset = m_BC.SetData(lbData, liOffset);
                    liOffset = m_LLC.SetData(lbData, liOffset);
                    liOffset = m_HLC.SetData(lbData, liOffset);
                    liOffset = m_AdditionalInfo.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_MWIActivate : CapiStruct_Base
        {
            #region Vars
            protected ushort m_BasicService = 0;
            protected NumberOfMessages m_NumberOfMessages = new NumberOfMessages();
            protected MessageStatus_Enum m_MessageStatus = MessageStatus_Enum.AddedMessage;
            protected ushort m_MessageReference = 0;
            protected InvocationMode_Enum m_Invocation = InvocationMode_Enum.Deferred;
            protected CapiStruct_FacilityPartyNumber m_ReceivingUserNumber = null;
            protected CapiStruct_FacilityPartyNumber m_ControllingUserNumber = null;
            protected CapiStruct_FacilityPartyNumber m_ControllingUserProvidedNumber = null;
            protected CapiStruct_Data m_TimeData = null;
            #endregion

            #region Properties
            public ushort BasicService
            {
                get { return m_BasicService; }
                set { m_BasicService = value; }
            }
            public NumberOfMessages NumberOfMessages
            {
                get { return m_NumberOfMessages; }
                set { m_NumberOfMessages = value; }
            }
            public MessageStatus_Enum MessageStatus
            {
                get { return m_MessageStatus; }
                set { m_MessageStatus = value; }
            }
            public ushort MessageReference
            {
                get { return m_MessageReference; }
                set { m_MessageReference = value; }
            }
            public InvocationMode_Enum Invocation
            {
                get { return m_Invocation; }
                set { m_Invocation = value; }
            }
            public CapiStruct_FacilityPartyNumber ReceivingUserNumber
            {
                get { return m_ReceivingUserNumber; }
                set { m_ReceivingUserNumber = value; }
            }
            public CapiStruct_FacilityPartyNumber ControllingUserNumber
            {
                get { return m_ControllingUserNumber; }
                set { m_ControllingUserNumber = value; }
            }
            public CapiStruct_FacilityPartyNumber ControllingUserProvidedNumber
            {
                get { return m_ControllingUserProvidedNumber; }
                set { m_ControllingUserProvidedNumber = value; }
            }
            public CapiStruct_Data TimeData
            {
                get { return m_TimeData; }
                set { m_TimeData = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 14 + m_ReceivingUserNumber.StructSize + m_ControllingUserNumber.StructSize + m_ControllingUserProvidedNumber.StructSize + m_TimeData.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_MWIActivate(object loParent)
                : base(loParent)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserProvidedNumber = new CapiStruct_FacilityPartyNumber(this);
                m_TimeData = new CapiStruct_Data(this);
            }
            public CapiStruct_MWIActivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserProvidedNumber = new CapiStruct_FacilityPartyNumber(this);
                m_TimeData = new CapiStruct_Data(this);
            }
            public CapiStruct_MWIActivate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserProvidedNumber = new CapiStruct_FacilityPartyNumber(this);
                m_TimeData = new CapiStruct_Data(this);
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
                    Array.Copy(BitConverter.GetBytes(m_BasicService), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_NumberOfMessages.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes((ushort)m_MessageStatus), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_MessageReference), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes((ushort)m_Invocation), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_ReceivingUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ReceivingUserNumber.StructSize;
                    lbData = m_ControllingUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ControllingUserNumber.StructSize;
                    lbData = m_ControllingUserProvidedNumber.AsByteArray(lbData, liOffset); liOffset += m_ControllingUserProvidedNumber.StructSize;
                    lbData = m_TimeData.AsByteArray(lbData, liOffset); liOffset += m_TimeData.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_BasicService = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_NumberOfMessages.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                    m_MessageStatus = (MessageStatus_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_MessageReference = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Invocation = (InvocationMode_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_ReceivingUserNumber.SetData(lbData, liOffset);
                    liOffset = m_ControllingUserNumber.SetData(lbData, liOffset);
                    liOffset = m_ControllingUserProvidedNumber.SetData(lbData, liOffset);
                    liOffset = m_TimeData.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_MWIDeactivate : CapiStruct_Base
        {
            #region Vars
            protected ushort m_BasicService = 0;
            protected InvocationMode_Enum m_Invocation = InvocationMode_Enum.Deferred;
            protected CapiStruct_FacilityPartyNumber m_ReceivingUserNumber = null;
            protected CapiStruct_FacilityPartyNumber m_ControllingUserNumber = null;
            #endregion

            #region Properties
            public ushort BasicService
            {
                get { return m_BasicService; }
                set { m_BasicService = value; }
            }
            public InvocationMode_Enum Invocation
            {
                get { return m_Invocation; }
                set { m_Invocation = value; }
            }
            public CapiStruct_FacilityPartyNumber ReceivingUserNumber
            {
                get { return m_ReceivingUserNumber; }
                set { m_ReceivingUserNumber = value; }
            }
            public CapiStruct_FacilityPartyNumber ControllingUserNumber
            {
                get { return m_ControllingUserNumber; }
                set { m_ControllingUserNumber = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4 + m_ReceivingUserNumber.StructSize + m_ControllingUserNumber.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_MWIDeactivate(object loParent)
                : base(loParent)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_MWIDeactivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_MWIDeactivate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
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
                    Array.Copy(BitConverter.GetBytes(m_BasicService), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes((ushort)m_Invocation), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_ReceivingUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ReceivingUserNumber.StructSize;
                    lbData = m_ControllingUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ControllingUserNumber.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_BasicService = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Invocation = (InvocationMode_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_ReceivingUserNumber.SetData(lbData, liOffset);
                    liOffset = m_ControllingUserNumber.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
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
                switch (m_Function)
                {
                    case SupplementaryServicesFunction_Enum.GetSupportedServices:
                    case SupplementaryServicesFunction_Enum.Hold:
                    case SupplementaryServicesFunction_Enum.Retrieve:
                    case SupplementaryServicesFunction_Enum.Suspend:
                    case SupplementaryServicesFunction_Enum.Resume:
                    case SupplementaryServicesFunction_Enum.MaliciousCallIdentification:
                        lbData = m_EmptyStructure.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.Listen:
                        lbData = m_Listen.AsByteArray(lbData, liOffset); liOffset += m_Listen.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.ExplicitCallTransfer:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceBegin:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceEnd:
                        Array.Copy(BitConverter.GetBytes(m_ECT_PLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingActivate:
                        lbData = m_CFActivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingDeactivate:
                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateParameters:
                        lbData = m_CFDeactivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateNumbers:
                        Array.Copy(BitConverter.GetBytes(m_CFInterrogateNumbersHandle), 0, lbData, liOffset, 4); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CallDeflection:
                        lbData = m_CD.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubRequest:
                    case SupplementaryServicesFunction_Enum.CCNRRequest:
                        lbData = m_CCBSRequest.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubDeactivate:
                        lbData = m_CCBSDeactivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubInterrogate:
                    case SupplementaryServicesFunction_Enum.CCNRInterrogate:
                        lbData = m_CCBSInterrogate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubCall:
                        lbData = m_CCBSCall.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.MWIActivate:
                        lbData = m_MWIActivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.MWIDeactivate:
                        lbData = m_MWIDeactivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFBegin:
                        Array.Copy(BitConverter.GetBytes(m_CONFBegin_ConferenceSize), 0, lbData, liOffset, 4); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFAdd:
                        Array.Copy(BitConverter.GetBytes(m_CONFAdd_PLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFSplit:
                    case SupplementaryServicesFunction_Enum.CONFDrop:
                    case SupplementaryServicesFunction_Enum.CONFIsolate:
                    case SupplementaryServicesFunction_Enum.CONFReattach:
                        Array.Copy(BitConverter.GetBytes(m_CONFSplit_Identifier), 0, lbData, liOffset, 4); liOffset += 4;
                        break;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            m_Numbers = null;
            if (liLen > 0)
            {
                switch (m_Function)
                {
                    case SupplementaryServicesFunction_Enum.GetSupportedServices:
                    case SupplementaryServicesFunction_Enum.Hold:
                    case SupplementaryServicesFunction_Enum.Retrieve:
                    case SupplementaryServicesFunction_Enum.Suspend:
                    case SupplementaryServicesFunction_Enum.Resume:
                    case SupplementaryServicesFunction_Enum.MaliciousCallIdentification:
                        liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.Listen:
                        m_ListenMask = (NotificationMask_Enum)BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.ExplicitCallTransfer:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceBegin:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceEnd:
                        liOffset = m_ECT_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingActivate:
                        liOffset = m_CFActivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingDeactivate:
                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateParameters:
                        liOffset = m_CFDeactivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateNumbers:
                        m_CFInterrogateNumbersHandle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CallDeflection:
                        liOffset = m_CD.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubRequest:
                    case SupplementaryServicesFunction_Enum.CCNRRequest:
                        liOffset = m_CCBSRequest.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubDeactivate:
                        liOffset = m_CCBSDeactivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubInterrogate:
                    case SupplementaryServicesFunction_Enum.CCNRInterrogate:
                        liOffset = m_CCBSInterrogate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubCall:
                        liOffset = m_CCBSCall.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.MWIActivate:
                        liOffset = m_MWIActivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.MWIDeactivate:
                        liOffset = m_MWIDeactivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CONFBegin:
                        m_CONFBegin_ConferenceSize = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFAdd:
                        m_CONFAdd_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFSplit:
                    case SupplementaryServicesFunction_Enum.CONFDrop:
                    case SupplementaryServicesFunction_Enum.CONFIsolate:
                    case SupplementaryServicesFunction_Enum.CONFReattach:
                        m_CONFSplit_Identifier = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                        break;
                }
            }

            return liOffset;
        }
        #endregion
    }

    public class CapiStruct_FC_SupplementaryServices : CapiStruct_Base
    {
        #region Vars
        protected SupplementaryServicesFunction_Enum m_Function = SupplementaryServicesFunction_Enum.GetSupportedServices;

        protected CapiStruct_Data m_EmptyStructure = null;
        protected NotificationMask_Flags m_ListenMask = 0;
        protected PLCIClass m_ECT_PLCI = new PLCIClass();
        protected CapiStruct_CFActivate m_CFActivate = null;
        protected CapiStruct_CFDeactivate m_CFDeactivate = null;
        protected uint m_CFInterrogateNumbersHandle = 0;
        protected CapiStruct_CD m_CD = null;
        protected CapiStruct_CCBSRequest m_CCBSRequest = null;
        protected CapiStruct_CCBSDeactivate m_CCBSDeactivate = null;
        protected CapiStruct_CCBSInterrogate m_CCBSInterrogate = null;
        protected CapiStruct_CCBSCall m_CCBSCall = null;
        protected CapiStruct_MWIActivate m_MWIActivate = null;
        protected CapiStruct_MWIDeactivate m_MWIDeactivate = null;
        protected uint m_CONFBegin_ConferenceSize = 0;
        protected PLCIClass m_CONFAdd_PLCI = new PLCIClass();
        protected uint m_CONFSplit_Identifier = 0;
        #endregion

        #region Properties
        public CapiStruct_Data GetSupportedServices
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public NotificationMask_Flags ListenMask
        {
            get { return m_ListenMask; }
            set { m_ListenMask = value; }
        }
        public CapiStruct_Data Hold
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_Data Retrieve
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_Data Suspend
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_Data Resume
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public PLCIClass ECT
        {
            get { return m_ECT_PLCI; }
            set { m_ECT_PLCI = value; }
        }
        public PLCIClass ThreePartyBegin
        {
            get { return m_ECT_PLCI; }
            set { m_ECT_PLCI = value; }
        }
        public PLCIClass ThreePartyEnd
        {
            get { return m_ECT_PLCI; }
            set { m_ECT_PLCI = value; }
        }
        public CapiStruct_CFActivate CFActivate
        {
            get { return m_CFActivate; }
            set { m_CFActivate = value; }
        }
        public CapiStruct_CFDeactivate CFDeactivate
        {
            get { return m_CFDeactivate; }
            set { m_CFDeactivate = value; }
        }
        public CapiStruct_CFDeactivate CFInterrogateParameters
        {
            get { return m_CFDeactivate; }
            set { m_CFDeactivate = value; }
        }
        public uint CFInterrogateNumbersHandle
        {
            get { return m_CFInterrogateNumbersHandle; }
            set { m_CFInterrogateNumbersHandle = value; }
        }
        public CapiStruct_CD CD
        {
            get { return m_CD; }
            set { m_CD = value; }
        }
        public CapiStruct_Data MCID
        {
            get { return m_EmptyStructure; }
            set { m_EmptyStructure = value; }
        }
        public CapiStruct_CCBSRequest CCBSRequest
        {
            get { return m_CCBSRequest; }
            set { m_CCBSRequest = value; }
        }
        public CapiStruct_CCBSDeactivate CCBSDeactivate
        {
            get { return m_CCBSDeactivate; }
            set { m_CCBSDeactivate = value; }
        }
        public CapiStruct_CCBSInterrogate CCBSInterrogate
        {
            get { return m_CCBSInterrogate; }
            set { m_CCBSInterrogate = value; }
        }
        public CapiStruct_CCBSCall CCBSCall
        {
            get { return m_CCBSCall; }
            set { m_CCBSCall = value; }
        }
        public CapiStruct_MWIActivate MWIActivate
        {
            get { return m_MWIActivate; }
            set { m_MWIActivate = value; }
        }
        public CapiStruct_MWIDeactivate MWIDeactivate
        {
            get { return m_MWIDeactivate; }
            set { m_MWIDeactivate = value; }
        }
        public CapiStruct_CCBSRequest CCNRRequest
        {
            get { return m_CCBSRequest; }
            set { m_CCBSRequest = value; }
        }
        public CapiStruct_CCBSInterrogate m_CCNRInterrogate
        {
            get { return m_CCBSInterrogate; }
            set { m_CCBSInterrogate = value; }
        }
        public uint CONFBegin_ConferenceSize
        {
            get { return m_CONFBegin_ConferenceSize; }
            set { m_CONFBegin_ConferenceSize = value; }
        }
        public PLCIClass CONFAdd_PLCI
        {
            get { return m_CONFAdd_PLCI; }
            set { m_CONFAdd_PLCI = value; }
        }
        public uint CONFSplit_Identifier
        {
            get { return m_CONFSplit_Identifier; }
            set { m_CONFSplit_Identifier = value; }
        }
        public uint CONFDrop_Identifier
        {
            get { return m_CONFSplit_Identifier; }
            set { m_CONFSplit_Identifier = value; }
        }
        public uint CONFIsolate_Identifier
        {
            get { return m_CONFSplit_Identifier; }
            set { m_CONFSplit_Identifier = value; }
        }
        public uint CONFReattach_Identifier
        {
            get { return m_CONFSplit_Identifier; }
            set { m_CONFSplit_Identifier = value; }
        }

        public override int DataSize
        {
            get
            {
                switch (m_Function)
                {
                    case SupplementaryServicesFunction_Enum.GetSupportedServices:
                    case SupplementaryServicesFunction_Enum.Hold:
                    case SupplementaryServicesFunction_Enum.Retrieve:
                    case SupplementaryServicesFunction_Enum.Suspend:
                    case SupplementaryServicesFunction_Enum.Resume:
                    case SupplementaryServicesFunction_Enum.MaliciousCallIdentification:
                        return 2;
                        break;

                    case SupplementaryServicesFunction_Enum.Listen:
                        return 6;
                        break;

                    case SupplementaryServicesFunction_Enum.ExplicitCallTransfer:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceBegin:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceEnd:
                        return 6;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingActivate:
                        return 2 + m_CFActivate.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingDeactivate:
                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateParameters:
                        return 10 + m_CFDeactivate.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateNumbers:
                        return 6;
                        break;

                    case SupplementaryServicesFunction_Enum.CallDeflection:
                        return 4 + m_CD.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubRequest:
                    case SupplementaryServicesFunction_Enum.CCNRRequest:
                        return 2 + m_CCBSRequest.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubDeactivate:
                        return 2 + m_CCBSDeactivate.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubInterrogate:
                    case SupplementaryServicesFunction_Enum.CCNRInterrogate:
                        return 2 + m_CCBSInterrogate.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubCall:
                        return 2 + m_CCBSCall.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.MWIActivate:
                        return 2 + m_MWIActivate.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.MWIDeactivate:
                        return 2 + m_MWIDeactivate.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFBegin:
                        return 6;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFAdd:
                        return 6;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFSplit:
                    case SupplementaryServicesFunction_Enum.CONFDrop:
                    case SupplementaryServicesFunction_Enum.CONFIsolate:
                    case SupplementaryServicesFunction_Enum.CONFReattach:
                        return 6;
                        break;

                    default: return 0;
                }
            }
        }
        #endregion

        #region Constructor
        public CapiStruct_FC_SupplementaryServices(object loParent)
            : base(loParent)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_CFActivate = new CapiStruct_CFActivate(this);
            m_CFDeactivate = new CapiStruct_CFDeactivate(this);
            m_CD = new CapiStruct_CD(this);
            m_CCBSRequest = new CapiStruct_CCBSRequest(this);
            m_CCBSDeactivate = new CapiStruct_CCBSDeactivate(this);
            m_CCBSInterrogate = new CapiStruct_CCBSInterrogate(this);
            m_CCBSCall = new CapiStruct_CCBSCall(this);
            m_MWIActivate = new CapiStruct_MWIActivate(this);
            m_MWIDeactivate = new CapiStruct_MWIDeactivate(this);
        }
        public CapiStruct_FC_SupplementaryServices(object loParent, byte[] lbData)
            : base(loParent, lbData)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_CFActivate = new CapiStruct_CFActivate(this);
            m_CFDeactivate = new CapiStruct_CFDeactivate(this);
            m_CD = new CapiStruct_CD(this);
            m_CCBSRequest = new CapiStruct_CCBSRequest(this);
            m_CCBSDeactivate = new CapiStruct_CCBSDeactivate(this);
            m_CCBSInterrogate = new CapiStruct_CCBSInterrogate(this);
            m_CCBSCall = new CapiStruct_CCBSCall(this);
            m_MWIActivate = new CapiStruct_MWIActivate(this);
            m_MWIDeactivate = new CapiStruct_MWIDeactivate(this);
        }
        public CapiStruct_FC_SupplementaryServices(object loParent, byte[] lbData, int liOffset)
            : base(loParent, lbData, liOffset)
        {
            m_EmptyStructure = new CapiStruct_Data(this);
            m_CFActivate = new CapiStruct_CFActivate(this);
            m_CFDeactivate = new CapiStruct_CFDeactivate(this);
            m_CD = new CapiStruct_CD(this);
            m_CCBSRequest = new CapiStruct_CCBSRequest(this);
            m_CCBSDeactivate = new CapiStruct_CCBSDeactivate(this);
            m_CCBSInterrogate = new CapiStruct_CCBSInterrogate(this);
            m_CCBSCall = new CapiStruct_CCBSCall(this);
            m_MWIActivate = new CapiStruct_MWIActivate(this);
            m_MWIDeactivate = new CapiStruct_MWIDeactivate(this);
        }
        #endregion

        #region Internal Classes
        public class CapiStruct_CFActivate : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected TypeOfCallForwarding_Enum m_CFType = TypeOfCallForwarding_Enum.Unconditional;
            protected ushort m_BasicService = 0;
            protected CapiStruct_FacilityPartyNumber m_ServedUserNumber = null;
            protected CapiStruct_FacilityPartyNumber m_ForwardedToNumber = null;
            protected CapiStruct_SubAddress m_ForwardedToSubaddress = null;
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public TypeOfCallForwarding_Enum CFType
            {
                get { return m_CFType; }
                set { m_CFType = value; }
            }
            public ushort BasicService
            {
                get { return m_BasicService; }
                set { m_BasicService = value; }
            }
            public CapiStruct_FacilityPartyNumber ServedUserNumber
            {
                get { return m_ServedUserNumber; }
                set { m_ServedUserNumber = value; }
            }
            public CapiStruct_FacilityPartyNumber ForwardedToNumber
            {
                get { return m_ForwardedToNumber; }
                set { m_ForwardedToNumber = value; }
            }
            public CapiStruct_SubAddress ForwardedToSubaddress
            {
                get { return m_ForwardedToSubaddress; }
                set { m_ForwardedToSubaddress = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 8 + m_ServedUserNumber.StructSize + m_ForwardedToNumber.StructSize + m_ForwardedToSubaddress.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CFActivate(object loParent)
                : base(loParent)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToSubaddress = new CapiStruct_SubAddress(this);
            }
            public CapiStruct_CFActivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToSubaddress = new CapiStruct_SubAddress(this);
            }
            public CapiStruct_CFActivate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ForwardedToSubaddress = new CapiStruct_SubAddress(this);
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes((ushort)m_CFType), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_BasicService), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_ServedUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ServedUserNumber.StructSize;
                    lbData = m_ForwardedToNumber.AsByteArray(lbData, liOffset); liOffset += m_ForwardedToNumber.StructSize;
                    lbData = m_ForwardedToSubaddress.AsByteArray(lbData, liOffset); liOffset += m_ForwardedToSubaddress.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_CFType = (TypeOfCallForwarding_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_BasicService = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_ServedUserNumber.SetData(lbData, liOffset);
                    liOffset = m_ForwardedToNumber.SetData(lbData, liOffset);
                    liOffset = m_ForwardedToSubaddress.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CFDeactivate : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected TypeOfCallForwarding_Enum m_CFType = TypeOfCallForwarding_Enum.Unconditional;
            protected ushort m_BasicService = 0;
            protected CapiStruct_FacilityPartyNumber m_ServedUserNumber = null;
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public TypeOfCallForwarding_Enum CFType
            {
                get { return m_CFType; }
                set { m_CFType = value; }
            }
            public ushort BasicService
            {
                get { return m_BasicService; }
                set { m_BasicService = value; }
            }
            public CapiStruct_FacilityPartyNumber ServedUserNumber
            {
                get { return m_ServedUserNumber; }
                set { m_ServedUserNumber = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 8 + m_ServedUserNumber.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CFDeactivate(object loParent)
                : base(loParent)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_CFDeactivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_CFDeactivate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_ServedUserNumber = new CapiStruct_FacilityPartyNumber(this);
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes((ushort)m_CFType), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_BasicService), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_ServedUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ServedUserNumber.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_CFType = (TypeOfCallForwarding_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_BasicService = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_ServedUserNumber.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CD : CapiStruct_Base
        {
            #region Vars
            protected PresentationAllowed_Enum m_Presentation = 0;
            protected CapiStruct_FacilityPartyNumber m_DeflectedToNumber = null;
            protected CapiStruct_SubAddress m_DeflectedToSubaddress = null;
            #endregion

            #region Properties
            public PresentationAllowed_Enum Presentation
            {
                get { return m_Presentation; }
                set { m_Presentation = value; }
            }
            public CapiStruct_FacilityPartyNumber DeflectedToNumber
            {
                get { return m_DeflectedToNumber; }
                set { m_DeflectedToNumber = value; }
            }
            public CapiStruct_SubAddress DeflectedToSubaddress
            {
                get { return m_DeflectedToSubaddress; }
                set { m_DeflectedToSubaddress = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 2 + m_DeflectedToNumber.StructSize + m_DeflectedToSubaddress.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CD(object loParent)
                : base(loParent)
            {
                m_DeflectedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_DeflectedToSubaddress = new CapiStruct_SubAddress(this);
            }
            public CapiStruct_CD(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_DeflectedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_DeflectedToSubaddress = new CapiStruct_SubAddress(this);
            }
            public CapiStruct_CD(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_DeflectedToNumber = new CapiStruct_FacilityPartyNumber(this);
                m_DeflectedToSubaddress = new CapiStruct_SubAddress(this);
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
                    Array.Copy(BitConverter.GetBytes((ushort)m_Presentation), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_DeflectedToNumber.AsByteArray(lbData, liOffset); liOffset += m_DeflectedToNumber.StructSize;
                    lbData = m_DeflectedToSubaddress.AsByteArray(lbData, liOffset); liOffset += m_DeflectedToSubaddress.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Presentation = (PresentationAllowed_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_DeflectedToNumber.SetData(lbData, liOffset);
                    liOffset = m_DeflectedToSubaddress.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CCBSRequest : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected CCBSID m_LinkageID = new CCBSID();
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public CCBSID LinkageID
            {
                get { return m_LinkageID; }
                set { m_LinkageID = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 6;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CCBSRequest(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_CCBSRequest(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_CCBSRequest(object loParent, byte[] lbData, int liOffset)
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_LinkageID.GetValue()), 0, lbData, liOffset, 2); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_LinkageID.SetValue(BitConverter.ToUInt16(lbData, liOffset)); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CCBSDeactivate : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected CCBSReference m_Reference = new CCBSReference();
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public CCBSReference Reference
            {
                get { return m_Reference; }
                set { m_Reference = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 6;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CCBSDeactivate(object loParent)
                : base(loParent)
            {
            }
            public CapiStruct_CCBSDeactivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
            }
            public CapiStruct_CCBSDeactivate(object loParent, byte[] lbData, int liOffset)
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Reference.GetValue()), 0, lbData, liOffset, 2); liOffset += 2;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Reference.SetValue(BitConverter.ToUInt16(lbData, liOffset)); liOffset += 2;
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CCBSInterrogate : CapiStruct_Base
        {
            #region Vars
            protected uint m_Handle = 0;
            protected CCBSReference m_Reference = new CCBSReference();
            protected CapiStruct_FacilityPartyNumber m_FacilityPartyNumber = null;
            #endregion

            #region Properties
            public uint Handle
            {
                get { return m_Handle; }
                set { m_Handle = value; }
            }
            public CCBSReference Reference
            {
                get { return m_Reference; }
                set { m_Reference = value; }
            }
            public CapiStruct_FacilityPartyNumber FacilityPartyNumber
            {
                get { return m_FacilityPartyNumber; }
                set { m_FacilityPartyNumber = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 6 + m_FacilityPartyNumber.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CCBSInterrogate(object loParent)
                : base(loParent)
            {
                m_FacilityPartyNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_CCBSInterrogate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_FacilityPartyNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_CCBSInterrogate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_FacilityPartyNumber = new CapiStruct_FacilityPartyNumber(this);
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
                    Array.Copy(BitConverter.GetBytes(m_Handle), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes(m_Reference.GetValue()), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_FacilityPartyNumber.AsByteArray(lbData, liOffset); liOffset += m_FacilityPartyNumber.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Handle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                    m_Reference.SetValue(BitConverter.ToUInt16(lbData, liOffset)); liOffset += 2;
                    liOffset = m_FacilityPartyNumber.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_CCBSCall : CapiStruct_Base
        {
            #region Vars
            protected CCBSReference m_Reference = new CCBSReference();
            protected CIPValue_Enum m_CIPValue = CIPValue_Enum.NoPredefinedProfile;
            protected ushort m_Reserved = 0;
            protected CapiStruct_BProtocol m_BProtocol = null;
            protected CapiStruct_Data m_BC = null;
            protected CapiStruct_Data m_LLC = null;
            protected CapiStruct_Data m_HLC = null;
            protected CapiStruct_AdditionalInfo m_AdditionalInfo = null;
            #endregion

            #region Properties
            public CCBSReference Reference
            {
                get { return m_Reference; }
                set { m_Reference = value; }
            }
            public CIPValue_Enum CIPValue
            {
                get { return m_CIPValue; }
                set { m_CIPValue = value; }
            }
            public ushort Reserved
            {
                get { return m_Reserved; }
                set { m_Reserved = value; }
            }
            public CapiStruct_BProtocol BProtocol
            {
                get { return m_BProtocol; }
                set { m_BProtocol = value; }
            }
            public CapiStruct_Data BC
            {
                get { return m_BC; }
                set { m_BC = value; }
            }
            public CapiStruct_Data LLC
            {
                get { return m_LLC; }
                set { m_LLC = value; }
            }
            public CapiStruct_Data HLC
            {
                get { return m_HLC; }
                set { m_HLC = value; }
            }
            public CapiStruct_AdditionalInfo AdditionalInfo
            {
                get { return m_AdditionalInfo; }
                set { m_AdditionalInfo = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 6 + m_BProtocol.StructSize + m_BC.StructSize + m_LLC.StructSize + m_HLC.StructSize + m_AdditionalInfo.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_CCBSCall(object loParent)
                : base(loParent)
            {
                m_BProtocol = new CapiStruct_BProtocol(this);
                m_BC = new CapiStruct_Data(this);
                m_LLC = new CapiStruct_Data(this);
                m_HLC = new CapiStruct_Data(this);
                m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
            }
            public CapiStruct_CCBSCall(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_BProtocol = new CapiStruct_BProtocol(this);
                m_BC = new CapiStruct_Data(this);
                m_LLC = new CapiStruct_Data(this);
                m_HLC = new CapiStruct_Data(this);
                m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
            }
            public CapiStruct_CCBSCall(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_BProtocol = new CapiStruct_BProtocol(this);
                m_BC = new CapiStruct_Data(this);
                m_LLC = new CapiStruct_Data(this);
                m_HLC = new CapiStruct_Data(this);
                m_AdditionalInfo = new CapiStruct_AdditionalInfo(this);
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
                    Array.Copy(BitConverter.GetBytes(m_Reference.GetValue()), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes((ushort)m_CIPValue), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_Reserved), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_BProtocol.AsByteArray(lbData, liOffset); liOffset += m_BProtocol.StructSize;
                    lbData = m_BC.AsByteArray(lbData, liOffset); liOffset += m_BC.StructSize;
                    lbData = m_LLC.AsByteArray(lbData, liOffset); liOffset += m_LLC.StructSize;
                    lbData = m_HLC.AsByteArray(lbData, liOffset); liOffset += m_HLC.StructSize;
                    lbData = m_AdditionalInfo.AsByteArray(lbData, liOffset); liOffset += m_AdditionalInfo.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_Reference.SetValue(BitConverter.ToUInt16(lbData, liOffset)); liOffset += 2;
                    m_CIPValue = (CIPValue_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Reserved = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_BProtocol.SetData(lbData, liOffset);
                    liOffset = m_BC.SetData(lbData, liOffset);
                    liOffset = m_LLC.SetData(lbData, liOffset);
                    liOffset = m_HLC.SetData(lbData, liOffset);
                    liOffset = m_AdditionalInfo.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_MWIActivate : CapiStruct_Base
        {
            #region Vars
            protected ushort m_BasicService = 0;
            protected NumberOfMessages m_NumberOfMessages = new NumberOfMessages();
            protected MessageStatus_Enum m_MessageStatus = MessageStatus_Enum.AddedMessage;
            protected ushort m_MessageReference = 0;
            protected InvocationMode_Enum m_Invocation = InvocationMode_Enum.Deferred;
            protected CapiStruct_FacilityPartyNumber m_ReceivingUserNumber = null;
            protected CapiStruct_FacilityPartyNumber m_ControllingUserNumber = null;
            protected CapiStruct_FacilityPartyNumber m_ControllingUserProvidedNumber = null;
            protected CapiStruct_Data m_TimeData = null;
            #endregion

            #region Properties
            public ushort BasicService
            {
                get { return m_BasicService; }
                set { m_BasicService = value; }
            }
            public NumberOfMessages NumberOfMessages
            {
                get { return m_NumberOfMessages; }
                set { m_NumberOfMessages = value; }
            }
            public MessageStatus_Enum MessageStatus
            {
                get { return m_MessageStatus; }
                set { m_MessageStatus = value; }
            }
            public ushort MessageReference
            {
                get { return m_MessageReference; }
                set { m_MessageReference = value; }
            }
            public InvocationMode_Enum Invocation
            {
                get { return m_Invocation; }
                set { m_Invocation = value; }
            }
            public CapiStruct_FacilityPartyNumber ReceivingUserNumber
            {
                get { return m_ReceivingUserNumber; }
                set { m_ReceivingUserNumber = value; }
            }
            public CapiStruct_FacilityPartyNumber ControllingUserNumber
            {
                get { return m_ControllingUserNumber; }
                set { m_ControllingUserNumber = value; }
            }
            public CapiStruct_FacilityPartyNumber ControllingUserProvidedNumber
            {
                get { return m_ControllingUserProvidedNumber; }
                set { m_ControllingUserProvidedNumber = value; }
            }
            public CapiStruct_Data TimeData
            {
                get { return m_TimeData; }
                set { m_TimeData = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 14 + m_ReceivingUserNumber.StructSize + m_ControllingUserNumber.StructSize + m_ControllingUserProvidedNumber.StructSize + m_TimeData.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_MWIActivate(object loParent)
                : base(loParent)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserProvidedNumber = new CapiStruct_FacilityPartyNumber(this);
                m_TimeData = new CapiStruct_Data(this);
            }
            public CapiStruct_MWIActivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserProvidedNumber = new CapiStruct_FacilityPartyNumber(this);
                m_TimeData = new CapiStruct_Data(this);
            }
            public CapiStruct_MWIActivate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserProvidedNumber = new CapiStruct_FacilityPartyNumber(this);
                m_TimeData = new CapiStruct_Data(this);
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
                    Array.Copy(BitConverter.GetBytes(m_BasicService), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_NumberOfMessages.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                    Array.Copy(BitConverter.GetBytes((ushort)m_MessageStatus), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes(m_MessageReference), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes((ushort)m_Invocation), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_ReceivingUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ReceivingUserNumber.StructSize;
                    lbData = m_ControllingUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ControllingUserNumber.StructSize;
                    lbData = m_ControllingUserProvidedNumber.AsByteArray(lbData, liOffset); liOffset += m_ControllingUserProvidedNumber.StructSize;
                    lbData = m_TimeData.AsByteArray(lbData, liOffset); liOffset += m_TimeData.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_BasicService = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_NumberOfMessages.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                    m_MessageStatus = (MessageStatus_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_MessageReference = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Invocation = (InvocationMode_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_ReceivingUserNumber.SetData(lbData, liOffset);
                    liOffset = m_ControllingUserNumber.SetData(lbData, liOffset);
                    liOffset = m_ControllingUserProvidedNumber.SetData(lbData, liOffset);
                    liOffset = m_TimeData.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
        }
        public class CapiStruct_MWIDeactivate : CapiStruct_Base
        {
            #region Vars
            protected ushort m_BasicService = 0;
            protected InvocationMode_Enum m_Invocation = InvocationMode_Enum.Deferred;
            protected CapiStruct_FacilityPartyNumber m_ReceivingUserNumber = null;
            protected CapiStruct_FacilityPartyNumber m_ControllingUserNumber = null;
            #endregion

            #region Properties
            public ushort BasicService
            {
                get { return m_BasicService; }
                set { m_BasicService = value; }
            }
            public InvocationMode_Enum Invocation
            {
                get { return m_Invocation; }
                set { m_Invocation = value; }
            }
            public CapiStruct_FacilityPartyNumber ReceivingUserNumber
            {
                get { return m_ReceivingUserNumber; }
                set { m_ReceivingUserNumber = value; }
            }
            public CapiStruct_FacilityPartyNumber ControllingUserNumber
            {
                get { return m_ControllingUserNumber; }
                set { m_ControllingUserNumber = value; }
            }

            public override int DataSize
            {
                get
                {
                    return 4 + m_ReceivingUserNumber.StructSize + m_ControllingUserNumber.StructSize;
                }
            }
            #endregion

            #region Constructor
            public CapiStruct_MWIDeactivate(object loParent)
                : base(loParent)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_MWIDeactivate(object loParent, byte[] lbData)
                : base(loParent, lbData)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
            }
            public CapiStruct_MWIDeactivate(object loParent, byte[] lbData, int liOffset)
                : base(loParent, lbData, liOffset)
            {
                m_ReceivingUserNumber = new CapiStruct_FacilityPartyNumber(this);
                m_ControllingUserNumber = new CapiStruct_FacilityPartyNumber(this);
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
                    Array.Copy(BitConverter.GetBytes(m_BasicService), 0, lbData, liOffset, 2); liOffset += 2;
                    Array.Copy(BitConverter.GetBytes((ushort)m_Invocation), 0, lbData, liOffset, 2); liOffset += 2;
                    lbData = m_ReceivingUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ReceivingUserNumber.StructSize;
                    lbData = m_ControllingUserNumber.AsByteArray(lbData, liOffset); liOffset += m_ControllingUserNumber.StructSize;
                }

                return lbData;
            }
            public override int SetData(byte[] lbData, int liOffset)
            {
                int liLen = ReadStructHeader(lbData, ref liOffset);

                m_Numbers = null;
                if (liLen > 0)
                {
                    m_BasicService = BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    m_Invocation = (InvocationMode_Enum)BitConverter.ToUInt16(lbData, liOffset); liOffset += 2;
                    liOffset = m_ReceivingUserNumber.SetData(lbData, liOffset);
                    liOffset = m_ControllingUserNumber.SetData(lbData, liOffset);
                }

                return liOffset;
            }
            #endregion
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
                switch (m_Function)
                {
                    case SupplementaryServicesFunction_Enum.GetSupportedServices:
                    case SupplementaryServicesFunction_Enum.Hold:
                    case SupplementaryServicesFunction_Enum.Retrieve:
                    case SupplementaryServicesFunction_Enum.Suspend:
                    case SupplementaryServicesFunction_Enum.Resume:
                    case SupplementaryServicesFunction_Enum.MaliciousCallIdentification:
                        lbData = m_EmptyStructure.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.Listen:
                        Array.Copy(BitConverter.GetBytes((uint)m_ListenMask), 0, lbData, liOffset, 4); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.ExplicitCallTransfer:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceBegin:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceEnd:
                        Array.Copy(BitConverter.GetBytes(m_ECT_PLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingActivate:
                        lbData = m_CFActivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingDeactivate:
                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateParameters:
                        lbData = m_CFDeactivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateNumbers:
                        Array.Copy(BitConverter.GetBytes(m_CFInterrogateNumbersHandle), 0, lbData, liOffset, 4); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CallDeflection:
                        lbData = m_CD.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubRequest:
                    case SupplementaryServicesFunction_Enum.CCNRRequest:
                        lbData = m_CCBSRequest.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubDeactivate:
                        lbData = m_CCBSDeactivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubInterrogate:
                    case SupplementaryServicesFunction_Enum.CCNRInterrogate:
                        lbData = m_CCBSInterrogate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubCall:
                        lbData = m_CCBSCall.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.MWIActivate:
                        lbData = m_MWIActivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.MWIDeactivate:
                        lbData = m_MWIDeactivate.AsByteArray(lbData, liOffset); liOffset += m_EmptyStructure.StructSize;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFBegin:
                        Array.Copy(BitConverter.GetBytes(m_CONFBegin_ConferenceSize), 0, lbData, liOffset, 4); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFAdd:
                        Array.Copy(BitConverter.GetBytes(m_CONFAdd_PLCI.GetValue()), 0, lbData, liOffset, 4); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFSplit:
                    case SupplementaryServicesFunction_Enum.CONFDrop:
                    case SupplementaryServicesFunction_Enum.CONFIsolate:
                    case SupplementaryServicesFunction_Enum.CONFReattach:
                        Array.Copy(BitConverter.GetBytes(m_CONFSplit_Identifier), 0, lbData, liOffset, 4); liOffset += 4;
                        break;
                }
            }

            return lbData;
        }
        public override int SetData(byte[] lbData, int liOffset)
        {
            int liLen = ReadStructHeader(lbData, ref liOffset);

            m_Numbers = null;
            if (liLen > 0)
            {
                switch (m_Function)
                {
                    case SupplementaryServicesFunction_Enum.GetSupportedServices:
                    case SupplementaryServicesFunction_Enum.Hold:
                    case SupplementaryServicesFunction_Enum.Retrieve:
                    case SupplementaryServicesFunction_Enum.Suspend:
                    case SupplementaryServicesFunction_Enum.Resume:
                    case SupplementaryServicesFunction_Enum.MaliciousCallIdentification:
                        liOffset = m_EmptyStructure.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.Listen:
                        m_ListenMask = (NotificationMask_Enum)BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.ExplicitCallTransfer:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceBegin:
                    case SupplementaryServicesFunction_Enum.ThreePartyConferenceEnd:
                        liOffset = m_ECT_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingActivate:
                        liOffset = m_CFActivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingDeactivate:
                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateParameters:
                        liOffset = m_CFDeactivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CallForwardingInterrogateNumbers:
                        m_CFInterrogateNumbersHandle = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CallDeflection:
                        liOffset = m_CD.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubRequest:
                    case SupplementaryServicesFunction_Enum.CCNRRequest:
                        liOffset = m_CCBSRequest.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubDeactivate:
                        liOffset = m_CCBSDeactivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubInterrogate:
                    case SupplementaryServicesFunction_Enum.CCNRInterrogate:
                        liOffset = m_CCBSInterrogate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CompletitionOfCallToBusySubCall:
                        liOffset = m_CCBSCall.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.MWIActivate:
                        liOffset = m_MWIActivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.MWIDeactivate:
                        liOffset = m_MWIDeactivate.SetData(lbData, liOffset);
                        break;

                    case SupplementaryServicesFunction_Enum.CONFBegin:
                        m_CONFBegin_ConferenceSize = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFAdd:
                        m_CONFAdd_PLCI.SetValue(BitConverter.ToUInt32(lbData, liOffset)); liOffset += 4;
                        break;

                    case SupplementaryServicesFunction_Enum.CONFSplit:
                    case SupplementaryServicesFunction_Enum.CONFDrop:
                    case SupplementaryServicesFunction_Enum.CONFIsolate:
                    case SupplementaryServicesFunction_Enum.CONFReattach:
                        m_CONFSplit_Identifier = BitConverter.ToUInt32(lbData, liOffset); liOffset += 4;
                        break;
                }
            }

            return liOffset;
        }
        #endregion
    }
    //CIPValue_Enum
    //SupplementaryServicesFunction_Enum
}
