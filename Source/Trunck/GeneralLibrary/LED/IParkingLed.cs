using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.LED
{
    public interface IParkingLed
    {
        string PermanentSentence { get; set; }

        void DisplayMsg(string msg);

        void DisplayMsg(string msg, int persistSeconds);

        void Open();

        void Close();
    }
}
