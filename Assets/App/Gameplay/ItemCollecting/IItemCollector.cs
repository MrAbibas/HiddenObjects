using System;
using App.Gameplay.Items;
using VContainer.Unity;

namespace App.Gameplay.ItemCollecting
{
    public interface IItemCollector: IInitializable
    {
        event Action<ItemOnField> OnItemCollected;
        bool CollectingEnabled { get;}
        void EnableCollect();
        void DisableCollect();
        bool CanCollect(ItemOnField item);
        void CollectItem(ItemOnField itemOnField);
    }
}