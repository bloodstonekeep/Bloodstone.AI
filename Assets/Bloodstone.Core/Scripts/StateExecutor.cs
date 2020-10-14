using UnityEngine;

namespace Bloodstone
{
    public class StateExecutor : MonoBehaviour
    {
        private StateMachine<State> _stateMachine;

        private IdleState _idleState;
        private MineState _mineState;
        private FleeState _fleeState;

        public void Awake()
        {
            _idleState = new IdleState();
            _mineState = new MineState();
            _fleeState = new FleeState();

            _stateMachine = new StateMachine<State>(_idleState);
        }

        [ContextMenu(nameof(SetIdleState))]
        public void SetIdleState()
        {
            _stateMachine.SetState(_idleState);
        }

        [ContextMenu(nameof(SetMineState))]
        public void SetMineState()
        {
            _stateMachine.SetState(_mineState);
        }

        [ContextMenu(nameof(SetFleeState))]
        public void SetFleeState()
        {
            _stateMachine.SetState(_fleeState);
        }
    }

    public class IdleState : State
    {
        public override void Activate()
        {
            Debug.Log($"{nameof(Activate)} - {GetType().Name}");
        }

        public override void Deactivate()
        {
            Debug.Log($"{nameof(Deactivate)} - {GetType().Name}");
        }

        public override void Tick()
        {
        }
    }

    public class FleeState : State
    {
        public override void Activate()
        {
            Debug.Log($"{nameof(Activate)} - {GetType().Name}");
        }

        public override void Deactivate()
        {
            Debug.Log($"{nameof(Deactivate)} - {GetType().Name}");
        }

        public override void Tick()
        {
        }
    }

    public class MineState : State
    {
        public override void Activate()
        {
            Debug.Log($"{nameof(Activate)} - {GetType().Name}");
        }

        public override void Deactivate()
        {
            Debug.Log($"{nameof(Deactivate)} - {GetType().Name}");
        }

        public override void Tick()
        {
        }
    }
}