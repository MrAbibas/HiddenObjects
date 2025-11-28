using System;
using System.Collections.Generic;
using System.Linq;
using App.Gameplay.Items;
using App.UI.CollectedItemsPanel;

namespace App.Gameplay.ItemCollecting
{
    public class CollectedItemsContainer
    {
        public ItemType[] CollectedItems { get; private set; }
        private readonly CollectedItemsPanel _collectedItemsPanel;

        public bool HasEmptySlots => CollectedItems.Any(x => x == ItemType.None);
        private List<CollectedItemSlot> Slots => _collectedItemsPanel.Slots;
        
        public CollectedItemsContainer(CollectedItemsPanel collectedItemsPanel)
        {
            _collectedItemsPanel = collectedItemsPanel;
            CollectedItems = new ItemType[this._collectedItemsPanel.Slots.Count];
        }

        public CollectedItemSlot GetSlotForItem(ItemType itemType)
        {
            for (int i = 0; i < CollectedItems.Length; i++)
            {
                if (Slots[i].ItemType == ItemType.None)
                {
                    return Slots[i];
                }
            }

            return null;
        }
    }
}