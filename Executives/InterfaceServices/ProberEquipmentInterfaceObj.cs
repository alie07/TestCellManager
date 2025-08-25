using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCellManager.Executives.InterfaceServices
{
    public class ProberEquipmentInterfaceObj : TCMComponentClass
    {
        public ProberEquipmentInterfaceObj() : base(OBJECTNAME.PROBER_EQUIPMENT_INTERFACE.ToString())
        {
            TCMSystem.m_msgHandler.RegisterMessage(OBJECTNAME.PROBER_EQUIPMENT_INTERFACE, MessageID.TM_SYS_INITILIZE);
        }
        //================================================================================
        //                              DERIVED FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Derived Functions>
        protected override nint WindowProc(nint hWnd, uint msg, nint wParam, nint lParam)
        {
            switch (msg)
            {
                case (uint)MessageID.TM_SYS_INITILIZE:
                    OnInitializeShutdown();
                    break;
                default:
                    break;
            }
            return base.WindowProc(hWnd, msg, wParam, lParam);
        }

        protected override void OnInitializeShutdown()
        {
            // This method can be overridden to handle initialization or shutdown logic.
            base.OnInitializeShutdown();
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
