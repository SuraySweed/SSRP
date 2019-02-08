using System;
using System.Security.Cryptography;
using System.Text;


class RSA
{
    private static RSAParameters publicKey;
    private static RSAParameters privateKey;

    static void generateKeys()
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            publicKey = rsa.ExportParameters(false);
            privateKey = rsa.ExportParameters(true);

            CspParameters cspParams = new CspParameters { ProviderType = 1 };

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(1024, cspParams);

            string publickey = Convert.ToBase64String(rsaProvider.ExportCspBlob(false));
            string privatekey = Convert.ToBase64String(rsaProvider.ExportCspBlob(true));
        }
    }
    static byte[] Encrypt(byte[] data)
    {
        byte[] encryptedData;
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(publicKey);
            encryptedData = rsa.Encrypt(data, true);
        }
        return encryptedData;
    }

    static byte[] Decrypt(byte[] data)
    {
        byte[] DecryptedData;
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(privateKey);
            DecryptedData = rsa.Decrypt(data, true);
        }
        return DecryptedData;
    }
}
