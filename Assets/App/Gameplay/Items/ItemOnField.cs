using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace App.Gameplay.Items
{
    public class ItemOnField : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] public ItemType Type {get; private set;}
        [field: SerializeField] public SpriteRenderer SpriteRenderer {get; private set;}
        
        public UnityEvent<ItemOnField> OnClick = new();

        public int SortOrder
        {
            get => SpriteRenderer.sortingOrder;
            set => SpriteRenderer.sortingOrder = value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
    }
}