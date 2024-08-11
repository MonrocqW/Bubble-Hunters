using UnityEngine;

namespace BubHun.Players
{
    [CreateAssetMenu(fileName = "A_PlayerData", menuName = "Bubble Hunter/Player Data", order = 1)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField]
        private string m_playerName;
        [SerializeField]
        private PlayerStats m_stats;
        [SerializeField]
        private Color m_mainColor = Color.blue;
        [SerializeField]
        private Color m_secondaryColor = Color.cyan;
        [SerializeField] 
        private GameObject m_appearancePrefab;

        public string Name => m_playerName;
        public PlayerStats Stats => m_stats;
        public Color MainColor => m_mainColor;
        public Color SecondaryColor => m_secondaryColor;
        public GameObject AppearancePrefab => m_appearancePrefab;
    }
}