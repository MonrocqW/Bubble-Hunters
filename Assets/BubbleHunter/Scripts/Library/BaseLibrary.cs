using System;
using UnityEngine;

namespace BubHun.Library
{
    public abstract class BaseLibrary : ScriptableObject
    {
        [Tooltip("Elements can only be selected by one user?")] [SerializeField]
        protected bool m_exclusiveLibrary;

        public abstract ILibraryItem[] Items { get; }
        public int NbItems => Items.Length;

        protected bool[] m_availableItems;

        public event Action OnAvailabilityUpdated;

        protected virtual void OnEnable()
        {
            m_availableItems = new bool[NbItems];
            Array.Fill(m_availableItems, true);
        }

        public virtual bool IsAvailable(int p_id)
        {
            this.CheckIdInLength(p_id);

            if (!m_exclusiveLibrary)
                return true;
            return m_availableItems[p_id];
        }

        public void Select(int p_id)
        {
            this.SetItemAvailable(p_id, false);
        }

        public void DeSelect(int p_id)
        {
            this.SetItemAvailable(p_id, true);
        }

        private void SetItemAvailable(int p_id, bool p_available)
        {
            this.CheckIdInLength(p_id);
            m_availableItems[p_id] = p_available;
            OnAvailabilityUpdated?.Invoke();
        }

        public ILibraryItem GetItem(int p_id)
        {
            this.CheckIdInLength(p_id);
            return Items[p_id];
        }

        protected void CheckIdInLength(int p_id)
        {
            if (p_id >= NbItems)
            {
                throw new ArgumentException(
                    $"Trying to get an element from {this.name} outside of it ({p_id} >= {NbItems})");
            }
        }
    }
}