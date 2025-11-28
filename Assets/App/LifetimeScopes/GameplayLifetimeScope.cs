using System.Timers;
using App.Factories;
using App.Gameplay.GameplayFSM;
using App.Gameplay.GameplayFSM.States;
using App.Gameplay.Level;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private LevelConfig testLevel; 
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterGameplayStates(builder);
            builder.RegisterEntryPoint<GameplayStateFactory>();
            builder.RegisterEntryPoint<GameplayStateMachine>();
            builder.Register<Timer>(Lifetime.Singleton);
            builder.RegisterInstance(testLevel);
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
