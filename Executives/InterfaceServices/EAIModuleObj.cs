using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TestCellManager;

namespace TestCellManager.SystemTCM.Exec
{
    /// <summary>
    /// Represents a module for handling EAI Messages / SMOM Server
    /// 
    /// </summary>
    /// <remarks>This class is designed to manage EAI-related functionality, such as connecting to a WebSocket
    /// server,  receiving messages, and handling system alarms. It extends <see cref="TCMComponentClass"/> to integrate
    /// with the broader system framework.  Typical usage involves initializing the module, establishing a connection to
    /// the server, and processing  incoming messages. The class also registers specific message IDs for handling
    /// system-level events.</remarks>
    public class EAIModuleObj : TCMComponentClass
    {
        private Thread m_receive_thread;
        private ClientWebSocket m_websocket_client;
        private string m_uri_conn = "ws://localhost:8081";
        private CancellationTokenSource m_cancellation_token;

        MessageID[] m_eai_mssg_id; 
        public EAIModuleObj() : base(OBJECTNAME.EAI_MODULE.ToString())
        {
            //TODO: Register all messages to message handler
            initialize();
            register_all_message();
        }


        //================================================================================
        //                              DERIVED FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Derived Functions>
        protected override nint WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            bool isHandled = true;
            switch (msg)
            {
                case (int)MessageID.TM_SYS_INITILIZE:
                    base.OnInitialize();
                    break;
                default:
                    break;
            }
            return base.WindowProc(hWnd, msg, wParam, lParam, ref isHandled);
        }

        #endregion

        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Private Functions>
        private void initialize()
        {
            m_eai_mssg_id = new MessageID[] { MessageID.TM_SYS_TRACE, MessageID.TM_SYS_MSGLOG, MessageID.TM_SYS_CLOSING, MessageID.TM_SYS_ALARM, MessageID.TM_SYS_MSGBOX,
            MessageID.TR_SMOM_SEND, MessageID.TR_SMOM_RCVD, MessageID.TM_SYS_INITILIZE};
            m_websocket_client = new ClientWebSocket();
            m_cancellation_token = new CancellationTokenSource(); 
        }

        private void register_all_message()
        {
            for (int i = 0; i < m_eai_mssg_id.Length; i++)
            {
                TCMSystem.m_msgHandler.RegisterMessage(OBJECTNAME.EAI_MODULE, m_eai_mssg_id[i]);
            }
        }
        private async void connect_to_server()
        {
            try
            {
                Uri uri = new Uri(m_uri_conn);
                await m_websocket_client.ConnectAsync(uri, m_cancellation_token.Token);
                _ = Task.Run(async () => await on_eai_rcv_message());
            }
            catch(Exception ex)
            {
                TCMSystem.m_alarmObj.LogAlarm(ALARMTYPE.EXCEPTION, "SMOM Connection Fail", ex.Message);
            }

        }
        private async Task on_eai_rcv_message()
        {
            byte[] data = new byte[1024 * 4];
            try
            {
                while (m_websocket_client.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result = await m_websocket_client.ReceiveAsync(new ArraySegment<byte>(data), m_cancellation_token.Token);
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string message = Encoding.UTF8.GetString(data, 0, result.Count);
                        //Dispatcher.Invoke(() => TCMSystem.m_alarmObj.AddReceiveMessage(OBJECTNAME.EAI_MODULE, message));
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        //Dispatcher.Invoke(() => TCMSystem.m_alarmObj.AddReceiveMessage(OBJECTNAME.EAI_MODULE, "[SYSTEM] Server closed the connection"));
                        //TODO: Fire alarm here
                    }
                }
            }
            catch (Exception ex)
            {
                TCMSystem.m_alarmObj.LogAlarm(ALARMTYPE.EXCEPTION, "Receiving Message Error", ex.Message);
            }
        }
        #endregion

        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Public Functions>
        public bool On_Initialize_Shutdown()
        {
            connect_to_server();
            return true;
        }
        #endregion

    }
}
