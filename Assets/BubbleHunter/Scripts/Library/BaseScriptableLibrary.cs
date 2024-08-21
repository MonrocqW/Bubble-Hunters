using System;
using UnityEngine;

namespace BubHun.Library
{
    public class BaseScriptableLibrary<T> : BaseLibrary where T : ScriptableObject, ILibraryItem
    {
        [SerializeField] private T[] m_elements = Array.Empty<T>();

        public T[] Elements => m_elements;
        public override ILibraryItem[] Items => Elements;

        public T GetElement(int p_id) => (T)GetItem(p_id);
    }
}