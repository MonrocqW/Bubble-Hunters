using BubHun.Players.Movement;
using UnityEngine;
using UnityEngine.VFX;

namespace BubHun.Players
{
    public class CharacterHolder : MonoBehaviour
    {
        [SerializeField] 
        private CharacterData m_characterData;
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
            this.SetCharacter(m_characterData);
        }

        public void SetCharacter(CharacterData p_characterData)
        {
            if (p_characterData == null)
                return;

            m_characterData = p_characterData;
            m_movement.SetPlayer(p_characterData);
            m_sphere.material.color = p_characterData.MainColor;
            m_sphere.material.SetColor("_EmissionColor", p_characterData.SecondaryColor + new Color(0,0,0,m_sphereEmissive-1));
            m_trail.startColor = p_characterData.MainColor - new Color(0,0,0,1-m_trailAlphaStart);
            m_trail.endColor = p_characterData.SecondaryColor - new Color(0,0,0,1-m_trailAlphaEnd);
        }

        [ContextMenu("Apply character")]
        private void DebugApplyCharacter()
        {
            this.SetCharacter(m_characterData);
        }
    }
}