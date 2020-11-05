using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LinxServiceProxy
{
    public class LinxNative
    {
        private const string DTL_DLL = "linx_dtl_lite.dll";
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern int DTL_INIT_RSI_EX(uint maxDefines, uint cookie, uint flags);
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern int DTL_INIT_RSI(uint maxDefines, uint cookie);
        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
        public static extern uint DTL_UNINIT(uint errorCode);

        [DllImport(DTL_DLL, CharSet = CharSet.Ansi)]
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
