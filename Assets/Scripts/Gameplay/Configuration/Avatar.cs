using System;
using Unity.MobaRPG.Infrastructure;
using UnityEngine;

namespace Unity.MobaRPG.Gameplay.Configuration
{
    /// <summary>
    /// This ScriptableObject defines a Player Character for MobaRPG. It defines its CharacterClass field for
    /// associated game-specific properties, as well as its graphics representation.
    /// </summary>
    [CreateAssetMenu]
    [Serializable]
    public class Avatar : GuidScriptableObject
    {
        public CharacterClass CharacterClass;
        public GameObject Graphics;
        public GameObject GraphicsCharacterSelect;
        public Sprite Portrait;
    }
}
