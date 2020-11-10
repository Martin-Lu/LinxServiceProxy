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
        private async Task PollDevice(CancellationToken cancellationToken)
        {
            do
            {
                // open connection
                DeviceStatus = ChangeDetectConstants.ControllerStatus.Connecting;
                await Task.Delay(30000);
                DeviceStatus = ChangeDetectConstants.ControllerStatus.SucceedConnected;

                // send message
                DeviceStatus = ChangeDetectConstants.ControllerStatus.SucceedConnected;
                await Task.Delay(5000);

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
        }
        #endregion
    }
}