using System;
using App.Gameplay.Items;
using App.UI.CollectedItemsPanel;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Gameplay.ItemCollecting
{
    public class ItemCollectAnimator
    {
        private Sequence _animationSequence;
        private readonly ItemCollectAnimationConfig _animationConfig;
        private readonly CollectedItemsContainer _collectedItemsContainer;
        public bool IsAnimationPlaying => _animationSequence?.IsPlaying() ?? false;
        public ItemOnUI CurrentItem { get; private set; }

        public ItemCollectAnimator(ItemCollectAnimationConfig animationConfig)
        {
            _animationConfig = animationConfig;
        }

        public void PlayCollectAnimation(ItemOnUI itemOnUI, CollectedItemSlot slot, Action onComplete)
        {
            _animationSequence = DOTween.Sequence();
            CurrentItem = itemOnUI;
            RectTransform rectTransform = itemOnUI.transform as RectTransform;
            Vector2 targetJumpPos = rectTransform.anchoredPosition + new Vector2(
                _animationConfig.JumpOffset.x * -1 * Mathf.Sign(rectTransform.anchoredPosition.x),
                _animationConfig.JumpOffset.y);
            _animationSequence
                .Append(rectTransform
                .DOJumpAnchorPos(targetJumpPos, _animationConfig.JumpPower, _animationConfig.NumJumps, _animationConfig.JumpDuration)
                .SetEase(_animationConfig.JumpEase));

            _animationSequence.Join(itemOnUI.transform
                .DOPunchScale(_animationConfig.JumpPunch, _animationConfig.JumpPunchDuration, _animationConfig.JumpPunchElasticity))
                .SetEase(_animationConfig.JumpPunchEase);

            _animationSequence.Append(rectTransform.DOAnchorPos(slot.AnchorPos, _animationConfig.MoveDuration).SetEase(_animationConfig.MoveEase));
            Vector2 targetSize = slot.Container.rect.size;
            _animationSequence.Join(rectTransform.DOSizeDelta(targetSize, _animationConfig.MoveDuration)
                .SetEase(_animationConfig.SizeEase));
                
            _animationSequence
                .Append(itemOnUI.transform
                .DOPunchScale(_animationConfig.Punch, _animationConfig.PunchDuration, _animationConfig.Elasticity))
                .SetEase(_animationConfig.PunchEase);
            
            _animationSequence.OnComplete(() =>
            {
                onComplete?.Invoke();
                CurrentItem = null;
            });
            _animationSequence.Play();
        }

        public void AddTweenAfterCollect(Tween tween)
        {
            Sequence sequence = _animationSequence;
            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(sequence);
            _animationSequence.Append(tween);
        }
    }
}