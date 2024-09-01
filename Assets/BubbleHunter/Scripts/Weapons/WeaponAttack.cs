using UnityEngine;

namespace BubHun.Weapons
{
    public abstract class WeaponAttack : MonoBehaviour
    {
        [SerializeField] protected float m_duration = 1;

        protected float m_timer = 0;

        public virtual void SetInit(Transform p_origin)
        {
            this.transform.position = p_origin.position;
            this.transform.rotation = p_origin.rotation;
        }

        protected virtual void Update()
        {
            if (m_timer >= m_duration)
                this.DestroyAttack();

            m_timer += Time.deltaTime;
            this.ProgressAttack();
        }

        protected abstract void ProgressAttack();

        protected virtual void DestroyAttack()
        {
            Destroy(this.gameObject);
        }
    }
}