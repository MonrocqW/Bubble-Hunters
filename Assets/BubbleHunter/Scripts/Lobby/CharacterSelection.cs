using BubHun.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BubHun.Lobby
{
    public class CharacterSelection : MonoBehaviour
    {
        [Header("Character")]
        [SerializeField] private CharactersLibrary m_allCharacters;
        [SerializeField] private Slider m_character;
        [SerializeField] private TextMeshProUGUI m_characterName;
        //[SerializeField] private WeaponsLibrary m_allWeapons;
        //[SerializeField] private Slider m_weapon;
        // Start is called before the first frame update

        private int m_playerIndex = 0;
        private int m_currentChar = 0;
        private bool m_validated = false;

        void Start()
        {
            m_character.minValue = -1;
            m_character.maxValue = m_allCharacters.NbElements;
            m_character.onValueChanged.AddListener(this.OnCharacterChanged);
            m_allCharacters.OnUsedUpdated += this.CheckCurrentCharUsed;
        }

        public void SetPlayerIndex(int p_index) => m_playerIndex = p_index;

        private void CheckCurrentCharUsed()
        {
            if (m_validated)
                return;
            
            if(!m_allCharacters.IsAvailable(m_currentChar))
                this.OnCharacterChanged(m_currentChar+1);
        }

        public void ValidateCharacter(bool p_validate)
        {
            m_validated = p_validate;
            if(p_validate)
            {
                PlayersManager.Instance.SelectCharacter(m_playerIndex, m_allCharacters.GetElement(m_currentChar));
                m_allCharacters.Select(m_currentChar);
            }
            else
            {
                m_allCharacters.DeSelect(m_currentChar);
            }
            m_character.interactable = !p_validate;
        }

        private void OnCharacterChanged(float p_charId)
        {
            int l_next = NextValidCharacter((int) p_charId, p_charId>m_currentChar);
            if (l_next == m_currentChar)
                return;

            m_currentChar = l_next;
            m_character.value = l_next;

            this.SetHoveringCharacter(m_allCharacters.GetElement(l_next));
        }

        private void SetHoveringCharacter(CharacterData p_char)
        {
            m_characterName.text = p_char.Name;
        }

        private int NextValidCharacter(int p_charId, bool p_ascending)
        {
            if (p_charId == m_currentChar)
                return p_charId;

            if (p_charId == -1)
            {
                return this.NextValidCharacter((int)m_character.maxValue-1, p_ascending);
            }
            if (p_charId == m_character.maxValue)
            {
                return this.NextValidCharacter(0, p_ascending);
            }

            if (!m_allCharacters.IsAvailable(p_charId))
            {
                if(p_ascending)
                    return this.NextValidCharacter(p_charId+1,p_ascending);
                else
                    return this.NextValidCharacter(p_charId-1,p_ascending);
            }

            return p_charId;
        }
    }
}