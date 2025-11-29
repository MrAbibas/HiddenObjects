using DG.Tweening;
using UnityEngine;

namespace App.Gameplay.ItemCollecting
{
    [CreateAssetMenu(fileName = "CollectedItemsAnimations", menuName = "Gameplay/Animations/CollectedItemsAnimations")]
    public class CollectedItemsAnimationsConfig : ScriptableObject
    {
        [field: Header("MoveToSlot")]
        [field: SerializeField] public float MoveToSlotDuration { get; private set; }
        [field: SerializeField] public Ease MoveToSlotEase { get; private set; }

        [field: Header("MergeSlots")]
        [field: SerializeField] public Vector3 MergeSideItemsEndScale { get; private set; }
        [field: SerializeField] public float MergeSideItemsHideDuration { get; private set; }
        [field: SerializeField] public Ease MergeSideItemsHideErase { get; private set; }
        [field: SerializeField] public Vector2 MergeCenterItemJumpOffset { get; private set; }
        [field: SerializeField] public float MergeCenterItemJumpPower { get; private set; }
        [field: SerializeField] public int MergeCenterItemNumJumps { get; private set; }
        [field: SerializeField] public float MergeCenterItemJumpDuration { get; private set; }
        [field: SerializeField] public Ease MergeCenterItemJumpEase { get; private set; }
    }
}