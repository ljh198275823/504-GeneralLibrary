using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace LJH.GeneralLibrary
{
    public static class RSASignHelper
    {
        public static bool VerifySignedHash(string data, string sign, string Key)
        {
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.FromXmlString(Key);
                var DataToVerify = HexStringConverter.StringToHex(data);
                var SignedData = HexStringConverter.StringToHex(sign);
                return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
