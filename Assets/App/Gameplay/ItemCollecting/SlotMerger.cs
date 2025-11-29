using System;
using System.Collections.Generic;
using System.Linq;
using App.Gameplay.Items;
using App.UI.CollectedItemsPanel;
using DG.Tweening;
using UnityEngine;
using VContainer.Unity;

namespace App.Gameplay.ItemCollecting
{
    public class SlotMerger: IInitializable, IDisposable
    {
        public const int MERGE_COUNT = 3;
        private List<ItemOnUI> _mergedItems;
        private readonly ItemCollector _collector;
        private readonly CollectedItemsContainer _collectedItemsContainer;
        private readonly CollectedItemsContainerAnimator _collectedItemsAnimator;
        private readonly ItemCollectAnimator _itemCollectAnimator;

        public SlotMerger(ItemCollector collector,
            CollectedItemsContainer collectedItemsContainer,
            CollectedItemsContainerAnimator collectedItemsAnimator,
            ItemCollectAnimator itemCollectAnimator)
        {
            _collector = collector;
            _collectedItemsContainer = collectedItemsContainer;
            _collectedItemsAnimator = collectedItemsAnimator;
            _itemCollectAnimator = itemCollectAnimator;
            _mergedItems = new List<ItemOnUI>();
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
                if(_mergedItems.Contains(slot.Item)) continue;
                if(_itemCollectAnimator.ItemIsMovingToSlot(slot.Item)) continue;
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
                        Debug.Log($"Try Merge Items");
                        _mergedItems.AddRange(slots.Select(x => x.Item));
                        _collectedItemsAnimator.MergeSlotsAnimation(slots, OnMergeSlotsAnimationCompleteHandler);
                        return;
                    }
                }
            }
        }

        private void OnMergeSlotsAnimationCompleteHandler(List<ItemOnUI> items)
        {
            _mergedItems.RemoveAll(items.Contains);
            _collectedItemsContainer.RemoveItems(items);
        }

        public void Dispose()
        {
            _collector.OnCollectAnimationComplete -= MergeItemsInSlots;
        }
    }
}