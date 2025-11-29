using App.Core.FSM;
using App.Factories;
using App.GameFSM.States;
using VContainer.Unity;

namespace App.GameFSM
{
    public class GameStateMachine : StateMachine, IInitializable, ITickable
    {
        private readonly IStateFactory<IGameState> _stateFactory;
        public bool GameplaySceneLoaded { get; set; } = false;
        public bool RestartInvoked { get; set; } = false;

        public GameStateMachine(IStateFactory<IGameState> stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Initialize()
        {
            var bootstrapState = _stateFactory.Create<BootstrapGameState>();
            var gameplayState = _stateFactory.Create<GameplayState>();
            AddTransition(bootstrapState, gameplayState, new FuncPredicate(() => GameplaySceneLoaded));
            AddTransition(gameplayState, bootstrapState, new FuncPredicate(() => RestartInvoked));
            SetState(bootstrapState);
        }

        public void Tick()
        {
            Update();
        }
    }
}
