using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCellManager.MessageHandler;

namespace TestCellManager.Executives
{
    class TCMSystem : TCMComponentClass
    {
        private static TCMSystem _instance = null;
        public static Exec m_tcm_exec;
        public static MessageObj m_msgHandler;
        private static readonly object _lock = new object();

        private TCMSystem() : base(OBJECTNAME.TCM_SYSTEM.ToString()) { }

        public static TCMSystem tcmSystem
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TCMSystem();
                            m_tcm_exec = new Exec();
                        }
                    }
                }
                return _instance;
            }
        }









    }
}
