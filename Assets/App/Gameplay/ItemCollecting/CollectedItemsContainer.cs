using System;
using System.Collections.Generic;
using System.Linq;
using App.Gameplay.Items;
using App.UI.CollectedItemsPanel;
using VContainer.Unity;

namespace App.Gameplay.ItemCollecting
{
    public class CollectedItemsContainer
    {
        private readonly CollectedItemsPanel _collectedItemsPanel;
        private readonly CollectedItemsContainerAnimator _animator;
        public bool HasEmptySlots => Slots.Any(x => x.ItemType == ItemType.None);
        private List<CollectedItemSlot> Slots => _collectedItemsPanel.Slots;
        
        public CollectedItemsContainer(CollectedItemsPanel collectedItemsPanel,  CollectedItemsContainerAnimator animator)
        {
            _collectedItemsPanel = collectedItemsPanel;
            _animator = animator;
        }

        public CollectedItemSlot GetSlotForItem(ItemType itemType)
        {
            bool typeExists = false;
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].ItemType == ItemType.None)
                    return Slots[i];

                if (i == Slots.Count - 1) break;
                if (Slots[i].ItemType == itemType)
                {
                    typeExists = true;
                    continue;
                }

                if (typeExists && Slots[i].ItemType != itemType)
                {
                    ShiftItemsToRightSlot(i);
                    Slots[i].ClearItem();
                    return Slots[i];
                }
            }

            return null;
        }

        private void ShiftItemsToRightSlot(int i)
        {
            for (int j = Slots.Count - 1; j > i; j--)
            {
                if(Slots[j - 1].ItemType == ItemType.None) continue;
                Slots[j].SetItem(Slots[j - 1].Item);
                Slots[j].Item.transform.SetParent(Slots[j].Container, true);
                _animator.MoveItemToSlotAnimation(Slots[j]);
            }
        }
    }
}