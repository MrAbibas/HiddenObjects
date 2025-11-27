using App.Factories;
using App.Gameplay.GameplayFSM;
using App.Gameplay.GameplayFSM.States;
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
            RegisterGameplayStates(builder);
            builder.RegisterEntryPoint<GameplayStateFactory>();
            builder.RegisterEntryPoint<GameplayStateMachine>();
        }

        private void RegisterGameplayStates(IContainerBuilder builder)
        {
            builder.Register<InitLevelGameplayState>(Lifetime.Transient);
            builder.Register<MainLoopGameplayState>(Lifetime.Transient);
            builder.Register<LevelLoseGameplayState>(Lifetime.Transient);
            builder.Register<LevelWinGameplayState>(Lifetime.Transient);
        }
    }
}
