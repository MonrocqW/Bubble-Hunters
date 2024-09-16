using BubHun.Cooldown;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BubHun.Weapons
{
    public class WeaponHolder : MonoBehaviour, ICooldownProvider
    {
        [SerializeField] 
        private WeaponData m_weaponData;
        [SerializeField] 
        private Transform m_weaponParent;

        private GameObject m_spawnedWeaponObject;
        private IWeapon m_spawnedWeapon;

        private float m_lastAttackTime = -3;

        private bool m_usingKeyboard;

        // Start is called before the first frame update
        void Start()
        {
            this.SetWeapon(m_weaponData);
            m_usingKeyboard = this.GetComponent<PlayerInput>().currentControlScheme.Contains("Keyboard");
        }
        
        #region Init

        public void SetWeapon(WeaponData p_weaponData)
        {
            if (p_weaponData == null)
                return;

            m_weaponData = p_weaponData;
            this.SpawnWeapon(p_weaponData.WeaponPrefab);
        }

        private void SpawnWeapon(GameObject p_weapon)
        {
            if (p_weapon == null)
                return;
            if(m_spawnedWeaponObject != null)
                Destroy(m_spawnedWeaponObject);

            m_spawnedWeaponObject = Instantiate(p_weapon, m_weaponParent);
            m_spawnedWeapon = m_spawnedWeaponObject.GetComponent<IWeapon>();
        }
        
        #endregion
        
        #region Input actions

        void OnAttack()
        {
            if (m_spawnedWeapon == null)
                return;
            if (Time.time - m_lastAttackTime < m_weaponData.CooldownTime)
                return;
            m_lastAttackTime = Time.time;
            m_spawnedWeapon.LaunchAttack();
        }

        void OnAttackOrientation(InputValue p_value)
        {
            Vector2 l_direction = p_value.Get<Vector2>();
            
            if (m_usingKeyboard)
                l_direction =
                    (Camera.main.ScreenToWorldPoint(new Vector3(l_direction.x, l_direction.y,
                        -Camera.main.transform.position.z)) - this.transform.position).normalized;

            m_weaponParent.localEulerAngles = Vector3.forward * Vector2.SignedAngle(this.transform.right, l_direction);
        }
        
        #endregion

        public virtual CooldownData GetCooldownData()
        {
            if (m_weaponData == null)
                return new CooldownData();
            CooldownData l_data = new CooldownData
            {
                timeLeft = Mathf.Max(m_weaponData.CooldownTime - (Time.time - m_lastAttackTime), 0),
                maxCharges = 1
            };

            l_data.progress = 1 - l_data.timeLeft / m_weaponData.CooldownTime;
            return l_data;
        }

        [ContextMenu("Apply weapon")]
        private void DebugApplyWeapon()
        {
            this.SetWeapon(m_weaponData);
        }
    }
}