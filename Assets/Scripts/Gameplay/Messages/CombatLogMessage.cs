using System;
using Unity.MobaRPG.Gameplay.GameplayObjects;
using Unity.MobaRPG.Gameplay.GameplayObjects.Character;
using Unity.MobaRPG.Gameplay.Actions;
using Unity.MobaRPG.Utils;
using Unity.Netcode;


namespace Unity.MobaRPG.Gameplay.Messages
{
    public struct CombatLogMessage : INetworkSerializeByMemcpy
    {
        public FixedPlayerName CharacterName;
        public FixedPlayerName? TargetName;
        public ActionID actionID;
    }
}
