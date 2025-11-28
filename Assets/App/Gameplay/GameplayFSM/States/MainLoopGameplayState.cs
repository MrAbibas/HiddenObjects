using App.Gameplay.Common;
using UnityEngine;

namespace App.Gameplay.GameplayFSM.States
{
    public class MainLoopGameplayState : IGameplayState
    {
        private readonly Timer _levelTimer;
        
        public void Enter()
        {
        }

        public void Update()
        {
            _levelTimer.Tick(Time.deltaTime);
        }

        public void Exit()
        {
            
        }
    }
}