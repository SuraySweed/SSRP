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

        public RSAParameters ConvertPKfromStringToRsaParameter(string returnedMsg)
        {
            string[] str = returnedMsg.Split('@');
            string exponent = str[0];
            string[] exponentArray = exponent.Split('#');
            string modulus = str[1];
            string[] modulusArray = modulus.Split('#');
            byte[] PkExponent = new byte[exponentArray.Length - 1];
            byte[] PkModulus = new byte[modulusArray.Length - 1];
            RSAParameters key = new RSAParameters();

            for (int i = 0; i < exponentArray.Length - 1; i++)
            {
                PkExponent[i] = (byte)Int32.Parse(exponentArray[i]);
            }

            for (int i = 0; i < modulusArray.Length - 1; i++)
            {
                PkModulus[i] = (byte)Int32.Parse(modulusArray[i]);
            }

            key.Exponent = PkExponent;
            key.Modulus = PkModulus;

            return key;
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

        public string EncryptMessageSeveralTimes(string msg, List<string> keys)
        {
            byte[] EncryptedText = new byte[256];
            List<byte[]> encryptedList = new List<byte[]>();
            string finalMessage = ""; //message to send
            string[] splitedMsg = msg.Split(new string[] { "||||" }, StringSplitOptions.None);
            string currentMsg = splitedMsg[1];
            /*
              splited[0] --> msgCode
              splited[1] --> msgData
              splited[2] --> addN
              splited[3] --> add(N-1)
                     *
                     * 
                     * 
              splited[n-2] --> add1
            */
            int msgCounter = 2;
            for (int i = 0; i < keys.Count; i++)
            {
                if (i == 0) 
                    EncryptedText = Encrypt(Encoding.UTF8.GetBytes(currentMsg), ConvertPKfromStringToRsaParameter(keys[i]));
                else
                {
                    // combining the encrypted part with the next address
                    List<byte> list1 = new List<byte>(EncryptedText);
                    List<byte> list2 = new List<byte>(Encoding.UTF8.GetBytes("||||" + splitedMsg[msgCounter]));
                    list1.AddRange(list2);
                    EncryptedText = list1.ToArray();
                    list1.Clear();

                    for(int k=0; k<EncryptedText.Length/32 + 1; k++)
                    {
                        encryptedList.Add(EncryptedText.Skip(32 * k).Take(32).ToArray());
                    }
                    for(int k=0;k<encryptedList.Count; k++)
                    {
                        encryptedList[k] = Encrypt(encryptedList[k], ConvertPKfromStringToRsaParameter(keys[i]));
                        list1.AddRange(encryptedList[k]);
                    }
                    EncryptedText = list1.ToArray();

                    msgCounter++;
                }
            }

            
            finalMessage = Encoding.UTF8.GetString(EncryptedText);
            

            
            return splitedMsg[0]+ "||||" + finalMessage;
        }
    }
}
