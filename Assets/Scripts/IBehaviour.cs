using UnityEngine;

namespace Bloodstone.AI
{
    public interface IBehaviour
    {
        Vector2 GetVelocity();

        float GetAngularVelocity();
    }

    public abstract class Behaviour : MonoBehaviour, IBehaviour
    {
        protected Agent2D agent;

        public abstract float GetAngularVelocity();
        public abstract Vector2 GetVelocity();

        protected virtual void Awake()
        {
            agent = GetComponent<Agent2D>();
        }
    }
}