using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestCellManager.Executives;

namespace TestCellManager.MessageHandler
{
    public class MessageObj : TCMComponentClass
    {
        Dictionary<OBJECTNAME, HashSet<MessageID>> m_obj_message;
        object m_lock_post_mssg = new object();


        /// <summary>
        /// Message Object Constructor
        /// </summary>
        public MessageObj() : base (OBJECTNAME.TCM_MESSAGE_OBJECT.ToString()) 
        {
            m_obj_message = new Dictionary<OBJECTNAME, HashSet<MessageID>>();
        }


        //================================================================================
        //                              DERIVED FUNCTIONS
        //--------------------------------------------------------------------------------


        #region <Derived Functions>
        /// <summary>
        /// Override WinProw Function
        /// 
        /// </summary>
        /// <param name="hWnd">Sender Handle</param>
        /// <param name="msg">Message from Message ID</param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        protected override nint WindowProc(nint hWnd, uint msg, nint wParam, nint lParam)
        {
            bool exists = Enum.IsDefined(typeof(MessageID), (int)msg);
            if (exists)
            {
                send_message(msg, wParam, lParam);
            }
            return base.WindowProc(hWnd, msg, wParam, lParam);
        }

        #endregion

        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Private Functions>

        /// <summary>
        /// Registers the message ID in the object’s message dictionary
        /// Enregistre l’ID du message dans le dictionnaire de messages de l’objet.
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        private bool register_message(OBJECTNAME objName, MessageID messageId)
        {
            if (!m_obj_message.ContainsKey(objName))
            {
                m_obj_message.Add(objName, new HashSet<MessageID> { messageId });
            }
            else
            {
                m_obj_message[objName].Add(messageId);

            }
                return true;
        }

        private void send_message(uint mssg, IntPtr wParam, IntPtr lParam)
        {
            foreach (var item in m_obj_message)
            {
                if (is_message_exist(item.Key, (MessageID)mssg))
                {
                    WinAPI.PostMessage(TCMSystem.m_tcm_exec.GetWndwHndle(item.Key), mssg, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }

        private bool is_message_exist(OBJECTNAME objectName, MessageID messageId)
        {
            return m_obj_message.TryGetValue(objectName, out var messages)
                   && messages.Contains(messageId);
        }
        #endregion

        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Public Functions>

        public bool RegisterMessage(OBJECTNAME objName, MessageID messageId)
        {
            return register_message(objName, messageId);
        }



        public void OnInitializeShutdown()
        {
            foreach (var item in m_obj_message)
            {
                if (is_message_exist(item.Key, MessageID.TM_SYS_INITILIZE))
                {
                    WinAPI.PostMessage(TCMSystem.m_tcm_exec.GetWndwHndle(item.Key), (uint)MessageID.TM_SYS_INITILIZE, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }

        public void AssertPostMessage( MessageID msgID, IntPtr wParam, IntPtr lParam)
        {
            lock (m_obj_message)
            {
               WinAPI.PostMessage(Handle, (uint)msgID, IntPtr.Zero, IntPtr.Zero);
            }
        }

        #endregion

    }
}
