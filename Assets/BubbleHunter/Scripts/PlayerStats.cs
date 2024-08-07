using UnityEngine;

namespace BubHun.Players
{
    [CreateAssetMenu(fileName = "A_PlayerStats", menuName = "Bubble Hunter/Player Stats", order = 0)]
    public class PlayerStats : ScriptableObject
    {
        [SerializeField]
        private float m_baseSpeed = 5;
        [SerializeField]
        private float m_dashNb = 1;
        [SerializeField]
        private float m_dashSpeedMultiplier = 3;
        [SerializeField]
        private float m_dashCooldownModifier = 1;
        [SerializeField]
        private float m_dashDurationModifier = 1;
        [SerializeField]
        private bool m_phantomDash = false;

        private float m_speedBoost = 1;

        private const float DASH_COOLDOWN = 1f;
        private const float DASH_DURATION = 0.2f;

        public float MoveSpeed => m_baseSpeed * m_speedBoost;
        public float DashNumber => m_dashNb;
        public float DashSpeed => MoveSpeed * m_dashSpeedMultiplier;
        public float DashCooldownTime => DASH_COOLDOWN * m_dashCooldownModifier;
        public float DashDurationTime => DASH_DURATION * m_dashDurationModifier;
        public bool PhantomDash => m_phantomDash;
    }
}