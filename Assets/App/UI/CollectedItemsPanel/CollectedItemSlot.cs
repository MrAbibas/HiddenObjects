using System;
using App.Gameplay.Items;
using UnityEngine;

namespace App.UI.CollectedItemsPanel
{
    public class CollectedItemSlot : MonoBehaviour
    {
        private RectTransform _rectTransform;
        [field: SerializeField] public RectTransform Container { get; private set; }
        public Vector2 AnchorPos => _rectTransform.anchoredPosition;
        public ItemOnUI Item { get; private set; }
        public ItemType ItemType => Item != null ? Item.Type : ItemType.None;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;
        }

        public void SetItem(ItemOnUI itemOnUI)
        {
            Item = itemOnUI;
        }

        public void ClearItem() => Item = null;
    }
}