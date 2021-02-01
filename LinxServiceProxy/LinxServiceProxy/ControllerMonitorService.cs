using ChangeDetectInterface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace LinxServiceProxy
{
    public class ControllerMonitorService : IMonitorService
    {
        #region static feild
        static ControllerMonitorService() => Initialize();
        private static void Initialize()
        {
            // DTL_INIT
            LinxNative.DTL_INIT_RSI(0, DtlConstant.DTL_INIT_RSI_COOKIE);
        }
        private static void UnInitialize()
        {
            //DTL_UNINT
            LinxNative.DTL_UNINIT(0);
        }
        #endregion

        #region feilds
        private ConcurrentDictionary<string, ControllerMonitor> _monitors = new ConcurrentDictionary<string, ControllerMonitor>();
        #endregion

        #region IMonitoService
        public async Task<int> StartMonitoDeviceAsync(string linxPath)
        {
            if(string.IsNullOrWhiteSpace(linxPath))
            {
                return -1;
            }
            var monitor = new ControllerMonitor(linxPath);
            if (!_monitors.TryAdd(linxPath, monitor))
            {
                return -1;
            }

            return await monitor.StartMonitor();
        }

        public async Task<int> StopMonitorDevice(string linxPath)
        {
            if(string.IsNullOrWhiteSpace(linxPath))
            {
                return -1;
            }
            if(_monitors.TryRemove(linxPath, out ControllerMonitor monitor))
            {
                if(monitor != null)
                {
                    return await monitor.StopMonitor();
                }
            }
            return -1;
        }

        public ChangeDetectConstants.ControllerStatus GetDeviceStatus(string linxPath)
        {
            if (string.IsNullOrWhiteSpace(linxPath))
            {
                throw new ArgumentException($"Invalid path: {linxPath}.");
            }
            else if (_monitors.TryGetValue(linxPath, out ControllerMonitor monitor))
            {
                return monitor.DeviceStatus;
            }
            else
            {
                throw new ArgumentException($"{linxPath} not monitored.");
            }
        }
        // when shut down, start/stop should not be called, guaranteed by other means
        public Task<int> ShutDownMonitorService()
        {
            _monitors.AsParallel().ForAll(async kpv =>
            {
                var monitor = kpv.Value;
                if (monitor != null)
                {
                    await monitor.StopMonitor();
                }
            });
            _monitors.Clear();
            UnInitialize();
            return Task.FromResult(0);
        }
        #endregion



    }
}
