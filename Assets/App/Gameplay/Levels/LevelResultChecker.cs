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

        public LevelResultChecker(LeftToCollectItemsContainer leftToCollectItemsContainer, SlotMerger slotMerger, CollectedItemsContainer collectedItemsContainer)
        {
            _leftToCollectItemsContainer = leftToCollectItemsContainer;
            _slotMerger = slotMerger;
            _collectedItemsContainer = collectedItemsContainer;
        }

        public void Initialize()
        {
            _leftToCollectItemsContainer.UpdateLeftToCollect += UpdateLeftToCollectHandler;
            _slotMerger.OnSlotMergerHandleCollectedItem += OnSlotMergerHandleCollectedItemHandler;
            _levelWin = false;
            _levelLose = false;
        }

        private void OnSlotMergerHandleCollectedItemHandler(bool isMerged)
        {
            if (isMerged) return;
            if (_collectedItemsContainer.HasEmptySlots) return;
            if (_slotMerger.MergeAvailable()) return;
            
            _levelLose = true;
        }

        private void UpdateLeftToCollectHandler(ItemType itemType, int count)
        {
            if(_leftToCollectItemsContainer.AllItemsCollected) _levelWin = true;
        }

        public bool LevelWin() => _levelWin;

        public bool LevelLose() => _levelLose;

        public void Dispose()
        {
            _leftToCollectItemsContainer.UpdateLeftToCollect -= UpdateLeftToCollectHandler;
            _slotMerger.OnSlotMergerHandleCollectedItem -= OnSlotMergerHandleCollectedItemHandler;
        }
    }
}