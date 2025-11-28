using App.Gameplay.Common;
using App.Gameplay.ItemCollecting;
using App.Gameplay.Levels;
using App.UI.HUD;
using UnityEngine;

namespace App.Gameplay.GameplayFSM.States
{
    public class MainLoopGameplayState : IGameplayState
    {
        private readonly Timer _levelTimer;
        private readonly HUD _hud;
        private readonly LevelConfig _levelConfig;
        private readonly IItemCollector _itemCollector;

        public MainLoopGameplayState(Timer levelTimer,
            HUD hud,
            LevelConfig levelConfig,
            IItemCollector itemCollector)
        {
            _levelTimer = levelTimer;
            _hud = hud;
            _levelConfig = levelConfig;
            _itemCollector = itemCollector;
        }

        public void Enter()
        {
            _hud.Init(_levelConfig.targetItems);
            _itemCollector.Initialize();
            _itemCollector.EnableCollect();
        }

        public void Update()
        {
            _levelTimer.Tick(Time.deltaTime);
        }

        public void Exit()
        {
            _itemCollector.DisableCollect();
        }
    }
}