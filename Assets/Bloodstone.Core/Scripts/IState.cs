namespace Bloodstone
{
    public interface IState<T>
        where T : StateContext
    {
        void Activate();

        void Deactivate();

        void Tick();

        void TickPhysics();
    }
}