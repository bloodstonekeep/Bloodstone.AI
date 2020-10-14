using System;

namespace Bloodstone
{
    [Serializable]
    public abstract class State
    {
        public abstract void Activate();

        public abstract void Deactivate();

        public abstract void Tick();

        public virtual void LateTick()
        {
        }

        public virtual void TickPhysics()
        {
        }
    }
}