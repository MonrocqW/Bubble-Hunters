using UnityEngine;

namespace BubHun.Players
{
    [CreateAssetMenu(fileName = "A_CharacterData", menuName = "Bubble Hunter/Character Data", order = 1)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField]
        private string m_playerName;
        [SerializeField]
        private CharacterStats m_stats;
        [SerializeField]
        private Color m_mainColor = Color.blue;
        [SerializeField]
        private Color m_secondaryColor = Color.cyan;
        [SerializeField] 
        private GameObject m_appearancePrefab;

        public string Name => m_playerName;
        public CharacterStats Stats => m_stats;
        public Color MainColor => m_mainColor;
        public Color SecondaryColor => m_secondaryColor;
        public GameObject AppearancePrefab => m_appearancePrefab;
    }
}