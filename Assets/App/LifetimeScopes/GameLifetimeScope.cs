using App.Factories;
using App.GameFSM;
using App.GameFSM.States;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
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
