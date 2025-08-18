using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCellManager
{
    

    public enum OBJECTNAME
    {
        TCM_SYSTEM,
        TCM_EXEC,
        TCM_MESSAGE_OBJECT,
        TCM_ALARM_OBJECT,
        EAI_MODULE
    }


    public enum MessageID
    {
        //=============================
        //  SYTEM MESSAGE
        //-----------------------------
        TM_SYS_CLOSING = 0xCCCD + 1,
        TM_SYS_MSGBOX,
        TM_SYS_MSGLOG,
        TM_SYS_ALARM,
        TM_SYS_TRACE,

        //=============================
        //  TESTER EQPT MESSAGE
        //-----------------------------
        TE_LOAD_TPGM,
        TE_TGPM_LOADED,
        TE_PGM_EXIT,
        TE_WAFER_BEGIN,
        TE_WAFER_END,
        TE_CASSETTE_END,
        TE_TEST_BEGIN,
        TE_TEST_COMPLETED,
        TE_TEST_MAPCOD,
        TE_EQPT_ONLINE,
        TE_EQPT_OFFLINE,
        TE_STOP_PROBER,
        TE_CLEAN_NEEDLE,

        //=============================
        //  LOT PRODUCTION RUN
        //-----------------------------
        TR_LOT_SET,
        TR_SMOM_SEND,
        TR_SMOM_RCVD,
        TR_LOT_END,
        TR_LOT_CLOSE,
        TR_YIELD_ALARM,
        TR_TFAIL_ALARM,
        TR_EQPT_DOWN
    }

    public enum ALARMTYPE
    {
        NOTIFICATION, EXCEPTION, FATAL
    }


}
