using UnityEngine;

namespace Bloodstone.AI.Steering.Rotation
{
    public class LookForward : Align
    {
        public override Vector3 GetSteering()
        {
            if(Agent.Velocity == Vector3.zero)
            {
                return Vector3.zero;
            }

            if(Agent.WorldOrientation == WorldOrientation.XY)
            {
                targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(Agent.Velocity.y, Agent.Velocity.x) * Mathf.Rad2Deg);
            }
            else
            {
                targetRotation = Quaternion.LookRotation(Agent.Velocity.normalized);
            }

            return base.GetSteering();
        }
    }
}