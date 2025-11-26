using UnityEngine;

namespace App.Gameplay.Items
{
    public class ItemOnField : MonoBehaviour
    {
        [field: SerializeField] public ItemType Type {get; private set;}
        [field: SerializeField] public SpriteRenderer SpriteRenderer {get; private set;}

        public int SortOrder
        {
            get => SpriteRenderer.sortingOrder;
            set => SpriteRenderer.sortingOrder = value;
        }
    }
}