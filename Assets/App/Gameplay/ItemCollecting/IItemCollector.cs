using App.Gameplay.Items;
using VContainer.Unity;

namespace App.Gameplay.ItemCollecting
{
    public interface IItemCollector: IInitializable
    {
        bool CollectingEnabled { get;}
        void EnableCollect();
        void DisableCollect();
        bool CanCollect(ItemOnField item);
        void CollectItem(ItemOnField item);
    }
}