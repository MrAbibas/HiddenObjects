using App.Gameplay.Common;
using App.Gameplay.Levels;

namespace App.Gameplay.GameplayFSM.States
{
    public class InitLevelGameplayState : IGameplayState
    {
        private readonly Timer _lvlTimer;
        private readonly LevelConfig _levelConfig;
        private readonly GameplayStateMachine _stateMachine;
        private readonly ILevelResultChecker _levelResultChecker;
        
        public InitLevelGameplayState(Timer lvlTimer,
            LevelConfig levelConfig,
            GameplayStateMachine stateMachine,
            ILevelResultChecker levelResultChecker)
        {
            _lvlTimer = lvlTimer;
            _levelConfig = levelConfig;
            _stateMachine = stateMachine;
            _levelResultChecker = levelResultChecker;
        }

        public void Enter()
        {
            _lvlTimer.Start(_levelConfig.Time);
            _stateMachine.LvlInitialized = true;
            _levelResultChecker.Initialize();
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}