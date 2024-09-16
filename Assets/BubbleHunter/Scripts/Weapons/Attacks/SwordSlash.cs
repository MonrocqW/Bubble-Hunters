using UnityEngine;

namespace BubHun.Weapons
{
    public class SwordSlash : WeaponAttack
    {
        [SerializeField] private float m_speed = 1;
        [SerializeField] private float m_maxScale = 1.5f;

        protected override void ProgressAttack()
        {
            this.transform.Translate(Time.deltaTime * m_speed * Vector3.right, Space.Self);
            this.transform.localScale = Vector3.one * Mathf.Lerp(1,m_maxScale, Progress);
        }
    }
}