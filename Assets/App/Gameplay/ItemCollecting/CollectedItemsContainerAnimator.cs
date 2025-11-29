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

        public CollectedItemsContainerAnimator(CollectedItemsAnimationsConfig animationsConfig)
        {
            _animationsConfig = animationsConfig;
            _movingToOtherSlotItems = new ();
        }

        public void MoveItemToSlotAnimation(CollectedItemSlot slot)
        {
            if (slot.ItemType == ItemType.None) return;
            
            RectTransform rectTransform = slot.Item.transform as RectTransform;
            Tween tween = rectTransform
                .DOAnchorPos(Vector2.zero, _animationsConfig.MoveToSlotDuration)
                .SetEase(_animationsConfig.MoveToSlotEase)
                .Play();
            
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
    }
}