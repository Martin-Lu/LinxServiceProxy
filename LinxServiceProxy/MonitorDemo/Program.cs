using ChangeDetectInterface;
using LinxServiceProxy;
using System;
using System.Threading.Tasks;

namespace MonitorDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IMonitorService s = new ControllerMonitorService();
            for (int i = 0; i < 1000; i++)
            {
                var path = $"controller1path10.10.10.11plc{i.ToString()}";
                await s.StartMonitoDeviceAsync(path);
            }

            while (true)
            {
                await Task.Delay(5000);
            }
        }
    }
}
