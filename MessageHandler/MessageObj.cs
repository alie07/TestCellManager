using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCellManager.MessageHandler
{
    public class MessageObj : TCMComponentClass
    {
        public MessageObj() : base(OBJECTNAME.TCM_MESSAGE_OBJECT.ToString())
        {
        }


        //================================================================================
        //                              DERIVED FUNCTIONS
        //--------------------------------------------------------------------------------

        protected override nint WindowProc(nint hWnd, uint msg, nint wParam, nint lParam)
        {
            return base.WindowProc(hWnd, msg, wParam, lParam);
        }

        //================================================================================
        //                              PRIVATE FUNCTIONS
        //--------------------------------------------------------------------------------

        //================================================================================
        //                              PUBLIC FUNCTIONS
        //--------------------------------------------------------------------------------


    }
}
