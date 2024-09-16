using BubHun.Library;
using UnityEngine;

namespace BubHun.Weapons
{
    [CreateAssetMenu(fileName = "A_WeaponData", menuName = "Bubble Hunter/Weapon Data", order = 2)]
    public class WeaponData : ScriptableObject, ILibraryItem
    {
        [SerializeField]
        private string m_weaponName;
        [SerializeField]
        private string m_description;
        [SerializeField] 
        private float m_cooldownTime;
        [SerializeField] 
        private GameObject m_weaponPrefab;

        private void OnValidate()
        {
            this.CheckWeaponPrefab();
        }

        private void CheckWeaponPrefab()
        {
            if (m_weaponPrefab == null)
                return;
            if (!m_weaponPrefab.TryGetComponent(out IWeapon l_weapon))
            {
                m_weaponPrefab = null;
                Debug.LogError($"Weapon prefab must have a \'IWeapon\' component on {this.name}");
                return;
            }
        }

        public string Name => m_weaponName;
        public string Description => m_description;
        public GameObject WeaponPrefab => m_weaponPrefab;
        public float CooldownTime => m_cooldownTime;
    }
}