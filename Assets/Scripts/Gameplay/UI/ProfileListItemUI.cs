using System;
using TMPro;
using Unity.MobaRPG.Utils;
using UnityEngine;
using VContainer;

namespace Unity.MobaRPG.Gameplay.UI
{
    public class ProfileListItemUI : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI m_ProfileNameText;

        [Inject] ProfileManager m_ProfileManager;

        public void SetProfileName(string profileName)
        {
            m_ProfileNameText.text = profileName;
        }       

        public void OnSelectClick()
        {
            m_ProfileManager.Profile = m_ProfileNameText.text;
        }

        public void OnDeleteClick()
        {
            m_ProfileManager.DeleteProfile(m_ProfileNameText.text);
        }
    }
}
