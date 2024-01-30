using System;
using System.Linq;
using Core.WindowsService.Api.Service;
using Core.WindowsService.Data;
using Core.WindowsService.Impl.Repository;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Core.WindowsService.Editor
{
    public class WindowsServiceEditor: OdinEditorWindow
    {
        [TableList(IsReadOnly = true)]
        public WindowInfo[] Windows;
        
        [MenuItem("Tools/WindowsService")]
        private static void OpenWindow()
        {
            GetWindow<WindowsServiceEditor>().Show();
        }

        private void OnFocus()
        {
            var windowsPrefabs = new ResourceWindowsRepository().GetWindowsPrefabs().Cast<BaseWindow>();
            Windows = windowsPrefabs.Select((window) => new WindowInfo(window.name, window.Layer.ToString())).ToArray();

        }
    }
}