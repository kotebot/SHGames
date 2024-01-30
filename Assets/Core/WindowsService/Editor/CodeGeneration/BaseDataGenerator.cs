using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Core.WindowsService.Editor.CodeGeneration
{
    [InitializeOnLoad]
	public class BaseDataGenerator
	{
		public const string FilePath = "Assets/Scripts/WindowsService";
		public const string FileEnum = "WindowsLayers";

		public static string PathCombined { get; } = $"{FilePath}/{FileEnum}.cs";

		public static readonly List<string> DefaultLayerNames = new List<string>()
		{
			"Window", 
			"Popup"
		};

		static BaseDataGenerator()
		{
			if (Directory.Exists(FilePath) 
			    && File.Exists(PathCombined))
			{
				return;
			}
			
			SaveEnum(new List<string>(){});
			
			Debug.Log("Windows Enum Generated");
		}

		public static void SaveEnum(List<string> enums = null)
		{
			if (!Directory.Exists(FilePath) || !File.Exists(PathCombined))
			{
				Directory.CreateDirectory(FilePath);
				File.WriteAllText(PathCombined, null);
				AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
			}
			
			using (var streamWriter = new StreamWriter(PathCombined))
			{
				streamWriter.WriteLine("//GENERATED ENUM WITH CORE");
				streamWriter.WriteLine("namespace Core.WindowsService");
				streamWriter.WriteLine("{");
				streamWriter.WriteLine($"\tpublic enum {FileEnum}");
				streamWriter.WriteLine("\t{");
				WriteLineEnums(streamWriter, DefaultLayerNames);
				WriteLineEnums(streamWriter, enums);
				streamWriter.WriteLine("\t}");
				streamWriter.WriteLine("}");
			}
			
			AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
		}

		private static void WriteLineEnums(StreamWriter streamWriter, List<string> enums = null)
		{
			if (enums == null || enums.Count.Equals(0))
			{
				return;
			}
			
			foreach (var windowEnum in enums)
			{
				if (!string.IsNullOrEmpty(windowEnum))
				{
					streamWriter.WriteLine($"\t\t{windowEnum},");
				}
			}
		}
	}
}