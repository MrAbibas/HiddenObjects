using System.Collections.Generic;
using App.Gameplay.Items;
using App.UI.CollectedItemsPanel;

namespace App.Gameplay.ItemCollecting
{
    public class CollectedItemsContainer
    {
        public ItemType[] CollectedItems { get; private set; }
        private readonly CollectedItemsPanel _collectedItemsPanel;
        private List<CollectedItemSlot> Slots => _collectedItemsPanel.Slots;
        
        public CollectedItemsContainer(CollectedItemsPanel collectedItemsPanel)
        {
            _collectedItemsPanel = collectedItemsPanel;
            CollectedItems = new ItemType[this._collectedItemsPanel.Slots.Count];
        }

        public void AddItem(ItemOnUI itemOnUI, out CollectedItemSlot collectedItemSlot)
        {
            for (int i = 0; i < CollectedItems.Length; i++)
            {
                if (Slots[i].ItemType == ItemType.None)
                {
                    Slots[i].SetItem(itemOnUI);
                    collectedItemSlot = Slots[i];
                    return;
                }
            }

            collectedItemSlot = null;
        }
    }
}