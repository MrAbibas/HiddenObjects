using System;
using App.Gameplay.Items;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Gameplay.ItemCollecting
{
    public class ItemCollectAnimator
    {
        private readonly ItemCollectAnimationConfig _animationConfig;
        private Sequence _animationSequence;
        private readonly CollectedItemsContainer _collectedItemsContainer;
        public bool IsAnimationPlaying => _animationSequence.IsPlaying();
        
        public ItemCollectAnimator(ItemCollectAnimationConfig animationConfig)
        {
            _animationConfig = animationConfig;
        }

        public void PlayCollectAnimation(ItemOnUI itemOnUI, Action  onComplete)
        {
            _animationSequence = DOTween.Sequence();
            _animationSequence
                .Append(itemOnUI.transform
                .DOLocalJump(Vector3.zero, _animationConfig.JumpPower, _animationConfig.NumJumps, _animationConfig.JumpDuration)
                .SetEase(_animationConfig.JumpEasy));

            _animationSequence
                .Append(itemOnUI.transform
                .DOPunchScale(_animationConfig.Punch, _animationConfig.PunchDuration, _animationConfig.Elasticity))
                .SetEase(_animationConfig.PunchEasy);

            _animationSequence.OnComplete(() => onComplete?.Invoke());
        }
    }
}