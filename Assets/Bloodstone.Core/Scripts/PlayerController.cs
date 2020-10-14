using UnityEngine;

namespace Bloodstone
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStateContext _context = null;

        private StateMachine<PlayerState, PlayerStateContext> _stateMachine;

        private void Awake()
        {
            var idleState = new IdlePlayerState();

            _stateMachine = new StateMachine<PlayerState, PlayerStateContext>(idleState);
        }
    }

    public abstract class PlayerState : State<PlayerStateContext>
    {
    }

    public class IdlePlayerState : PlayerState
    {
        public override void Activate()
        {
        }

        public override void Deactivate()
        {
        }
    }
}