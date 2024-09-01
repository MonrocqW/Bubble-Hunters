using UnityEngine;

namespace BubHun.Weapons
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] 
        private WeaponData m_weaponData;
        [SerializeField] 
        private Transform m_weaponParent;

        private GameObject m_spawnedWeaponObject;
        private IWeapon m_spawnedWeapon;

        // Start is called before the first frame update
        void Start()
        {
            this.SetWeapon(m_weaponData);
        }

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

        [ContextMenu("Apply weapon")]
        private void DebugApplyWeapon()
        {
            this.SetWeapon(m_weaponData);
        }
    }
}