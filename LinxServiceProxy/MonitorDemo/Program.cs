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
            LinxProxy proxy = new LinxProxy();
            await proxy.ConnectAsync("10.224.86.49");

            while (true)
            {
                await Task.Delay(5000);
            }
        }

        private static async Task NewMethod()
        {
            IMonitorService s = new ControllerMonitorService();
            for (int i = 0; i < 10000; i++)
            {
                var path = $"controller1path10.10.10.11plc{i.ToString()}";
                await s.StartMonitoDeviceAsync(path);
            }

            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(5000);
            }

            await s.ShutDownMonitorService();

            while (true)
            {
                await Task.Delay(5000);
            }
        }
    }
}
