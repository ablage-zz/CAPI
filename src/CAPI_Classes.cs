#region Using
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
#endregion

namespace DSELib.CAPI
{
    /// <summary>
    /// CAPI-Versions-Klasse
    /// </summary>
    public class CapiVersion
    {
        protected uint m_CAPIMajor = 0;
        protected uint m_CAPIMinor = 0;
        protected uint m_ManufacturerMajor = 0;
        protected uint m_ManufacturerMinor = 0;

        public CapiVersion(uint liCAPIMajor, uint liCAPIMinor, uint liManufacturerMajor, uint liManufacturerMinor)
        {
            m_CAPIMajor = liCAPIMajor;
            m_CAPIMinor = liCAPIMinor;
            m_ManufacturerMajor = liManufacturerMajor;
            m_ManufacturerMinor = liManufacturerMinor;
        }

        public uint CAPIMajor { get { return m_CAPIMajor; } set { m_CAPIMajor = value; } }
        public uint CAPIMinor { get { return m_CAPIMinor; } set { m_CAPIMinor = value; } }
        public uint ManufacturerMajor { get { return m_ManufacturerMajor; } set { m_ManufacturerMajor = value; } }
        public uint ManufacturerMinor { get { return m_ManufacturerMinor; } set { m_ManufacturerMinor = value; } }
    }

    public class NCCIClass : PLCIClass
    {
        protected ushort m_NCCI = 0;

        public ushort NCCI
        {
            get { return m_NCCI; }
            set { m_NCCI = value; }
        }

        public override void SetValue(uint liData)
        {
            m_ControllerType = (ControllerType_Enum)(liData & 64);
            m_Controller = (byte)(liData & 63);
            m_PLCI = (byte)((liData >> 8) & 0xff);
            m_NCCI = (byte)((liData >> (8 + 16)) & 0xffff);
        }
        public override uint GetValue()
        {
            return (uint)m_ControllerType + (uint)m_Controller + (uint)(m_PLCI << 8) + (uint)(m_NCCI << (8 + 16));
        }
    }
    public class PLCIClass : ControllerClass
    {
        protected byte m_PLCI = 0;

        public byte PLCI
        {
            get { return m_PLCI; }
            set { m_PLCI = value; }
        }

        public override void SetValue(uint liData)
        {
            m_ControllerType = (ControllerType_Enum)(liData & 64);
            m_Controller = (byte)(liData & 63);
            m_PLCI = (byte)((liData >> 8) & 0xff);
        }
        public override uint GetValue()
        {
            return (uint)m_ControllerType + (uint)m_Controller + (uint)(m_PLCI << 8);
        }
    }
    public class ControllerClass
    {
        protected ControllerType_Enum m_ControllerType = ControllerType_Enum.InternalController;
        protected byte m_Controller = 1;

        public ControllerType_Enum ControllerType
        {
            get { return m_ControllerType; }
            set { m_ControllerType = value; }
        }
        public byte Controller
        {
            get { return m_Controller; }
            set
            {
                if (value == 0)
                {
                    throw new Exception("Controller 0 is reserved from CAPI.");
                }
                else if (value > 63)
                {
                    throw new Exception("Controller higher than 63 are not allowed from CAPI.");
                }
                else
                {
                    m_Controller = value;
                }
            }
        }
        public virtual void SetValue(uint liData)
        {
            m_ControllerType = (ControllerType_Enum)(liData & 64);
            m_Controller = (byte)(liData & 63);
        }
        public virtual uint GetValue()
        {
            return (uint)m_ControllerType + (uint)m_Controller;
        }
    }

    public class CCBSID
    {
        protected ushort m_ID = 0;

        public ushort ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public virtual void SetValue(ushort liData)
        {
            m_ID = (ushort)(liData & 0x7F);
        }
        public virtual ushort GetValue()
        {
            return (ushort)(m_ID & 0x7F);
        }
    }
    public class CCBSReference
    {
        protected ushort m_ID = 0;

        public ushort ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        public bool AllReferences
        {
            get { return ((m_ID & 0xFF) == 0xFF); }
            set
            {
                if (value)
                {
                    m_ID = 0xFF;
                }
                else
                {
                    m_ID &= 0x7F;
                }
            }
        }

        public virtual void SetValue(ushort liData)
        {
            m_ID = (ushort)(liData & 0xFF);
        }
        public virtual ushort GetValue()
        {
            return (ushort)(m_ID & 0xFF);
        }
    }
    public class CCBSStatusReport
    {
        protected LineStatus_Enum m_Status = LineStatus_Enum.Busy;

        public LineStatus_Enum Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public virtual void SetValue(ushort liData)
        {
            if (liData == 0)
            {
                m_Status = LineStatus_Enum.Busy;
            }
            else
            {
                m_Status = LineStatus_Enum.Free;
            }
        }
        public virtual ushort GetValue()
        {
            return (ushort)m_Status;
        }
    }
    public class NumberOfMessages
    {
        protected uint m_Number = 0;

        public uint Number
        {
            get { return m_Number; }
            set { m_Number = value; }
        }
        public bool Suppress
        {
            get { return ((m_Number & 0xFFFFFFFF) == 0xFFFFFFFF); }
            set
            {
                if (value)
                {
                    m_Number = 0xFFFFFFFF;
                }
                else
                {
                    m_Number &= 0xFFFF0000;
                }
            }
        }

        public virtual void SetValue(uint liData)
        {
            m_Number = liData;
        }
        public virtual uint GetValue()
        {
            return m_Number;
        }
    }
}
