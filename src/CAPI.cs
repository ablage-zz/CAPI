#region Using
using System;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using DSELib.Language;
#endregion

namespace DSELib.CAPI
{
    public delegate void CapiReceiveHandler(object sender, ManagedCapi.ReceiveArgs e);

    /// <summary>
    /// CAPI-Wrapper-Klasse
    /// </summary>
    public class ManagedCapi : IDisposable
    {
        #region Vars
        /// <summary>
        /// Die ID nach Anmeldung der Anwendung beim CAPI
        /// </summary>
        private ushort m_AppID = 0;
        /// <summary>
        /// Gibt an, ob eine CAPI-Verbindung besteht
        /// </summary>
        private bool m_Connected = false;
        /// <summary>
        /// Nummer für die nächste Capi-Nachricht
        /// </summary>
        private ushort m_MessageNumber = UInt16.MinValue;
        /// <summary>
        /// Thread, welcher kontinuierlich die CAPI auf neue Nachrichten für die
        /// Anwendung überprüft
        /// </summary>
        private Thread m_MessageThread = null;

        /// <summary>
        /// Maximale mögliche logische Verbindungen
        /// </summary>
        private int m_MaxLogicalConnections = 2;
        /// <summary>
        /// Maximale mögliche simultane Datenblöcke, die von der Anwendung verarbeitet werden kann
        /// </summary>
        private int m_MaxBDataBlocks = 7;
        /// <summary>
        /// Maximale Datenblock-Größe
        /// </summary>
        private int m_MaxBDataLen = 0x800;
        #endregion

