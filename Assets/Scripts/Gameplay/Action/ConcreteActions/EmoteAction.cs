using System;
using Unity.MobaRPG.Gameplay.GameplayObjects.Character;
using UnityEngine;

namespace Unity.MobaRPG.Gameplay.Actions
{
    [CreateAssetMenu(menuName = "MobaRPG/Actions/Emote Action")]
    public class EmoteAction : Action
    {
        public override bool OnStart(ServerCharacter serverCharacter)
        {
            serverCharacter.serverAnimationHandler.NetworkAnimator.SetTrigger(Config.Anim);
            return false;
        }

        public override bool OnUpdate(ServerCharacter clientCharacter)
        {
            // since we return false at Start(), this method should not execute
            throw new InvalidOperationException("No logic defined.");
        }

        public override void Cancel(ServerCharacter serverCharacter)
        {
            if (!string.IsNullOrEmpty(Config.Anim2))
            {
                serverCharacter.serverAnimationHandler.NetworkAnimator.SetTrigger(Config.Anim2);
            }
        }

        public override bool OnUpdateClient(ClientCharacter clientCharacter)
        {
            return ActionConclusion.Continue;
        }
    }
}
