using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestCellManager.SystemTCM.Exec;

namespace TestCellManager.SystemTCM.MessageHandler
{
    public class MessageDispatchCtrl : TCMComponentClass
    {
        /// <summary>
        /// Message dictionary mapping object names to their registered message IDs.
        /// </summary>
        Dictionary<OBJECTNAME, HashSet<MessageID>> m_obj_message;

        /// <summary>
        /// Lock object for thread-safe message posting.
        /// </summary>
        object m_lock_post_mssg = new object();


        /// <summary>
        /// Initializes a new instance of the MessageObj class.
        /// Sets up the message dictionary for tracking registered messages per object.
        /// </summary>
        public MessageDispatchCtrl() : base (OBJECTNAME.TCM_MESSAGE_OBJECT.ToString()) 
        {
            m_obj_message = new Dictionary<OBJECTNAME, HashSet<MessageID>>();
        }

        //================================================================================
        //                              DERIVED FUNCTIONS
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Handles incoming window messages.
        /// - If the message ID is defined in MessageID, forwards the message to registered objects.
        /// </summary>
        /// <param name="hWnd">Window handle of the sender.</param>
        /// <param name="msg">Message identifier.</param>
        /// <param name="wParam">Additional message information.</param>
        /// <param name="lParam">Additional message information.</param>
        /// <returns>Result of base WindowProc processing.</returns>
        ///
        protected override nint WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            bool isHandled = true;
            bool exists = Enum.IsDefined(typeof(MessageID), (int)msg);
            if (exists)
            {
                send_message(msg, wParam, lParam);
            }
            return base.WindowProc(hWnd, msg, wParam, lParam, ref isHandled);
        }

        /// <summary>
        /// 
        /// </summary>
        private void HelloWorld()
        {
            Console.WriteLine("Hello, World!");
        }

        

        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------

        /// <summary>
        /// Registers a message ID for a specific object in the message dictionary.
        /// Adds the message ID to the object's set of registered messages.
        /// </summary>
        /// <param name="objName">The object name (enum value).</param>
        /// <param name="messageId">The message ID to register.</param>
        /// <returns>True if registration succeeds.</returns>
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

        /// <summary>
        /// Sends a message to all registered objects that have the specified message ID.
        /// Iterates through the message dictionary and posts the message to each object's window handle
        /// if the message ID is registered for that object.
        /// </summary>
        /// <param name="mssg">The message ID to send (as uint).</param>
        /// <param name="wParam">Additional message-specific information (IntPtr).</param>
        /// <param name="lParam">Additional message-specific information (IntPtr).</param>
        private void send_message(int mssg, IntPtr wParam, IntPtr lParam)
        {
            foreach (var item in m_obj_message)
            {
                if (is_message_exist(item.Key, (MessageID)mssg))
                {
                    PostMessage(TCMSystem.m_tcm_exec.GetWndwHndle(item.Key), (uint)mssg, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }

        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Checks if a specific message ID is registered for the given object.
        /// </summary>
        /// <param name="objectName">The object name to check.</param>
        /// <param name="messageId">The message ID to look for.</param>
        /// <returns>True if the message ID exists for the object; otherwise, false.</returns>
        private bool is_message_exist(OBJECTNAME objectName, MessageID messageId)
        {
            return m_obj_message.TryGetValue(objectName, out var messages)
                   && messages.Contains(messageId);
        }

        /// <summary>
        /// Public method to register a message ID for an object.
        /// Calls the internal register_message function.
        /// </summary>
        /// <param name="objName">The object name (enum value).</param>
        /// <param name="messageId">The message ID to register.</param>
        /// <returns>True if registration succeeds.</returns>
        public bool RegisterMessage(OBJECTNAME objName, MessageID messageId)
        {
            return register_message(objName, messageId);
        }

        /// <summary>
        /// Sends the TM_SYS_INITILIZE message to all objects that have registered for it.
        /// Used during system initialization or shutdown.
        /// </summary>
        public void OnInitializeShutdown()
        {
            foreach (var item in m_obj_message)
            {
                if (is_message_exist(item.Key, MessageID.TM_SYS_INITILIZE))
                {
                    PostMessage(TCMSystem.m_tcm_exec.GetWndwHndle(item.Key), (uint)MessageID.TM_SYS_INITILIZE, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }

        /// <summary>
        /// Posts a message to the current object's window handle in a thread-safe manner.
        /// Used to assert and send a message directly.
        /// </summary>
        /// <param name="msgID">The message ID to post.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        public void AssertPostMessage(MessageID msgID, IntPtr wParam, IntPtr lParam)
        {
            lock (m_obj_message)
            {
               PostMessage(Handle, (uint)msgID, IntPtr.Zero, IntPtr.Zero);
            }
        }


    }
}
