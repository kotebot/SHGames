using UnityEngine;
using System.IO;
using System.Text;
using System;

namespace SafeKepper
{
    using Crypting;
    
    public class Saver
    {
        /// <summary>
        /// Get path file with key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static string GetPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key + ".sh");
        }

        /// <summary>
        /// Returns exists save with key 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>If save exist return true</returns>
        public static bool HasKey(string key)
        {
            try
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove all saves
        /// </summary>
        public static void DeleteAll()
        {
            var dir = Directory.GetFiles(Application.persistentDataPath);
            foreach (var key in dir)
            {
                File.Delete(key);
            }
        }

        /// <summary>
        /// Remove save with key
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException">Key not exist</exception>
        public static void DeleteKey(string key)
        {
            if(!HasKey(key))
                throw new ArgumentException(key + "not exist");
            
            File.Delete(GetPath(key));
        }

        #region SaveData

        #region Int
        /// <summary>
        /// Save int with key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetInt(string key, int value)
        {
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetInt(value, key);
                #if UNITY_EDITOR
                crypt += " Bit192 int";
                #endif
                sw.Write(crypt);
            }
        }
        
        /// <summary>
        /// Get int with key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Decrypt int with key</returns>
        /// <exception cref="ArgumentException">Key not exist</exception>
        public static int GetInt(string key)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
                    #if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
                    #endif
                    
