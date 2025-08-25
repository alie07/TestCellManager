using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCellManager.AlarmHandler;
using TestCellManager.MessageHandler;

namespace TestCellManager.Executives
{
    class TCMSystem : TCMComponentClass
    {
        static TCMSystem _instance;
        public static Exec m_tcm_exec;
        public static MessageObj m_msgHandler;
        public static AlarmObj m_alarmObj;
        private static readonly object _lock = new object();

        private TCMSystem() : base(OBJECTNAME.TCM_SYSTEM.ToString()) {
            m_msgHandler = new MessageObj();
            m_tcm_exec = new Exec();
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
