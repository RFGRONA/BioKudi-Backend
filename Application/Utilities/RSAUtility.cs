using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace Biokudi_Backend.Application.Utilities
{
    public class RSAUtility
    {
        private readonly RSA _rsa;
        private readonly RsaKeys _rsaKeys;

        public RSAUtility(IConfiguration configuration)
        {
            _rsaKeys = configuration.GetSection("RsaKeys").Get<RsaKeys>();
            if (_rsaKeys == null || string.IsNullOrEmpty(_rsaKeys.PrivateKey) || string.IsNullOrEmpty(_rsaKeys.PublicKey))
                throw new InvalidOperationException("RSA keys are not configured correctly.");
            _rsa = RSA.Create();
            _rsaKeys.PrivateKey = Encoding.UTF8.GetString(Convert.FromBase64String(_rsaKeys.PrivateKey));
            _rsaKeys.PublicKey = Encoding.UTF8.GetString(Convert.FromBase64String(_rsaKeys.PublicKey));
            _rsa.ImportFromPem(_rsaKeys.PrivateKey);
        }

        public string EncryptWithPublicKey(string data)
        {
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
            byte[] encryptedData = _rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA512);
            return Convert.ToBase64String(encryptedData);
        }

        public string DecryptWithPrivateKey(string encryptedData)
        {
            byte[] dataToDecrypt = Convert.FromBase64String(encryptedData);
            byte[] decryptedData = _rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA512);
            return Encoding.UTF8.GetString(decryptedData);
        }

        public string GetPublicKey()
        {
            return _rsaKeys.PublicKey;
        }
    }

    public class RsaKeys
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }
}
