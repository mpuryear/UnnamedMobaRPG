using System;

namespace Unity.MobaRPG.Gameplay.GameplayObjects
{
    /// <summary>
    /// Describes how the character's movement shoudl be animated: as standing idle, running normally,
    /// magically slowed, sped up, etc. (Not all statuses are currently used by game content,
    /// but they are set up to be displayed correctly for future use.)
    /// </summary>
    [Serializable]
    public enum MovementStatus
    {
        Idle,               // not trying to move
        Normal,             // Character is moving (normally)
        Uncontrolled,       // character is being moved e.g. a knockback -- they are not in control!
        Slowed,             // character's movement is magically hindered
        Hasted,             // character's movement is magically enhanced
        Walking,            // character should appear to be "walking" rather than normal running (e.g. for cut-scenes)
    }
}
