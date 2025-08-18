using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TestCellManager;

namespace TestCellManager.Executives
{
    public class EAIModuleObj : TCMComponentClass
    {
        private Thread m_receive_thread;
        private ClientWebSocket m_websocket_client;
        private string m_uri_conn;
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

        protected override nint WindowProc(nint hWnd, uint msg, nint wParam, nint lParam)
        {

            return base.WindowProc(hWnd, msg, wParam, lParam);
        }

        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------
        private void initialize()
        {
            m_eai_mssg_id = new MessageID[] { MessageID.TM_SYS_TRACE, MessageID.TM_SYS_MSGLOG, MessageID.TM_SYS_CLOSING, MessageID.TM_SYS_ALARM, MessageID.TM_SYS_MSGBOX,
            MessageID.TR_SMOM_SEND, MessageID.TR_SMOM_RCVD};
            m_websocket_client = new ClientWebSocket();
            m_cancellation_token = new CancellationTokenSource();
        }

        private void register_all_message()
        {
            for (int i = 0; m_eai_mssg_id.Length > 0; i++)
            {
                //TODO: Call message handler function "REGISTER_MESSAGE"
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


            }

        }
        private async Task on_eai_rcv_message()
        {
        }

        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------
        public bool On_Initialize_Shutdown()
        {
            return false;
        }


    }
}
