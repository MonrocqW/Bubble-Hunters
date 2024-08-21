using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BubHun.Library
{
    public class LibraryWheelSelection : MonoBehaviour
    {
        [SerializeField] private BaseLibrary m_library;
        [SerializeField] private Slider m_wheel;

        [SerializeField] private TextMeshProUGUI m_selectedName;
        [SerializeField] private GameObject m_descriptionZone;
        [SerializeField] private TextMeshProUGUI m_description;
        //[SerializeField] private WeaponsLibrary m_allWeapons;
        //[SerializeField] private Slider m_weapon;
        // Start is called before the first frame update

        private int m_currentItem = -1;
        private bool m_validated = false;

        public ILibraryItem CurrentItem => m_library.GetItem(m_currentItem);

        void Start()
        {
            m_wheel.minValue = -1;
            m_wheel.maxValue = m_library.NbItems;
            m_wheel.value = 0;
            m_wheel.onValueChanged.AddListener(this.OnItemChanged);
            m_library.OnAvailabilityUpdated += this.CheckCurrentItemAvailable;
            OnItemChanged(m_wheel.value);
        }

        private void CheckCurrentItemAvailable()
        {
            if (m_validated)
                return;

            if (!m_library.IsAvailable(m_currentItem))
                this.OnItemChanged(m_currentItem + 1);
        }

        private void OnItemChanged(float p_itemId)
        {
            int l_next = NextValidItem((int) p_itemId, p_itemId > m_currentItem);
            if (l_next == m_currentItem)
                return;

            m_currentItem = l_next;
            m_wheel.value = l_next;

            this.SetHoveringItem(m_library.GetItem(l_next));
        }

        public void ValidateSelection(bool p_validate)
        {
            m_validated = p_validate;
            if(p_validate)
            {
                m_library.Select(m_currentItem);
            }
            else
            {
                m_library.DeSelect(m_currentItem);
            }
            m_wheel.interactable = !p_validate;
        }

        private void SetHoveringItem(ILibraryItem p_item)
        {
            m_selectedName.text = p_item.Name;
            m_description.text = p_item.Description;
            m_descriptionZone.SetActive(!string.IsNullOrEmpty(p_item.Description));
        }

        private int NextValidItem(int p_charId, bool p_ascending)
        {
            if (p_charId == m_currentItem)
                return p_charId;

            if (p_charId == -1)
            {
                return this.NextValidItem((int) m_wheel.maxValue - 1, p_ascending);
            }

            if (p_charId == m_wheel.maxValue)
            {
                return this.NextValidItem(0, p_ascending);
            }

            if (!m_library.IsAvailable(p_charId))
            {
                if (p_ascending)
                    return this.NextValidItem(p_charId + 1, p_ascending);
                else
                    return this.NextValidItem(p_charId - 1, p_ascending);
            }

            return p_charId;
        }
    }
}
