using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ChangeDetectInterface;

namespace LinxServiceProxy
{
    public class LinxProxy
    {
        #region fields
        private TaskCompletionSource<uint> _connectTcs;
        private IntPtr _dtsaPtr = IntPtr.Zero;
        private IntPtr _connPtr = IntPtr.Zero;
        private uint _connID = 0;
        private IntPtr _transportConn = IntPtr.Zero;
        private IntPtr _statusCallbackDelegatePointer = IntPtr.Zero;
        private LinxNative.StatusDelegate _statusDelegate;
        #endregion
        #region private function
        private IntPtr CreateStatusCallback()
        {
            if (_statusCallbackDelegatePointer == IntPtr.Zero)
            {
                _statusDelegate = ConnectedStatusProcessing;
                _statusCallbackDelegatePointer = Marshal.GetFunctionPointerForDelegate(_statusDelegate);
            }
            return _statusCallbackDelegatePointer;
        }
        private uint ConnectedStatusProcessing(uint connId, uint connParam, uint status, IntPtr info, uint infoSize)
        {
            ConnectionStatus connStatus;
            switch (((DtlConstant.DTL_CONNECTION_STATE)status))
            {
                case DtlConstant.DTL_CONNECTION_STATE.DTL_CONN_ESTABLISHED:
                    _connID = connId;
                    connStatus = ConnectionStatus.Established;
                    _connectTcs.TrySetResult((uint)connStatus);
                    break;

                case DtlConstant.DTL_CONNECTION_STATE.DTL_CONN_ERROR:
                    connStatus = ConnectionStatus.ClosedWithError;
                    break;

                case DtlConstant.DTL_CONNECTION_STATE.DTL_CONN_FAILED:
                    connStatus = ConnectionStatus.CreationFailed;
                    break;

                case DtlConstant.DTL_CONNECTION_STATE.DTL_CONN_TIMEOUT:
                    connStatus = ConnectionStatus.ClosedByTimeout;
                    FreeResource();
                    break;

                case DtlConstant.DTL_CONNECTION_STATE.DTL_CONN_CLOSED:
                    connStatus = ConnectionStatus.ClosedManually;
                    break;
                default:
                    connStatus = ConnectionStatus.ClosedUnknown;
                    break;
            }

            return 0;
        }

        private void FreeResource()
        {
            _connPtr = IntPtr.Zero;
            _connID = 0;
            if (_transportConn != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_transportConn);
                _transportConn = IntPtr.Zero;
            }
            LinxNative.DTL_DestroyDtsa(_dtsaPtr);
            _dtsaPtr = IntPtr.Zero;
        }

        internal Task<int> SendMessage()
        {
            throw new NotImplementedException();
        }

        private int CreateDtsa(string path)
        {
            uint error = 0;
            _dtsaPtr = LinxNative.DTL_CreateDtsaFromPathString(path, ref error, DtlConstant.DTL_FLAGS_ROUTE_TYPE_CIP);

            return (int)error;
        }
        
        #endregion
        #region public function
        public void CloseConnection()
        {
            LinxNative.DTL_CIP_CONNECTION_CLOSE(_connID, DtlConstant.DTL_CLOSE_TIMEOUT);
        }
        internal async Task<int> ConnectAsync(string path)
        {
            int error = CreateDtsa(path);
            if (error != DtlConstant.DTL_SUCCESS)
            {
                return await Task.FromResult(error);
            }

            var connTask = OpenConnectionAsync();
            if (await Task.WhenAny(connTask, Task.Delay((int)DtlConstant.DTL_CONNECT_TIMEOUT)) == connTask)
            {
                // connected
                return await Task.FromResult((int)ConnectionStatus.Established);

            }
            else
            {
                // timeout
                CancelConnection();
                return await Task.FromResult((int)ConnectionStatus.ClosedByTimeout);
            }
        }
        private Task<uint> OpenConnectionAsync()
        {
            _transportConn = LinxNative.CreateTransportConnStruct();
            var cb = CreateStatusCallback();
            _connectTcs = new TaskCompletionSource<uint>();
            int error = LinxNative.DTL_CIP_CONNECTION_OPEN(_dtsaPtr,
                                                        DtlConstant.DTL_IOI,
                                                        ref _connID,
                                                        DtlConstant.ConnUserToken,
                                                        _transportConn,
                                                        IntPtr.Zero,
                                                        cb,
                                                        DtlConstant.DTL_CONNECT_TIMEOUT);
            return _connectTcs.Task;
        }
        private void CancelConnection()
        {
            if(_connectTcs.Task.Status != TaskStatus.Running)
            {
                _connectTcs.TrySetCanceled();
                _connectTcs = null;
            }
        }
        #endregion


        //DTL_RETVAL LIBMEM DTL_CIP_CONNECTION_OPEN(DTSA_TYPE LIBPTR *, unsigned char LIBPTR *,
        //                unsigned long LIBPTR *, unsigned long,
        //                DTL_CIP_TRANSPORT_CONNECTION LIBPTR *,
        //                DTL_CIP_CONNECTION_PACKET_PROC,
        //                DTL_CIP_CONNECTION_STATUS_PROC,
        //                unsigned long );
        public int CipConnectionOpen(IntPtr dtsaPointer,
                                    byte[] ioi,
                                    ref uint conn_id,
                                    uint conn_param,
                                    IntPtr cip_conn_ptr,
                                    IntPtr packet_proc,
                                    IntPtr status_proc,
                                    uint timeout)
        {

            throw new NotImplementedException();
        }

        IntPtr CreateDtsaFromPathString(string pathString, ref uint error, uint flags)
        {
            throw new NotImplementedException();
        }
        //DTL_RETVAL LIBMEM DTL_OpenDtsa(DTSA_TYPE* pDtsa);
        //DTL_RETVAL LIBMEM DTL_CloseDtsa(DTSA_TYPE* pDtsa);
        //void* LIBMEM DTL_CreateDriverList(DWORD* dwNumDrivers,DWORD dwTimeout);
        //void* LIBMEM DTL_GetDriverListEntryFromDriverListIndex(void* pDriverList,int nIndex);
        //void LIBMEM DTL_DestroyDriverList(void* pDriverList, DWORD dwTimeout);
        //char* LIBMEM DTL_GetDriverNameFromDriverListEntry(void* pDriverListEntry);
        //DWORD LIBMEM DTL_GetHandleFromDriverListEntry(void* pDriverListEntry);

        int DriverOpen(uint driver_id,
                           string driver_name,
                           uint timeout)
        {
            throw new NotImplementedException();
        }
        int CipConnectionClose(uint conn_id, uint timeout)
        {
            throw new NotImplementedException();
        }
        //int LIBMEM DTL_CloseDtsa(DTSA_TYPE* pDtsa);

        int DriverClose(int driver_id, uint timeout)
        {
            throw new NotImplementedException();
        }

        void DestroyDtsa(IntPtr dtsaPointer)
        {
            throw new NotImplementedException();
        }

        int CipMessageSendCb(IntPtr dtsaPointer,
                            int svc_code,
                            byte[] ioi,
                            byte[] src_buf,
                            uint src_size,
                            IntPtr dst_buf,
                            IntPtr dst_size,
                            IntPtr ext_buf,
                            IntPtr ext_size,
                            uint timeout,
                            IntPtr callbackPointer,
                            uint callback_param)
        {
            throw new NotImplementedException();
        }
    }
}
