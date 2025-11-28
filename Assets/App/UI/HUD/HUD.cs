using System;
using System.Collections.Generic;
using App.Gameplay.Common;
using App.Gameplay.Items;
using App.Gameplay.Level;
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
        [SerializeField] private string lvlFormat = "{0}";
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private string timeFormat = "{mm:ss}";
        private Dictionary<ItemType, TargetItemOnUI> _targetItems;
        private ItemConfigs _itemConfigs;
        private Timer _levelTimer;
        private LevelConfig _levelConfig;

        [Inject]
        public void Construct(ItemConfigs itemConfigs, Timer levelTimer, LevelConfig levelConfig)
        {
            _itemConfigs = itemConfigs;
            _levelTimer = levelTimer;
            _levelConfig = levelConfig;
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
            
            lvlText.text = string.Format(lvlFormat, _levelConfig.ID + 1);

            _levelTimer.OnTimeUpdated += UpdateTimer;
            UpdateTimer(_levelTimer.TimeLeft);
        }

        public void UpdateTimer(float time)
        {
            timerText.text = new TimeSpan(0, 0, 0, 0, (int)(_levelTimer.TimeLeft * 1000)).ToString(timeFormat);
        }
    }
}