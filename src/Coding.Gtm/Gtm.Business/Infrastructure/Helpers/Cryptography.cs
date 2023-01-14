using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Gtm.Business.Infrastructure.Helpers
{
    // <summary>
    // Defines a class that provides the mechanisms to cryptography
    // </summary>
    internal static class Cryptography
    {
        #region Constants private

        /// <summary>
        /// This constant is used to determine the keysize of the encryption algorithm in bits.
        /// We divide this by 8 within the code below to get the equivalent number of bytes.
        /// </summary>
        private const int _keysize = 128;

        /// <summary>
        /// This constant determines the number of iterations for the password bytes generation function.
        /// </summary>
        private const int _derivationIterations = 1000;

        #endregion

        #region Methods public

        /// <summary>
        /// Encrypts the object with password.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="obj">Object that will be encrypted</param>
        /// <param name="password">The password used to derive the key.</param>
        /// <returns>Encrypted object</returns>
        public static string Encrypt<T>(T obj, string password)
        {
            return Encrypt(JsonSerializer.Serialize(obj), password);
        }

        /// <summary>
        /// Decrypts the object with password.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="encryptedObj">Encrypted object</param>
        /// <param name="password">The password used to derive the key.</param>
        /// <returns>Object</returns>
        public static T Decrypt<T>(string encryptedObj, string password)
        {
            return JsonSerializer.Deserialize<T>(Decrypt(encryptedObj, password));
        }

        /// <summary>
        /// Encrypts the value with password.
        /// </summary>
        /// <remarks>
        /// Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
        /// so that the same Salt and IV values can be used when decrypting. 
        /// Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
        /// </remarks>
        /// <param name="value">Value that will be encrypted</param>
        /// <param name="password">The password used to derive the key.</param>
        /// <returns>Encrypted value</returns>
        public static string Encrypt(string value, string password)
        {
            if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException(nameof(value)); }
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentNullException(nameof(password)); }

            byte[] saltStringBytes = Generate128BitsOfRandomEntropy();
            byte[] ivStringBytes = Generate128BitsOfRandomEntropy();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(value);

            using Rfc2898DeriveBytes deriveBytes = new(password, saltStringBytes, _derivationIterations);

            var keyBytes = deriveBytes.GetBytes(_keysize / 8);

            SymmetricAlgorithm symmetricKey = Aes.Create();

            symmetricKey.BlockSize = 128;
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.PKCS7;

            using ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes);
            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = saltStringBytes;

            cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
            cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();

            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Decrypts the value with password.
        /// </summary>
        /// <remarks>
        /// Get the complete stream of bytes that represent:
        /// [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
        /// 
        /// Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
        /// Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
        /// Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
        /// 
        /// </remarks>
        /// <param name="encryptedValue">Encrypted value</param>
        /// <param name="password">The password used to derive the key.</param>
        /// <returns>Decrypted value</returns>
        public static string Decrypt(string encryptedValue, string password)
        {
            if (string.IsNullOrWhiteSpace(encryptedValue)) { throw new ArgumentNullException(nameof(encryptedValue)); }
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentNullException(nameof(password)); }

            byte[] cipherTextBytesWithSaltAndIv = Convert.FromBase64String(encryptedValue);
            byte[] saltStringBytes = cipherTextBytesWithSaltAndIv.Take(_keysize / 8).ToArray();
            byte[] ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(_keysize / 8).Take(_keysize / 8).ToArray();
            byte[] cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((_keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((_keysize / 8) * 2)).ToArray();

            using Rfc2898DeriveBytes deriveBytes = new(password, saltStringBytes, _derivationIterations);

            byte[] keyBytes = deriveBytes.GetBytes(_keysize / 8);

            SymmetricAlgorithm symmetricKey = Aes.Create();

            symmetricKey.BlockSize = 128;
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.PKCS7;

            using ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes);
            using MemoryStream memoryStream = new(cipherTextBytes);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        /// <summary>
        /// Encrypts the query string with password and key salt.
        /// </summary>
        /// <param name="queryString">Query string that will be encrypted</param>
        /// <param name="password">The password to derive the key for</param>
        /// <returns>Encrypted query string</returns>
        public static string EncryptQueryString(string queryString, string password)
        {
            if (string.IsNullOrWhiteSpace(queryString)) { throw new ArgumentNullException(nameof(queryString)); }
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentNullException(nameof(password)); }

            byte[] plainText = Encoding.UTF8.GetBytes(queryString);

            using Aes? rijndaelCipher = Aes.Create("AesManaged");
            PasswordDeriveBytes secretKey = new(Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(GetSaltByPassword(password)));

            using ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainText, 0, plainText.Length);
            cryptoStream.FlushFinalBlock();

            string base64 = Convert.ToBase64String(memoryStream.ToArray());

            string urlEncoded = HttpUtility.UrlEncode(base64);

            return urlEncoded;
        }

        /// <summary>
        /// Decrypts the query string with password and key salt.
        /// </summary>
        /// <param name="encryptedQueryString">Encrypted query string</param>
        /// <param name="password">The password to derive the key for.</param>
        /// <param name="useUrlDecode">Indicates whether it can convert a string that has been encoded for transmission in a URL into a decoded string.
        /// </param>
        /// <returns>Decrypted query string</returns>
        public static string DecryptQueryString(string encryptedQueryString, string password, bool useUrlDecode = false)
        {
            if (string.IsNullOrWhiteSpace(encryptedQueryString)) { throw new ArgumentNullException(nameof(encryptedQueryString)); }
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentNullException(nameof(password)); }

            byte[] encryptedData = Convert.FromBase64String(useUrlDecode ? HttpUtility.UrlDecode(encryptedQueryString) : encryptedQueryString);

            PasswordDeriveBytes secretKey = new(Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(GetSaltByPassword(password)));

            using Aes? rijndaelCipher = Aes.Create("AesManaged");
            using ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
            using MemoryStream memoryStream = new(encryptedData);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainText = new byte[encryptedData.Length];
            int count = cryptoStream.Read(plainText, 0, plainText.Length);
            string utf8 = Encoding.UTF8.GetString(plainText, 0, count);

            return utf8;
        }

        #endregion

        #region Methods private

        /// <summary>
        /// Generates the array with cryptographically secure random bytes.
        /// </summary>
        /// <remarks>
        ///  16 Bytes will give us 128 bits.
        /// </remarks>
        /// <returns>Returns random bytes</returns>
        private static byte[] Generate128BitsOfRandomEntropy()
        {
            var randomBytes = new byte[16];

            using (RandomNumberGenerator rngCsp = RandomNumberGenerator.Create())
            {
                rngCsp.GetBytes(randomBytes);
            }

            return randomBytes;
        }

        /// <summary>
        /// Get The key salt used to derive the key by password
        /// </summary>
        /// <param name="password">The password used to derive the key.</param>
        /// <returns>The key salt used to derive the key</returns>
        private static string GetSaltByPassword(string password)
        {
            return password.Select(c => ((byte)c).ToString()).Aggregate((current, item) => string.Concat(current, "&", item));
        }


        #endregion        
    }
}
