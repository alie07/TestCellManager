using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCellManager.SystemTCM.Exec
{
    public class TestDataRecordSvc : TCMComponentClass
    {
        MessageID[] m_message_ids;

        //LotSetType MyLOtSet;

        public TestDataRecordSvc() : base(OBJECTNAME.TEST_DATA_RECORD_SVC.ToString())
        {
            OnRegisterMessage(OBJECTNAME.TEST_DATA_RECORD_SVC, MessageID.TM_SYS_INITILIZE, OnInitialize);
            OnRegisterMessage(OBJECTNAME.TEST_DATA_RECORD_SVC, MessageID.TM_SYS_START_SVC, OnStartService);
            OnRegisterMessage(OBJECTNAME.TEST_DATA_RECORD_SVC, MessageID.TM_SYS_STOP_SVC, OnStopService);
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
    }
}
