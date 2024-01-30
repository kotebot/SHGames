using System;
using System.Collections.Generic;
using System.IO;
using SafeKepper;
using SafeKepper.Crypting;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;


namespace SafeKepperEditor.Table
{
	internal class NativeSaveTable : EditorWindow
	{
		[NonSerialized] bool m_Initialized;
		[SerializeField] TreeViewState m_TreeViewState; // Serialized in the window layout file so it survives assembly reloading
		[SerializeField] MultiColumnHeaderState m_MultiColumnHeaderState;
		SearchField m_SearchField;
		NativeSaveTableView m_TreeView;

		[MenuItem("Tools/Save Table")]
		public static NativeSaveTable GetWindow ()
		{
			var window = GetWindow<NativeSaveTable>();
			window.titleContent = new GUIContent("Save table");
			window.Focus();
			window.Repaint();
			return window;
		}
		

		Rect multiColumnTreeViewRect
		{
			get { return new Rect(20, 30, position.width-40, position.height-60); }
		}

		Rect toolbarRect
		{
			get { return new Rect (20f, 10f, position.width-40f, 20f); }
		}

		Rect bottomToolbarRect
		{
			get { return new Rect(20f, position.height - 18f, position.width - 40f, 16f); }
		}

		public NativeSaveTableView treeView
		{
			get { return m_TreeView; }
		}

		public void UpdateTable()
		{
			m_Initialized = false;
			InitIfNeeded();
		}


		void InitIfNeeded ()
		{
			if (!m_Initialized)
			{
				// Check if it already exists (deserialized from window layout file or scriptable object)
				if (m_TreeViewState == null)
					m_TreeViewState = new TreeViewState();

				bool firstInit = m_MultiColumnHeaderState == null;
				var headerState = NativeSaveTableView.CreateDefaultMultiColumnHeaderState(multiColumnTreeViewRect.width);
				if (MultiColumnHeaderState.CanOverwriteSerializedFields(m_MultiColumnHeaderState, headerState))
					MultiColumnHeaderState.OverwriteSerializedFields(m_MultiColumnHeaderState, headerState);
				m_MultiColumnHeaderState = headerState;
				
				var multiColumnHeader = new MultiColumnHeader(headerState);
				if (firstInit)
					multiColumnHeader.ResizeToFit ();

				var data = new List<SaveData>();
				
				if(data.Count == 0)
					data.Add(new SaveData("root", TypeSave.@bool, "", "","", KeySize.Bit128, -1, 0, this));
				
					
				data.AddRange(GetData());
				
				var treeModel = new TreeModel<SaveData>(data);
				
				m_TreeView = new NativeSaveTableView(m_TreeViewState, multiColumnHeader, treeModel);

				m_SearchField = new SearchField();
				m_SearchField.downOrUpArrowKeyPressed += m_TreeView.SetFocusAndEnsureSelectedItem;

				m_Initialized = true;
			}
		}
		
		IList<SaveData> GetData ()
		{
			var SavesTable = new List<SaveData>();
			var dir = new DirectoryInfo(Application.persistentDataPath); // папка с файлами 

			int i = 0;
			foreach (FileInfo file in dir.GetFiles())
			{
				string DataFile="";
				using (StreamReader sr = file.OpenText())
				{
					DataFile = sr.ReadToEnd();
				}

				string uncryptName = file.Name.Replace(file.Extension, "");
				string uncryptData = "";
				string type = "";
				string key = "";
                
				var data = DataFile.Split(' ');
				if (data.Length > 1)
				{
					type = data[2];
					key = data[1];
					DataFile = data[0];
				}
                    
                    
				try
				{
					if (data.Length <= 1)
						uncryptData = Crypt.GetString(DataFile, uncryptName);
					else uncryptData = GetUncryptData(data[0], uncryptName, type, key);
				}
				catch (Exception e)
				{
					throw e;
				}
				
				SavesTable.Add(new SaveData(uncryptName ,StringToType(type), uncryptData , Path.GetFileNameWithoutExtension(file.FullName),data[0], StringToKeySize(key), 0, i + 1, this));
				i++;
			}
			
			
			return SavesTable;
		}

		private KeySize StringToKeySize(string key)
		{
			switch (key)
			{
				case "Bit128":
					return KeySize.Bit128;
				case "Bit192":
					return KeySize.Bit192;
				case "Bit256":
					return KeySize.Bit256;
			}

			return KeySize.Bit128;
		}
		private TypeSave StringToType(string type)
		{ 
			switch (type)
			{
				case "string":
					return TypeSave.@string;
				case "int":
					return TypeSave.@int;
				case "float":
					return TypeSave.@float;
				case "bool":
					return TypeSave.@bool;
				case "object":
					return TypeSave.@object;
			}
			return TypeSave.@object;
		}

		void OnGUI ()
		{
			InitIfNeeded();

			SearchBar (toolbarRect);
			DoTreeView (multiColumnTreeViewRect);
		}

		void SearchBar (Rect rect)
		{
			rect.width /= 2;
			treeView.searchString = m_SearchField.OnGUI (rect, treeView.searchString);
			var updateRect = new Rect(rect.width + 5, rect.y, rect.width / 2, rect.height);
			var removeRect = new Rect(rect.width + updateRect.width + 5, rect.y, rect.width / 2, rect.height);
			var defaultColor = GUI.contentColor; 
			GUI.backgroundColor = Color.green;
			if (GUI.Button(updateRect, "Update saves"))
			{
				UpdateTable();
			}
			GUI.backgroundColor = Color.red;
			if (GUI.Button(removeRect, "Remove all saves"))
			{
				Saver.DeleteAll();
				UpdateTable();
			}
			GUI.backgroundColor = defaultColor;


		}

		void DoTreeView (Rect rect)
		{
			m_TreeView.OnGUI(rect);
		}
		
		 private string GetUncryptData(string cryptData,string salt, string type, string key)
        {
            switch (type)
            {
	            case "string":
	            case "object":
		            switch (key)
                    {
                        case "Bit128":
                            return Crypt.GetString(cryptData, salt, KeySize.Bit128);
                        case "Bit192":
                            return Crypt.GetString(cryptData, salt, KeySize.Bit192);
                        case "Bit256":
                            return Crypt.GetString(cryptData, salt, KeySize.Bit256);
                    }
                    break;
                case "int":
                    switch (key)
                    {
                        case "Bit128":
                            return Crypt.GetInt(cryptData, salt, KeySize.Bit128).ToString();
                        case "Bit192":
                            return Crypt.GetInt(cryptData, salt, KeySize.Bit192).ToString();
                        case "Bit256":
                            return Crypt.GetInt(cryptData, salt, KeySize.Bit256).ToString();
                    }
                    break;
                case "float":
                    switch (key)
                    {
                        case "Bit128":
                            return Crypt.GetFloat(cryptData, salt, KeySize.Bit128).ToString();
                        case "Bit192":
                            return Crypt.GetFloat(cryptData, salt, KeySize.Bit192).ToString();
                        case "Bit256":
                            return Crypt.GetFloat(cryptData, salt, KeySize.Bit256).ToString();
                    }

                    break;
                case "bool":
                    switch (key)
                    {
                        case "Bit128":
                            return Crypt.GetBool(cryptData, salt, KeySize.Bit128).ToString();
                        case "Bit192":
                            return Crypt.GetBool(cryptData, salt, KeySize.Bit192).ToString();
                        case "Bit256":
                            return Crypt.GetBool(cryptData, salt, KeySize.Bit256).ToString();
                    }
                    break;

            }
            Debug.LogError("Error type");
            return "";
        }
		

	}
	

}
