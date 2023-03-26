using System;
using Unity.MobaRPG.Gameplay.GameplayObjects.Character;
using Unity.MobaRPG.Gameplay.GameState;
using Unity.MobaRPG.Gameplay.Actions;
using Unity.MobaRPG.Gameplay.Messages;
using Unity.MobaRPG.Infrastructure;
using Unity.MobaRPG.Utils;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Unity.MobaRPG.Gameplay.GameplayObjects
{
    [RequireComponent(typeof(NetworkLifeState), typeof(ServerCharacter))]
    public class PublishMessageOnCombatLogEvent : NetworkBehaviour
    {

        NetworkVariable<Actions.Action> m_MostRecentAction;
        ServerActionPlayer m_ServerActionPlayer;
        ServerCharacter m_ServerCharacter;

        [SerializeField]
        string m_CharacterName;

        NetworkNameState m_NameState;

        [Inject]
        IPublisher<CombatLogMessage> m_Publisher;

        void Awake()
        {
            m_ServerCharacter = GetComponent<ServerCharacter>();
        }

        public override void OnNetworkSpawn()
        {
            if (true) { return; }

            if (IsServer)
            {
                m_NameState = GetComponent<NetworkNameState>();
                m_ServerActionPlayer = m_ServerCharacter.ActionPlayer;
                m_MostRecentAction.OnValueChanged += OnCombatLogEvent;

                var gameState = FindObjectOfType<ServerBossRoomState>();
                if (gameState != null)
                {
                    gameState.Container.Inject(this);
                }
            }
        }

        public void OnCombatLogEvent(Actions.Action previousAction, Actions.Action newAction)
        {
            if (newAction == null) { return; }

            m_Publisher.Publish(new CombatLogMessage()
            {
                CharacterName = m_NameState != null ? m_NameState.Name.Value : (FixedPlayerName)m_CharacterName,
                TargetName = $"id: {newAction.Data.TargetIds[0]}",
                actionID = newAction.ActionID, 
            });
        }
    }
}
