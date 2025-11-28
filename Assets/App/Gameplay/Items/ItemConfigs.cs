using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.Gameplay.Items
{
    [CreateAssetMenu(fileName = "ItemConfigs", menuName = "Gameplay/ItemConfigs")]
    public class ItemConfigs : ScriptableObject
    {
        [Serializable]
        public class ItemConfig
        {
            public Sprite icon;
            public ItemOnUI onUIPrefab;
        }
        [field: SerializeField] public SerializedDictionary<ItemType, ItemConfig> Configs { get; private set; }
    }
}