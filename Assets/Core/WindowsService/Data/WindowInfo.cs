using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.WindowsService.Data
{
    [Serializable]
    public class WindowInfo
    {
        [LabelWidth(30)]
        [TableColumnWidth(130)]
        [ReadOnly]
        public string Name;

        [TableColumnWidth(100, false)]
        [ReadOnly]
        public string Layer;        

        public WindowInfo(string name, string layer)
        {
            Name = name;
            Layer = layer;
        }
    }
}