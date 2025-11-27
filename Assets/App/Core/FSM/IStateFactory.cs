namespace App.Core.FSM
{
    public interface IStateFactory<T> where T : IState
    {
        TState Create<TState>() where TState : T;
    }
}