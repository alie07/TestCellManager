using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCellManager.SystemTCM.Exec
{
    public class TesterEventInterfaceObj : TCMComponentClass
    {
        MessageID[] m_message_ids;

        //LotSetType MyLOtSet;

        public TesterEventInterfaceObj() : base(OBJECTNAME.TESTER_EVENT_INTERFACE.ToString())
        {
            OnRegisterMessage(OBJECTNAME.TESTER_EVENT_INTERFACE, MessageID.TM_SYS_INITILIZE, OnInitialize);
            OnRegisterMessage(OBJECTNAME.TESTER_EVENT_INTERFACE, MessageID.TM_SYS_START_SVC, OnStartService);
            OnRegisterMessage(OBJECTNAME.TESTER_EVENT_INTERFACE, MessageID.TM_SYS_STOP_SVC, OnStopService);
        }

        //================================================================================
        //                              DERIVED FUNCTIONS
        //--------------------------------------------------------------------------------
        protected override IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            bool isHandled = true;
            foreach (var item in m_mssg_id)
            {
                if ((int)item.Key == msg)
                {
                    item.Value.Invoke();
                }
            }
            return base.WindowProc(hwnd, msg, wParam, lParam, ref isHandled);
        }

        void OnLotSet()
        {
            //todo: do something with MyLotSet
            //MyLOtSet = null;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
        }
        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
        protected override void OnStartService()
        {
            base.OnStartService();
        }
        protected override void OnStopService()
        {
            base.OnStopService();
        }


        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------

        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------


        //================================================================================
        //                              DERIVED FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <MODEL>
        private string m_mssg;
        public string MSSG
        {
            get => m_mssg;
            set
            {
                if (m_mssg != value)
                {
                    m_mssg = value;
                    OnPropertyChanged();
                }
            }
            #endregion
        }
    }
}
