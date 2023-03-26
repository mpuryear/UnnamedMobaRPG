using System;

namespace Unity.MobaRPG.Gameplay.Actions
{
    /// <summary>
    /// List of all Types of Actions. There is a many-to-one mapping of Actions to ActionLogics.
    /// </summary>
    public enum ActionLogic
    {
        Melee,
        FrontalCleave,
        RangedTargeted,
        Chase,
        Revive,
        LaunchProjectile,
        Emote,
        RangedFXTargeted,
        AoE,
        Trample,
        ChargedShield,
        Stunned,
        Target,
        ChargedLaunchProjectile,
        StealthMode,
        DashAttack,
        ImpToss,
        PickUp,
        Drop
    }
}
