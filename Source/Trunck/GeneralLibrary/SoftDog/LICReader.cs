﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.SoftDog
{
    public class LICReader
    {
        public static SoftDogInfo ReadDog(string file)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = new byte[fs.Length];
                    if (fs.Read(data, 0, data.Length) == data.Length)
                    {
                        string sb = new DTEncrypt().DSEncrypt(System.Text.ASCIIEncoding.ASCII.GetString(data));
                        string[] temp = sb.Split(';');
                        var dic = new Dictionary<string, string>();
                        foreach (var str in temp)
                        {
                            string[] kp = str.Split(':');
                            if (kp.Length == 2)
                            {
                                dic.Add(kp[0], kp[1]);
                            }
                        }
                        SoftDogInfo dog = new SoftDogInfo();
                        if (dic.ContainsKey("ProjectNo")) dog.ProjectNo = int.Parse(dic["ProjectNo"]);
                        if (dic.ContainsKey("SoftwareList")) dog.SoftwareList = (SoftwareType)(int.Parse(dic["SoftwareList"]));
                        if (dic.ContainsKey("StartDate")) dog.StartDate = DateTime.Parse(dic["StartDate"]);
                        if (dic.ContainsKey("ExpiredDate")) dog.ExpiredDate = DateTime.Parse(dic["ExpiredDate"]);
                        if (dic.ContainsKey("IsHost")) dog.IsHost = bool.Parse(dic["IsHost"]);
                        if (dic.ContainsKey("DBName")) dog.DBName = dic["DBName"].Trim();
                        if (dic.ContainsKey("DBUser")) dog.DBUser = dic["DBUser"].Trim();
                        if (dic.ContainsKey("DBPassword")) dog.DBPassword = dic["DBPassword"].Trim();
                        return dog;
                    }
                }
            }
            catch
            {
            }
            return null;
        }
    }
}