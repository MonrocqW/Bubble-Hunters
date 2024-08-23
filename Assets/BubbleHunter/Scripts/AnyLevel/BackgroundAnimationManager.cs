using UnityEngine;

namespace BubHun.Level
{
    public class BackgroundAnimationManager : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem m_gravityParticles;
        // Start is called before the first frame update
        void Start()
        {
            // Gravity set in LevelSettings during Awake
            this.SetGravityParticles(Physics2D.gravity);
        }

        private void SetGravityParticles(Vector2 p_gravity)
        {
            m_gravityParticles.gameObject.SetActive(p_gravity != Vector2.zero);
            m_gravityParticles.transform.localEulerAngles =
                (Vector2.SignedAngle(Vector2.down, p_gravity)) * Vector3.forward;
        }
    }
}