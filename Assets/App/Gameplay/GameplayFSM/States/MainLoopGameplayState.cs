using App.Gameplay.Common;
using App.Gameplay.ItemCollecting;
using App.Gameplay.Levels;
using App.UI.CollectedItemsPanel;
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
        private readonly CollectedItemsContainer _collectedItemsContainer;
        private readonly CollectedItemsPanel _collectedItemsPanel;

        public MainLoopGameplayState(Timer levelTimer,
            HUD hud,
            LevelConfig levelConfig,
            IItemCollector itemCollector,
            CollectedItemsContainer collectedItemsContainer,
            CollectedItemsPanel collectedItemsPanel)
        {
            _levelTimer = levelTimer;
            _hud = hud;
            _levelConfig = levelConfig;
            _itemCollector = itemCollector;
            _collectedItemsContainer = collectedItemsContainer;
            _collectedItemsPanel = collectedItemsPanel;
        }

        public void Enter()
        {
            _hud.Init(_levelConfig.targetItems);
            _itemCollector.Initialize();
            _itemCollector.EnableCollect();
            _collectedItemsPanel.Initialize();
            _collectedItemsContainer.Initialize();
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