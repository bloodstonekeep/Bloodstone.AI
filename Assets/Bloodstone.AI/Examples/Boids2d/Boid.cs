using System;
using UnityEngine;

namespace Bloodstone.AI.Examples.Boids
{
    public class Boid : MonoBehaviour
    {
        [SerializeField]
        private Agent _agent;

        public Agent Agent => _agent;

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}