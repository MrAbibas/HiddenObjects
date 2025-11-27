using App.Factories;
using App.GameFSM;
using App.GameFSM.States;
using App.Gameplay.Items;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ItemConfigs itemConfigs;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(itemConfigs);
            RegisterGameStates(builder);
            builder.RegisterEntryPoint<GameStateFactory>();
            builder.RegisterEntryPoint<GameStateMachine>().AsSelf();
        }

        private static void RegisterGameStates(IContainerBuilder builder)
        {
            builder.Register<BootstrapGameState>(Lifetime.Transient);
            builder.Register<GameplayState>(Lifetime.Transient);
        }
    }
}
