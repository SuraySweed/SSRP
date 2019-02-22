using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    public class RSA
    {
        /*
         byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(message), publicKey);
         byte[] decrypted = Decrypt(encrypted);

         Console.WriteLine("orginal: " + message + "\n");
         Console.WriteLine("encrypted:\n" + BitConverter.ToString(encrypted).Replace("-", ""));
         Console.WriteLine("\ndecrypted :\n" + Encoding.UTF8.GetString(decrypted));
        */
        static private RSAParameters publicKey;
        static private RSAParameters privateKey;

        public RSA()
        {
            generateKeys();
        }

        public void generateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }
        }

        private List<byte[]> getExponentAndMosulusForPublicKey()
        {
            byte[] exponent = publicKey.Exponent;
            byte[] modulus = publicKey.Modulus;

            List<byte[]> ExponentAndModulusList = new List<byte[]>();
            ExponentAndModulusList.Insert(0, exponent);
            ExponentAndModulusList.Insert(1, modulus);

            return ExponentAndModulusList;
        }

        public string getPublicKeyInString()
        {
            List<byte[]> list = new List<byte[]>(getExponentAndMosulusForPublicKey());

            string ExponentAndModulusInString = "";

            for (int i = 0; i < list[0].Length; i++)
            {
                ExponentAndModulusInString = ExponentAndModulusInString + list[0][i] + "#";
            }

            ExponentAndModulusInString += "@"; // to separate between the exponent and the modulus

            for (int i = 0; i < list[1].Length; i++)
            {
                ExponentAndModulusInString = ExponentAndModulusInString + list[1][i] + "#";
            }

            return ExponentAndModulusInString;
        }

        public void ConvertPKfromStringToRsaParameter(string returnedMsg)
        {
            string[] str = returnedMsg.Split('@');
            string exponent = str[0];
            string[] exponentArray = exponent.Split('#');
            string modulus = str[1];
            string[] modulusArray = modulus.Split('#');
            byte[] PkExponent = new byte[exponentArray.Length];
            byte[] PkModulus = new byte[modulusArray.Length - 1];

            for (int i = 0; i < exponentArray.Length; i++)
            {
                PkExponent[i] = (byte)Int32.Parse(exponentArray[i]);
            }

            for (int i = 0; i < modulusArray.Length - 1; i++)
            {
                PkModulus[i] = (byte)Int32.Parse(modulusArray[i]);
            }
            for (int i = 0; i < PkExponent.Length; i++)
            {
                publicKey.Exponent[i] = PkExponent[i];
            }

            for (int i = 0; i < PkModulus.Length; i++)
            {
                publicKey.Modulus[i] = PkModulus[i];
            }
        }

        public byte[] Encrypt(byte[] dataToEcrypt, RSAParameters pkey)
        {
            byte[] encryptedData;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(pkey);
                encryptedData = rsa.Encrypt(dataToEcrypt, true);
            }
            return encryptedData;
        }

        public byte[] Decrypt(byte[] dataToDecrypt)
        {
            byte[] DecryptedData;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);
                DecryptedData = rsa.Decrypt(dataToDecrypt, true);
            }
            return DecryptedData;
        }
    }
}
