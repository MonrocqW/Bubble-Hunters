using System.Threading.Tasks;
using UnityEngine;

namespace BubHun.Weapons
{
    public class BaseWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Animator m_anim;
        [SerializeField] private WeaponAttack m_attackPrefab;
        [SerializeField] private Transform m_attackOrigin;
        [SerializeField] private float m_delaySpawn = 0;

        public async void LaunchAttack()
        {
            m_anim.SetTrigger("Attack");
            if (m_delaySpawn > 0)
                await Task.Delay((int) (m_delaySpawn * 1000));
            WeaponAttack l_attack = Instantiate(m_attackPrefab);
            l_attack.SetInit(m_attackOrigin);
        }
    }
}