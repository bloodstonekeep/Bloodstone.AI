using System;
using UnityEngine;

namespace Bloodstone.AI.Examples.Boids
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TeleporterTrigger : MonoBehaviour
    {
        public event Action<Transform> TriggerOccuredEvent;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            TriggerOccuredEvent?.Invoke(collider.transform);
        }
    }
}