        #region CAPI-API-Win32
        /// <summary>
        /// Abfrage von Herstellerangaben
        /// </summary>
        /// <param name="buffer"></param>
        [DllImport("capi2032.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern void CAPI_GET_MANUFACTURER(StringBuilder buffer);
        /// <summary>
        /// Der Applikation wird eine Nachricht aus dem CAPI Empfangspuffer zugestellt.
        /// Nachrichten, die von der ISDN-PC-Karte empfangen wurden, werden vom 
        /// ISDN-Handler in den Empfangspuffer der Anwendung eingereiht.
        /// </summary>
        /// <param name="appID">CAPI-Application-ID</param>
        /// <param name="message"></param>
        /// <returns></returns>
        [DllImport("capi2032.dll", ExactSpelling = true)]
        internal static extern int CAPI_GET_MESSAGE(int appID, [Out] out IntPtr message);
        /// <summary>
        /// Ermittelt Daten über die instalierten Adapter.
        /// </summary>
        /// <param name="Buffer">Daten-Buffer, indem geschrieben werden soll.</param>
        /// <param name="CtrlNr">Bit-Maske für abzufragenden Adapter</param>
        /// <returns></returns>
        [DllImport("capi2032.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern int CAPI_GET_PROFILE([Out] out IntPtr Buffer, int CtrlNr);
        /// <summary>
        /// Abfrage der Seriennummer der ISDN-Karte
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        [DllImport("capi2032.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern int CAPI_GET_SERIAL_NUMBER(StringBuilder buffer);
        /// <summary>
        /// Abfrage der Version des CAPI-Treibers
        /// </summary>
        /// <param name="capiMajor"></param>
        /// <param name="capiMinor"></param>
        /// <param name="manufacturerMajor"></param>
        /// <param name="manufacturerMinor"></param>
        /// <returns></returns>
        [DllImport("capi2032.dll", ExactSpelling = true)]
        internal static extern int CAPI_GET_VERSION([Out] out uint capiMajor, [Out] out uint capiMinor, [Out] out uint manufacturerMajor, [Out] out uint manufacturerMinor);
        /// <summary>
        /// Abfrage, ob CAPI installiert/verfügbar ist
        /// </summary>
        /// <returns></returns>
        [DllImport("capi2032.dll", ExactSpelling = true)]
        internal static extern int CAPI_INSTALLED();
        /// <summary>
        /// Es wird eine Nachricht an das CAPI geschickt. Das CAPI fügt diese 
        /// Nachricht in den Ausgangspuffer ein. Nachrichten aus dem Ausgangspuffer 
        /// werden ständig vom ISDNHandler an die ISDN-PC-Karte übergeben und 
        /// befinden sich nur kurzfristig im Puffer.
        /// </summary>
        /// <param name="appID">CAPI-Application-ID</param>
        /// <param name="message"></param>
        /// <returns></returns>
        [DllImport("capi2032.dll", ExactSpelling = true)]
        internal static extern int CAPI_PUT_MESSAGE(int appID, IntPtr message);
        /// <summary>
        /// Anmeldung der eigenen Anwendung beim CAPI-Treiber für die weitere Kommunikation
        /// </summary>
        /// <param name="messageBufferSize"></param>
        /// <param name="maxLogicalConnection"></param>
        /// <param name="maxBDataBlocks"></param>
        /// <param name="maxBDataLen"></param>
        /// <param name="appID">CAPI-Application-ID</param>
        /// <returns></returns>
        [DllImport("capi2032.dll", ExactSpelling = true)]
        internal static extern int CAPI_REGISTER(int messageBufferSize, int maxLogicalConnection, int maxBDataBlocks, int maxBDataLen, [Out] out int appID);
        /// <summary>
        /// Abmeldung der Anwendung vom CAPI-Treiber
        /// </summary>
        /// <param name="appID">CAPI-Application-ID</param>
        /// <returns></returns>
        [DllImport("capi2032.dll", ExactSpelling = true)]
        internal static extern int CAPI_RELEASE(int appID);
        /// <summary>
        /// Abfrage des CAPI-Treibers auf neue Nachrichten
        /// </summary>
        /// <param name="appID">CAPI-Application-ID</param>
        /// <returns></returns>
        [DllImport("capi2032.dll", ExactSpelling = true)]
        internal static extern int CAPI_WAIT_FOR_SIGNAL(int appID);
        /// <summary>
        /// Call-Back-Funktion einrichten
        /// </summary>
        /// <param name="appID">CAPI-Application-ID</param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [DllImport("capi2032.dll", ExactSpelling = true)]
        internal static extern int CAPI_SET_SIGNAL(int appID, IntPtr callback, int param);
        #endregion

        #region Events
        public event CapiReceiveHandler OnReceive = null;
        protected void onReceive(CapiInMessage loInMessage, CapiOutMessage loOutMessage)
        {
            if (OnReceive != null) { OnReceive(this, new ReceiveArgs(loInMessage, loOutMessage)); }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gibt an, ob CAPI verfügbar ist (true) oder nicht (false)
        /// </summary>
        public bool isInstalled
        {
            get
            {
                return (CAPI_INSTALLED() == 0);
            }
        }

        /// <summary>
        /// Angaben zum Hersteller abfragen
        /// </summary>
        public string Manufacturer
        {
            get
            {
                StringBuilder loBuilder = new StringBuilder(64);
                CAPI_GET_MANUFACTURER(loBuilder);
                return loBuilder.ToString().Trim();
            }
        }

        /// <summary>
        /// Angaben zur CAPI-Version abfragen
        /// </summary>
        public CapiVersion Version
        {
            get
            {
                uint liCAPIMajor = 0;
                uint liCAPIMinor = 0;
                uint liManufacturerMajor = 0;
                uint liManufacturerMinor = 0;

                CAPI_GET_VERSION(out liCAPIMajor, out liCAPIMinor, out liManufacturerMajor, out liManufacturerMinor);

                CapiVersion loVersion = new CapiVersion(liCAPIMajor, liCAPIMinor, liManufacturerMajor, liManufacturerMinor);
                return loVersion;
            }
        }

        /// <summary>
        /// Abfragen der Seriennummer des ISDN-Controllers
        /// </summary>
        public string SerialNumber
        {
            get
            {
                StringBuilder loBuilder = new StringBuilder(8);
                CAPI_GET_SERIAL_NUMBER(loBuilder);
                return loBuilder.ToString().Trim();
            }
        }

        /// <summary>
        /// Gibt die maximale mögliche logische Verbindungen an, oder legt diese fest.
        /// </summary>
        public int MaxLogicalConnections { get { return m_MaxLogicalConnections; } set { m_MaxLogicalConnections = value; } }
        /// <summary>
        /// Gibt die maximale mögliche simultane Datenblöcke, die von der Anwendung verarbeitet werden kann an, oder legt diese fest.
        /// </summary>
        public int MaxBDataBlocks { get { return m_MaxBDataBlocks; } set { m_MaxBDataBlocks = value; } }
        /// <summary>
        /// Gibt die maximale Datenblock-Größe an, oder legt diese fest.
        /// </summary>
        public int MaxBDataLen { get { return m_MaxBDataLen; } set { m_MaxBDataLen = value; } }
        #endregion

        #region SendMessage
        /// <summary>
        /// Eine Capi-Nachricht an den Capi-Treiber senden
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(CapiOutMessage loMessage)
        {
            // Ist Verbunden?
            if (!this.m_Connected)
            {
                throw new Exception("CAPI is not connected.");
            }

            loMessage.Header.AppID = m_AppID;
            loMessage.Header.MessageNumber = (byte)m_MessageNumber; // Nächste Sequenznummer zuweisen

            m_MessageNumber++; // Sequenznummer hochzählen
            if (m_MessageNumber >= UInt16.MaxValue) m_MessageNumber = UInt16.MinValue; // Capi-Message-Nummern haben nur 16 Bit

            byte[] lbBuffer = loMessage.AsByteArray(); // Header als Byte-Array zurückgeben
            
            // Speicherbereich reservieren
            IntPtr loPtr = Marshal.AllocHGlobal(lbBuffer.Length);
            Marshal.Copy(lbBuffer, 0, loPtr, lbBuffer.Length);
            
            // Nachricht senden
            int liResult = CAPI_PUT_MESSAGE(this.m_AppID, loPtr);
            
            Marshal.FreeHGlobal(loPtr); // Speicher freigeben
        }
        #endregion

        #region Property Active
        public bool Active
        {
            get
            {
                return m_Connected;
            }
            set
            {
                if (value)
                {
                    if (Active)
                    {
                        Active = false;
                    }

                    int lsAppID = 0;

                    int liResult = CAPI_REGISTER(1024 + (1024 * m_MaxLogicalConnections), m_MaxLogicalConnections, m_MaxBDataBlocks, m_MaxBDataLen, out lsAppID);
                    if (liResult != 0)
                    {
                        this.m_Connected = false;
                        return ;
                    }
                    
                    m_Connected = true;
                    m_AppID = (ushort)lsAppID;
                    m_MessageThread = new Thread(new ThreadStart(MessageInThread));
                    m_MessageThread.IsBackground = true;
                    m_MessageThread.Start();
                }
                else
                {
                    if (Active)
                    {
                        int liResult = CAPI_RELEASE(this.m_AppID);
                        if (liResult != 0) return ;
                        this.m_Connected = false;
                    }
                }
            }
        }
        #endregion

        #region Constructor / Deconstructor
        // Wrapper-Methoden
        /// <summary>
        /// Standard-Konstruktor
        /// </summary>
        public ManagedCapi()
        {
            // Ist die CAPI-Schnittstelle installiert?
            if (!isInstalled)
            {
                throw new Exception("CAPI not found.");
            }
        }
        /// <summary>
        /// Destruktor
        /// </summary>
        public void Dispose()
        {
            if (Active)
            {
                Active = false;
            }
        }
        #endregion

        #region Message-In-Thread
        /// <summary>
        /// Thread-In-Routine
        /// </summary>
        private void MessageInThread()
        {
            CapiInMessage loInMessage = null;
            CapiOutMessage loOutMessage = null;

            while (Active)
            {
                CAPI_WAIT_FOR_SIGNAL((int)this.m_AppID);

                // Neue Nachricht auslesen
                IntPtr loPtr = IntPtr.Zero;
                int liResult = CAPI_GET_MESSAGE(this.m_AppID, out loPtr);

                int liSize = (int)Marshal.ReadByte(loPtr);
                byte[] lbMsg = new byte[liSize];

                Marshal.Copy(loPtr, lbMsg, 0, liSize);
                CapiMessageHeader loHeader = new CapiMessageHeader(lbMsg, 0);

                if (loHeader.SubCommand == CapiMessages_SubCommands_Enum.CONF)
                {
                    switch (loHeader.Command)
                    {
                        // Messages concerning the signaling protocol
                        case CapiMessages_Commands_Enum.CONNECT: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new CONNECT_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.DISCONNECT: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new DISCONNECT_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.ALERT: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new ALERT_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.INFO: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new INFO_CONF(lbMsg, 0);
                            }
                            break;


                        // Messages concerning logical connections
                        case CapiMessages_Commands_Enum.CONNECT_B3: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new CONNECT_B3_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.DISCONNECT_B3: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new DISCONNECT_B3_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.DATA_B3: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new DATA_B3_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.RESET_B3: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new RESET_B3_CONF(lbMsg, 0);
                            }
                            break;


                        // Administrative and other messages
                        case CapiMessages_Commands_Enum.LISTEN: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new LISTEN_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.FACILITY: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new FACILITY_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.SELECT_B_PROTOCOL: // local confirmation of the request
                            {
                                loInMessage = (CapiInMessage)new SELECT_B_PROTOCOL_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.MANUFACTURER: // manufacturer-specific operation
                            {
                                loInMessage = (CapiInMessage)new MANUFACTURER_CONF(lbMsg, 0);
                            }
                            break;

                        case CapiMessages_Commands_Enum.INTEROPERABILITY: // interoperability operation
                            {
                                loInMessage = (CapiInMessage)new INTEROPERABILITY_CONF(lbMsg, 0);
                            }
                            break;


                        default:
                            throw new Exception("Unknown message received as confimation.");
                    }

                    loInMessage.Header = loHeader;

                    onReceive(loInMessage, null);
                }


                // Messages needed to be replied
                else if (loHeader.SubCommand == CapiMessages_SubCommands_Enum.IND)
                {
                    switch (loHeader.Command)
                    {
                        // Messages concerning the signaling protocol
                        case CapiMessages_Commands_Enum.CONNECT: // indicates an incoming physical connection
                            {
                                loInMessage = (CapiInMessage)new CONNECT_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new CONNECT_RESP();
                                //// Preparing
                            }
                            break;

                        case CapiMessages_Commands_Enum.CONNECT_ACTIVE: // indicates the activation of a physical connection
                            {
                                loInMessage = (CapiInMessage)new CONNECT_ACTIVE_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new CONNECT_ACTIVE_RESP();
                                //// Preparing
                            }
                            break;

                        case CapiMessages_Commands_Enum.DISCONNECT: // indicates the clearing of a physical connection
                            {
                                loInMessage = (CapiInMessage)new DISCONNECT_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new DISCONNECT_RESP();
                                //// Preparing
                            }
                            break;

                        case CapiMessages_Commands_Enum.INFO: // indicates specified signaling information
                            {
                                loInMessage = (CapiInMessage)new INFO_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new INFO_RESP();
                                //// Preparing
                            }
                            break;


                        // Messages concerning logical connections
                        case CapiMessages_Commands_Enum.CONNECT_B3: // indicates an incoming logical connection
                            {
                                loInMessage = (CapiInMessage)new CONNECT_B3_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new CONNECT_B3_RESP();
                                //// Preparing
                            }
                            break;

                        case CapiMessages_Commands_Enum.CONNECT_B3_ACTIVE: // indicates the activation of a logical connection
                            {
                                loInMessage = (CapiInMessage)new CONNECT_B3_ACTIVE_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new CONNECT_B3_ACTIVE_RESP();
                                //// Preparing
                            }
                            break;

                        case CapiMessages_Commands_Enum.CONNECT_B3_T90_ACTIVE: // indicates switching from T.70NL to T.90NL
                            {
                                loInMessage = (CapiInMessage)new CONNECT_B3_T90_ACTIVE_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new CONNECT_B3_T90_ACTIVE_RESP();
                                //// Preparing
                            }
                            break;

                        case CapiMessages_Commands_Enum.DISCONNECT_B3: // indicates the clearing down of a logical connection
                            {
                                loInMessage = (CapiInMessage)new DISCONNECT_B3_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new DISCONNECT_B3_RESP();
                                //// Preparing
                            }
                            break;

                        case CapiMessages_Commands_Enum.DATA_B3: // indicates incoming data over a logical connection
                            {
                                loInMessage = (CapiInMessage)new DATA_B3_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new DATA_B3_RESP();
                                //// Preparing
                            }
                            break;

                        case CapiMessages_Commands_Enum.RESET_B3: // indicates the resetting of a logical connection
                            {
                                loInMessage = (CapiInMessage)new RESET_B3_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new RESET_B3_RESP();
                                //// Preparing
                            }
                            break;


                        // Administrative and other messages
                        case CapiMessages_Commands_Enum.FACILITY: // indicates additional facilities (e.g. ext. equipment)
                            {
                                loInMessage = (CapiInMessage)new FACILITY_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new FACILITY_RESP();

                                ((FACILITY_RESP)loOutMessage).Controller_PLCI_NCCI = ((FACILITY_IND)loInMessage).Controller_PLCI_NCCI;
                                ((FACILITY_RESP)loOutMessage).FacilitySelector = ((FACILITY_IND)loInMessage).FacilitySelector;
                                ////
                                //// Preparing
                            }
                            break;

                        case CapiMessages_Commands_Enum.MANUFACTURER: // manufacturer-specific operation
                            {
                                loInMessage = (CapiInMessage)new MANUFACTURER_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new MANUFACTURER_RESP();

                                ((MANUFACTURER_RESP)loOutMessage).Controller = ((MANUFACTURER_IND)loInMessage).Controller;
                                ((MANUFACTURER_RESP)loOutMessage).ManuID = ((MANUFACTURER_IND)loInMessage).ManuID;
                                //// Manufacturer dependend data; may need to extend
                            }
                            break;

                        case CapiMessages_Commands_Enum.INTEROPERABILITY: // interoperability operation
                            {
                                loInMessage = (CapiInMessage)new INTEROPERABILITY_IND(lbMsg, 0);
                                loOutMessage = (CapiOutMessage)new INTEROPERABILITY_RESP();

                                //// Preparing
                            }
                            break;


                        default:
                            throw new Exception("Unknown message received as indication.");
                    }

                    loOutMessage.Header.AppID = loHeader.AppID;
                    loOutMessage.Header.MessageNumber = loHeader.MessageNumber;

                    onReceive(loInMessage, loOutMessage);
                }
                else
                {
                    throw new Exception("Unknown message received.");
                }
            }
        }
        public class ReceiveArgs : EventArgs
        {
            #region Vars
            protected CapiInMessage m_InMessage = null;
            protected CapiOutMessage m_OutMessage = null;
            #endregion  

            #region Properties
            public CapiInMessage InMessage 
            {
                get { return m_InMessage; }
                set { m_InMessage = value; }
            }
            public CapiOutMessage OutMessage 
            {
                get { return m_OutMessage; }
                set { m_OutMessage = value; }
            }
            #endregion

            #region Constructors
            public ReceiveArgs(CapiInMessage loInMessage) 
            {
                m_InMessage = loInMessage;
                m_OutMessage = null;
            }
            public ReceiveArgs(CapiInMessage loInMessage, CapiOutMessage loOutMessage) 
            {
                m_InMessage = loInMessage;
                m_OutMessage = loOutMessage;
            }
            #endregion
        }
        #endregion

        #region Capabilities
        public CapiProfile Capabilities() 
        {
            return Capabilities(1);
        }
        public CapiProfile Capabilities(int liController)
        {
            byte[] lbData = new byte[64];
            IntPtr loPtr = IntPtr.Zero;
            int liResult = CAPI_GET_PROFILE(out loPtr, liController);
            Marshal.PtrToStructure(loPtr, lbData);
            return new CapiProfile(lbData, 0);
        }
        #endregion
    }
}