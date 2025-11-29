using System;
using System.Collections.Generic;
using App.Gameplay.Common;
using App.Gameplay.ItemCollecting;
using App.Gameplay.Items;
using App.Gameplay.Levels;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.UI.HUD
{
    public class HUD : MonoBehaviour, IInitializable
    {
        [SerializeField] private RectTransform targetItemsContainer;
        [SerializeField] private TargetItemOnUI targetItemOnUIPrefab;
        [SerializeField] private TMP_Text lvlText;
        [SerializeField] private string lvlFormat = "{0}";
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private string timeFormat = "{mm:ss}";
        private Dictionary<ItemType, TargetItemOnUI> _targetItems;
        private ItemConfigs _itemConfigs;
        private Timer _levelTimer;
        private LevelConfig _levelConfig;
        private LeftToCollectItemsContainer _leftToCollectItemsContainer;

        [Inject]
        public void Construct(ItemConfigs itemConfigs,
            Timer levelTimer,
            LevelConfig levelConfig,
            LeftToCollectItemsContainer leftToCollectItemsContainer)
        {
            _itemConfigs = itemConfigs;
            _levelTimer = levelTimer;
            _levelConfig = levelConfig;
            _leftToCollectItemsContainer = leftToCollectItemsContainer;
        }

        public void Initialize()
        {
            _leftToCollectItemsContainer.UpdateLeftToCollect += UpdateLeftToCollectHandler;
            ShowTargetItems(_leftToCollectItemsContainer.Items);

            lvlText.text = string.Format(lvlFormat, _levelConfig.ID + 1);

            _levelTimer.OnTimeUpdated += UpdateTimer;
            UpdateTimer(_levelTimer.TimeLeft);
        }

        private void UpdateLeftToCollectHandler(ItemType itemType, int countLeft)
        {
            if (countLeft > 0)
                _targetItems[itemType].UpdateCount(countLeft);
            else
            {
                _targetItems[itemType].Hide();
                _targetItems.Remove(itemType);
            }
        }

        private void ShowTargetItems(Dictionary<ItemType, int> targetItems)
        {
            _targetItems = new Dictionary<ItemType, TargetItemOnUI>();
            foreach (var targetItem in targetItems)
            {
                var itemOnUI = Instantiate(targetItemOnUIPrefab, targetItemsContainer);
                itemOnUI.Show(_itemConfigs.Configs[targetItem.Key].icon, targetItem.Value);
                _targetItems.Add(targetItem.Key, itemOnUI);
            }
        }

        public void UpdateTimer(float time)
        {
            timerText.text = new TimeSpan(0, 0, 0, 0, (int)(_levelTimer.TimeLeft * 1000)).ToString(timeFormat);
        }

        private void OnDestroy()
        {
            _leftToCollectItemsContainer.UpdateLeftToCollect -= UpdateLeftToCollectHandler;
        }
    }
}