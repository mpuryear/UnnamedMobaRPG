using Unity.Netcode;

namespace Unity.MobaRPG.Utils.SceneManagement
{
    /// <summary>
    /// Simple object that keeps track of scene loading progress of a specific instance.
    /// </summary>
    public class NetworkedLoadingProgressTracker : NetworkBehaviour
    {
        /// <summary>
        /// The current loading progress associated with the owner of this NetworkBehaviour
        /// </summary>
        public NetworkVariable<float> Progress { get; } = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    }
}
