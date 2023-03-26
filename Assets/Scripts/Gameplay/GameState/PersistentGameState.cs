using System;
using UnityEngine;

namespace Unity.MobaRPG.Gameplay.GameState
{
    public enum WinState
    {
        Invalid,
        Win,
        Loss
    }


    /// <summary>
    /// Class containing some data that needs to be passed between ServerMobaRPGState and PostGameState to represent the games session's win state.
    /// </summary>
    public class PersistentGameState
    {
        public WinState WinState{ get; private set; }

        public void SetWinState(WinState winState)
        {
            WinState = winState;
        }

        public void Reset()
        {
            WinState = WinState.Invalid;
        }
    }
}
