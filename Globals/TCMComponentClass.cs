using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Xml.Linq;

namespace TestCellManager
{
    public class TCMComponentClass : Window, INotifyPropertyChanged
    {

        private HwndSource m_hwnd_source;
        private IntPtr m_hwnd;
        protected string ClassName;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public IntPtr Handle => m_hwnd;

        protected Dictionary<MessageID, Action> m_mssg_id;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public TCMComponentClass(string name)
        {
            ClassName = $"{name}";
            create_mssg_window_only(name);
            m_mssg_id = new Dictionary<MessageID, Action>();
        }


        /// <summary>
        /// Override this method in derived classes to customize message processing
        /// </summary>
        /// <param name="receivedMessage">The raw message received</param>
        /// <returns>The processed message to be sent via the event</returns>
       // protected abstract string ProcessReceivedMessage(string receivedMessage);


        /// <summary>
        /// Finalizer to ensure proper cleanup
        /// </summary>
        ~TCMComponentClass()
        {
            Dispose();
        }

        //================================================================================
        //                              PROTOTYPE FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Prototype Functions>
        protected virtual IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //var messageName = GetMessageName(msg);
            //var eventArgs = new MessageReceivedEventArgs(hWnd, msg, wParam, lParam, messageName);
            //OnMessageReceived(eventArgs);
            return IntPtr.Zero;
        }

        protected virtual bool TCMPostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return PostMessage(hWnd, msg, wParam, lParam);
        }

        protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        protected virtual void OnRegisterMessage(OBJECTNAME eObjName, MessageID eMssgID, Action action)
        {
            if (TCMSystem.m_msgHandler != null)
            {
                m_mssg_id.Add(eMssgID, action);
                TCMSystem.m_msgHandler.RegisterMessage(eObjName, eMssgID);
            }
            else
            {
                MessageBox.Show("Message Handler is Null"); //TODO: Send to AlarmObj, this is a system error
            }
        }

        /// <summary>
        /// Dispose method for proper resource cleanup
        /// </summary>
        public virtual void Dispose()
        {
            m_hwnd_source?.Dispose();
            m_hwnd_source = null;
        }

        /// <summary>
        /// Override this method to customize memory cleanup behavior
        /// Default implementation frees the memory
        /// </summary>
        /// <param name="lParam">The memory pointer to clean up</param>
        protected virtual void HandleMemoryCleanup(IntPtr lParam)
        {
            if (lParam != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(lParam);
            }
        }

        //protected virtual string GetMessageName(uint msg)
        //{
        //    switch (msg)
        //    {
        //        case WinAPI.WM_SERVICE_INITIALIZE: return "WM_SERVICE_INITIALIZE";
        //        case WinAPI.WM_SERVICE_START: return "WM_SERVICE_START";
        //        case WinAPI.WM_SERVICE_STOP: return "WM_SERVICE_STOP";
        //        case WinAPI.WM_SERVICE_EXIT: return "WM_SERVICE_EXIT";
        //        default: return $"0x{msg:X}";
        //    }
        //}

        protected virtual void OnInitialize()
        {
            Debug.WriteLine($"{ClassName} initialized.");
        }

        protected virtual void OnStartService()
        {
            Debug.WriteLine($"{ClassName} start service.");
        }

        protected virtual void OnStopService()
        {
            Debug.WriteLine($"{ClassName} stop service.");
        }
        protected virtual void OnShutdown()
        {
            Debug.WriteLine($"{ClassName} shutdown.");
        }


        #endregion

        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Private Functions>

        private void create_mssg_window_only(string windowName)
        {
            HwndSourceParameters parameters = new HwndSourceParameters(windowName);
            parameters.WindowStyle = 0;
            parameters.ExtendedWindowStyle = 0;
            parameters.SetPosition(0, 0);
            parameters.SetSize(0, 0);
            parameters.ParentWindow = IntPtr.Zero;

            m_hwnd_source = new HwndSource(parameters);
            m_hwnd = m_hwnd_source.Handle;
            m_hwnd_source.AddHook(WindowProc);
        }
        #endregion

        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Public Functions>
        #endregion

        //================================================================================
        //                              INOTIFYPROPERTYCHANGED IMPLEMENTATION
        //--------------------------------------------------------------------------------
        #region <INotifyPropertyChanged>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

