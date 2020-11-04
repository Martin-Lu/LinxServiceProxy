using System;

namespace LinxServiceProxy
{
    public class LinxProxy
    {
        //DTL_RETVAL(int) LIBMEM(__stdcall) DTL_INIT_RSI_EX(unsigned long max_defines,unsigned long cookie,unsigned long flags);
        public int InitRsiEx(uint maxDefines, uint cookie, uint flags)
        {
            throw new NotImplementedException();
        }
        public void Uninit(uint errorCode)
        {
            throw new NotImplementedException();
        }

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
