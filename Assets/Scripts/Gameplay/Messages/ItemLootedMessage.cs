using System;
using Unity.MobaRPG.Utils;
using Unity.Collections;
using Unity.Netcode;

namespace Unity.MobaRPG.Gameplay.Messages
{
    public struct ItemLootedMessage : INetworkSerializeByMemcpy
    {
        FixedString32Bytes m_ItemName;
        FixedPlayerName m_PlayerName;

        public string ItemName => m_ItemName.ToString();
        public string PlayerName => m_PlayerName.ToString();

        public ItemLootedMessage(string itemName, string playerName)
        {
            m_ItemName = itemName;
            m_PlayerName = playerName;
        }
    }
}
