using App.Gameplay.Common;
using App.Gameplay.ItemCollecting;
using App.Gameplay.Items;

namespace App.Gameplay.Levels
{
    public class LevelResultChecker: ILevelResultChecker
    {
        private bool _levelWin;
        private bool _levelLose;
        private readonly LeftToCollectItemsContainer _leftToCollectItemsContainer;
        private readonly SlotMerger _slotMerger;
        private readonly CollectedItemsContainer _collectedItemsContainer;
        private readonly Timer _levelTimer;

        public LevelResultChecker(LeftToCollectItemsContainer leftToCollectItemsContainer,
            SlotMerger slotMerger,
            CollectedItemsContainer collectedItemsContainer,
            Timer levelTimer)
        {
            _leftToCollectItemsContainer = leftToCollectItemsContainer;
            _slotMerger = slotMerger;
            _collectedItemsContainer = collectedItemsContainer;
            _levelTimer = levelTimer;
        }

        public void Initialize()
        {
            _slotMerger.OnSlotMergerHandleCollectedItem += OnSlotMergerHandleCollectedItemHandler;
            _levelTimer.OnTimerCompleted += OnTimerCompletedHandler;
            _levelWin = false;
            _levelLose = false;
        }

        private void OnTimerCompletedHandler()
        {
            _levelLose = true;
        }

        private void OnSlotMergerHandleCollectedItemHandler(bool isMerged)
        {
            if (_leftToCollectItemsContainer.AllItemsCollected)
            {
                _levelWin = true;
                return;
            }
            if (isMerged) return;
            if (_collectedItemsContainer.HasEmptySlots) return;
            if (_slotMerger.MergeAvailable()) return;
            
            _levelLose = true;
        }

        public bool LevelWin() => _levelWin;

        public bool LevelLose() => _levelLose;

        public void Dispose()
        {
            _slotMerger.OnSlotMergerHandleCollectedItem -= OnSlotMergerHandleCollectedItemHandler;
        }
    }
}