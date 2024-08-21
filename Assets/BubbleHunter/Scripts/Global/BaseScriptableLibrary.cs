using System;
using UnityEngine;

namespace BubHun.Global
{
    public class BaseScriptableLibrary<T> : ScriptableObject where T : ScriptableObject
    {
        [Tooltip("Elements can only be selected by one user?")]
        [SerializeField] private bool m_exclusiveLibrary;
        [SerializeField] private T[] m_elements = Array.Empty<T>();

        public event Action OnUsedUpdated;

        private static bool[] s_used;

        private void OnEnable()
        {
            s_used = new bool[NbElements];
        }

        public bool IsAvailable(int p_id)
        {
            this.CheckIdInLength(p_id);

            if (!m_exclusiveLibrary)
                return true;
            return !s_used[p_id];
        }

        public void Select(int p_id)
        {
            this.CheckIdInLength(p_id);
            s_used[p_id] = true;
            OnUsedUpdated?.Invoke();
        }

        public void DeSelect(int p_id)
        {
            this.CheckIdInLength(p_id);
            s_used[p_id] = false;
            OnUsedUpdated?.Invoke();
        }

        public T[] Elements => m_elements;
        public int NbElements => m_elements.Length;

        public T GetElement(int p_id)
        {
            this.CheckIdInLength(p_id);
            return m_elements[p_id];
        }

        private void CheckIdInLength(int p_id)
        {
            if (p_id >= NbElements)
            {
                throw new ArgumentException($"Trying to get an element from {this.name} outside of it ({p_id} >= {NbElements})");
            }
        }
    }
}