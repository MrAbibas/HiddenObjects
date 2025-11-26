using App.GameFSM.States;

namespace App.Factories
{
    public interface IGameStateFactory
    {
        T Create<T>() where T: IGameState;
    }
}