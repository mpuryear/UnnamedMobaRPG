using UnityEngine;

namespace Unity.MobaRPG.VisualEffects
{
    public class ScrollingMaterialUVs : MonoBehaviour
    {
        public float scrollX = 0.1f;
        public float scrollY = 0.1f;

        [SerializeField]
        Material m_Material;

        float m_OffsetX; 
        float m_OffsetY;

        void Update()
        {
            m_OffsetX = Time.time * scrollX;
            m_OffsetY = Time.time * scrollY;
            m_Material.mainTextureOffset = new Vector2(m_OffsetX, m_OffsetY);
        }

        void OnDestroy()
        {
            ResetMaterialOffset();
        }

        void OnApplicationQuit()
        {
            ResetMaterialOffset();
        }

        void ResetMaterialOffset() 
        {
            // reset UVs to avoid modifying the material file; this will be refactored.
            m_Material.mainTextureOffset = new Vector2(0f, 0f);
        }
    }
}
