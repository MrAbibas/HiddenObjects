using System;
using System.Collections.Generic;
using App.Gameplay.Items;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Gameplay.ItemCollecting
{
    public class ItemCollector: IItemCollector
    {
        public event Action<ItemOnField> OnItemCollected;
        private readonly List<ItemOnField> _activeItems;
        private readonly List<ItemOnField> _inactiveItems;
        private readonly CollectedItemsContainer _collectedItemsContainer;
        private readonly ItemCollectAnimator _collectAnimator;
        private readonly ItemConfigs _itemConfigs;
        private Queue<ItemOnField> _queuedItems;
        
        public bool CollectingEnabled { get; private set; }

        public ItemCollector(List<ItemOnField> activeItems,
            CollectedItemsContainer collectedItemsContainer,
            ItemCollectAnimator collectAnimator,
            ItemConfigs itemConfigs)
        {
            _activeItems = activeItems;
            _collectedItemsContainer = collectedItemsContainer;
            _collectAnimator = collectAnimator;
            _itemConfigs = itemConfigs;
            _inactiveItems = new();
            _queuedItems = new Queue<ItemOnField>();
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

        private void OnItemClicked(ItemOnField itemOnField)
        {
            if (CollectingEnabled == false) return;
            if (CanCollect(itemOnField) == false) return;

            if (_collectAnimator.IsAnimationPlaying || _queuedItems.Count > 0)
                _queuedItems.Enqueue(itemOnField);
            else
                CollectItem(itemOnField);
        }

        public void CollectItem(ItemOnField itemOnField)
        {
            if (CanCollect(itemOnField) == false) return;

            itemOnField.OnClick.RemoveListener(OnItemClicked);
            _activeItems.Remove(itemOnField);
            itemOnField.gameObject.SetActive(false);
            _inactiveItems.Add(itemOnField);
            var slot = _collectedItemsContainer.GetSlotForItem(itemOnField.Type);
            var prefab = _itemConfigs.Configs[itemOnField.Type].onUIPrefab;
            ItemOnUI itemOnUI = Object.Instantiate(prefab, slot.Container);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                slot.Container, 
                Camera.main.WorldToScreenPoint(itemOnField.transform.position),
                null, 
                out Vector2 localPoint
            );
            itemOnUI.GetComponent<RectTransform>().anchoredPosition = localPoint;
            slot.SetItem(itemOnUI);
            _collectAnimator.PlayCollectAnimation(itemOnUI, slot, OnCollectAnimationComplete);
            OnItemCollected?.Invoke(itemOnField);
        }

        private void OnCollectAnimationComplete()
        {
            if (_queuedItems.Count > 0)
                CollectItem(_queuedItems.Dequeue());
        }

        public bool CanCollect(ItemOnField item)
        {
            return _collectedItemsContainer.HasEmptySlots;
        }
    }
}
