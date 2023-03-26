using System.Collections;
using System.Collections.Generic;
using Unity.MobaRPG.Gameplay.GameplayObjects.Character;
using Unity.MobaRPG.Gameplay.UserInput;
using Unity.Netcode;
using UnityEngine;

namespace Unity.MobaRPG.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class PlayerTeleporter : NetworkBehaviour
    {
        [SerializeField]
        private PlayerTeleporter m_TeleporterToAppearAt;

        [SerializeField]
        Collider m_Collider;

        [SerializeField] 
        private Transform m_LandingPoint;

        private ServerCharacter m_ServerCharater;

        private bool m_TeleporterUsed;

        private bool IsEnabled = true;

        void Awake()
        {
            m_Collider.isTrigger = true;
            Transform t = GetComponent<Transform>();
        }

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                enabled = false;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!IsEnabled) { return; }
            IsEnabled = false;

            m_ServerCharater = other.GetComponent<ServerCharacter>();
            m_ServerCharater.Movement.Teleport(m_TeleporterToAppearAt.m_LandingPoint.position);
        }

        void OnTriggerExit(Collider other) 
        {
            IsEnabled = true;
        }
    }
}
