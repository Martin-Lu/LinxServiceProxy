using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeDetectInterface
{
    #region nested type
    public enum ConnectionStatus : uint
    {
        ClosedUnknown = 5,
        ClosedByTimeout = 4,
        CreationFailed = 3,
        ClosedWithError = 2,
        ClosedManually = 1,
        Established = 0
    }
    #endregion
}
