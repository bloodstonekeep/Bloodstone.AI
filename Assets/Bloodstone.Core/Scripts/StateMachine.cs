using Bloodstone.Extensions;

namespace Bloodstone
{
    public class StateMachine<TState, TContext>
        where TContext : StateContext
        where TState : IState<TContext>
    {
        private TState _currentState;

        public StateMachine(TState state)
        {
            State = state.ThrowIfNull(nameof(state));
        }

        public TState State
        {
            get => _currentState;
            set
            {
                if (value != null
                    && !_currentState.Equals(value))
                {
                    _currentState.Deactivate();
                    _currentState = value;
                    _currentState.Activate();
                }
            }
        }

        public void Tick()
        {
            _currentState.Tick();
        }

        public void TickPhysics()
        {
            _currentState.TickPhysics();
        }
    }
}