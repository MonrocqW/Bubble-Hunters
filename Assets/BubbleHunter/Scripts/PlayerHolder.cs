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
        private float m_sphereEmissive = 2;
        
        [Header("Trail")]
        [SerializeField] 
        private TrailRenderer m_trail;
        [SerializeField] 
        private float m_trailAlphaStart = 0.5f;
        [SerializeField] 
        private float m_trailAlphaEnd = 0.2f;

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
            m_sphere.material.SetColor("_EmissionColor", p_playerData.SecondaryColor + new Color(0,0,0,m_sphereEmissive-1));
            m_trail.startColor = p_playerData.MainColor - new Color(0,0,0,1-m_trailAlphaStart);
            m_trail.endColor = p_playerData.SecondaryColor - new Color(0,0,0,1-m_trailAlphaEnd);
        }

        [ContextMenu("Apply player")]
        private void DebugApplyPlayer()
        {
            this.SetPlayer(m_playerData);
        }
    }
}