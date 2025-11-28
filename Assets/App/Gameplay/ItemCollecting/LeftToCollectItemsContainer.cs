using System.Collections.Generic;
using App.Gameplay.Items;
using App.Gameplay.Levels;

namespace App.Gameplay.ItemCollecting
{
    public class LeftToCollectItemsContainer
    {
        public Dictionary<ItemType, int> Items {get; private set;}

        public LeftToCollectItemsContainer(LevelConfig levelConfig)
        {
            Items = new(levelConfig.targetItems);
        }
    }
}