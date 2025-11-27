using App.Factories;
using App.Gameplay.GameplayFSM;
using App.Gameplay.GameplayFSM.States;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
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
