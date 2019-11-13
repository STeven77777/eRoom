using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MetroOil.LoyaltyOps.Models;

namespace MetroOil.LoyaltyOps.Helpers
{
    public class Helper
    {
        public static string GetClaimsInfo(string type)
        {
            string Value = String.Empty;
            var Identity = ClaimsPrincipal.Current.Identities.First();
            if (type.ToLower() == "userid")
            {
                var claim = Identity.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);
                if (claim != null)
                {
                    return claim.Value;
                }
            }

            if (type.ToLower() == Enums.ACCESS_TOKEN)
            {
                var claim = Identity.Claims.FirstOrDefault(p => p.Type == Enums.ACCESS_TOKEN);
                if (claim != null)
                {
                    return claim.Value;
                }
            }

            if (type.ToLower() == Enums.REFRESH_TOKEN)
            {
                var claim = Identity.Claims.FirstOrDefault(p => p.Type == Enums.REFRESH_TOKEN);
                if (claim != null)
                {
                    return claim.Value;
                }
            }

            if (type.ToLower() == Enums.EXPIRY_TOKEN)
            {
                var claim = Identity.Claims.FirstOrDefault(p => p.Type == Enums.EXPIRY_TOKEN);
                if (claim != null)
                {
                    return claim.Value;
                }
            }

            return Value;
        }

        private static Claim GetClaim(string type)
        {
            string Value = String.Empty;
            var Identity = ClaimsPrincipal.Current.Identities.First();
            var claim = Identity.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);
            return claim;
        }

        public static void SetCliamsTokenInfo(IdentityUserModel model)
        {
            var Identity = ClaimsPrincipal.Current.Identities.First();
            
            Identity.RemoveClaim(GetClaim(Enums.ACCESS_TOKEN));
            Identity.RemoveClaim(GetClaim(Enums.REFRESH_TOKEN));
            Identity.RemoveClaim(GetClaim(Enums.EXPIRY_TOKEN));

            Identity.AddClaim(new Claim(Enums.ACCESS_TOKEN, model.AccessToken));
            Identity.AddClaim(new Claim(Enums.REFRESH_TOKEN, model.RefreshToken));
            Identity.AddClaim(new Claim(Enums.EXPIRY_TOKEN, model.Expiry.ToString()));
        }

        public static string HashingPassword(string password)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(password);
            byte[] hashValue = mySHA256.ComputeHash(bytes);
            int i;
            string tempHash = "";
            for (i = 0; i < hashValue.Length; i++)
            {
                tempHash += String.Format("{0:X2}", hashValue[i]);
                if ((i % 4) == 3) tempHash += " ";
            }
            return tempHash;
        }

        public static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        public static string Encrypt(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Encoding encoding = Encoding.Unicode;
                Byte[] stringBytes = encoding.GetBytes(str);
                StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
                foreach (byte b in stringBytes)
                {
                    sbBytes.AppendFormat("{0:X2}", b);
                }
                return sbBytes.ToString();
                //EncryptionClassLibrary.Encryption.Symmetric sym = new EncryptionClassLibrary.Encryption.Symmetric(EncryptionClassLibrary.Encryption.Symmetric.Provider.DES);
                //EncryptionClassLibrary.Encryption.Data key = new EncryptionClassLibrary.Encryption.Data("L0y@lty@671!");
                //EncryptionClassLibrary.Encryption.Data encryptedData = sym.Encrypt(new EncryptionClassLibrary.Encryption.Data(str), key);
                //return encryptedData.Hex;
            }
            return string.Empty;
        }

        public static string Decrypt(string encryptedString)
        {
            if (!string.IsNullOrEmpty(encryptedString))
            {
                try
                {
                    Encoding encoding = Encoding.Unicode;
                    int numberChars = encryptedString.Length;
                    byte[] bytes = new byte[numberChars / 2];
                    for (int i = 0; i < numberChars; i += 2)
                    {
                        bytes[i / 2] = Convert.ToByte(encryptedString.Substring(i, 2), 16);
                    }
                    return encoding.GetString(bytes);

                    //EncryptionClassLibrary.Encryption.Symmetric sym = new EncryptionClassLibrary.Encryption.Symmetric(EncryptionClassLibrary.Encryption.Symmetric.Provider.DES);
                    //EncryptionClassLibrary.Encryption.Data key = new EncryptionClassLibrary.Encryption.Data("L0y@lty@671!");
                    //EncryptionClassLibrary.Encryption.Data encryptedData = new EncryptionClassLibrary.Encryption.Data();
                    //encryptedData.Hex = encryptedString;
                    //EncryptionClassLibrary.Encryption.Data decryptedData = sym.Decrypt(encryptedData, key);
                    //return decryptedData.ToString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        //private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        //private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        //public static string Encrypt(string text)
        //{
        //    SymmetricAlgorithm algorithm = DES.Create();
        //    ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
        //    byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
        //    byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
        //    return Convert.ToBase64String(outputBuffer);
        //}

        //public static string Decrypt(string text)
        //{
        //    SymmetricAlgorithm algorithm = DES.Create();
        //    ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
        //    byte[] inputbuffer = Convert.FromBase64String(text);
        //    byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
        //    return Encoding.Unicode.GetString(outputBuffer);
        //}
    }
}