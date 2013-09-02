using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Collections;
using WIA;

namespace LJH.GeneralLibrary.WinformControl
{
    public class Scaner
    {
        /// <summary>
        /// 扫描证件
        /// </summary>
        /// <returns></returns>
        public ImageFile GetCameraImage()
        {
            try
            {
                ImageFile img = new ImageFile();
                DeviceManager manager = new DeviceManager();
                Device device = null;
                foreach (WIA.DeviceInfo info in manager.DeviceInfos)
                {
                    if (info.Type != WiaDeviceType.ScannerDeviceType)
                        continue;
                    device = info.Connect();
                    break;
                }
                Item item = device.Items[1];
                CommonDialog cdc = new WIA.CommonDialog();
                img = cdc.ShowAcquireImage(WIA.WiaDeviceType.ScannerDeviceType,
WIA.WiaImageIntent.TextIntent, WIA.WiaImageBias.MaximizeQuality, "{00000000-0000-0000-0000-000000000000}", false, false, false);
                if (img != null)
                {
                    return img;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        /// <summary>
        /// 顺时针旋转90°
        /// </summary>
        /// <returns></returns>
        public Image TurnRight(ImageFile imafile)
        {
            if (imafile != null)
            {
                try
                {
                    ImageProcess ip = new ImageProcess();
                    object filterName = "RotateFlip";
                    Object propertyName = "RotationAngle";
                    Object propertyValue = 90;
                    ip.Filters.Add(ip.FilterInfos.get_Item(ref filterName).FilterID, 0);
                    ip.Filters[1].Properties.get_Item(ref propertyName).set_Value(ref propertyValue);

                    var buffer = ip.Apply(imafile).FileData.get_BinaryData() as byte[];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(buffer, 0, buffer.Length);
                        return Image.FromStream(ms);
                    }
                }
                catch
                {
                    return null;
                }

            }
            return null;
        }
    }
}
