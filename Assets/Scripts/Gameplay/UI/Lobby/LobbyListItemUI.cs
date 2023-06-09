using System;
using TMPro;
using Unity.MobaRPG.UnityServices.Lobbies;
using UnityEngine;
using VContainer;

namespace Unity.MobaRPG.Gameplay.UI
{
    /// <summary>
    /// An individual LobbyUI in the list of available lobbies
    /// </summary>
    public class LobbyListItemUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_lobbyNameText;
        [SerializeField] TextMeshProUGUI m_lobbyCountText;

        [Inject] LobbyUIMediator m_LobbyUIMediator;

        LocalLobby m_Data;

        public void SetData(LocalLobby data)
        {
            m_Data = data;
            m_lobbyNameText.SetText($"name: {data.LobbyName}");
            m_lobbyCountText.SetText($"{data.PlayerCount}/{data.MaxPlayerCount}");
        }

        public void OnClick()
        {
            m_LobbyUIMediator.JoinLobbyRequest(m_Data);
        }
    }
}
