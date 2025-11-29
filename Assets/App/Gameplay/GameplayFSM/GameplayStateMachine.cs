using App.Core.FSM;
using App.Gameplay.GameplayFSM.States;
using App.Gameplay.Levels;
using VContainer.Unity;

namespace App.Gameplay.GameplayFSM
{
    public class GameplayStateMachine : StateMachine, IInitializable, ITickable
    {
        public bool LvlInitialized { get; set; } = false;
        private readonly IStateFactory<IGameplayState> _stateFactory;
        private readonly ILevelResultChecker _levelResultChecker;

        public GameplayStateMachine(IStateFactory<IGameplayState> stateFactory, ILevelResultChecker levelResultChecker)
        {
            _stateFactory = stateFactory;
            _levelResultChecker = levelResultChecker;
        }

        public void Initialize()
        {
            var initLvlState = _stateFactory.Create<InitLevelGameplayState>();
            var mainLoopState = _stateFactory.Create<MainLoopGameplayState>();
            var levelLoseState = _stateFactory.Create<LevelLoseGameplayState>();
            var levelWinState = _stateFactory.Create<LevelWinGameplayState>();
            
            AddTransition(initLvlState, mainLoopState, new FuncPredicate(() => LvlInitialized));
            AddTransition(mainLoopState, levelLoseState, new FuncPredicate(_levelResultChecker.LevelLose));
            AddTransition(mainLoopState, levelWinState, new FuncPredicate(_levelResultChecker.LevelWin));
            SetState(initLvlState);
        }

        public void Tick()
        {
            Update();
        }
    }
}
