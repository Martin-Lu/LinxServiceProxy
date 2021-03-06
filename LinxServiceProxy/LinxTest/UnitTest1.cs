using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using LinxServiceProxy;
using ChangeDetectInterface;

namespace LinxTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            IMonitorService s = new ControllerMonitorService();
            for(int i =0; i<10; i++)
            {
                var path = $"controller1path10.10.10.11plc{i.ToString()}";
                await s.StartMonitoDeviceAsync(path);
            }

            while (true)
            {
                await Task.Delay(1000);
            }
        }
    }
}
