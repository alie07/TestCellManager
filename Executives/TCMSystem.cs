using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCellManager.SystemTCM.AlarmHandler;
using TestCellManager.SystemTCM.MessageHandler;
using TestCellManager.SystemTCM;

namespace TestCellManager
{
    class TCMSystem : TCMComponentClass
    {
        static TCMSystem _instance;
        public static ExecutiveObj m_tcm_exec;
        public static MessageDispatchCtrl m_msgHandler;
        public static AlarmObj m_alarmObj;
        private static readonly object _lock = new object();

        private TCMSystem() : base(OBJECTNAME.TCM_SYSTEM.ToString()) {
            m_msgHandler = new MessageDispatchCtrl();
            m_tcm_exec = new ExecutiveObj();
            m_alarmObj = new AlarmObj();
        }

        public static TCMSystem tcmSystem
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TCMSystem();
                }
                return _instance;
            }
        }









    }
}
