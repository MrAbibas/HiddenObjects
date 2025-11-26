using System.Collections.Generic;
using App.Gameplay.Items;
using TMPro;
using UnityEngine;
using VContainer;

namespace App.UI.HUD
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private RectTransform targetItemsContainer;
        [SerializeField] private TargetItemOnUI targetItemOnUIPrefab;
        [SerializeField] private TMP_Text lvlText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private string timeFormat;
        private Dictionary<ItemType, TargetItemOnUI> _targetItems;
        private ItemConfigs _itemConfigs;

        [Inject]
        public void Construct(ItemConfigs itemConfigs)
        {
            _itemConfigs = itemConfigs;
        }

        public void Init(Dictionary<ItemType, int> targetItems)
        {
            _targetItems = new Dictionary<ItemType, TargetItemOnUI>();
            foreach (var targetItem in targetItems)
            {
                var itemOnUI = Instantiate(targetItemOnUIPrefab, targetItemsContainer);
                itemOnUI.Show(_itemConfigs.Configs[targetItem.Key].Sprite, targetItem.Value);
                _targetItems.Add(targetItem.Key, itemOnUI);
            }
        }
    }
}