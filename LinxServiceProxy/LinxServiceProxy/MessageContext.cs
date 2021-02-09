using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LinxServiceProxy
{
    class MessageContext
    {
        private IntPtr _data = IntPtr.Zero;
        private IntPtr _extData = IntPtr.Zero;
        private IntPtr _dataSize = IntPtr.Zero;
        private IntPtr _extDataSize = IntPtr.Zero;
        public LinxNative.CcuidResponse CcuidResult { get; set; }
        public IntPtr DataPtr
        {
            get { return _data; }
        }
        public IntPtr DataSizePtr
        {
            get { return _dataSize; }
        }
        public IntPtr ExtDataPtr
        {
            get { return _extData; }
        }
        public IntPtr ExtDataSizePtr
        {
            get { return _extDataSize; }
        }
        public MessageContext(int dataSize, int extDataSize)
        {
            Initialize(dataSize, extDataSize);
        }
        private void Initialize(int dataSize, int extDataSize )
        {
            UnInitialize();

            _data = Marshal.AllocHGlobal(dataSize);
            _dataSize = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(uint)));
            Marshal.WriteInt32(_dataSize, dataSize);

            _extData = Marshal.AllocHGlobal(extDataSize);
            _extDataSize = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(uint)));
            Marshal.WriteInt32(_extDataSize, extDataSize);
        }
        public void UnInitialize()
        {
            if(_data != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_data);
                _data = IntPtr.Zero;
            }
            if (_dataSize != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_dataSize);
                _dataSize = IntPtr.Zero;
            }
            if (_extData != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_extData);
                _extData = IntPtr.Zero;
            }
            if (_extDataSize != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_extDataSize);
                _extDataSize = IntPtr.Zero;
            }
        }

    }
}
