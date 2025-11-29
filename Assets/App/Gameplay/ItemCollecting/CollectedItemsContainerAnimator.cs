using System;
using System.Collections.Generic;
using App.Gameplay.Items;
using App.UI.CollectedItemsPanel;
using DG.Tweening;
using UnityEngine;

namespace App.Gameplay.ItemCollecting
{
    public class CollectedItemsContainerAnimator
    {
        private Dictionary<ItemOnUI, Tween> _movingToOtherSlotItems;
        private readonly CollectedItemsAnimationsConfig _animationsConfig;
        private readonly ItemCollectAnimator _collectAnimator;

        public CollectedItemsContainerAnimator(CollectedItemsAnimationsConfig animationsConfig,  ItemCollectAnimator collectAnimator)
        {
            _animationsConfig = animationsConfig;
            _collectAnimator = collectAnimator;
            _movingToOtherSlotItems = new();
        }

        public void MoveItemToSlotAnimation(CollectedItemSlot slot)
        {
            if (slot.ItemType == ItemType.None) return;
            RectTransform rectTransform = slot.Item.transform as RectTransform;
            Tween tween = rectTransform
                .DOAnchorPos(Vector2.zero, _animationsConfig.MoveToSlotDuration)
                .SetEase(_animationsConfig.MoveToSlotEase);
            if (_collectAnimator.IsAnimationPlaying && _collectAnimator.CurrentItem == slot.Item)
                _collectAnimator.AddTweenAfterCollect(tween);
            else
                tween.Play();
            
            if (_movingToOtherSlotItems.TryGetValue(slot.Item, out var animatedItem))
            {
                animatedItem.Kill();
                _movingToOtherSlotItems[slot.Item] = tween;
            }
            else
            {
                _movingToOtherSlotItems.Add(slot.Item, tween);
            }
        }

        public void MergeSlotsAnimation(List<CollectedItemSlot> slots, Action<List<ItemOnUI>> onComplete)
        {
            int centerInd = slots.Count / 2;
            Sequence seq = DOTween.Sequence();
            List<ItemOnUI> items = new List<ItemOnUI>();
            RectTransform rectTransform;
            for (int i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];
                var item = slot.Item;
                items.Add(item);
                if (_movingToOtherSlotItems.TryGetValue(slot.Item, out var animatedItem))
                {
                    animatedItem.Kill();
                    _movingToOtherSlotItems.Remove(slot.Item);
                }

                if (i == centerInd) continue;

                rectTransform = slot.Item.transform as RectTransform;
                seq.Join(rectTransform
                    .DOScale(_animationsConfig.MergeSideItemsEndScale, _animationsConfig.MergeSideItemsHideDuration)
                    .SetEase(_animationsConfig.MergeSideItemsHideErase)
                    .OnComplete(() => item.gameObject.SetActive(false)));
            }

            var centerSlot = slots[centerInd];
            var centerItem = centerSlot.Item;
            items.Add(centerItem);
            rectTransform = centerSlot.Item.transform as RectTransform;
            seq.Append(rectTransform
                    .DOJumpAnchorPos(_animationsConfig.MergeCenterItemJumpOffset,
                        _animationsConfig.MergeCenterItemJumpPower,
                        _animationsConfig.MergeCenterItemNumJumps,
                        _animationsConfig.MergeCenterItemJumpDuration)
                    .SetEase(_animationsConfig.MergeCenterItemJumpEase)
                    .OnComplete(() => centerItem.gameObject.SetActive(false)))
                .OnComplete(() => onComplete?.Invoke(items));
            seq.Play();
        }
    }
}