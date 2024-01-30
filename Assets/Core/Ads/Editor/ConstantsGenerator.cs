using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Core.Ads.Editor
{
    [InitializeOnLoad]
    public class ConstantsGenerator
    {
        public const string FilePath = "Assets/Scripts/Ads";
        public const string FileConstants = "AdsConstants";

        public static string PathCombined => $"{FilePath}/{FileConstants}.cs";
        
        public static readonly Dictionary<string, string> DefaultIds = new Dictionary<string, string>()
        {
            {"APP_KEY", ""} ,
            {"REWARDED_ID", ""},
            {"BANNER_ID", ""},
            {"INTERSTITIAL_ID", ""},
        };
        
        static ConstantsGenerator()
        {
            if (Directory.Exists(FilePath) 
                && File.Exists(PathCombined))
            {
                return;
            }
            
            SaveEnum();
			
            Debug.Log("Ads constants Generated");
        }
        
        public static void SaveEnum(Dictionary<string, string> ids = null)
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
                streamWriter.WriteLine("namespace Core.Ads");
                streamWriter.WriteLine("{");
                streamWriter.WriteLine($"\tpublic class {FileConstants}");
                streamWriter.WriteLine("\t{");
                WriteLineEnums(streamWriter, DefaultIds);
                WriteLineEnums(streamWriter, ids);
                streamWriter.WriteLine("\t}");
                streamWriter.WriteLine("}");
            }
			
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }
        
        private static void WriteLineEnums(StreamWriter streamWriter, Dictionary<string, string> constants = null)
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