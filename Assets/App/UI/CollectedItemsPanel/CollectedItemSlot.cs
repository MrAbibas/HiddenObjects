using App.Gameplay.Items;
using UnityEngine;

namespace App.UI.CollectedItemsPanel
{
    public class CollectedItemSlot : MonoBehaviour
    {
        [field: SerializeField] public RectTransform Container { get; private set; }
        public ItemOnUI Item { get; private set; }
        public ItemType ItemType => Item != null ? Item.Type : ItemType.None;
        
        public void SetItem(ItemOnUI itemOnUI)
        {
            Item = itemOnUI;
        }

        public void ClearItem() => Item = null;
    }
}