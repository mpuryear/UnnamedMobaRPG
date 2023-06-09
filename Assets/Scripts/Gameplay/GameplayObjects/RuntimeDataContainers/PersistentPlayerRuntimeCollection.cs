using System.Collections;
using Unity.MobaRPG.Infrastructure;
using UnityEngine;

namespace Unity.MobaRPG.Gameplay.GameplayObjects
{
    /// <summary>
    /// A runtime list of <see cref="PersistentPlayer"/> objects that is populated on both clients and server.
    /// </summary>
    [CreateAssetMenu]
    public class PersistentPlayerRuntimeCollection : RuntimeCollection<PersistentPlayer>
    {
        public bool TryGetPlayer(ulong clientID, out PersistentPlayer persistentPlayer)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (clientID == Items[i].OwnerClientId)
                {
                    persistentPlayer = Items[i];
                    return true;
                }
            }

            persistentPlayer = null;
            return false;
        }
    }
}
