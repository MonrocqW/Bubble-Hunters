using BubHun.Library;
using UnityEngine;

namespace BubHun.Players
{
    [CreateAssetMenu(fileName = "A_CharacterData", menuName = "Bubble Hunter/Character Data", order = 1)]
    public class CharacterData : ScriptableObject, ILibraryItem
    {
        [SerializeField]
        private string m_characterName;
        [SerializeField]
        private string m_description;
        [SerializeField]
        private CharacterStats m_stats;
        [SerializeField]
        private Color m_mainColor = Color.blue;
        [SerializeField]
        private Color m_secondaryColor = Color.cyan;
        [SerializeField] 
        private GameObject m_appearancePrefab;

        public string Name => m_characterName;
        public string Description => m_description;
        public CharacterStats Stats => m_stats;
        public Color MainColor => m_mainColor;
        public Color SecondaryColor => m_secondaryColor;
        public GameObject AppearancePrefab => m_appearancePrefab;
    }
}