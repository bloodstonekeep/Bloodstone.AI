using System;
using UnityEngine;

namespace Bloodstone
{
    [Serializable]
    public class StateMachine<T> where T : State
    {
        [SerializeField]
        private State _currentState;

        public StateMachine(State initialState)
        {
            if (initialState is null)
            {
                throw new ArgumentNullException(nameof(initialState));
            }

            SetState(initialState);
        }

        public void Tick()
        {
            _currentState?.Tick();
        }

        public void SetState(State newState)
        {
            if (_currentState != newState)
            {
                _currentState?.Deactivate();
                _currentState = newState;
                _currentState.Activate();
            }
        }
    }
}