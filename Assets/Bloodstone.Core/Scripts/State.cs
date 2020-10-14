using UnityEngine;

namespace Bloodstone
{
    public abstract class State<T> : IState<T>
        where T : StateContext
    {
        [SerializeField]
        private T _context;

        public T Context => _context;

        public abstract void Activate();

        public abstract void Deactivate();

        public virtual void Tick()
        {
        }

        public virtual void TickPhysics()
        {
        }
    }
}