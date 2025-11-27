using VContainer;

namespace App.Core.FSM
{
    public abstract class BaseStateFactory<T>: IStateFactory<T> where T : IState
    {
        protected readonly IObjectResolver ObjectResolver;

        public BaseStateFactory(IObjectResolver objectResolver)
        {
            ObjectResolver = objectResolver;
        }

        public TState Create<TState>() where TState: T
        {
            return ObjectResolver.Resolve<TState>();
        }
    }
}