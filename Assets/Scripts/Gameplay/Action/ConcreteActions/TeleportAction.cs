using System;
using Unity.MobaRPG.Gameplay.GameplayObjects;
using Unity.MobaRPG.Gameplay.GameplayObjects.Character;
using UnityEngine;

namespace Unity.MobaRPG.Gameplay.Actions
{

    /// <summary>
    /// Causes the player to teleport to a specified location. 
    /// 
    /// After the ExecTime has elapsed, the character is immune to damage until the action ends.
    /// </summary>
    [CreateAssetMenu(menuName = "MobaRPG/Actions/Teleport Action")]
    public class TeleportAction : Action
    {
        [SerializeField]
        private Vector3 m_TargetDestination;

        private bool m_Teleported;

        public override bool OnStart(ServerCharacter serverCharacter)
        {

            serverCharacter.serverAnimationHandler.NetworkAnimator.SetTrigger(Config.Anim);
            serverCharacter.clientCharacter.RecvDoActionClientRPC(Data);
            return ActionConclusion.Continue;
        }

        public override void Reset()
        {
            base.Reset();
            m_TargetDestination = default;
            m_Teleported = false;
        }

        public override bool OnUpdate(ServerCharacter clientCharacter)
        {
            return ActionConclusion.Continue;
        }

        public override void End(ServerCharacter serverCharacter)
        {
            // Anim2 contains the name of the end-loop-sequence trigger
            if (!string.IsNullOrEmpty(Config.Anim2))
            {
                serverCharacter.serverAnimationHandler.NetworkAnimator.SetTrigger(Config.Anim2);
            }

            // we're done, time to teleport!
            serverCharacter.Movement.Teleport(m_TargetDestination);
            m_Teleported = true;
        }

        public override void Cancel(ServerCharacter serverCharacter)
        {
            // OtherAnimatorVariable contains the name of the cancellation trigger
            if (!string.IsNullOrEmpty(Config.OtherAnimatorVariable))
            {
                serverCharacter.serverAnimationHandler.NetworkAnimator.SetTrigger(Config.OtherAnimatorVariable);
            }

            // because the client-side visualization of the action moves the character visualization around,
            // we need to explicitly end the client-side visuals when we abort
            serverCharacter.clientCharacter.RecvCancelActionsByPrototypeIDClientRpc(ActionID);
        }

        public override bool OnUpdateClient(ClientCharacter clientCharacter)
        {
            if (m_Teleported) { return ActionConclusion.Stop; } // we're done!

            return ActionConclusion.Continue;
        }
    }
}
