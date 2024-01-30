using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Tools.Repository
{
    public abstract class Repository<TData> : ScriptableObject, IReadOnlyList<TData> where TData : Data
    {
        [SerializeField] private List<TData> _data;
        
        public IEnumerator<TData> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_data).GetEnumerator();
        }

        public int Count => _data.Count;

        public TData this[int index] => _data[index];
        public TData this[string index] => _data.FirstOrDefault(data => data.Id == index);
    }
}