using System;
using Unity.Netcode;

namespace Unity.MobaRPG.Utils
{
    public class NetcodeHooks : NetworkBehaviour 
    {
        public event Action OnNetworkSpawnHook;
        public event Action OnNetworkDespawnHook;

        public override void OnNetworkDespawn() 
        { 
            base.OnNetworkDespawn();
            OnNetworkDespawnHook?.Invoke();
        }
        public override void OnNetworkSpawn() 
        { 
            base.OnNetworkSpawn();
            OnNetworkSpawnHook?.Invoke();
        }
    }
}
