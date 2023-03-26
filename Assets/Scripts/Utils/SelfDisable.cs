using UnityEngine;

namespace Unity.MobaRPG.Utils
{
    public class SelfDisable : MonoBehaviour
    {
        [SerializeField]
        float m_DisabledDelay;
        float m_DisableTimestamp;

        void Update()
        {
            if (Time.time >= m_DisableTimestamp)
            {
                gameObject.SetActive(false);
            }
        }

        void OnEnable()
        {
            m_DisableTimestamp = Time.time + m_DisabledDelay;
        }
    }
}