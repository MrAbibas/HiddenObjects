using App.Gameplay.Common;
using App.Gameplay.Level;

namespace App.Gameplay.GameplayFSM.States
{
    public class InitLevelGameplayState : IGameplayState
    {
        private readonly Timer _lvlTimer;
        private readonly LevelConfig _levelConfig;
        private readonly GameplayStateMachine _stateMachine;
        
        public InitLevelGameplayState(Timer lvlTimer, LevelConfig levelConfig,  GameplayStateMachine stateMachine)
        {
            _lvlTimer = lvlTimer;
            _levelConfig = levelConfig;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _lvlTimer.Start(_levelConfig.Time);
            _stateMachine.LvlInitialized = true;
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}