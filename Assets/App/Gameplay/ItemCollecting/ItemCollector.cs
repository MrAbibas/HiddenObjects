using System;
using System.Collections.Generic;
using App.Gameplay.Items;
using UnityEngine;

namespace App.Gameplay.ItemCollecting
{
    public class ItemCollector: IItemCollector
    {
        public event Action<ItemOnField> OnItemCollected;
        private readonly List<ItemOnField> _activeItems;
        private readonly List<ItemOnField> _inactiveItems;

        public bool CollectingEnabled { get; private set; }

        public ItemCollector(List<ItemOnField> activeItems)
        {
            _activeItems = activeItems;
            _inactiveItems = new();
        }

        public void EnableCollect() => CollectingEnabled = true;
        public void DisableCollect() => CollectingEnabled = false;
        
        public void Initialize()
        {
            foreach (var item in _activeItems)
            {
                item.OnClick.AddListener(OnItemClicked);
            }
        }

        private void OnItemClicked(ItemOnField item)
        {
            if(CollectingEnabled == false) return;
            if(CanCollect(item) == false) return;
            
            CollectItem(item);
        }

        public void CollectItem(ItemOnField item)
        {
            Debug.Log($"Collecting {item.Type}");
        }

        public bool CanCollect(ItemOnField item)
        {
            return true;
        }
    }
}
