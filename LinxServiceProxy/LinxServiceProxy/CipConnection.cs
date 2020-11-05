using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinxServiceProxy
{
    public class CipConnection
    {
        private IntPtr _dtsaPtr = IntPtr.Zero;
        private IntPtr _connDtsaPtr = IntPtr.Zero;
        public bool Initialize() 
        {
            var dtlInit = LinxNative.DTL_INIT_RSI(0, DtlConstant.DTL_INIT_RSI_COOKIE);
            return dtlInit == DtlConstant.DTL_SUCCESS;
        }
        public void UnInitialize()
        {
            DestroyDtsa(_dtsaPtr);
            _dtsaPtr = IntPtr.Zero;
            DestroyDtsa(_connDtsaPtr);
            _connDtsaPtr = IntPtr.Zero;
            LinxNative.DTL_UNINIT(0);
        }
        // path format: "AVCNDAEW7X861!Ethernet\\10.224.82.61" 
        public bool CreateDtsa(string path)
        {
            var address = ExtractIPAddress(path);
            if (string.IsNullOrWhiteSpace(address))
            {
                return false;
            }

            uint error = 0;
            _dtsaPtr = LinxNative.DTL_CreateDtsaFromPathString(address, ref error, DtlConstant.DTL_FLAGS_ROUTE_TYPE_CIP);
            if (error != DtlConstant.DTL_SUCCESS)
            {
                _dtsaPtr = IntPtr.Zero;
                return false;
            }
            else
            {
                return true;
            }
        }
        public Task<bool> OpenConnection(IntPtr dtsaPointer,
            byte[] ioi,
            ref uint connID,
            uint connParam,
            IntPtr cipConnPtr,
            IntPtr packetProc,
            IntPtr statusProc,
            uint timeout)
        {
            var result = LinxNative.DTL_CIP_CONNECTION_OPEN(dtsaPointer,
                                                            ioi,
                                                            ref connID,
                                                            connParam,
                                                            cipConnPtr,
                                                            packetProc,
                                                            statusProc,
                                                            timeout);
        

            return Task.FromResult(false);
        }
        private string ExtractIPAddress(string path)
        {
            var first = path.IndexOf(@"\\") + 2;
            if (first == -1)
                return null;
            var len = path.Length - first;
            if (len < 0)
                return null;
            var address = path.Substring(first, len);
            return address;
        }
        private void DestroyDtsa(IntPtr dtsaPointer)
        {
            LinxNative.DTL_DestroyDtsa(dtsaPointer);
        }
    }
}
