using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCellManager;

namespace TestCellManager.SystemTCM.AlarmHandler
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

        public void AddReceiveMessage(OBJECTNAME eObjName, string strMessage)
        {
            string _mssg = string.Format("{0} {1} [RECEIVED] : {2}", DateTime.UtcNow.ToString(), eObjName.ToString(), strMessage);
            //TODO: Log storage and Display
        }
    }


}
