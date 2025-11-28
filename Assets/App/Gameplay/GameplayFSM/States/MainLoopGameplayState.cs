using App.Gameplay.Common;
using App.Gameplay.Level;
using App.UI.HUD;
using UnityEngine;

namespace App.Gameplay.GameplayFSM.States
{
    public class MainLoopGameplayState : IGameplayState
    {
        private readonly Timer _levelTimer;
        private readonly HUD _hud;
        private readonly LevelConfig _levelConfig;

        public MainLoopGameplayState(Timer levelTimer, HUD hud, LevelConfig levelConfig)
        {
            _levelTimer = levelTimer;
            _hud = hud;
            _levelConfig = levelConfig;
        }

        public void Enter()
        {
            _hud.Init(_levelConfig.targetItems);
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