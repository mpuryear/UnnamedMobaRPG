using System;
using Unity.MobaRPG.Gameplay.GameplayObjects;
using Unity.MobaRPG.Gameplay.GameplayObjects.Character;
using Unity.MobaRPG.Utils;
using Unity.Netcode;

namespace Unity.MobaRPG.Gameplay.Messages
{
    public struct LifeStateChangedEventMessage : INetworkSerializeByMemcpy
    {
        public LifeState NewLifeState;
        public CharacterTypeEnum CharacterType;
        public FixedPlayerName CharacterName;
    }
}
