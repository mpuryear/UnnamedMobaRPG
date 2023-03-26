using System;
using UnityEngine;

namespace Unity.MobaRPG.Gameplay.Configuration
{
    /// <summary>
    /// This ScritableObject will be the container for all possible Avatars inside MobaRPG.
    /// <see cref="Avatar"/>
    /// </summary>
    [CreateAssetMenu]
    public class AvatarRegistry : ScriptableObject
    {
        [SerializeField]
        Avatar[] m_Avatars;

        public bool TryGetAvatar(Guid guid, out Avatar avatarValue)
        {
            avatarValue = Array.Find(m_Avatars, avatar => avatar.Guid == guid);
            return avatarValue != null;
        }

        public Avatar GetRandomAvatar()
        {
            if (m_Avatars == null || m_Avatars.Length == 0) {
                return null;
            }

            return m_Avatars[UnityEngine.Random.Range(0, m_Avatars.Length)];
        }
    }
}