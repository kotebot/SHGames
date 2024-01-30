using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Core.Purchases.Editor
{
    [InitializeOnLoad]
    public class PurchasesConstantsGenerator
    {
        public const string FilePath = "Assets/Scripts/Purchases";
        public const string PurchasesConstants = "PurchasesConstants";

        public static string PathCombined => $"{FilePath}/{PurchasesConstants}.cs";
        
        public static readonly Dictionary<string, string> DefaultIds = new Dictionary<string, string>()
        {
            {"NO_ADS_IOS_ID", ""} ,
            {"NO_ADS_ANDROID_ID", ""},
        };
        
        static PurchasesConstantsGenerator()
        {
            if (Directory.Exists(FilePath) 
                && File.Exists(PathCombined))
            {
                return;
            }
            
            SaveConstants();
			
            Debug.Log("Purchases constants Generated");
        }
        
        public static void SaveConstants(Dictionary<string, string> ids = null)
        {
            if (!Directory.Exists(FilePath) || !File.Exists(PathCombined))
            {
                Directory.CreateDirectory(FilePath);
                File.WriteAllText(PathCombined, null);
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
			
            using (var streamWriter = new StreamWriter(PathCombined))
            {
                streamWriter.WriteLine("//GENERATED CLASS WITH CORE");
                streamWriter.WriteLine("namespace Core.Purchases");
                streamWriter.WriteLine("{");
                streamWriter.WriteLine($"\tpublic class {PurchasesConstants}");
                streamWriter.WriteLine("\t{");
                WriteLineConstants(streamWriter, DefaultIds);
                WriteLineConstants(streamWriter, ids);
                streamWriter.WriteLine("\t}");
                streamWriter.WriteLine("}");
            }
			
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }
        
        private static void WriteLineConstants(StreamWriter streamWriter, Dictionary<string, string> constants = null)
        {
            if (constants == null || constants.Count.Equals(0))
            {
                return;
            }
			
            foreach (var adConst in constants)
            {
                if (!string.IsNullOrEmpty(adConst.Key))
                {
                    streamWriter.WriteLine($"\t\tpublic const string {adConst.Key} = \"{adConst.Value}\";");
                }
            }
        }
    }
}