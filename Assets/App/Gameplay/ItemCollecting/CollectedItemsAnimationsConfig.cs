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
    }
}