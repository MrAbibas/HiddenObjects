using App.Gameplay.Items;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Gameplay.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Gameplay/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public SerializedDictionary<ItemType, int> targetItems;
        [field: SerializeField] public int ID {get; private set;}
        [field: SerializeField] public float Time { get; private set; }
        [field: SerializeField] public Sprite BackgroundSprite {get; private set;}
        [field: SerializeField] public Vector3 BackgroundScale {get; private set;}
        [field: SerializeField] public ItemOnFieldMarker[] ItemsOnField { get; private set; }

        public void SetItemOnField(ItemOnField[] itemsOnField)
        {
            ItemsOnField = new ItemOnFieldMarker[itemsOnField.Length];
            for (int i = 0; i < itemsOnField.Length; i++)
            {
                ItemsOnField[i] = new ItemOnFieldMarker()
                {
                    Type = itemsOnField[i].Type,
                    Position = itemsOnField[i].transform.position,
                    SortOrder = itemsOnField[i].SortOrder
                };
            }
        }

        public void SetBackground(Sprite sprite, Vector3 scale)
        {
            BackgroundSprite = sprite;
            BackgroundScale = scale;
        }
    }
}