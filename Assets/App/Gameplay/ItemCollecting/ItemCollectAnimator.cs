using System;
using System.Collections.Generic;
using App.Gameplay.Items;
using App.UI.CollectedItemsPanel;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace App.Gameplay.ItemCollecting
{
    public class ItemCollectAnimator
    {
        private Dictionary<ItemOnUI, (Sequence sequence, Vector2 targetSize)> _movingToSlotItems;
        private readonly ItemCollectAnimationConfig _animationConfig;
        private readonly CollectedItemsContainer _collectedItemsContainer;

        public ItemCollectAnimator(ItemCollectAnimationConfig animationConfig)
        {
            _animationConfig = animationConfig;
            _movingToSlotItems = new();
        }

        public void PlayCollectAnimation(ItemOnUI itemOnUI, CollectedItemSlot slot, Action onComplete)
        {
            var sequence = DOTween.Sequence();
            RectTransform rectTransform = itemOnUI.transform as RectTransform;
            Vector2 targetJumpPos = rectTransform.anchoredPosition + new Vector2(
                _animationConfig.JumpOffset.x * -1 * Mathf.Sign(rectTransform.anchoredPosition.x),
                _animationConfig.JumpOffset.y);
            sequence
                .Append(rectTransform
                    .DOJumpAnchorPos(targetJumpPos, _animationConfig.JumpPower, _animationConfig.NumJumps,
                        _animationConfig.JumpDuration)
                    .SetEase(_animationConfig.JumpEase));

            sequence.Join(itemOnUI.transform
                    .DOPunchScale(_animationConfig.JumpPunch, _animationConfig.JumpPunchDuration,
                        _animationConfig.JumpPunchElasticity))
                .SetEase(_animationConfig.JumpPunchEase);

            sequence.Append(CreateMoveTween(slot.AnchorPos, rectTransform));
            Vector2 targetSize = slot.Container.rect.size;
            sequence.Join(CreateSizeTween(rectTransform, targetSize));
            sequence.Append(CreatePunchTween(itemOnUI));
            sequence.OnComplete(() =>
            {
                _movingToSlotItems.Remove(itemOnUI);
                onComplete?.Invoke();
            });
            sequence.Play();
            _movingToSlotItems.Add(itemOnUI, (sequence, targetSize));
        }

        public void ChangeMoveTweenForItem(ItemOnUI itemOnUI, Vector2 newPosition)
        {
            if (_movingToSlotItems.TryGetValue(itemOnUI, out var data) == false) return;

            Sequence newSequence = DOTween.Sequence();
            RectTransform rectTransform = itemOnUI.transform as RectTransform;

            newSequence.Append(CreateMoveTween(newPosition, rectTransform));
            newSequence.Join(CreateSizeTween(rectTransform, data.targetSize));
            newSequence.Append(CreatePunchTween(itemOnUI));
            newSequence.OnComplete(data.sequence.onComplete);
            data.sequence.Kill();
            newSequence.Play();
            data.sequence = newSequence;
        }

        private Tween CreatePunchTween(ItemOnUI itemOnUI) =>
            itemOnUI.transform
                .DOPunchScale(_animationConfig.Punch, _animationConfig.PunchDuration, _animationConfig.Elasticity)
                .SetEase(_animationConfig.PunchEase);

        private Tween CreateSizeTween(RectTransform rectTransform, Vector2 targetSize) =>
            rectTransform.DOSizeDelta(targetSize, _animationConfig.MoveDuration)
                .SetEase(_animationConfig.SizeEase);

        private Tween CreateMoveTween(Vector2 newPosition, RectTransform rectTransform) =>
            rectTransform
                .DOAnchorPos(newPosition, _animationConfig.MoveDuration)
                .SetEase(_animationConfig.MoveEase);

        public bool ItemIsMovingToSlot(ItemOnUI itemOnUI)
        {
            if (_movingToSlotItems.TryGetValue(itemOnUI, out var data) == false) return false;
            return data.sequence.IsPlaying();
        }
    }
}