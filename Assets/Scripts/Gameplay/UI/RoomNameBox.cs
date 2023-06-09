using System;
using TMPro;
using Unity.MobaRPG.UnityServices.Lobbies;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Unity.MobaRPG.Gameplay.UI
{
    public class RoomNameBox : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI m_RoomNameText;
        [SerializeField]
        Button m_CopytoClipboardButton;

        LocalLobby m_LocalLobby;

        string m_LobbyCode;

        [Inject]
        private void InjectDependencies(LocalLobby localLobby)
        {
            m_LocalLobby = localLobby;
            m_LocalLobby.changed += UpdateUI;
            UpdateUI(localLobby);
        }

        void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            m_LocalLobby.changed -= UpdateUI;
        }

        private void UpdateUI(LocalLobby localLobby)
        {
            if (!string.IsNullOrEmpty(localLobby.LobbyCode))
            {
                m_LobbyCode = localLobby.LobbyCode;
                m_RoomNameText.text = $"Lobby Code: {m_LobbyCode}";
                gameObject.SetActive(true);
                m_CopytoClipboardButton.gameObject.SetActive(true);
            }
        }

        public void CopyToClipboard()
        {
            GUIUtility.systemCopyBuffer = m_LobbyCode;
        }
    }
}