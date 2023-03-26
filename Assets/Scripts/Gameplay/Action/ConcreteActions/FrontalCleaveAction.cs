using System;
using Unity.MobaRPG.Gameplay.GameplayObjects;
using Unity.MobaRPG.Gameplay.GameplayObjects.Character;
using UnityEngine;

namespace Unity.MobaRPG.Gameplay.Actions
{
    [CreateAssetMenu(menuName = "MobaRPG/Actions/Frontal Cleave Action")]
    public class FrontalCleaveAction : AOEAction 
    {
        public override bool OnStart(ServerCharacter serverCharacter)
        {
            // broadcasting to all players including myself.
            // We don't know our actual targets for this attack until it triggers, so the client can't use the TargetIds list (and we clear it out for clarity).
            // This means we are responsible for triggering reaction-anims ourselves, which we do in PerformAoe()
            Data.TargetIds = new ulong[0];

            serverCharacter.serverAnimationHandler.NetworkAnimator.SetTrigger(Config.Anim);
            serverCharacter.clientCharacter.RecvDoActionClientRPC(Data);
            return ActionConclusion.Continue;
        }

        /// <summary>
        /// Perform the AOE in a frontal cone. The frontal cone uses Dot product to determine what is directly in front of the character.
        /// While this technically uses a radius around our character to determine the cone, it seems more intuitive to use the Config.Range field to determine how far the
        /// attack should reach.
        /// We use the Config.Radius to instead represent the degree's of which we should make the cone, defaulting to 90
        /// </summary>
        /// <remarks>
        /// The actual explanation and code used can be found here "https://gamedev.stackexchange.com/questions/118675/need-help-with-a-field-of-view-like-collision-detector"
        /// </remarks>
        /// <param name="parent"></param>
        override protected void PerformAoE(ServerCharacter parent)
        {
            var colliders = Physics.OverlapSphere(parent.transform.position, Config.Range, LayerMask.GetMask("NPCs"));
            Vector3 characterToCollider;
            float dot;
            float detectionAngle = Config.Radius / 2;
            foreach (var collider in colliders)
            {
                characterToCollider = (collider.transform.position-parent.transform.position).normalized;
                dot = Vector3.Dot(characterToCollider, parent.transform.forward);
                if (dot >= Mathf.Cos(detectionAngle))
                {
                    var enemy = collider.GetComponent<IDamageable>();
                    if (enemy != null)
                    {
                        // actually deal the damage
                        enemy.ReceiveHP(parent, -Config.Amount);
                    }
                }
            }
        }
    }
}
