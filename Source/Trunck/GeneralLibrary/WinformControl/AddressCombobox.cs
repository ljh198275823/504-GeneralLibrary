using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinformControl
{
    public partial class AddressComboBox :ComboBox 
    {
        public AddressComboBox()
        {
            InitializeComponent();
        }

        public AddressComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            Items.Clear();
            for (int i = 1; i < 63; i++)
            {
                this.Items.Add(i);
            }
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public byte EntranceAddress
        {
            get
            {
                return (byte)(SelectedIndex == -1 ? 0 : SelectedIndex + 1);
            }
            set
            {
                if (value > 0 && value < 64)
                {
                    SelectedIndex = value - 1;
                }
                else
                {
                    SelectedIndex = -1;
                }
            }
        }
    }
}
