using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDetectInterface
{
    public interface IMonitorService
    {
        Task<int> StartMonitoDeviceAsync(string linxPath);
        Task<int> StopMonitorDevice(string linxPath);
        ChangeDetectConstants.ControllerStatus GetDeviceStatus(string linxPath);
        Task<int> ShutDownMonitorService();
    }
}
