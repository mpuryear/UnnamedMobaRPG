using System;
using Unity.MobaRPG.UnityServices.Lobbies;
using UnityEngine;
using VContainer;

namespace Unity.MobaRPG.ConnectionManagement
{
    /// <summary>
    /// Connection state corresponding to a connected client. When being disconnected, transitions to the 
    /// ClientReconnecting state if no reason is given, or to the Offline state.
    /// </summary>
    class ClientConnectedState : ConnectionState
    {
        [Inject]
        protected LobbyServiceFacade m_LobbyServiceFacade;

        public override void Enter()
        {
            if (m_LobbyServiceFacade.CurrentUnityLobby != null)
            {
                m_LobbyServiceFacade.BeginTracking();
            }
        }

        public override void Exit() { }

        public override void OnClientDisconnect(ulong _)
        {
            var disconnectReason = m_ConnectionManager.NetworkManager.DisconnectReason;
            if (string.IsNullOrEmpty(disconnectReason))
            {
                m_ConnectStatusPublisher.Publish(ConnectStatus.Reconnecting);
                m_ConnectionManager.ChangeState(m_ConnectionManager.m_ClientReconnecting);
            }
            else
            {
                var connectStatus = JsonUtility.FromJson<ConnectStatus>(disconnectReason);
                m_ConnectStatusPublisher.Publish(connectStatus);
                m_ConnectionManager.ChangeState(m_ConnectionManager.m_Offline);
            }
        }

        public override void OnUserRequestedShutdown()
        {
            m_ConnectStatusPublisher.Publish(ConnectStatus.UserRequestedDisconnect);
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_Offline);
        }
    }
}
