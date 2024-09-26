namespace Biokudi_Backend.Application.Utilities
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class RSAUtility
    {
        private readonly RsaKeys _rsaKeys;

        public RSAUtility()
        {
            _rsaKeys = new RsaKeys();
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                _rsaKeys.PrivateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                _rsaKeys.PublicKey = rsa.ExportSubjectPublicKeyInfoPem();
            }
        }

        public string EncryptWithPublicKey(string data)
        {
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
            using (var rsa = RSA.Create())
            {
                rsa.ImportFromPem(_rsaKeys.PublicKey);
                byte[] encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(encryptedData);
            }
        }

        public string DecryptWithPrivateKey(string encryptedData)
        {
            byte[] dataToDecrypt = Convert.FromBase64String(encryptedData);
            using (var rsa = RSA.Create())
            {
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(_rsaKeys.PrivateKey), out _);
                byte[] decryptedData = rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }

        public string GetPublicKey()
        {
            return _rsaKeys.PublicKey.Trim();
        }
    }

    public class RsaKeys
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }

    public class PasswordRequest
    {
        public string Password { get; set; }
    }

    public static class RSAExtensions
    {
        public static string ToStringPem(this byte[] data, string label)
        {
            var pemBuilder = new StringBuilder();
            pemBuilder.AppendLine($"-----BEGIN {label}-----");

            var base64 = Convert.ToBase64String(data);
            for (int i = 0; i < base64.Length; i += 64)
            {
                if (i + 64 < base64.Length)
                {
                    pemBuilder.AppendLine(base64.Substring(i, 64));
                }
                else
                {
                    pemBuilder.AppendLine(base64.Substring(i));
                }
            }

            pemBuilder.AppendLine($"-----END {label}-----");
            return pemBuilder.ToString();
        }
        public static string ExportSubjectPublicKeyInfoPem(this RSA rsa)
        {
            var publicKey = rsa.ExportSubjectPublicKeyInfo();
            return publicKey.ToStringPem("PUBLIC KEY");
        }
    }
}
