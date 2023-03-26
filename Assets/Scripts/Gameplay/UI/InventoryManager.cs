using System;
using Unity.MobaRPG.ConnectionManagement;
using Unity.MobaRPG.Gameplay.GameplayObjects;
using Unity.MobaRPG.Gameplay.GameplayObjects.Character;
using Unity.MobaRPG.Gameplay.Messages;
using Unity.MobaRPG.Infrastructure;
using Unity.MobaRPG.Utils;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Unity.MobaRPG.Gameplay.UI
{
    /// <summary>
    ///  Handles all inventory events, applies them on the server and logs them on all clients.
    /// </summary>
    public class InventoryManager : NetworkBehaviour
    {
        [SerializeField]
        GameObject m_InventoryPanel;

        [SerializeField]
        KeyCode m_OpenWindowKeyCode = KeyCode.I;

        [Inject]
        IPublisher<ItemLootedMessage> m_ItemLootedMessagePublisher;

        void Awake()
        {
            m_InventoryPanel.SetActive(false);
        }

        void Update()
        {
            if (m_OpenWindowKeyCode != KeyCode.None && Input.GetKeyDown(m_OpenWindowKeyCode))
            {
                m_InventoryPanel.SetActive(!m_InventoryPanel.activeSelf);
                m_ItemLootedMessagePublisher.Publish(new ItemLootedMessage("testItem", "testPlayer"));
            }
        }
    }
}
