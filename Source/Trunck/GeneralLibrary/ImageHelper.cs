using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace LJH.GeneralLibrary
{
    public class ImageHelper
    {
        private static string _dic = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache); 

        public static byte[] GetBytesFromPhoto(Image photo)
        {
            if (photo != null)
            {
                string path = Path.Combine(_dic, Guid.NewGuid ().ToString () + ".jpg");
                photo.Save(path, ImageFormat.Jpeg);
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read ))
                {
                    byte[] bs = new byte[fs.Length];
                    fs.Position = 0;
                    fs.Read(bs, 0, (int)fs.Length);
                    return bs;
                }
            }
            else
            {
                return null;
            }
        }

        public static void SaveImage(Image img,string file)
        {
            if (img != null)
            {
                byte[] data = GetBytesFromPhoto(img);
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                }
            }
        }

        public static Image GetImageFromBytes(byte[] photo)
        {
            try
            {
                if (photo != null)
                {
                    string path = Path.Combine(_dic, Guid.NewGuid().ToString() + ".jpg");
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(photo, 0, photo.Length);
                    }
                    Image img = Image.FromFile(path);
                    return img;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static Image CutBitMap(Rectangle div, Bitmap src)
        {
            Bitmap b = new Bitmap(div.Width, div.Height);
            for (int j = 0; j < div.Height; j++)
            {
                for (int i = 0; i < div.Width; i++)
                {
                    b.SetPixel(i, j, src.GetPixel(div.Left + i, div.Top + j));
                }
            }
            string path = Path.Combine(_dic, Guid.NewGuid().ToString() + ".jpg");
            b.Save(path, ImageFormat.Jpeg);
            Image img = Image.FromFile (path);
            return img;
        }
    }
}
