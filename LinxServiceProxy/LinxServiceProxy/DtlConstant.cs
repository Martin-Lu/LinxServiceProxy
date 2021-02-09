using System;
using System.Collections.Generic;
using System.Text;

namespace LinxServiceProxy
{
    static class DtlConstant
    {
        public static readonly uint DTL_INIT_RSI_COOKIE = 0x38110905;
        public static readonly int DTL_SUCCESS = 0;
        public static readonly uint DTL_FLAGS_ROUTE_TYPE_CIP = 0x00000001;
        public static readonly uint DTL_CLOSE_TIMEOUT = 5000;
        public static readonly uint DTL_CONNECT_TIMEOUT = 30*1000;
        public static readonly byte[] DTL_IOI = { 0x02, 0x20, 0x02, 0x24, 0x01 };
        public static readonly uint DTL_CONN_CIP = 0x100;
        public static readonly byte DTL_CIP_CONN_MODE_IS_CLIENT = 0x01;
        public static readonly int DTL_CIP_CONN_PACKET_SIZE_FIXED = 0;
        public static readonly byte DTL_CIP_CONN_PACKET_SIZE_VARIABLE = 0x01;
        public static readonly byte DTL_CIP_CONN_TRIGGER_APPLICATION = 0x02;
        public static readonly byte DTL_CIP_CONN_TRANSPORT_CLIENT = 0x03;
        public static readonly byte DTL_CIP_CONN_TIMEOUT_MULTIPLIER = 0x00;
        public static readonly byte DTL_CIP_CONN_TYPE_POINT_TO_POINT = 0x02;
        public static readonly byte DTL_CIP_PRIORITY_LOW = 0;
        public static readonly int DTL_CONN_CLOSE_REQUEST_TIMEOUT = 0x1388;
        public static readonly int DTL_CONN_OPEN_REQUEST_TIMEOUT = 0x1388;
        public static readonly int DTL_DRIVER_ID_MIN = 0;
        public static readonly ushort ConnectionMaxPackageSize = 400;
        public static readonly uint ConnectionDefaultRPI = 2000*1000;
        public static readonly uint ConnUserToken = 0x123123;
        public static readonly int CIP_READ_ATTRIBUTE_SERVICE = 0x03;
        public enum DTL_CONNECTION_STATE : uint
        {
            DTL_CONN_ESTABLISHED = 1,   /* connection successfully opened */
            DTL_CONN_ERROR = 2,         /* error establishing connection */
            DTL_CONN_FAILED = 3,        /* connection failed */
            DTL_CONN_TIMEOUT = 4,       /* connection timed out */
            DTL_CONN_CLOSED = 5,        /* connection successfully closed */
            DTL_CONN_PKT_DUP = 6,       /* duplicate packet received */
            DTL_CONN_PKT_LOST = 7,      /* packet lost */
            DTL_CONN_ACK = 8,           /* ACK received */
            DTL_CONN_NAK_GENERAL = 9,   /* NAK - unspecified type received */
            DTL_CONN_NAK_BAD_CMD = 10,  /* NAK - "Bad Command" received */
            DTL_CONN_NAK_SEQ_ERR = 11,  /* NAK - "Sequence Error" received */
            DTL_CONN_NAK_NO_MEM = 12    /* NAK - "Not Enough Memory" received */
        }
    }
    static class CcuidConstant
    {
        public static readonly byte[] DTL_IOI_CCUID = { 0x02, 0x20, 0x8e, 0x24, 0x01 };
        public static readonly uint DTL_IOI_LEN = 5;
        public static readonly byte[] DTL_SERVICE_DATA = { 0x01, 0, 0x1b, 0 };
        public static readonly uint DTL_SERVICE_DATA_LEN = 4;
    }
}
