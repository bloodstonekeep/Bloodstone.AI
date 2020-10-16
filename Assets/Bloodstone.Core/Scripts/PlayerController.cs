using UnityEngine;

namespace Bloodstone
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStateContext _context = null;

        private StateMachine<PlayerState, PlayerStateContext> _stateMachine;

        private SuperPlayerState _findState;
        private NormalPlayerState _idleState;

        private void Awake()
        {
            _findState = new SuperPlayerState();
            _idleState = new NormalPlayerState();

            _stateMachine = new StateMachine<PlayerState, PlayerStateContext>(_idleState);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void FixedUpdate()
        {
            _stateMachine.TickPhysics();
        }
    }

    public abstract class PlayerState : State<PlayerStateContext>
    {
        public override void Activate()
        {
            Debug.Log($"Activate: {GetType().Name} on object: {Context.name}");
        }

        public override void Deactivate()
        {
            Debug.Log($"Deactivate: {GetType().Name} on object: {Context.name}");
        }
    }

    public class NormalPlayerState : PlayerState
    {
    }

    public class SuperPlayerState : PlayerState
    {
    }
}