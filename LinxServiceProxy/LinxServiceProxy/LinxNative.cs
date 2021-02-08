using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LinxServiceProxy
{
    public class LinxNative
    {
        #region nested struct
        [StructLayout(LayoutKind.Sequential)]
        public struct DtlCIPNetworkConnection
        {
            [MarshalAs(UnmanagedType.U1)]
            public byte conn_type; /* specifies the network connection type. */
            [MarshalAs(UnmanagedType.U1)]
            public byte priority; /* specifies the priority of network connection. */
            [MarshalAs(UnmanagedType.U1)]
            public byte pkt_type; /* specifies the priority of network connection. */
            [MarshalAs(UnmanagedType.U2)]
            public ushort pkt_size; /* specifies whether data packets sent on this network connection. */
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 rpi; /* specify the RPI. */
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 api; /* specify the API. */
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DtlCIPTransportConnection
        {
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 ctype;		/* connection structure type	*/
            [MarshalAs(UnmanagedType.U1)]
            public byte mode;		/* client/server mode		*/
            [MarshalAs(UnmanagedType.U1)]
            public byte trigger;	/* trigger mode			*/
            [MarshalAs(UnmanagedType.U1)]
            public byte transport;	/* transport type		*/
            [MarshalAs(UnmanagedType.U1)]
            public byte tmo_mult;	/* timeout multiplier		*/
            [MarshalAs(UnmanagedType.Struct)]
            public DtlCIPNetworkConnection OT; /* Target-to-originator network connection */
            [MarshalAs(UnmanagedType.Struct)]
            public DtlCIPNetworkConnection TO; /* Originator-to-target network connection */
            [MarshalAs(UnmanagedType.Bool)]
            public bool isLargeConnection;
            [MarshalAs(UnmanagedType.Bool)]
            public bool isLicensedConnection;
        }
        #endregion
        #region helper function
        public delegate uint StatusDelegate(uint connId, uint connParam, uint status, IntPtr info, uint infoSize);
        public static IntPtr CreateTransportConnStruct()
        {
            var cipTransportConnonn = new LinxNative.DtlCIPTransportConnection();
            cipTransportConnonn.ctype = DtlConstant.DTL_CONN_CIP;
            cipTransportConnonn.mode = DtlConstant.DTL_CIP_CONN_MODE_IS_CLIENT;
            cipTransportConnonn.trigger = DtlConstant.DTL_CIP_CONN_TRIGGER_APPLICATION;
            cipTransportConnonn.transport = DtlConstant.DTL_CIP_CONN_TRANSPORT_CLIENT;
            cipTransportConnonn.tmo_mult = DtlConstant.DTL_CIP_CONN_TIMEOUT_MULTIPLIER;//*8
            cipTransportConnonn.OT.conn_type = DtlConstant.DTL_CIP_CONN_TYPE_POINT_TO_POINT;
            cipTransportConnonn.OT.priority = DtlConstant.DTL_CIP_PRIORITY_LOW;
            cipTransportConnonn.OT.pkt_type = DtlConstant.DTL_CIP_CONN_PACKET_SIZE_VARIABLE;
            // Note: For transport client, the headers are two bytes, so 502 bytes is the limit.
            cipTransportConnonn.OT.pkt_size = DtlConstant.ConnectionMaxPackageSize;
            cipTransportConnonn.OT.rpi = DtlConstant.ConnectionDefaultRPI; // Micro second
            cipTransportConnonn.OT.api = 0;
            cipTransportConnonn.TO.conn_type = DtlConstant.DTL_CIP_CONN_TYPE_POINT_TO_POINT;
            cipTransportConnonn.TO.priority = DtlConstant.DTL_CIP_PRIORITY_LOW;
            cipTransportConnonn.TO.pkt_type = DtlConstant.DTL_CIP_CONN_PACKET_SIZE_VARIABLE;
            cipTransportConnonn.TO.pkt_size = DtlConstant.ConnectionMaxPackageSize;
            cipTransportConnonn.TO.rpi = DtlConstant.ConnectionDefaultRPI;
            cipTransportConnonn.TO.api = 0;
            cipTransportConnonn.isLargeConnection = false;
            cipTransportConnonn.isLicensedConnection = false;

            var transportPtr = Marshal.AllocHGlobal(Marshal.SizeOf(cipTransportConnonn));
            Marshal.StructureToPtr(cipTransportConnonn, transportPtr, false);

            return transportPtr;
        }
        #endregion
        private const string DTL_DLL = "linx_dtl_lite.dll";
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern int DTL_INIT_RSI_EX(uint maxDefines, uint cookie, uint flags);
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern int DTL_INIT_RSI(uint maxDefines, uint cookie);
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern uint DTL_UNINIT(uint errorCode);

        [DllImport("LinxDll.dll", CharSet = CharSet.Ansi)]
        public static extern int DTL_CIP_CONNECTION_OPEN(
                IntPtr dtsaPointer, 
                byte[] ioi,
                ref uint connID,
                uint connParam,
                IntPtr cipConnPtr,
                IntPtr packetProc,
                IntPtr statusProc,
                uint timeout);
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern uint DTL_CIP_CONNECTION_CLOSE(uint connID, uint timeout);
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern IntPtr DTL_CreateDtsaFromPathString(string pathString, ref uint error, uint flags);
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern void DTL_DestroyDtsa(IntPtr dtsaPointer);
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern int DTL_DRIVER_OPEN(uint driverID,
                                    string driverName,
                                    uint timeout);
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern int DTL_DRIVER_CLOSE(int driverID, uint timeout);

        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern int DTL_CIP_MESSAGE_SEND_CB(
                                    IntPtr dtsaPointer,
                                    int svcCode,
                                    byte[] ioi,
                                    byte[] srcBuf,
                                    uint srcSize,
                                    IntPtr dstBuf,
                                    IntPtr dstSize,
                                    IntPtr extBuf,
                                    IntPtr extSize,
                                    uint timeout,
                                    IntPtr callbackPointer,
                                    uint callbackParam);

        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern uint DTL_GetHandleByDriverName(string driverName);
    }
}
