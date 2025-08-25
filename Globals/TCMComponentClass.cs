using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Xml.Linq;

namespace TestCellManager
{
    public class TCMComponentClass : UserControl
    {
        protected IntPtr windowHandle;
        protected string className;
        protected WinAPI.WndProc wndProcDelegate;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        
        public IntPtr Handle => windowHandle;


        public TCMComponentClass(string name)
        {
            className = $"{name}_{Guid.NewGuid():N}";
            wndProcDelegate = WindowProc;
            CreateMessageWindow();
        }


        //================================================================================
        //                              PROTOTYPE FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Prototype Functions>
        protected virtual IntPtr WindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            //var messageName = GetMessageName(msg);
            //var eventArgs = new MessageReceivedEventArgs(hWnd, msg, wParam, lParam, messageName);
            //OnMessageReceived(eventArgs);

            return WinAPI.DefWindowProc(hWnd, msg, wParam, lParam);
        }

        protected virtual bool TCMPostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return WinAPI.PostMessage(hWnd, msg, wParam, lParam);
        }

        protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        protected virtual string GetMessageName(uint msg)
        {
            switch (msg)
            {
                case WinAPI.WM_SERVICE_INITIALIZE: return "WM_SERVICE_INITIALIZE";
                case WinAPI.WM_SERVICE_START: return "WM_SERVICE_START";
                case WinAPI.WM_SERVICE_STOP: return "WM_SERVICE_STOP";
                case WinAPI.WM_SERVICE_EXIT: return "WM_SERVICE_EXIT";
                default: return $"0x{msg:X}";
            }
        }

        protected virtual void OnInitializeShutdown()
        {
            // This method can be overridden to handle initialization or shutdown logic.
            // For example, you might want to log the initialization or cleanup resources.
            Debug.WriteLine($"{className} initialized.");
        }


        #endregion

        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Private Functions>
        private void CreateMessageWindow()
        {
            var wndClass = new WinAPI.WNDCLASS
            {
                lpfnWndProc = wndProcDelegate,
                hInstance = WinAPI.GetModuleHandle(null),
                lpszClassName = className
            };

            WinAPI.RegisterClass(ref wndClass);
            windowHandle = WinAPI.CreateWindowEx(0, className, className, 0, 0, 0, 0, 0,
                new IntPtr(-3), IntPtr.Zero, WinAPI.GetModuleHandle(null), IntPtr.Zero);

            if (windowHandle == IntPtr.Zero)
            {
                throw new Exception($"Failed to create window for {className}");
            }

            Console.WriteLine($"{className} Handle: 0x{windowHandle:X}");
        }
        #endregion

        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Public Functions>
        public virtual void Dispose()
        {
            if (windowHandle != IntPtr.Zero)
            {
                WinAPI.DestroyWindow(windowHandle);
                windowHandle = IntPtr.Zero;
            }
        }
        #endregion
    }
}
