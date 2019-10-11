using UnityEngine;

public class KinematicActuator : MonoBehaviour
{
    public Quaternion Rotation
    {
        get => transform.rotation;
        set => transform.rotation = value;
    }

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = new Vector3(value.x, value.y, 0);
    }

    private void Update()
    {
        Velocity += _flocking.GetVelocity();
        Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);
        Position += Velocity * Time.deltaTime;

        if (UseAngularVelocity)
        {
            AngularVelocity = _flocking.GetAngularVelocity();
            AngularVelocity = Mathf.Clamp(AngularVelocity, -MaxAngularVelocity, MaxAngularVelocity);
            Rotation = Quaternion.Euler(0, 0, Rotation.eulerAngles.z + AngularVelocity);
        }
        else
        {
            Rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Velocity.y, Velocity.x) * Mathf.Rad2Deg);
        }

        BoundToScreen();
    }
}
