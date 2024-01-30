using System;
using System.IO;
using SafeKepper;
using UnityEngine;

namespace SafeKepperEditor.Table
{

	[Serializable]
	internal class SaveData : TreeElement
	{
		public string Key;
        
        public string CryptKey;
        
        public string Data;
       
        public string CryptData;
        
        public TypeSave Type;

        public KeySize KeySize;

        private NativeSaveTable _table;
        
        internal SaveData(string key, TypeSave type, string data, string cryptKey, string cryptData, KeySize keySize, int depth, int id, NativeSaveTable table) : base (data, depth, id)
        {
            Key = key;
            Type = type;
            Data = data;
            CryptData = cryptData;
            CryptKey = cryptKey;
            KeySize = keySize;
            _table = table;
        }
        
        internal void Update()
        {
            switch (Type)
            {
                case TypeSave.@int:
                    Saver.SetInt(Key, int.Parse(Data), KeySize);
                    break;
                case TypeSave.@float:
                    Saver.SetFloat(Key, float.Parse(Data), KeySize);
                    break;
                case TypeSave.@string:
                    Saver.SetString(Key, Data, KeySize);
                    break;
                case TypeSave.@bool:
                    Saver.SetBool(Key, bool.Parse(Data.ToLower()), KeySize);
                    break;
                case TypeSave.@object:
                    Saver.SetObject(Key, Data, KeySize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void Delete()
        {
            File.Delete(GetPath(CryptKey));
            _table.UpdateTable();
        }
        
        private static string GetPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key + ".sh");
        }
    }

    internal enum TypeSave
    {
        @int,
        @float,
        @bool,
        @string,
        @object
    }
}
