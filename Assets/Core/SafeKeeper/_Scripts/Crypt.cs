using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SafeKepper
{
    namespace Crypting
    { 
        public class Crypt
        {
            #region Float

            /// <summary>
            /// Crypt to float
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static float GetFloat(string crypt, string salt)
            {
                string decrypt = StringCipher.Decrypt(crypt, salt);//decrypt string

                return float.Parse(decrypt);//transform to float
            }

            /// <summary>
            /// Float to crypt
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetFloat(float input, string salt)
            {
                string preCrypt = input.ToString();//transform to string

                return StringCipher.Encrypt(preCrypt, salt);//crypt string
            }

            /// <summary>
            /// Crypt to float using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static float GetFloat(string crypt, string salt, KeySize size)
            {
                string decrypt = StringCipher.Decrypt(crypt, salt, size);

                return float.Parse(decrypt);
            }

            /// <summary>
            /// Float to crypt using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetFloat(float input, string salt, KeySize size)
            {
                string preCrypt = input.ToString();

                return StringCipher.Encrypt(preCrypt, salt, size);
            }
            #endregion

            #region Int
            /// <summary>
            /// Crypt to int
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static int GetInt(string crypt, string salt)
            {
                string decrypt = StringCipher.Decrypt(crypt, salt);

                return int.Parse(decrypt);
            }

            /// <summary>
            /// Int to crypt
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetInt(int input, string salt)
            {
                string preCrypt = input.ToString();

                return StringCipher.Encrypt(preCrypt, salt);
            }

            /// <summary>
            /// Crypt to int using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static int GetInt(string crypt, string salt, KeySize size)
            {
                string decrypt = StringCipher.Decrypt(crypt, salt, size);

                return int.Parse(decrypt);
            }

            /// <summary>
            /// Int to crypt using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetInt(int input, string salt, KeySize size)
            {
                string preCrypt = input.ToString();

                return StringCipher.Encrypt(preCrypt, salt, size);
            }
            #endregion

            #region String
            /// <summary>
            /// Crypt to string
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string GetString(string crypt, string salt)
            {
                string decrypt = StringCipher.Decrypt(crypt, salt);

                return decrypt;
            }

            /// <summary>
            /// String to crypt
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetString(string input, string salt)
            {
                string preCrypt = input.ToString();

                return StringCipher.Encrypt(preCrypt, salt);
            }

            /// <summary>
            /// Crypt to string using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string GetString(string crypt, string salt, KeySize size)
            {
                string decrypt = StringCipher.Decrypt(crypt, salt, size);

                return decrypt;
            }

            /// <summary>
            /// String to crypt using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetString(string input, string salt, KeySize size)
            {
                string preCrypt = input.ToString();

                return StringCipher.Encrypt(preCrypt, salt, size);
            }

            #region Binary
            /// <summary>
            /// Convert string to binary code
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static string StringToBinary(string data)
            {
                StringBuilder sb = new StringBuilder();

                foreach (char c in data.ToCharArray())
                {
                    sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
                }
                return sb.ToString();
            }

            /// <summary>
            /// Convert binary code to string
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static string BinaryToString(string data)
            {
                List<byte> byteList = new List<byte>();

                for (int i = 0; i < data.Length; i += 8)
                {
                    byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
                }
                return Encoding.ASCII.GetString(byteList.ToArray());
            }
            #endregion
            #endregion

            #region Bool
            /// <summary>
            /// Crypt to bool
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static bool GetBool(string crypt, string salt)
            {
                string decrypt = StringCipher.Decrypt(crypt, salt);

                return bool.Parse(decrypt);
            }

            /// <summary>
            /// Bool to crypt
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetBool(bool input, string salt)
            {
                string preCrypt = input.ToString();

                return StringCipher.Encrypt(preCrypt, salt);
            }

            /// <summary>
            /// Crypt to bool using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static bool GetBool(string crypt, string salt, KeySize size)
            {
                string decrypt = StringCipher.Decrypt(crypt, salt, size);

                return bool.Parse(decrypt);
            }

            /// <summary>
            /// Bool to crypt using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetBool(bool input, string salt, KeySize size)
            {
                string preCrypt = input.ToString();

                return StringCipher.Encrypt(preCrypt, salt, size);
            }
            #endregion

            #region Object
            /// <summary>
            /// Crypt to Object
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static T GetObject<T>(string crypt, string salt)
            {  
                string json = StringCipher.Decrypt(crypt, salt);

                return JsonConvert.DeserializeObject<T>(json);
            }

            /// <summary>
            /// Object to crypt
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetObject(object input, string salt)
            {
                string preCrypt = "";
                try
                {
                    preCrypt = JsonConvert.SerializeObject(input);
                }
                catch (Exception)
                {
                    throw new ArgumentException("SafeKepper not supporting this type.");
                }

                return StringCipher.Encrypt(preCrypt, salt);
            }

            /// <summary>
            /// Crypt to object using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static T GetObject<T>(string crypt, string salt, KeySize size)
            {
                string json = StringCipher.Decrypt(crypt, salt,size);

                return JsonConvert.DeserializeObject<T>(json);
            }

            /// <summary>
            /// Object to crypt using keysize
            /// </summary>
            /// <param name="crypt"></param>
            /// <param name="salt"></param>
            /// <returns></returns>
            public static string SetObject(object input, string salt, KeySize size)
            {
                string preCrypt = "";
                try
                {
                    preCrypt = JsonConvert.SerializeObject(input);
                    if (preCrypt[0] == '"')
                    {
                        preCrypt = preCrypt.Remove(0, 1);
                        preCrypt = preCrypt.Remove(preCrypt.Length - 1, 1);
                        preCrypt = preCrypt.Replace(@"\", "");
                    }
                }
                catch (Exception)
                {
                    throw new ArgumentException("SafeKepper not supporting this type.");
                }
            

                return StringCipher.Encrypt(preCrypt, salt, size);
            }
            #endregion
        }

        internal class StringCipher
        {
            // This constant is used to determine the keysize of the encryption algorithm in bits.
            // We divide this by 8 within the code below to get the equivalent number of bytes.
            private static int Keysize = 192;

            // This constant determines the number of iterations for the password bytes generation function.
            private const int DerivationIterations = 1000;

            #region Crypt
            public static string Encrypt(string plainText, string passPhrase)
            {
                Keysize = 192;
                // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
                // so that the same Salt and IV values can be used when decrypting.  
                var saltStringBytes = Generate192BitsOfRandomEntropy();
                var ivStringBytes = Generate192BitsOfRandomEntropy();
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 192;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                    var cipherTextBytes = saltStringBytes;
                                    cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                    cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Convert.ToBase64String(cipherTextBytes);
                                }
                            }
                        }
                    }
                }
                
            }

            public static string Decrypt(string cipherText, string passPhrase)
            {
                Keysize = 192;
                // Get the complete stream of bytes that represent:
                // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
                // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
                // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 192;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    var plainTextBytes = new byte[cipherTextBytes.Length];
                                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }

            public static string Encrypt(string plainText, string passPhrase, KeySize keySize)
            {

                // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
                // so that the same Salt and IV values can be used when decrypting. 
                byte[] saltStringBytes = new byte[0];
                byte[] ivStringBytes = new byte[0];
                switch (keySize)
                {
                    case KeySize.Bit256:
                        Keysize = 256;
                        saltStringBytes = Generate256BitsOfRandomEntropy();
                        ivStringBytes = Generate256BitsOfRandomEntropy();
                        break;
                    case KeySize.Bit192:
                        Keysize = 192;
                        saltStringBytes = Generate192BitsOfRandomEntropy();
                        ivStringBytes = Generate192BitsOfRandomEntropy();
                        break;
                    case KeySize.Bit128:
                        Keysize = 128;
                        saltStringBytes = Generate128BitsOfRandomEntropy();
                        ivStringBytes = Generate128BitsOfRandomEntropy();
                        break;
                    default:
                        break;
                }

                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = Keysize;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                    var cipherTextBytes = saltStringBytes;
                                    cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                    cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Convert.ToBase64String(cipherTextBytes);
                                }
                            }
                        }
                    }
                }
            }

            public static string Decrypt(string cipherText, string passPhrase, KeySize keySize)
            {
                switch (keySize)
                {
                    case KeySize.Bit256:
                        Keysize = 256;
                        break;
                    case KeySize.Bit128:
                        Keysize = 128;
                        break;
                    case KeySize.Bit192:
                        Keysize = 192;
                        break;
                    default:
                        break;
                }
                // Get the complete stream of bytes that represent:
                // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
                // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
                // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = Keysize;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    var plainTextBytes = new byte[cipherTextBytes.Length];
                                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region GenerateBits
            private static byte[] Generate256BitsOfRandomEntropy()
            {
                var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with cryptographically secure random bytes.
                    rngCsp.GetBytes(randomBytes);
                }
                return randomBytes;
            }

            private static byte[] Generate192BitsOfRandomEntropy()
            {
                var randomBytes = new byte[24]; // 8 Bytes will give us 64 bits.
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with cryptographically secure random bytes.
                    rngCsp.GetBytes(randomBytes);
                }
                return randomBytes;
            }

            private static byte[] Generate128BitsOfRandomEntropy()
            {
                var randomBytes = new byte[16]; // 16 Bytes will give us 128 bits.
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with cryptographically secure random bytes.
                    rngCsp.GetBytes(randomBytes);
                }
                return randomBytes;
            }



            private static byte[] Generate32BitsOfRandomEntropy()
            {
                var randomBytes = new byte[4]; // 4 Bytes will give us 32 bits.
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with cryptographically secure random bytes.
                    rngCsp.GetBytes(randomBytes);
                }
                return randomBytes;
            }

            private static byte[] Generate16BitsOfRandomEntropy()
            {
                var randomBytes = new byte[2]; // 2 Bytes will give us 16 bits.
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with cryptographically secure random bytes.
                    rngCsp.GetBytes(randomBytes);
                }
                return randomBytes;
            }
            #endregion
        }
        
    }

    public enum KeySize
    {
        Bit256,
        Bit192,
        Bit128
    }
}
