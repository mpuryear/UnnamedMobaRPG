using System;
using Unity.Netcode;


namespace Unity.MobaRPG.Gameplay.Messages
{
    public struct DoorStateChangedEventMessage : INetworkSerializeByMemcpy
    {
        public bool IsDoorOpen;
    }
}