                    return Crypt.GetInt(crypt,key);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }

        /// <summary>
        /// Get int with key, if key not exist return default value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">If key not exist return default value</param>
        /// <returns></returns>
        public static int GetInt(string key, int defaultValue)
        {
            try
            {
                return GetInt(key);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Save int with choose key size. The larger the key, the longer the execution.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="keySize"></param>
        public static void SetInt(string key, int value, KeySize keySize)
        {
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetInt(value, key, keySize);
                
#if UNITY_EDITOR
                crypt += " "+ keySize.ToString() + " int";
#endif
                sw.Write(crypt);
            }
        }

        /// <summary>
        /// Get int with key size. The larger the key, the longer the execution.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keySize"></param>
        /// <returns>Decrypt int with key</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int GetInt(string key, KeySize keySize)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
#if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
#endif
                    
                    return Crypt.GetInt(crypt, key, keySize);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }
        
        /// <summary>
        /// Get int with key size, if key not exist return default value. The larger the key, the longer the execution.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keySize"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Decrypt int with key or default value if key is not exist</returns>
        public static int GetInt(string key, KeySize keySize, int defaultValue)
        {
            try
            {
                return GetInt(key, keySize);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion

        #region Float
        /// <summary>
        /// Save float with key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetFloat(string key, float value)
        {
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetFloat(value, key);
                
#if UNITY_EDITOR
                crypt += " Bit192 float";
#endif
                sw.Write(crypt);
            }
        }
        
        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">Will return this, if key is not exist</param>
        /// <returns></returns>
        public static float GetFloat(string key, float defaultValue)
        {
            try
            {
                return GetFloat(key);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Key is not exist</exception>
        public static float GetFloat(string key)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
#if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
#endif

                    return Crypt.GetFloat(crypt, key);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }

        /// <summary>
        /// Save float with choose key size. The larger the key, the longer the execution.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="keySize"></param>
        public static void SetFloat(string key, float value, KeySize keySize)
        {
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetFloat(value, key, keySize);
                
#if UNITY_EDITOR
                crypt += " "+ keySize.ToString() + " float";
#endif
                
                sw.Write(crypt);
            }
        }

        /// <summary>
        /// Get data with key size
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keySize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Key is not exist</exception>
        public static float GetFloat(string key, KeySize keySize)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
#if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
#endif

                    return Crypt.GetFloat(crypt, key, keySize);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }
        
        /// <summary>
        /// Get data with key size, if key is not exist will return default value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keySize"></param>
        /// <param name="defaultValue">Will return this, if key is not exist</param>
        /// <returns></returns>
        public static float GetFloat(string key, KeySize keySize, float defaultValue)
        {
            try
            {
                return GetFloat(key, keySize);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion

        #region Bool
        /// <summary>
        /// Save bool with key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetBool(string key, bool value)
        {
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetBool(value, key);
                
#if UNITY_EDITOR
                crypt += " Bit192 bool";
#endif
                
                sw.Write(crypt);
            }
        }

        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Key is not exist</exception>
        public static bool GetBool(string key)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
#if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
#endif

                    return Crypt.GetBool(crypt, key);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }
        
        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">Will return this, if key is not exist</param>
        public static bool GetBool(string key, bool defaultValue)
        {
            try
            {
                return GetBool(key);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Save bool with choose key size. The larger the key, the longer the execution.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="keySize"></param>
        public static void SetBool(string key, bool value, KeySize keySize)
        {
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetBool(value, key, keySize);
                
#if UNITY_EDITOR
                crypt += " "+ keySize.ToString() + " bool";
#endif
                
                sw.Write(crypt);
            }
        }

        /// <summary>
        /// Get data with key size
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keySize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Key is not exist</exception>
        public static bool GetBool(string key, KeySize keySize)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
#if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
#endif

                    return Crypt.GetBool(crypt, key, keySize);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }
        
        /// <summary>
        /// Get data with key size, if key is not exist will return default value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keySize"></param>
        /// <param name="defaultValue">Will return this, if key is not exist</param>
        /// <returns></returns>
        public static bool GetBool(string key, KeySize keySize, bool defaultValue)
        {
            try
            {
                return GetBool(key, keySize);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion

        #region String
        /// <summary>
        /// Save string with key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetString(string key, string value)
        {
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetString(value, key);
                
#if UNITY_EDITOR
                crypt += " Bit192 string";
#endif
                
                sw.Write(crypt);
            }
        }

        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Key is not exist</exception>
        public static string GetString(string key)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
#if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
#endif

                    return Crypt.GetString(crypt, key);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }
        
        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">Will return this, if key is not exist</param>
        public static string GetString(string key, string defaultValue)
        {
            try
            {
                return GetString(key);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Save string with choose key size. The larger the key, the longer the execution.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="keySize"></param>
        public static void SetString(string key, string value, KeySize keySize)
        {
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetString(value, key, keySize);
                
#if UNITY_EDITOR
                crypt += " "+ keySize.ToString() + " string";
#endif
                
                sw.Write(crypt);
            }
        }

        /// <summary>
        /// Get data with key size
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keySize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Key is not exist</exception>
        public static string GetString(string key, KeySize keySize)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
#if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
#endif

                    return Crypt.GetString(crypt, key, keySize);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }
        
        /// <summary>
        /// Get data with key size, if key is not exist will return default value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keySize"></param>
        /// <param name="defaultValue">Will return this, if key is not exist</param>
        /// <returns></returns>
        public static string GetString(string key, KeySize keySize, string defaultValue)
        {
            try
            {
                return GetString(key, keySize);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion

        #region Object
        /// <summary>
        /// Save serializable object with key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void SetObject(string key, object value)
        {
            if(!value.GetType().IsSerializable)
                throw new ArgumentException("Value need be serializable");
            
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetObject(value, key);
                
#if UNITY_EDITOR
                crypt += " Bit192 object";
#endif
                
                sw.Write(crypt);
            }
        }

        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <typeparam name="T">Serializable class</typeparam>
        /// <exception cref="ArgumentException">Key is not exist</exception>
        public static T GetObject<T>(string key)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
#if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
#endif

                    return Crypt.GetObject<T>(crypt, key);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }
        
        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">Will return this, if key is not exist</param>
        public static T GetObject<T>(string key, T defaultValue)
        {
            try
            {
                return GetObject<T>(key);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Save serializable object with choose key size. The larger the key, the longer the execution.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="keySize"></param>
        /// <exception cref="ArgumentException">Type need be serializable</exception>
        public static void SetObject(string key, object value, KeySize keySize)
        {
            if(!value.GetType().IsSerializable)
                throw new ArgumentException("Type need be serializable");
            
            using (StreamWriter sw = new StreamWriter(GetPath(key), false, Encoding.Default))//save to file with binary name
            {
                string crypt = Crypt.SetObject(value, key, keySize);
                
#if UNITY_EDITOR
                crypt += " "+ keySize.ToString() + " object";
#endif
                
                sw.Write(crypt);
            }
        }

         /// <summary>
         /// Get data with key size
         /// </summary>
         /// <param name="key"></param>
         /// <param name="keySize"></param>
         /// <typeparam name="T">Serializable class</typeparam>
         /// <returns></returns>
         /// <exception cref="ArgumentException">Key is not exist</exception>
        public static T GetObject<T>(string key, KeySize keySize)
        {
            if(HasKey(key))
            {
                using (StreamReader sr = new StreamReader(GetPath(key), Encoding.Default))//reading with crypt file
                {
                    string crypt = sr.ReadToEnd();
                    
#if UNITY_EDITOR
                    crypt = crypt.Split(' ')[0];
#endif

                    return Crypt.GetObject<T>(crypt, key, keySize);
                }
            }
            else
            {
                throw new ArgumentException(key + " not found!");
            }
        }

         /// <summary>
         /// Get data with key size, if key is not exist will return default value
         /// </summary>
         /// <param name="key"></param>
         /// <param name="keySize"></param>
         /// <param name="defaultValue">Will return this, if key is not exist</param>
         /// <typeparam name="T">Serializable class</typeparam>
         /// <returns></returns>
         public static T GetObject<T>(string key, KeySize keySize, T defaultValue)
        {
            try
            {
                return GetObject<T>(key, keySize);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion

        #endregion
    }
}

