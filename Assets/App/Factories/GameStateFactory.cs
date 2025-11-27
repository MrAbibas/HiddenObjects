using App.Core.FSM;
using App.GameFSM.States;
using VContainer;

namespace App.Factories
{
    public class GameStateFactory : BaseStateFactory<IGameState>
    {
        public GameStateFactory(IObjectResolver objectResolver) : base(objectResolver)
        {
        }
    }
}