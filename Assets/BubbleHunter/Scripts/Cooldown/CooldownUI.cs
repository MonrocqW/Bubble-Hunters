using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BubHun.Cooldown
{
    public class CooldownUI : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour m_cooldownProvider;
        [SerializeField] private Image m_cooldownImage;
        [SerializeField] private TextMeshProUGUI m_cooldownLeft;
        [SerializeField] private TextMeshProUGUI m_storedCharges;

        [SerializeField] private bool m_inverseFill = false;

        private ICooldownProvider m_provider;
        private void OnValidate()
        {
            if (m_cooldownProvider != null && !(m_cooldownProvider is ICooldownProvider l_provider))
                m_cooldownProvider = null;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (m_cooldownProvider == null)
                return;
            m_provider = (ICooldownProvider)m_cooldownProvider;
        }

        // Update is called once per frame
        void Update()
        {
            CooldownData l_data = m_provider.GetCooldownData();
            if(m_cooldownImage != null)
            {
                m_cooldownImage.gameObject.SetActive(l_data.timeLeft != 0);
                m_cooldownImage.fillAmount = m_inverseFill ? 1 - l_data.progress : l_data.progress;
            }
            if(m_cooldownLeft != null)
            {
                m_cooldownLeft.gameObject.SetActive(l_data.timeLeft != 0);
                m_cooldownLeft.text = l_data.timeLeft.ToString(l_data.timeLeft > 1 ? "F0" : "F1");
            }
            if(m_storedCharges != null)
            {
                m_storedCharges.gameObject.SetActive(l_data.maxCharges > 1);
                m_storedCharges.text = l_data.storedCharges.ToString();
            }
        }
    }
}