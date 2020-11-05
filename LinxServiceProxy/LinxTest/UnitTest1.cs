using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using LinxServiceProxy;

namespace LinxTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            LinxProxy linxService = new LinxProxy();
            linxService.InitRsiEx(0,0,0);





        }
    }
}
