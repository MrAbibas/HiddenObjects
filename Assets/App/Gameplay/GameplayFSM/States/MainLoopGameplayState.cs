using App.Gameplay.Common;
using App.Gameplay.ItemCollecting;
using App.Gameplay.Levels;
using App.UI.CollectedItemsPanel;
using App.UI.HUD;
using UnityEditor.Graphs;
using UnityEngine;

namespace App.Gameplay.GameplayFSM.States
{
    public class MainLoopGameplayState : IGameplayState
    {
        private readonly Timer _levelTimer;
        private readonly HUD _hud;
        private readonly IItemCollector _itemCollector;
        private readonly CollectedItemsPanel _collectedItemsPanel;
        private readonly LeftToCollectItemsContainer _leftToCollectItemsContainer;
        private readonly SlotMerger _slotMerger;

        public MainLoopGameplayState(Timer levelTimer,
            HUD hud,
            IItemCollector itemCollector,
            CollectedItemsPanel collectedItemsPanel,
            LeftToCollectItemsContainer leftToCollectItemsContainer,
            SlotMerger slotMerger)
        {
            _levelTimer = levelTimer;
            _hud = hud;
            _itemCollector = itemCollector;
            _collectedItemsPanel = collectedItemsPanel;
            _leftToCollectItemsContainer = leftToCollectItemsContainer;
            _slotMerger = slotMerger;
        }

        public void Enter()
        {
            _itemCollector.Initialize();
            _itemCollector.EnableCollect();
            _collectedItemsPanel.Initialize();
            _leftToCollectItemsContainer.Initialize();
            _hud.Initialize();
            _slotMerger.Initialize();
        }

        public void Update()
        {
            _levelTimer.Tick(Time.deltaTime);
        }

        public void Exit()
        {
            _itemCollector.DisableCollect();
            _leftToCollectItemsContainer.Dispose();
            _slotMerger.Dispose();
        }
    }
}