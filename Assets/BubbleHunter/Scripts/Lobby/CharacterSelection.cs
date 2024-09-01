using BubHun.Library;
using BubHun.Players;
using BubHun.Weapons;
using UnityEngine;

namespace BubHun.Lobby
{
    public class CharacterSelection : MonoBehaviour
    {
        [Header("Wheels")]
        [SerializeField] private LibraryWheelSelection m_characterSelection;
        [SerializeField] private LibraryWheelSelection m_weaponSelection;

        private int m_playerIndex = 0;
        private bool m_validated = false;

        public void SetPlayerIndex(int p_index) => m_playerIndex = p_index;

        public void ValidateSelection(bool p_validate)
        {
            m_validated = p_validate;
            m_characterSelection.ValidateSelection(p_validate);
            if(p_validate)
            {
                PlayersManager.Instance.SelectCharacter(m_playerIndex, (CharacterData)m_characterSelection.CurrentItem);
                PlayersManager.Instance.SelectWeapon(m_playerIndex, (WeaponData)m_weaponSelection.CurrentItem);
            }
        }
    }
}