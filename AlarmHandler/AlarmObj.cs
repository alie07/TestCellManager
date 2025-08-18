using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCellManager;

namespace TestCellManager.AlarmHandler
{
    public class AlarmObj : TCMComponentClass
    {
        public AlarmObj() : base(OBJECTNAME.TCM_ALARM_OBJECT.ToString())
        {

        }

        public void LogAlarm(ALARMTYPE eAlarmTyp, string strCaption, string strMessage)
        {
            //TODO: Alarm Storage / Alarm Prompt
        }
    }
}
