using System;
using Unity.Netcode;

namespace Unity.MobaRPG.Gameplay.Actions
{
    /// <summary>
    /// This struct is used by Action system (and GameDataSource) to refer to a specific action in runtime.
    /// </summary>
    public struct ActionID : INetworkSerializable, IEquatable<ActionID>
    {
        public int ID;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ID);
        }

        public bool Equals(ActionID other) 
        {
            return ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            return obj is ActionID other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public static bool operator ==(ActionID lhs, ActionID rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ActionID lhs, ActionID rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"ActionID({ID})";
        }
    }
}