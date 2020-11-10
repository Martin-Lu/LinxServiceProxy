using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeDetectInterface
{
    public static class ChangeDetectConstants
    {
        public enum ControllerStatus
        {
            Unknown,
            Connecting, 
            SucceedConnected, 
            FailConnected,
            TimeoutConnected,
            SucceedSendMessage,
            FailSendMessage,
            ReceivingMessage,
            SucceedReceiveMessage,
            FailReceiveMessage,
            Disconnected
        }
    }
}
