using App.Gameplay.Common;
using App.Gameplay.Level;

namespace App.Gameplay.GameplayFSM.States
{
    public class InitLevelGameplayState : IGameplayState
    {
        private readonly Timer _lvlTimer;
        private readonly LevelConfig _levelConfig;
        
        public InitLevelGameplayState(Timer lvlTimer, LevelConfig levelConfig)
        {
            _lvlTimer = lvlTimer;
            _levelConfig = levelConfig;
        }

        public void Enter()
        {
            _lvlTimer.Start(_levelConfig.Time);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}