using UnityEngine;

namespace App.Gameplay.GameplayFSM.States
{
    public class LevelLoseGameplayState : IGameplayState
    {
        public void Enter()
        {
            Debug.Log("Entered LevelLoseGameplayState");
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}