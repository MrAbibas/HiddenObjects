using App.Gameplay.Items;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private ItemConfigs itemConfigs;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(itemConfigs);
        }
    }
}
