using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCellManager
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public IntPtr Handle { get; }
        public uint Message { get; }
        public IntPtr WParam { get; }
        public IntPtr LParam { get; }
        public string MessageName { get; }
        public DateTime Timestamp { get; }

        public MessageReceivedEventArgs(IntPtr handle, uint message, IntPtr wParam, IntPtr lParam, string messageName = null)
        {
            Handle = handle;
            Message = message;
            WParam = wParam;
            LParam = lParam;
            MessageName = messageName ?? $"0x{message:X}";
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss.fff}] Handle: 0x{Handle:X}, Message: {MessageName} (0x{Message:X}), WParam: 0x{WParam:X}, LParam: 0x{LParam:X}";
        }
    }
}
