#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace DSELib.CAPI
{
    #region Capi-Results
    /// <summary>
    /// CAPI-Fehlercodes
    /// 30xx, 31xx, 32xx: Software-Problem bei Ihnen selbst, z.B. inkompatibler CAPI-Treiber oder Software-Fehler in der benutzten Anwendung.
    /// 33xx: Gewöhnlich lokales Problem bei Ihnen selbst: Der ISDN-Bus ist nicht korrekt angeschlossen, ein anderes Endgerät blockiert den Bus, falsches Protokoll eingestellt (Analyse ggf. mit D-Kanal-Trace).
    /// 34xx: Problem meist beim angerufenen Teilnehmer, oder auf einer der beiden Seiten stimmt die abgehende oder gerufene MSN nicht.
    /// </summary>
    public enum CapiResults_Enum : uint
    {
        // CAPI-2.0-Applikations-Fehlercodes
        NoError = 0x0000, // Kein Fehler
        NcpiIgnored = 0x0001, // NCPI ignored
        FlagsIgnored = 0x0002, // Flags ignored
        AlertAlreadySend = 0x0003, // Alert already sent

        // Fehler bei Capi_Register
        TooManyApps = 0x1001, // Too many applications
        SmallBlockSize = 0x1002, // Logical block size too small
        BufferExceeds64k = 0x1003, // Buffer exceeds 64k
        SmallBufferSize = 0x1004, // Message buffer size too small
        TooManyConns = 0x1005, // Too many logical connections
        Reserved1 = 0x1006, // Reserved 1
        MsgCouldNotAccepted = 0x1007, // Message could not be accepted
        RegOsResErr = 0x1008, // Register OS Resource error
        CapiNotInstalled_Register = 0x1009, // CAPI not installed
        ExtEqNotSupported = 0x100A, // External equipment not supported
        ExtEqOnly = 0x100B, // External equipment only

        // Fehler bei Capi_PutMessage und Capi_GetMessage
        BadAppID = 0x1101, // Bad application ID
        IllegalCommand = 0x1102, // Illegal command or message length
        MsgQueueFull = 0x1103, // Message queue full
        MsgQueueEmpty = 0x1104, // Message queue empty
        MsgLost = 0x1105, // Message lost
        UnknownNotification = 0x1106, // Unknown notification
        MsgNotAccepted = 0x1107, // Message not accepted
        OsResErr = 0x1108, // OS resource error
        CapiNotInstalled = 0x1109, // CAPI not installed
        ExtEqMsgNotSupported = 0x110A, // External equipment not supported
        ExtEqMsgOnly = 0x110B, // External equipment only

        // Fehler bei ressourcen- oder programmierfehler
        IllegalContextMessage = 0x2001, // Message im aktuellen Zustand nicht erlaubt
        IllegalCtrlPlciNcci = 0x2002, // Falscher Controller oder PLCI/NCCI falsch
        NoPlciAvailable = 0x2003, // Kein PLCI frei
        NoNcciAvailable = 0x2004, // Kein NCCI frei
        NoListenAvailable = 0x2005, // Kein LISTEN frei
        NoFaxResolutionAvail = 0x2006, // Fax-Resourcen nicht verfügbar
        IllegalCapiMessageCode = 0x2007, // Falsche CAPI-Message-Codierung
        NoResourceAvailable = 0x2008, // Keine Resourcen zur Kanalzusammenschaltung vorhanden

        // CAPI-2.0-Protokoll- und Hardware-Fehler
        B1ProtNotSupported = 0x3001, // B1-Protokoll nicht unterstützt
        B2ProtNotSupported = 0x3002, // B2-Protokoll nicht unterstützt
        B3ProtNotSupported = 0x3003, // B3-Protokoll nicht unterstützt
        B1ProtParmNotSupported = 0x3004, // B1-Protokollparameter nicht unterstützt
        B2ProtParmNotSupported = 0x3005, // B2-Protokollparameter nicht unterstützt
        B3ProtParmNotSupported = 0x3006, // B3-Protokollparameter nicht unterstützt
        BProtCombNotSupported = 0x3007, // B-Protokollkombination nicht unterstützt
        NcpiNotSupported = 0x3008, // NCPI nicht unterstützt
        CipValueUnknown = 0x3009, // CIP-Wert unbekannt (falsche Dienstekennung)
        FlagsNotSupported = 0x300A, // Flags nicht unterstützt (reservierte Bits)
        FacilityNotSupported = 0x300B, // Facility-Wert nicht unterstützt
        DataLenNotYetSupported = 0x300C, // Datenlänge vom derzeitigen Protokoll nicht unterstützt
        ResetNotSupported = 0x300D, // Reset-Prozedur wird vom Protokoll nicht unterstützt
        SuppServiceNotSupported = 0x300E, // Supplementary Service nicht unterstützt
        UnSuppInteroperability = 0x300F, // Unsupported interoperability
        RequestNotAllowed = 0x3010, // Anforderung im aktuellen Zustand nicht erlaubt
        FacilFuncNotSupported = 0x3011, // Facility-spezifische Funktion wird nicht unterstützt

        
        Layer1ProtocolError = 0x3301, // Protokollfehler Schicht 1 (oft Kabel- oder Hardwareproblem)
        Layer2ProtocolError = 0x3302, // Protokollfehler Schicht 2 (z.B. 1TR6 statt DSS1)
        Layer3ProtocolError = 0x3303, // Protokollfehler Schicht 3 bzw. Timeout
        AnotherAppFetchCall = 0x3304, // Eine andere Applikation erhielt diesen Ruf
        SuppServDenied = 0x3305, // Supplementary Service zurückgewiesen

        // Fax-Fehlercodes bei faxfähigen CAPI-Treibern (T.30-Standard)
        T30_ConnectionNotSuccessful_RemoteStationNoG3FaxDevice = 0x3311, // Fax-Aushandlung erfolglos bzw. Gegenstelle ist kein Faxgerät
        T30_ConnectionNotSuccessful_TrainingError = 0x3312, // Training (Geschwindigkeits-Abgleich) erfolglos
        T30_DisconnectBeforeTransfer = 0x3313, // Gegenstelle unterstützt Verbindungsparameter nicht, z.B. Auflösung
        T30_DisconnectDuringTransfer_RemoteAbort = 0x3314, // Verbindungsabbruch durch Gegenstelle während der Übertragung
        T30_DisconnectDuringTransfer_RemoteProcedureError = 0x3315, // Abbruch durch Prozedurfehler, z.B. erfolglose Wiederholung
        T30_DisconnectDuringTransfer_LocalTxDataUnderflow = 0x3316, // Abbruch durch sendeseitigen Datenmangel (tx data underflow)
        T30_DisconnectDuringTransfer_LocalRxDataOverflow = 0x3317, // Abbruch durch Empfangs-Datenüberlauf (rx data overflow)
        T30_DisconnectDuringTransfer_LocalAbort = 0x3318, // Verbindungsabbruch durch lokales Auflegen
        T30_IllegalParameterCoding = 0x3319, // Ungültige Daten, z.B. Fehler in der SFF-/TIF-Datei

        // CAPI-2.0-Codes von der Vermittlung oder Tk-Anlage
        ConnCleared = 0x3480, // Normales Verbindungsende
        UnallocatedNumber = 0x3481, // Zielrufnummer oder eigene MSN falsch
        NoTransNetRouting = 0x3482, // Kein Routing zum angegebenen Transit-Netz
        NoDestRouting = 0x3483, // Kein Routing zum Ziel
        ChannelUnacceptable = 0x3486, // Kanal nicht annehmbar
        CurChannelCallIdent = 0x3487, // Ruf im aktiven Kanal erkannt
        NormalCallClearing = 0x3490, // Normales Verbindungsende oder Anwahl-Abbruch
        UserBusy = 0x3491, // Endgerät ist besetzt
        NoUserResponding = 0x3492, // Kein Endgerät antwortet (falsche/keine MSN beim Ziel eingestellt?)
        NoUserAnswer = 0x3493, // Kein Endgerät nimmt den Ruf an
        CallRejected = 0x3495, // Anruf abgelehnt
        CallNumberChanged = 0x3496, // Die Rufnummer hat sich geändert
        NonSelUserClearing = 0x349A, // Ruf wurde von anderem Endgerät entgegengenommen
        DestOutOfOrder = 0x349B, // Ziel derzeit nicht erreichbar
        InvalidNumberFormat = 0x349C, // Ungültiges Rufnummernformat bzw. eigene MSN falsch
        FacilityRejected = 0x349D, // Facility-Wert abgewiesen
        StatusResponse = 0x349E, // Antwort auf Statusabfrage
        ConnClearedUnSpec = 0x349F, // Normales Verbindungsende, unspezifiziert
        NoChannelAvailable = 0x34A2, // Kein Kanal frei
        NetworkOutOfOrder = 0x34A6, // Vermittlung nicht betriebsbereit
        TemporaryFailure = 0x34A9, // Vorübergehender Engpass im Netz
        SwitchingError = 0x34AA, // Fehler in der Vermittlung
        AccessInfoIgnored = 0x34AB, // Zugangsinformation ignoriert
        ReqChannelUnavailable = 0x34AC, // Gewünschter Kanal nicht verfügbar
        ResourceUnavilable = 0x34AF, // Resourcen nicht verfügbar
        QosUnavailable = 0x34B1, // Leitungsqualität nicht ausreichend
        ReqFacNotSubscribed = 0x34B2, // Gewünschtes Dienstmerkmal nicht beantragt
        FacilityLocked = 0x34B9, // Dienstmerkmal gesperrt
        FacilityUnavailable = 0x34BA, // Dienstmerkmal derzeit nicht verfügbar
        ServiceUnavailable = 0x34BF, // Dienst oder Option nicht verfügbar
        FacNotImplemented = 0x34C1, // Dienstmerkmal nicht implementiert
        ChannelTypNotImplement = 0x34C2, // Kanaltyp nicht implementiert
        ReqFacNotImplemented = 0x34C5, // Gewünschtes Merkmal nicht implementiert
        OnlyRestDataService = 0x34C6, // Nur eingeschränkter Datendienst verfügbar
        ServiceNotImplemented = 0x34CF, // Service oder Option nicht implementiert
        InvalidCallRefValue = 0x34D1, // Ungültiger Anruf-Referenzwert
        IdChannelNotExists = 0x34D2, // Angegebener Kanal existiert nicht
        CallExistsNotId = 0x34D3, // Gehaltene Verbindung existiert, aber nicht diese ID
        CallIdInUse = 0x34D4, // Verbindungs-ID ist in Benutzung
        NoCallSuspended = 0x34D5, // Keine gehaltene Verbindung vorhanden
        CallIdConnCleared = 0x34D6, // Die Verbindung mit dieser ID wurde beendet
        FalseDestCallNumber = 0x34D8, // Zielrufnummer falsch
        InvalidTransitNetwork = 0x34DB, // Ungültiges Transit-Netzwerk
        InvalidMessage = 0x34DF, // Ungültige Message
        MandInfoElementMissing = 0x34E0, // Ein vorgeschriebenes Informationselement fehlt
        MsgTypNotExistNotImplem = 0x34E1, // Message-Typ existiert nicht oder ist nicht implementiert
        MsgTypNotStateCompatible = 0x34E2, // Message-Typ passt nicht zum Verbindungszustand
        InfoNotExistNotImplement = 0x34E3, // Informationselement existiert nicht oder nicht implementiert
        InvalidInfoContent = 0x34E4, // Ungültiger Inhalt eines Informationselements
        MsgNotCompToCallState = 0x34E5, // Message passt nicht zum Verbindungszustand
        Timeout = 0x34E6, // Zeitüberschreitung aufgetreten
        ProtErrUnspecified = 0x34EF, // Protokollfehler ohne nähere Angabe
        ConnErrUnspecified = 0x34FF, // Verbindungsfehler ohne nähere Angabe

        Modem_NormalEndOfConnection = 0x3500,
        Modem_CarrierLost = 0x3501,
        Modem_ErrorInNegotiation = 0x3502,
        Modem_NoAnswerToProtocolRequest = 0x3503,
        Modem_RemoteModemOnlyWorksInSynchronousMode = 0x3504,
        Modem_FramingFails = 0x3505,
        Modem_ProtocolNegotiationFails = 0x3506,
        Modem_OtherModemSendsWrongProtocolRequest = 0x3507,
        Modem_SyncInformationMissing = 0x3508,
        Modem_NormalEndOfConnectionFromTheOtherModem = 0x3509,
        Modem_NoAnswerFromOtherModem = 0x350a,
        Modem_ProtocolError = 0x350b,
        Modem_ErrorInCompression = 0x350c,
        Modem_NoConnect = 0x350d,
        Modem_NoProtocolFallBackAllowed = 0x350e,
        Modem_NoModemOrFaxAtRequestNumber = 0x350f,
        Modem_HandshakeError = 0x3510,

        // ??
        SuppServiceErr = 0x3600, // Fehler bei Supplementary Service, z.B. Dienst nicht beantragt
        SuppServiceUnavailable = 0x3603, // Supplementary Service ist nicht verfügbar
        SuppServiceNotImplement = 0x3604, // Supplementary Service ist nicht implementiert
        InvalidConnState = 0x3607, // Ungültiger Verbindungszustand, z.B. 3er-Konferenz nicht möglich

        // Fehlercodes für Leitungsdienste
        NoPlciBInterConn = 0x3800, // PLCI hat keinen B-Kanal, Zusammenschaltung nicht möglich
        NoLineCompatibility = 0x3801, // Leitungen nicht kompatibel, Zusammenschaltung nicht möglich
        NoPlciInterConnAvail = 0x3802  // PLCI nicht verfügbar, Zusammenschaltung nicht möglich
    }
    #endregion

    #region Capi-Message-Types
    public enum CapiMessages_SubCommands_Enum : byte
    {
        REQ = 0x80, 
        CONF = 0x81,
        IND = 0x82, 
        RESP = 0x83
    }
    public enum CapiMessages_Commands_Enum : byte
    {
        CONNECT = 0x02,
        CONNECT_ACTIVE = 0x03,
        DISCONNECT = 0x04,
        ALERT = 0x01,
        INFO = 0x08,

        // Messages concerning logical connections
        CONNECT_B3 = 0x82,
        CONNECT_B3_ACTIVE = 0x83,
        CONNECT_B3_T90_ACTIVE = 0x88,
        DISCONNECT_B3 = 0x84,
        DATA_B3 = 0x86,
        RESET_B3 = 0x87,

        // Administrative and other messages
        LISTEN = 0x05,
        FACILITY = 0x80, 
        SELECT_B_PROTOCOL = 0x41,
        MANUFACTURER = 0xFF,

        // Interoperability
        INTEROPERABILITY = 0x20
    }

    /// <summary>
    /// Enumeration aller Capi-Nachrichten-Codes
    /// 0xAABB : AA=Command, BB = Subcommand
    /// </summary>
    public enum CapiMessages_Enum : ushort
    {
        // Messages concerning the signaling protocol
        CONNECT_REQ = 0x0280, // initiates an outgoing physical connection
        CONNECT_CONF = 0x0281, // local confirmation of the request
        CONNECT_IND = 0x0282, // indicates an incoming physical connection
        CONNECT_RESP = 0x0283, // response to the indication
        CONNECT_ACTIVE_IND = 0x0382, // indicates the activation of a physical connection
        CONNECT_ACTIVE_RESP = 0x0383, // response to the indication
        DISCONNECT_REQ = 0x0480, // initiates clearing down of a physical connection
        DISCONNECT_CONF = 0x0481, // local confirmation of the request
        DISCONNECT_IND = 0x0482, // indicates the clearing of a physical connection
        DISCONNECT_RESP = 0x0483, // response to the indication
        ALERT_REQ = 0x0180, // initiates sending of ALERT, i.e. compatibility with call
        ALERT_CONF = 0x0181, // local confirmation of the request
        INFO_REQ = 0x0880, // initiates sending of signaling information
        INFO_CONF = 0x0881, // local confirmation of the request
        INFO_IND = 0x0882, // indicates specified signaling information
        INFO_RESP = 0x0883, // response to the indication

        // Messages concerning logical connections
        CONNECT_B3_REQ = 0x8280, // initiates an outgoing logical connection
        CONNECT_B3_CONF = 0x8281, // local confirmation of the request
        CONNECT_B3_IND = 0x8282, // indicates an incoming logical connection
        CONNECT_B3_RESP = 0x8283, // response to the indication
        CONNECT_B3_ACTIVE_IND = 0x8382, // indicates the activation of a logical connection
        CONNECT_B3_ACTIVE_RESP = 0x8383, // response to the indication
        CONNECT_B3_T90_ACTIVE_IND = 0x8882, // indicates switching from T.70NL to T.90NL
        CONNECT_B3_T90_ACTIVE_RESP = 0x8883, // response to the indication
        DISCONNECT_B3_REQ = 0x8480, // initiates clearing down of a logical connection
        DISCONNECT_B3_CONF = 0x8481, // local confirmation of the request
        DISCONNECT_B3_IND = 0x8482, // indicates the clearing down of a logical connection
        DISCONNECT_B3_RESP = 0x8483, // response to the indication
        DATA_B3_REQ = 0x8680, // initiates sending of data over a logical connection
        DATA_B3_CONF = 0x8681, // local confirmation of the request
        DATA_B3_IND = 0x8682, // indicates incoming data over a logical connection
        DATA_B3_RESP = 0x8683, // response to the indication
        RESET_B3_REQ = 0x8780, // initiates the resetting of a logical connection
        RESET_B3_CONF = 0x8781, // local confirmation of the request
        RESET_B3_IND = 0x8782, // indicates the resetting of a logical connection
        RESET_B3_RESP = 0x8783, // response to the indication

        // Administrative and other messages
        LISTEN_REQ = 0x0580, // activates call and info indications
        LISTEN_CONF = 0x0581, // local confirmation of the request
        FACILITY_REQ = 0x8080, // requests additional facilities (e.g. ext. equipment)
        FACILITY_CONF = 0x8081, // local confirmation of the request
        FACILITY_IND = 0x8082, // indicates additional facilities (e.g. ext. equipment)
        FACILITY_RESP = 0x8083, // response to the indication
        SELECT_B_PROTOCOL_REQ = 0x4180, // selects protocol stack used for a logical connection
        SELECT_B_PROTOCOL_CONF = 0x4181, // local confirmation of the request
        MANUFACTURER_REQ = 0xFF80, // manufacturer-specific operation
        MANUFACTURER_CONF = 0xFF81, // manufacturer-specific operation
        MANUFACTURER_IND = 0xFF82, // manufacturer-specific operation
        MANUFACTURER_RESP = 0xFF83,  // manufacturer-specific operation

        // Interoperability
        INTEROPERABILITY_REQ = 0x2080,
        INTEROPERABILITY_CONF = 0x2081,
        INTEROPERABILITY_IND = 0x2082,
        INTEROPERABILITY_RESP = 0x2083
    }
    #endregion
    
    #region Parameter-Enumerations
    public enum SupplementaryServicesInfo_Enum : ushort
    {
        Success = 0x0000,
        SupplementaryServiceNotSupported = 0x300E,
        RejectedBySupplementaryServicesSupervision = 0x3305,
        RequestNotAllowedInThisState = 0x3010
    }
    public enum SupplementaryServicesReason_Enum : ushort
    {
        TimeOut = 0x3303,
        Disconnect = 0x3400,
        ErrorInfoConcerningTheRequest = 0x3600,
        ErrorInfoRegardingTheContext = 0x3700
    }
    public enum TypeOfPartyNumber_Enum : byte
    {
        Unknown = 0x00,
        PublicPartyNumber = 0x01
    }
    public enum TypeOfCallForwarding_Enum : ushort
    {
        Unconditional = 0,
        Busy = 1,
        NoReply = 2
    }
    public enum PresentationAllowed_Enum : ushort
    {
        DisplayOfOwnAddressNotAllowed = 0,
        DisplayOfOwnAddressAllowed = 1
    }
    public enum InvocationMode_Enum : ushort
    {
        Deferred = 0,
        Immediate = 1,
        Combined = 2,
        SuppressInvocationMode = 0xFFFF
    }
    public enum MessageStatus_Enum : ushort
    {
        AddedMessage = 0,
        RemoveMessage = 1,
        SuppressMessageStatusAndMessageReference = 0xFFFF
    }
    public enum CCBSRecallMode_Enum : ushort
    {
        GlobalCallBack = 0,
        SpecificCallBack = 1
    }
    public enum LineStatus_Enum : ushort
    {
        Busy = 0,
        Free = 1
    }
    public enum DTMFInformation_Enum : ushort
    {
        SendingOfDTMFSuccessfully = 0,
        IncorrectDTMFDigit = 1,
        UnknownDTMFRequest = 2
    }
    public enum CompressionMode_Enum : ushort
    {
        NoCompression = 0,
        V42bis = 1
    }
    public enum Format_Enum : ushort
    {
        SFF = 0,
        PlainFaxFormat = 1,
        PCX = 2,
        DCX = 3,
        TIFF = 4,
        ASCII = 5,
        ExtendedANSI = 6,
        BinaryFileTransfer = 7
    }
    public enum Resolution_Enum : ushort
    {
        Standard = 0,
        High = 1
    }
    public enum Direction_Enum : ushort
    {
        AllDirections = 0,
        IncomingDataOnly = 1,
        OutgoingDataOnly = 2
    }
    public enum TEI_Enum : byte
    {
        AutomaticTEI = 0,
        FixedTEI = 1
    }
    public enum ModuloMode_Enum : byte
    {
        NormalOperation = 8,
        ExtendedOperation = 128
    }
    public enum SpeedNegotiation_Enum : ushort
    {
        None = 0,
        WithinModulationClass = 1,
        V100 = 2,
        V8 = 3
    }
    public enum Parity_Enum : ushort
    {
        None = 0,
        Odd = 1,
        Even = 2
    }
    public enum StopBits_Enum : ushort
    {
        StopBits_1 = 0,
        StopBits_2 = 1
    }
    public enum Channel_Enum : ushort
    {
        ChannelB = 0,
        ChannelD = 1,
        NeitherChannelBNorChannelD = 2,
        ChannelAllocation = 3,
        ChannelIdentificationInformationElement = 4
    }
    public enum Channel3_Operation_Enum : ushort
    {
        DTEMode = 0,
        DCEMode = 1
    }

    public enum BChannel_Operation_Enum : ushort
    {
        Default = 0,
        DTEMode = 1,
        DCEMode = 2
    }
    public enum B1Protocol_Enum : ushort
    {
        Kbits64WithHDLCFraming = 0,
        Kbits64BitTransparentOperationWithByteFramingFromTheNetwork = 1,
        V110AsynchronousOperationWithStart_StopByteFraming = 2,
        V110SynchronousOperationWithHDLCFraming = 3,
        T30ModemForGroup3Fax = 4,
        Kbits64InvertedWithHDLCFraming = 5,
        Kbits56BitTransparentOperationWithByteFramingFromTheNetwork = 6,
        ModemWithFullNegotiation = 7,
        ModemAsynchronousOperationWithStart_StopByteFraming = 8,
        ModemSynchronousOperationWithHDLCFraming = 9
    }
    public enum B2Protocol_Enum : ushort
    {
        ISO7776_X76SLP = 0,
        Transparent = 1,
        SDLC = 2,
        LAPDInAccordanceWithQ921ForDChannelX25_SAPI16 = 3,
        T30ForGroup3Fax = 4,
        PointToPointProtocol = 5,
        Transparent_IgnoringFramingErrorOfB1Protocol = 6,
        ModemWithFullNegotiation = 7,
        ISO7776_X76SLP_ModifiedToSupportV42BisCompression = 8,
        V120AsynchronousMode = 9,
        V120AsynchronousModeWithV42BisCompression = 10,
        V120BitTransparentMode = 11,
        LAPDInAcordanceWithQ921IncludingFreSAPISelection = 12
    }
    public enum B3Protocol_Enum : ushort
    {
        Transparent = 0,
        T90NLWithCompatibilityToT70NLInAccordanceWithT90 = 1,
        ISO8208_X25_DTE_DTE = 2,
        X25DCE = 3,
        T30ForGroup3Fax = 4,
        T30ForGroup3FaxExtended = 5,
        Modem = 7
    }
    public enum FacilitySelector_Enum : ushort
    {
        Handset = 0,
        DTMF = 1,
        V42bisCompression = 2,
        SupplementaryServices = 3,
        PowerManagementWakeup = 4,
        LineInterconnect = 5
    }
    public enum InteroperabilitySelector_Enum : ushort
    {
        USBDeviceMenagement = 0,
        BluetoothDeviceManagement = 1
    }
    public enum CIPValue_Enum : ushort
    {
        NoPredefinedProfile = 0,
        Speech = 1,
        UnrestrictedDigitalInformation = 2,
        RestrictedDigitalInformation = 3,
        Audio_3_1kHz = 4,
        Audio_7kHz = 5,
        Video = 6,
        PacketMode = 7,
        RateAdaptation_56kBits = 8,
        UnrestrictedDigitalInformationWithTonesAnnouncements = 9,
        Reserved1 = 10,
        Reserved2 = 11,
        Reserved3 = 12,
        Reserved4 = 13,
        Reserved5 = 14,
        Reserved6 = 15,
        Telephony = 16,
        Group2_3Facsimilie = 17,
        Group4FacsimilieClass1 = 18,
        TeletexServiceBasicAndMixedModeAndGroup4FacsimilieServiceClasses2And3 = 19,
        TeletexServiceBasicAndProcessableMode = 20,
        TeletexServiceBasicMode = 21,
        InternationalInterworkingForVideotex = 22,
        Telex = 23,
        MessageHandlingSystemsX400 = 24,
        OSIApplicationX200 = 25,
        Telephony_7kHz = 26,
        VideoTelephonyFirstConnection = 27,
        VideoTelephonySecondConnection = 28
    }
    public enum CIPMask_Enum : uint
    {
        Telephony_ISDNWithoutHLCInfo = 1,
        Telephony_AnalogNetwork = 0x8,
        Telephony_ISDNWithHLCInfo = 0x8000,

        Group2_3Fax_AnalogNetwork = 0x8,
        Group2_3Fax_ISDN = 0x10000,

        NonStandard64Kbits_NoCheckingOfHLCInfo = 2,

        NonStandard56Kbits_NoCheckingOfHLCInfo = 0x80,

        Group4Fax_WithoutHLCInfo = 2,
        Group4Fax_WithHLCInfo = 0x20000
    }
    public enum Flags_Enum : ushort
    {
        QualifierBit = 1,
        MoreDataBit = 2,
        DeliveryCofirmationBit = 4,
        ExpeditedDataBit = 8,
        Break_UIFrame = 16,
        FramingErrorBit = 0x8000
    }
    public enum InfoMask_Enum : uint
    {
        Cause = 1,
        DateTime = 2,
        Display = 4,
        User_User = 8,
        CallProcess = 16,
        Facility = 32,
        ChargeInformation = 64,
        CalledPartyNumber = 128,
        ChannelIdentification = 256,
        EnablesEarlyB3Connect = 512,
        RedirectingInformation = 1024,
        Reserved = 2048,
        SendingComplete = 4096
    }
    public enum ControllerType_Enum : uint
    {
        InternalController = 0,
        ExternalController = 64
    }
    public enum Reject_Enum : ushort
    {
        AcceptCall = 0,
        IgnoreCall = 1,
        RejectCall_NormalCallClearing = 2,
        RejectCall_UserBusy = 3,
        RejectCall_RequestCircuitChannelNotAvailable = 4,
        RejectCall_FacilityRejected = 5,
        RejectCall_ChannelUnacceptable = 6,
        RejectCall_IncompatibleDestination = 7,
        RejectCall_DestinationOutOfOrder = 8,
        Cause_from_network = 0x3400
    }
    public enum ModeEnum : ushort
    {
        NotSendingInformation = 0,
        SendingInformation = 1
    }
    public enum Multiplier_Enum : ushort
    {
        M_1_of_1000 = 0,
        M_1_of_100 = 1,
        M_1_of_10 = 2,
        M_1 = 3,
        M_10 = 4,
        M_100 = 5,
        M_1000 = 6
    }
    public enum GeneralInfo_Enum : ushort
    {
        RequestAccepted = 0,
        MessageNtSupportedInCurrentState = 0x2001,
        IncorrectController_PLCI_NCCI = 0x2002,
        IllegalMessageParameterCoding = 0x2007,
        NoInterconnectionResourcesAvailable = 0x2008,
        UnsupportedInteroperability = 0x300F,
        FacilitySpecificFunctionNotSupported = 0x3011
    }
    public enum ReasonB3_Enum : ushort
    {
        Clearance_in_accordance_with_protocol = 0,
        Protocol_error_Layer_1 = 0x3301,
        Protocol_error_Layer_2 = 0x3302,
        Protocol_error_Layer_3 = 0x3303,
        Cleared_by_Call_Control_Supervision = 0x3305
    }
    public enum Reason_Enum : ushort
    {
        No_Cause_available = 0,
        Protocol_error_Layer_1 = 0x3301,
        Protocol_error_Layer_2 = 0x3302,
        Protocol_error_Layer_3 = 0x3303,
        Another_application_got_that_call = 0x3304,
        Cleared_by_Call_Control_Supervision = 0x3305,
        Disconnect_cause_from_network = 0x3400
    }
    public enum ServiceReason_Enum : ushort
    {
        UserInitiated = 0,
        PLCI_HasNoBChannel = 0x3800,
        LinesNotCompatible = 0x3801,
        PLCInotInterconnected = 0x3802
    }
    public enum GuardTone_Enum : byte
    {
        NoGuardTone = 0,
        Hz1800 = 1,
        Hz550 = 2
    }
    public enum Loudspeaker_Enum : byte
    {
        Off = 0,
        On = 1,
        AlwaysOn = 2
    }
    public enum LoudspeakerVolume_Enum : byte
    {
        Silent = 0,
        NormalLow = 1,
        NormalHigh = 2,
        Maximum = 3
    }
    public enum USBFunction_Enum : ushort
    {
        Register = 0,
        Release = 1,
        OpenInterface = 2,
        CloseInterface = 3,
        Write = 4,
        Read = 5
    }
    public enum BluetoothFunction_Enum : ushort
    {
        Register = 0,
        Release = 1,
        GetProfile = 2,
        GetManufacturer = 3,
        GetVersion = 4,
        GetSerialNumber = 5,
        Manufacturer = 6
    }
    public enum InteroperabilityInfo_Enum : ushort
    {
        NoError = 0,
        B1ProtocolNotSupported = 0x3001,
        B1ProtocolParameterNotSupported = 0x3004,
        FacilityNotSupported = 0x300B,
        OverlappingChannelMasks = 0x300E
    }
    public enum USBDeviceMode_Enum : ushort
    {
        Intelligent = 0,
        Simple = 1
    }
    public enum SupplementaryServicesFunction_Enum : ushort
    {
        GetSupportedServices = 0x0000,
        Listen = 0x0001,
        Hold = 0x0002,
        Retrieve = 0x0003,
        Suspend = 0x0004,
        Resume = 0x0005,
        ExplicitCallTransfer = 0x0006,
        ThreePartyConferenceBegin = 0x0007,
        ThreePartyConferenceEnd = 0x0008,
        CallForwardingActivate = 0x0009,
        CallForwardingDeactivate = 0x000A,
        CallForwardingInterrogateParameters = 0x000B,
        CallForwardingInterrogateNumbers = 0x000C,
        CallDeflection = 0x000D,
        MaliciousCallIdentification = 0x000E,
        CompletitionOfCallToBusySubRequest = 0x000F,
        CompletitionOfCallToBusySubDeactivate = 0x0010,
        CompletitionOfCallToBusySubInterrogate = 0x0011,
        CompletitionOfCallToBusySubCall = 0x0012,
        MWIActivate = 0x0013,
        MWIDeactivate = 0x0014,
        CCNRRequest = 0x0015,
        CCNRInterrogate = 0x0016,
        CONFBegin = 0x0017,
        CONFAdd = 0x0018,
        CONFSplit = 0x0019,
        CONFDrop = 0x001A,
        CONFIsolate = 0x001B,
        CONFReattach = 0x001C,

        HoldNotification = 0x8000,
        RetrieveNotification = 0x8001,
        SuspendNotification = 0x8002,
        ResumeNotification = 0x8003,
        CallIsDivertingNotification = 0x8004,
        DiversionActivatedNotification = 0x8005,
        CallForwardingActivateNotification = 0x8006,
        CallForwardingDeactivateNotification = 0x8007,
        DiversionInformation = 0x8008,
        CallTransferAlertedNotification = 0x8009,
        CallTransferActiveNotification = 0x800A,
        ConferenceEstablishedNotification = 0x800B,
        ConferenceDisconnectNotification = 0x800C,
        CCBSEraseCallLinkageID = 0x800D,
        CCBSStatus = 0x800E,
        CCBSRemoteUserFree = 0x800F,
        CCBSBFree = 0x8010,
        CCBSErase = 0x8011,
        CCBSStopAlerting = 0x8012,
        CCBSInfoRetain = 0x8013,
        MWIIdication = 0x8014,
        CCNRInfoRetain = 0x8015,
        CONFPartyDISC = 0x8016,
        CONFNotifications = 0x8017
    }
    #endregion

    public enum InfoDataType
    {
        MessageType,
        ElementType
    }
    public enum InfoChargeType
    {
        ChargeUnits,
        NationalCurrency
    }

    #region Flags
    [Flags()]
    public enum NotificationMask_Flags : uint
    {
        Hold_RetrieveNotifications = 1,
        TerminalPortabilityNotifications = 2,
        ECTNotifications = 4,
        ThreePartyNotifications = 8,
        CallForwarding_DeflectionNotifications_Information = 16,
        CCBSNotifuications_Information = 128,
        MWIIdication = 256,
        CCNRNotification = 512,
        CONFNotifications_Information = 1024
    }
    [Flags()]
    public enum SupportedServices_Flags : uint
    {
        CrossControllerSupport = 1,
        AsymetricConnectionsSupported = 2,
        MonitoringSupported = 4,
        MixingSupported = 8,
        RemoteMonitoringSupported = 16,
        RemoteMixingSupported = 32,
        LoopingOfLineDataSupported = 64,
        LoopingOfB3ApplicationDataSupported = 128,
        LoopingOfB3ConferenceDataSupported = 256
    }
    [Flags()]
    public enum ConnectRequestDataPath_Flags : uint
    {
        EnableDataTransmissionFromMainPLCI = 1,
        EnableDataTransmissionFromParticipatingPLCI = 2,
        EnableMonitoringOfChannelData = 4,
        EnableMixingForParticipatingPLCI = 8,
        EnableMonitoringOfAllData = 16,
        EnableMixingOf_DATA_B3_REQ_OfParticipatingPLCI = 32,
        IncomingLineDataWillBeLoopedBack = 64,
        IncomingApplicationDataWillBeLoopedBack = 128,
        IncomingConferenceDataWillBeLoopedBack = 256
    }
    [Flags()]
    public enum RequestParameterDataPath_Flags : uint
    {
        EnableMonitoringOfChannelDataForPLCI = 4,
        EnableMixingIntoDataChannelOfPLCI = 8,
        EnableMonitoringOfChannelDataForAllPLCI = 16,
        EnableMixingIntoDataChannelOfAllPLCI = 32,
        IncomingLineDataWillBeLoopedBack = 64,
        IncomingApplicationDataWillBeLoopedBack = 128,
        IncomingConferenceDataWillBeLoopedBack = 256
    }
    [Flags()]
    public enum B2ConfigurationProtocol7Options_Flags : ushort
    {
        DisableV42 = 1,
        DisableMNP4_MNP5 = 2,
        DisableTransparentMode = 4,
        DisableV42Negotiation = 8,
        DisableCompression = 16
    }
    [Flags()]
    public enum B3ConfigurationProtocol5Options_Flags : ushort
    {
        EnableHighResolution = 1,
        AcceptIncomingFaxPollingRequests = 2,
        EnableJPEG = 1024,
        EnableJBIG = 2048,
        DontUseJBIGCompression = 4096,
        DontUseMRCompression = 8192,
        DontUseMMRCompression = 16384,
        DontUseECM = 32768
    }
    [Flags()]
    public enum DataB3_Flags : ushort
    {
        Qualifier = 1,
        MoreData = 2,
        DeliveryConfirmation = 4,
        ExpeditedData = 8,
        Break_Or_UIFrame = 16,
        FramingError = 16384,
    }
    [Flags()]
    public enum InfoMask_Flags : uint
    {
        Cause = 1,
        Date_Time = 2,
        Display = 4,
        UserUserInformation = 8,
        CallProgression = 16,
        Facility = 32,
        Charging = 64,
        CalledPartyNumber = 128,
        ChannelInformation = 256,
        EarlyB3Information = 512,
        RedirectingInformation = 1024,
        SendingComplete = 2048
    }
    [Flags()]
    public enum CIPMask_Flags : uint
    {
        AnyMatch = 1,
        Speech = 2,
        UnrestrictedDigitalInformation = 4,
        RestrictedDigitalInformation = 8,
        kHz31Audio = 16,
        kHz7Audio = 32,
        Video = 64,
        PacketMode = 128,
        kbits56RateAdaptation = 256, 
        UnrestrictedDigitalInformationWithTonesAnnouncements = 512,

        Telephony = 0x10000,
        Group2_3Fax = 0x20000,
        Group4FaxClass1 = 0x40000,
        TeletexService_BasicAndMixed = 0x80000,
        TeletexService_BasicAndProcessable = 0x100000,
        TeletexService_Basic = 0x200000,
        Videotex = 0x400000,
        Teleex = 0x800000,
        MessageHandlingSystemAccordingX400 = 0x1000000,
        OSIApplicationsAccordingX200 = 0x2000000,
        kHz7Telephony = 0x4000000,
        VideoTelephonyF721FirstConnection = 0x8000000,
        VideoTelephonyF721SecondConnection = 0x10000000
    }
    #endregion
}
