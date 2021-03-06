using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarcodeLibTest
{
    /// <summary>
    /// This form is a test form to show what all you can do with the Barcode Library.
    /// Only one call is actually needed to do the encoding and return the image of the 
    /// barcode but the rest is just flare and user interface ... stuff.
    /// </summary>
    public partial class TestApp : Form
    {
        Barcode._1D.BarcodeGenerator b = new Barcode._1D.BarcodeGenerator();

        public TestApp()
        {
            InitializeComponent();
        }

        private void TestApp_Load(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(1, 1);
            temp.SetPixel(0, 0, this.BackColor);
            barcode.Image = (Image)temp;
            this.cbEncodeType.SelectedIndex = 0;
            this.cbBarcodeAlign.SelectedIndex = 0;
            this.cbLabelLocation.SelectedIndex = 0;

            this.cbRotateFlip.DataSource = System.Enum.GetNames(typeof(RotateFlipType));

            int i = 0;
            foreach (object o in cbRotateFlip.Items)
            {
                if (o.ToString().Trim().ToLower() == "rotatenoneflipnone")
                    break;
                i++;
            }//foreach
            this.cbRotateFlip.SelectedIndex = i;

            //Show library version
            this.tslblLibraryVersion.Text = "Barcode Library Version: " + Barcode._1D.BarcodeGenerator.Version.ToString();

            this.btnBackColor.BackColor = this.b.BackColor;
            this.btnForeColor.BackColor = this.b.ForeColor;
        }//Form1_Load

        private void btnEncode_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            int W = Convert.ToInt32(this.txtWidth.Text.Trim());
            int H = Convert.ToInt32(this.txtHeight.Text.Trim());
            b.Alignment = Barcode._1D.AlignmentPositions.CENTER;

            //barcode alignment
            switch (cbBarcodeAlign.SelectedItem.ToString().Trim().ToLower())
            {
                case "left": b.Alignment = Barcode._1D.AlignmentPositions.LEFT; break;
                case "right": b.Alignment = Barcode._1D.AlignmentPositions.RIGHT; break;
                default: b.Alignment = Barcode._1D.AlignmentPositions.CENTER; break;
            }//switch

            Barcode._1D.TYPE type = Barcode._1D.TYPE.UNSPECIFIED;
            switch (cbEncodeType.SelectedItem.ToString().Trim())
            {
                case "UPC-A": type = Barcode._1D.TYPE.UPCA; break;
                case "UPC-E": type = Barcode._1D.TYPE.UPCE; break;
                case "UPC 2 Digit Ext.": type = Barcode._1D.TYPE.UPC_SUPPLEMENTAL_2DIGIT; break;
                case "UPC 5 Digit Ext.": type = Barcode._1D.TYPE.UPC_SUPPLEMENTAL_5DIGIT; break;
                case "EAN-13": type = Barcode._1D.TYPE.EAN13; break;
                case "JAN-13": type = Barcode._1D.TYPE.JAN13; break;
                case "EAN-8": type = Barcode._1D.TYPE.EAN8; break;
                case "ITF-14": type = Barcode._1D.TYPE.ITF14; break;
                case "Codabar": type = Barcode._1D.TYPE.Codabar; break;
                case "PostNet": type = Barcode._1D.TYPE.PostNet; break;
                case "Bookland/ISBN": type = Barcode._1D.TYPE.BOOKLAND; break;
                case "Code 11": type = Barcode._1D.TYPE.CODE11; break;
                case "Code 39": type = Barcode._1D.TYPE.CODE39; break;
                case "Code 39 Extended": type = Barcode._1D.TYPE.CODE39Extended; break;
                case "Code 93": type = Barcode._1D.TYPE.CODE93; break;
                case "LOGMARS": type = Barcode._1D.TYPE.LOGMARS; break;
                case "MSI": type = Barcode._1D.TYPE.MSI_Mod10; break;
                case "Interleaved 2 of 5": type = Barcode._1D.TYPE.Interleaved2of5; break;
                case "Standard 2 of 5": type = Barcode._1D.TYPE.Standard2of5; break;
                case "Code 128": type = Barcode._1D.TYPE.CODE128; break;
                case "Code 128-A": type = Barcode._1D.TYPE.CODE128A; break;
                case "Code 128-B": type = Barcode._1D.TYPE.CODE128B; break;
                case "Code 128-C": type = Barcode._1D.TYPE.CODE128C; break;
                case "Telepen": type = Barcode._1D.TYPE.TELEPEN; break;
                case "FIM": type = Barcode._1D.TYPE.FIM; break;
                case "Pharmacode": type = Barcode._1D.TYPE.PHARMACODE; break;
                default: MessageBox.Show("Please specify the encoding type."); break;
            }//switch

            try
            {
                if (type != Barcode._1D.TYPE.UNSPECIFIED)
                {
                    b.IncludeLabel = this.chkGenerateLabel.Checked;

                    b.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), this.cbRotateFlip.SelectedItem.ToString(), true);

                    //label alignment and position
                    switch (this.cbLabelLocation.SelectedItem.ToString().Trim().ToUpper())
                    {
                        case "BOTTOMLEFT":  b.LabelPosition = Barcode._1D.LabelPositions.BOTTOMLEFT; break;
                        case "BOTTOMRIGHT": b.LabelPosition = Barcode._1D.LabelPositions.BOTTOMRIGHT; break;
                        case "TOPCENTER": b.LabelPosition = Barcode._1D.LabelPositions.TOPCENTER; break;
                        case "TOPLEFT": b.LabelPosition = Barcode._1D.LabelPositions.TOPLEFT; break;
                        case "TOPRIGHT": b.LabelPosition = Barcode._1D.LabelPositions.TOPRIGHT; break;
                        default: b.LabelPosition = Barcode._1D.LabelPositions.BOTTOMCENTER; break;
                    }//switch

                    //===== Encoding performed here =====
                    barcode.Image = b.Encode(type, this.txtData.Text.Trim(), this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H);
                    //===================================
                    
                    //show the encoding time
                    this.lblEncodingTime.Text = "(" + Math.Round(b.EncodingTime, 0, MidpointRounding.AwayFromZero).ToString() + "ms)";
                    
                    txtEncoded.Text = b.EncodedValue;

                    tsslEncodedType.Text = "Encoding Type: " + b.EncodedType.ToString();
                }//if

                barcode.Width = barcode.Image.Width;
                barcode.Height = barcode.Image.Height;
                
                //reposition the barcode image to the middle
                barcode.Location = new Point((this.groupBox2.Location.X + this.groupBox2.Width / 2) - barcode.Width / 2, (this.groupBox2.Location.Y + this.groupBox2.Height / 2) - barcode.Height / 2);
            }//try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//catch
        }//btnEncode_Click

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tif)|*.tif";
            sfd.FilterIndex = 2;
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Barcode._1D.SaveTypes savetype = Barcode._1D.SaveTypes.UNSPECIFIED;
                switch (sfd.FilterIndex)
                {
                    case 1: /* BMP */  savetype = Barcode._1D.SaveTypes.BMP; break;
                    case 2: /* GIF */  savetype = Barcode._1D.SaveTypes.GIF; break;
                    case 3: /* JPG */  savetype = Barcode._1D.SaveTypes.JPG; break;
                    case 4: /* PNG */  savetype = Barcode._1D.SaveTypes.PNG; break;
                    case 5: /* TIFF */ savetype = Barcode._1D.SaveTypes.TIFF; break;
                    default: break;
                }//switch
                b.SaveImage(sfd.FileName, savetype);
            }//if
        }//btnSave_Click

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            barcode.Location = new Point((this.groupBox2.Location.X + this.groupBox2.Width / 2) - barcode.Width / 2, (this.groupBox2.Location.Y + this.groupBox2.Height / 2) - barcode.Height / 2);
        }//splitContainer1_SplitterMoved

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cdialog = new ColorDialog())
            {
                cdialog.AnyColor = true;
                if (cdialog.ShowDialog() == DialogResult.OK)
                {
                    this.b.ForeColor = cdialog.Color;
                    this.btnForeColor.BackColor = cdialog.Color;
                }//if
            }//using
        }//btnForeColor_Click

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cdialog = new ColorDialog())
            {
                cdialog.AnyColor = true;
                if (cdialog.ShowDialog() == DialogResult.OK)
                {
                    this.b.BackColor = cdialog.Color;
                    this.btnBackColor.BackColor = cdialog.Color;
                }//if
            }//using
        }//btnBackColor_Click

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            btnEncode_Click(sender, e);

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "XML Files|*.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName))
                    {
                        sw.Write(b.XML);
                    }//using
                }//if
            }//using
        }//btnGetXML_Click

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (Barcode._1D.BarcodeXML XML = new Barcode._1D.BarcodeXML())
                    {
                        XML.ReadXml(ofd.FileName);

                        //load image from xml
                        this.barcode.Width = XML.Barcode[0].ImageWidth;
                        this.barcode.Height = XML.Barcode[0].ImageHeight;
                        this.barcode.Image = Barcode._1D.BarcodeGenerator.GetImageFromXML(XML);

                        //populate the screen
                        this.txtData.Text = XML.Barcode[0].RawData;
                        this.chkGenerateLabel.Checked = XML.Barcode[0].IncludeLabel;

                        switch (XML.Barcode[0].Type)
                        {
                            case "UCC12":
                            case "UPCA":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC-A");
                                break;
                            case "UCC13":
                            case "EAN13":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("EAN-13");
                                break;
                            case "Interleaved2of5":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Interleaved 2 of 5");
                                break;
                            case "Industrial2of5":
                            case "Standard2of5":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Standard 2 of 5");
                                break;
                            case "LOGMARS":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("LOGMARS");
                                break;
                            case "CODE39":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 39");
                                break;
                            case "CODE39Extended":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 39 Extended");
                                break;
                            case "Codabar":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Codabar");
                                break;
                            case "PostNet":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("PostNet");
                                break;
                            case "ISBN":
                            case "BOOKLAND":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Bookland/ISBN");
                                break;
                            case "JAN13":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("JAN-13");
                                break;
                            case "UPC_SUPPLEMENTAL_2DIGIT":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC 2 Digit Ext.");
                                break;
                            case "MSI_Mod10":
                            case "MSI_2Mod10":
                            case "MSI_Mod11":
                            case "MSI_Mod11_Mod10":
                            case "Modified_Plessey":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("MSI");
                                break;
                            case "UPC_SUPPLEMENTAL_5DIGIT":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC 5 Digit Ext.");
                                break;
                            case "UPCE":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC-E");
                                break;
                            case "EAN8":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("EAN-8");
                                break;
                            case "USD8":
                            case "CODE11":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 11");
                                break;
                            case "CODE128":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128");
                                break;
                            case "CODE128A":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128-A");
                                break;
                            case "CODE128B":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128-B");
                                break;
                            case "CODE128C":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128-C");
                                break;
                            case "ITF14":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("ITF-14");
                                break;
                            case "CODE93":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 93");
                                break;
                            case "FIM":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("FIM");
                                break;
                            case "Pharmacode":
                                this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Pharmacode");
                                break;

                            default: throw new Exception("ELOADXML-1: Unsupported encoding type in XML.");
                        }//switch

                        this.txtEncoded.Text = XML.Barcode[0].EncodedValue;
                        this.btnForeColor.BackColor = ColorTranslator.FromHtml(XML.Barcode[0].Forecolor);
                        this.btnBackColor.BackColor = ColorTranslator.FromHtml(XML.Barcode[0].Backcolor); ;
                        this.txtWidth.Text = XML.Barcode[0].ImageWidth.ToString();
                        this.txtHeight.Text = XML.Barcode[0].ImageHeight.ToString();
                        
                        //populate the local object
                        btnEncode_Click(sender, e);

                        //reposition the barcode image to the middle
                        barcode.Location = new Point((this.groupBox2.Location.X + this.groupBox2.Width / 2) - barcode.Width / 2, (this.groupBox2.Location.Y + this.groupBox2.Height / 2) - barcode.Height / 2);
                    }//using
                }//if
            }//using
        }

    }//class
}//namespace