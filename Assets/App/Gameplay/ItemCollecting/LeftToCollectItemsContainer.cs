using System;
using System.Collections.Generic;
using App.Gameplay.Items;
using App.Gameplay.Levels;
using VContainer.Unity;

namespace App.Gameplay.ItemCollecting
{
    public class LeftToCollectItemsContainer : IInitializable, IDisposable
    {
        public event Action<ItemType, int> UpdateLeftToCollect;
        private readonly IItemCollector _itemCollector;
        public Dictionary<ItemType, int> Items { get; private set; }

        public LeftToCollectItemsContainer(LevelConfig levelConfig, IItemCollector collector)
        {
            Items = new(levelConfig.targetItems);
            _itemCollector = collector;
        }

        public void Initialize()
        {
            _itemCollector.OnItemCollected += OnItemCollectedHandler;
        }

        private void OnItemCollectedHandler(ItemOnField itemOnField)
        {
            if (Items.ContainsKey(itemOnField.Type) == false) return;

            Items[itemOnField.Type]--;
            UpdateLeftToCollect?.Invoke(itemOnField.Type, Items[itemOnField.Type]);
            if (Items[itemOnField.Type] == 0)
                Items.Remove(itemOnField.Type);
        }

        public void Dispose()
        {
            _itemCollector.OnItemCollected -= OnItemCollectedHandler;
        }
    }
}