using BubHun.Players.Movement;
using UnityEngine;
using UnityEngine.VFX;

namespace BubHun.Players
{
    public class PlayerHolder : MonoBehaviour
    {
        [SerializeField] 
        private PlayerData m_playerData;
        [SerializeField] 
        private BubbleMovement m_movement;
        [SerializeField] 
        private Renderer m_sphere;
        [SerializeField] 
        private TrailRenderer m_trail;

        // Start is called before the first frame update
        void Start()
        {
            this.SetPlayer(m_playerData);
        }

        private void SetPlayer(PlayerData p_playerData)
        {
            if (p_playerData == null)
                return;

            m_playerData = p_playerData;
            m_movement.SetPlayer(p_playerData);
            m_sphere.material.color = p_playerData.MainColor;
            m_trail.startColor = p_playerData.MainColor;
            m_trail.endColor = p_playerData.SecondaryColor;
        }

        [ContextMenu("Apply player")]
        private void DebugApplyPlayer()
        {
            this.SetPlayer(m_playerData);
        }
    }
}