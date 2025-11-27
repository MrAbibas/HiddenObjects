using App.Core.FSM;
using App.Gameplay.GameplayFSM.States;
using VContainer;

namespace App.Factories
{
    public class GameplayStateFactory : BaseStateFactory<IGameplayState>
    {
        public GameplayStateFactory(IObjectResolver objectResolver) : base(objectResolver)
        {
        }
    }
}