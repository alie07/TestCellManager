using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCellManager.SystemTCM.Exec;

namespace TestCellManager.SystemTCM
{
    public class ExecutiveObj
    {
        public EAIModuleObj m_eai_module;
        public ProberEquipmentInterfaceObj m_prober_interface;
        public TesterEventInterfaceObj m_tester_event_interface;
        public TestDataRecordSvc m_test_data_record_svc;
        public WaferMapDataSvc m_wafermap_data_svc;

        public Dictionary<OBJECTNAME, TCMComponentClass> m_component_obj_arr;

        public ExecutiveObj() {
            m_component_obj_arr = new Dictionary<OBJECTNAME, TCMComponentClass>();
            m_eai_module = new EAIModuleObj();
            m_component_obj_arr.Add(OBJECTNAME.EAI_MODULE, m_eai_module);
            m_prober_interface = new ProberEquipmentInterfaceObj();
            m_component_obj_arr.Add(OBJECTNAME.PROBER_EQUIPMENT_INTERFACE, m_prober_interface);
            m_tester_event_interface = new TesterEventInterfaceObj();
            m_component_obj_arr.Add(OBJECTNAME.TESTER_EVENT_INTERFACE, m_tester_event_interface);
            m_test_data_record_svc = new TestDataRecordSvc();
            m_component_obj_arr.Add(OBJECTNAME.TEST_DATA_RECORD_SVC, m_test_data_record_svc);
            m_wafermap_data_svc = new WaferMapDataSvc();
            m_component_obj_arr.Add(OBJECTNAME.WAFER_MAP_DATA_SVC, m_wafermap_data_svc);
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
