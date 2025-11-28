using App.Factories;
using App.Gameplay.Common;
using App.Gameplay.GameplayFSM;
using App.Gameplay.GameplayFSM.States;
using App.Gameplay.Levels;
using App.UI.CollectedItemsPanel;
using App.UI.HUD;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private LevelConfig testLevel;
        [SerializeField] private HUD hud;
        [SerializeField] private CollectedItemsPanel collectedItemsPanel;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterGameplayStates(builder);
            builder.RegisterEntryPoint<GameplayStateFactory>();
            builder.RegisterEntryPoint<GameplayStateMachine>().AsSelf();
            builder.Register<Timer>(Lifetime.Singleton);
            builder.RegisterInstance(testLevel);
            
            RegisterUIElements(builder);
        }

        private void RegisterUIElements(IContainerBuilder builder)
        {
            builder.RegisterComponent(hud);
            builder.RegisterComponent(collectedItemsPanel);
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
