using DG.Tweening;
using UnityEngine;

namespace App.Gameplay.ItemCollecting
{
    [CreateAssetMenu(fileName = "ItemCollectAnimation", menuName = "Gameplay/Animations/ItemCollectAnimation")]
    public class ItemCollectAnimationConfig : ScriptableObject
    {
        [field: Header("Jump")]
        [field: SerializeField] public Vector2 JumpOffset { get; private set; }
        [field: SerializeField] public float JumpDuration { get; private set; }
        [field: SerializeField] public float JumpPower { get; private set; }
        [field: SerializeField] public int NumJumps { get; private set; }
        [field: SerializeField] public Ease JumpEase { get; private set; }
        
        [field: Header("PunchJump")]
        [field: SerializeField]public Vector3 JumpPunch { get; private set; }
        [field: SerializeField]public float JumpPunchDuration { get; private set; }
        [field: SerializeField]public int JumpPunchElasticity { get; private set; }
        [field: SerializeField]public Ease JumpPunchEase { get; private set; }

        [field: Header("Move")]
        [field: SerializeField] public float MoveDuration { get; private set; }
        [field: SerializeField] public Ease MoveEase { get; private set; }
        
        [field: Header("SizeDelta")]
        [field: SerializeField] public Ease SizeEase { get; set; }
        
        [field: Header("EndPunch")]
        [field: SerializeField] public Vector3 Punch { get; private set; }
        [field: SerializeField] public float PunchDuration { get; private set; }
        [field: SerializeField] public int Elasticity { get; private set; }
        [field: SerializeField] public Ease PunchEase { get; private set; }
        
        [field: Header("HideItemOnField")]
        [field: SerializeField] public Vector3 HideItemOnFieldEndScale { get; private set; }
        [field: SerializeField] public float HideItemOnFieldDuration { get; private set; }
    }
}