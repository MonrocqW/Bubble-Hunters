using UnityEngine;

namespace BubHun.Weapons
{
    public class SwordWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Animator m_anim;
        [SerializeField] private WeaponAttack m_attackPrefab;
        [SerializeField] private Transform m_attackOrigin;

        public void LaunchAttack()
        {
            WeaponAttack l_attack = Instantiate(m_attackPrefab);
            l_attack.SetInit(m_attackOrigin);
            m_anim.SetTrigger("Attack");
        }
    }
}