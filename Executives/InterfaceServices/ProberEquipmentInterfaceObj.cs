using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCellManager.SystemTCM.Exec
{
    public class ProberEquipmentInterfaceObj : TCMComponentClass
    {
        
        public ProberEquipmentInterfaceObj() : base(OBJECTNAME.PROBER_EQUIPMENT_INTERFACE.ToString())
        {
            OnRegisterMessage(OBJECTNAME.PROBER_EQUIPMENT_INTERFACE, MessageID.TM_SYS_INITILIZE, OnInitialize);
            OnRegisterMessage(OBJECTNAME.PROBER_EQUIPMENT_INTERFACE, MessageID.TM_SYS_START_SVC, OnStartService);
            OnRegisterMessage(OBJECTNAME.PROBER_EQUIPMENT_INTERFACE, MessageID.TM_SYS_STOP_SVC, OnStopService);
        }
        //================================================================================
        //                              DERIVED FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Derived Functions>
        protected override nint WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            bool isHandled = true;
            foreach (var item in m_mssg_id)
            {
                if ((int)item.Key == msg)
                {
                    item.Value.Invoke();
                }
            }
            return base.WindowProc(hWnd, msg, wParam, lParam, ref isHandled);
        }

        protected override void OnInitialize()
        {
            // This method can be overridden to handle initialization or shutdown logic.
            base.OnInitialize();
        }


        #endregion
        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Private Functions>

        #endregion
        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Public Functions>

        #endregion
    }
}
