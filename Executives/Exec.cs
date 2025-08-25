using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCellManager.Executives.InterfaceServices;

namespace TestCellManager.Executives
{
    public class Exec
    {
        public EAIModuleObj m_eai_module;
        public ProberEquipmentInterfaceObj m_prober_interface;
        public Dictionary<OBJECTNAME, TCMComponentClass> m_component_obj_arr;

        public Exec() {
            m_component_obj_arr = new Dictionary<OBJECTNAME, TCMComponentClass>();
            m_eai_module = new EAIModuleObj();
            m_component_obj_arr.Add(OBJECTNAME.EAI_MODULE, m_eai_module);
            m_prober_interface = new ProberEquipmentInterfaceObj();
            m_component_obj_arr.Add(OBJECTNAME.PROBER_EQUIPMENT_INTERFACE, m_prober_interface);
        }


        //================================================================================
        //                              DERIVED FUNCTIONS
        //--------------------------------------------------------------------------------
        protected virtual nint WindowProc(nint hWnd, uint msg, nint wParam, nint lParam)
        {
            // Handle window messages here if needed
            return 0;
        }
        protected virtual void OnInitializeShutdown()
        {
            // This method can be overridden to handle initialization or shutdown logic.
        }
        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Private Functions>
        #endregion
        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------
        #region <Public Functions>
        public nint GetWndwHndle(OBJECTNAME obj_name)
        {
            if (m_component_obj_arr.TryGetValue(obj_name, out TCMComponentClass component))
            {
                return component.Handle;
            }
            return 0; // Return 0 if the object is not found
        }
        #endregion


    }
}
