using System;
using System.Collections.Generic;
using App.Gameplay.Items;
using App.UI.CollectedItemsPanel;
using VContainer.Unity;

namespace App.Gameplay.ItemCollecting
{
    public class SlotMerger: IInitializable, IDisposable
    {
        public const int MERGE_COUNT = 3;
        private readonly ItemCollector _collector;
        private readonly CollectedItemsContainer _collectedItemsContainer;
        private readonly CollectedItemsContainerAnimator _animator;

        public SlotMerger(ItemCollector collector,
            CollectedItemsContainer collectedItemsContainer,
            CollectedItemsContainerAnimator animator)
        {
            _collector = collector;
            _collectedItemsContainer = collectedItemsContainer;
            _animator = animator;
        }

        public void Initialize()
        {
            _collector.OnCollectAnimationComplete += MergeItemsInSlots;
        }

        private void MergeItemsInSlots()
        {
            ItemType itemType = ItemType.None;
            List<CollectedItemSlot> slots = new();
            for (int i = 0; i < _collectedItemsContainer.Slots.Count; i++)
            {
                var slot = _collectedItemsContainer.Slots[i];
                if(slot.ItemType == ItemType.None) continue;
                if (slot.ItemType != itemType)
                {
                    itemType = slot.ItemType;
                    slots.Clear();
                    slots.Add(slot);
                }
                else
                {
                    slots.Add(slot);
                    if (slots.Count >= MERGE_COUNT)
                    {
                        _animator.MergeSlotsAnimation(slots, OnMergeSlotsAnimationCompleteHandler);
                        return;
                    }
                }
            }
        }

        private void OnMergeSlotsAnimationCompleteHandler(List<ItemOnUI> items)
        {
            _collectedItemsContainer.RemoveItems(items);
        }

        public void Dispose()
        {
            _collector.OnCollectAnimationComplete -= MergeItemsInSlots;
        }
    }
}