using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Core.UI
{
    public class ItemSelectedEventArgs: EventArgs
    {
        public ItemSelectedEventArgs()
        { }

        public object SelectedItem { get; set; }

        public bool Canceled { get; set; }
    }
}