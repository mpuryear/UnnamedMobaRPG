using System;
using Unity.MobaRPG.Gameplay.GameplayObjects.Character;
using Unity.MobaRPG.Infrastructure;
using UnityEngine;

namespace Unity.MobaRPG.Gameplay.GameplayObjects
{
    
    /// <summary>
    /// A runtime list of <see cref="PersistentPlayer"/> objects that is populated both on clients and server.
    /// </summary>
    [CreateAssetMenu]
    public class ClientPlayerAvatarRuntimeCollection : RuntimeCollection<ClientPlayerAvatar>
    {

    }
}
