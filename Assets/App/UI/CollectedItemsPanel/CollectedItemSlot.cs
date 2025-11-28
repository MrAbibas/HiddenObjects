using App.Gameplay.Items;
using UnityEngine;

namespace App.UI.CollectedItemsPanel
{
    public class CollectedItemSlot : MonoBehaviour
    {
        [SerializeField] private RectTransform _container; 
        public ItemOnUI Item { get; private set; }
        public ItemType ItemType => Item != null ? Item.Type : ItemType.None;
        
        public void SetItem(ItemOnUI itemOnUI)
        {
            itemOnUI.transform.SetParent(_container, true);
            Item = itemOnUI;
        }

        public void ClearItem() => Item = null;
    }
}