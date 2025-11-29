using UnityEngine;

namespace App.Gameplay.GameplayFSM.States
{
    public class LevelWinGameplayState : IGameplayState
    {
        public void Enter()
        {
            Debug.Log("Entered LevelWinGameplayState");
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}