using UnityEngine;

namespace BubHun.Level
{
    public class LevelSettings : MonoBehaviour
    {
        [SerializeField]
        private Vector2 m_gravity = Vector2.zero;

        // Start is called before the first frame update
        void Awake()
        {
            Physics2D.gravity = m_gravity;
        }
    }
}