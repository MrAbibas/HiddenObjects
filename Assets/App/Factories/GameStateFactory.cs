using App.GameFSM.States;
using VContainer;

namespace App.Factories
{
    public class GameStateFactory : IGameStateFactory
    {
        private readonly IObjectResolver _objectResolver;

        public GameStateFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public T Create<T>() where T: IGameState
        {
            return _objectResolver.Resolve<T>();
        }
    }

}