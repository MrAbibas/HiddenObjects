using DG.Tweening;
using UnityEngine;

namespace App.Gameplay.ItemCollecting
{
    [CreateAssetMenu(fileName = "ItemCollectAnimation", menuName = "Gameplay/Animations/ItemCollectAnimation")]
    public class ItemCollectAnimationConfig : ScriptableObject
    {
        [Header("Jump")]
        [field: SerializeField] public float JumpDuration { get; private set; }
        [field: SerializeField] public float JumpPower { get; private set; }
        [field: SerializeField] public int NumJumps { get; private set; }
        [field: SerializeField] public Ease JumpEasy { get; private set; }

        [Header("EndPunch")]
        [field: SerializeField] public Vector3 Punch { get; private set; }
        [field: SerializeField] public float PunchDuration { get; private set; }
        [field: SerializeField] public int Elasticity { get; private set; }
        [field: SerializeField] public Ease PunchEasy { get; private set; }
    }
}