using App.GameFSM;
using App.UI.LevelResult;

namespace App.Gameplay.GameplayFSM.States
{
    public class LevelWinGameplayState : IGameplayState
    {
        private readonly LevelResultPanel _levelResultPanel;
        private readonly GameStateMachine _gameplayStateMachine;

        public LevelWinGameplayState(LevelResultPanel levelResultPanel, GameStateMachine gameplayStateMachine)
        {
            _levelResultPanel = levelResultPanel;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void Enter()
        {
            _levelResultPanel.ShowWin();
            _levelResultPanel.OnRestartClicked.AddListener(OnRestartClickedHandler);
        }

        private void OnRestartClickedHandler()
        {
            _gameplayStateMachine.RestartInvoked = true;
        }

        public void Update()
        {
        }

        public void Exit()
        {
            _levelResultPanel.Hide();
            _levelResultPanel.OnRestartClicked.RemoveListener(OnRestartClickedHandler);
        }
    }
}