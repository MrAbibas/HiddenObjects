using App.Core.FSM;
using App.Gameplay.GameplayFSM.States;
using VContainer.Unity;

namespace App.Gameplay.GameplayFSM
{
    public class GameplayStateMachine : StateMachine, IInitializable, ITickable
    {
        public bool LvlInitialized { get; set; } = false;
        private readonly IStateFactory<IGameplayState> _stateFactory;

        public GameplayStateMachine(IStateFactory<IGameplayState> stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Initialize()
        {
            var initLvlState = _stateFactory.Create<InitLevelGameplayState>();
            var mainLoopState = _stateFactory.Create<MainLoopGameplayState>();
            var levelLoseState = _stateFactory.Create<LevelLoseGameplayState>();
            var levelWinState = _stateFactory.Create<LevelWinGameplayState>();
            
            AddTransition(initLvlState, mainLoopState, new FuncPredicate(() => LvlInitialized));
            SetState(initLvlState);
        }

        public void Tick()
        {
            Update();
        }
    }
}
