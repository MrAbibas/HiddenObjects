using App.Core.FSM;
using App.Gameplay.GameplayFSM.States;
using UnityEngine;
using VContainer.Unity;

namespace App.Gameplay.GameplayFSM
{
    public class GameplayStateMachine : StateMachine, IInitializable, ITickable
    {
        private readonly IStateFactory<IGameplayState> _stateFactory;
        
        public void Initialize()
        {
                
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}
