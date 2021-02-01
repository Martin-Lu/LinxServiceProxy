using ChangeDetectInterface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LinxServiceProxy
{
    public class ControllerMonitor
    {
        #region feilds
        private ChangeDetectConstants.ControllerStatus _status = ChangeDetectConstants.ControllerStatus.Unknown;
        private readonly string _path;
        private Task _monitorTask;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private LinxProxy _linxProxy = new LinxProxy();
        #endregion
        #region ctor
        public ControllerMonitor(string path)
        {
            _path = path;
        }
        #endregion
        #region property
        public ChangeDetectConstants.ControllerStatus DeviceStatus 
        {
            get => _status;
            private set 
            {
                Console.WriteLine($"{_path} mlu {_status.ToString()}");
                _status = value;
            }
        }

        #endregion
        #region public methods
        public async Task<int> StartMonitor()
        {
            _monitorTask = Task.Run(() => PollDevice(_cts.Token), _cts.Token);
            return await Task.FromResult(0);
        }
        public async Task<int> StopMonitor()
        {
            if (_monitorTask != null)
            {
                try
                {
                    _cts.Cancel();
                    await _monitorTask;
                    Console.WriteLine($"{_path} mlu canceled");
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    _cts.Dispose();
                }
                _cts = null;
                _monitorTask = null;
            }
            return 0;
        }
        #endregion
        #region private methods
        private async Task<int> PollDevice(CancellationToken cancellationToken)
        {
            do
            {
                // open connection
                DeviceStatus = ChangeDetectConstants.ControllerStatus.Connecting;
                int result = await _linxProxy.ConnectAsync(_path);
                if(result == (int)ConnectionStatus.Established)
                {
                    DeviceStatus = ChangeDetectConstants.ControllerStatus.SucceedConnected;
                }
                else
                {
                    DeviceStatus = ChangeDetectConstants.ControllerStatus.FailConnected;
                    return await Task.FromResult((int)DeviceStatus);
                }


                // send message
                await Task.Delay(5000);
                result = await _linxProxy.SendMessage();
                //receiving message
                DeviceStatus = ChangeDetectConstants.ControllerStatus.ReceivingMessage;
                await Task.Delay(10000);
                DeviceStatus = ChangeDetectConstants.ControllerStatus.SucceedReceiveMessage;

                // close connection
                await Task.Delay(10000);
                DeviceStatus = ChangeDetectConstants.ControllerStatus.Disconnected;

                // poll after 60s
                await Task.Delay(60000);
            }
            while (!cancellationToken.IsCancellationRequested);
            return await Task.FromResult(0);
        }

        #endregion
    }
}