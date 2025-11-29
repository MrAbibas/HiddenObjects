using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.GameFSM.States
{
    public class BootstrapGameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private AsyncOperation _loadSceneOperation;

        public BootstrapGameState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.GameplaySceneLoaded = false;
            _stateMachine.RestartInvoked = false;
            _loadSceneOperation = SceneManager.LoadSceneAsync("GameplayScene");
        }

        public void Update()
        {
            if (_loadSceneOperation.isDone)
                _stateMachine.GameplaySceneLoaded = true;
        }

        public void Exit()
        {
        }
    }
}