using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bloodstone.AI.Examples.Boids
{
    public class BoidsSymulation : MonoBehaviour
    {
        [SerializeField]
        private BoidsFactory _boidsFactory;
        [SerializeField]
        private BoundsController _bounds;

        private List<Boid> _boids = new List<Boid>();
        private Dictionary<Boid, AISubsystem> _subsystems = new Dictionary<Boid, AISubsystem>();

        private void Update()
        {
            foreach(var boid in _boids)
            {
                _subsystems[boid].Neighbourhood = _boids.Where(a => a != boid)
                                                        .Select(b => b.Agent)
                                                        .Where(a => (a.transform.position - boid.Agent.Position).sqrMagnitude < boid.Agent.PredictionRange * boid.Agent.PredictionRange)
                                                        .ToList();
            }
        }

        public void RemoveBoid()
        {
            if(_boids.Count == 0)
            {
                return;
            }

            var lastIndex = _boids.Count - 1;
            var boid = _boids[lastIndex];

            _subsystems.Remove(boid);
            _boids.RemoveAt(lastIndex);

            boid.Die();
        }

        public void AddBoid()
        {
            var (boid, system) = _boidsFactory.CreateNewBoid();

            _boids.Add(boid);
            _subsystems[boid] = system;
        }

        public void SwitchBorderMode(bool value)
        {
            var newMode = BoundsController.ParseToMode(value);
            _bounds.Mode = newMode;
        }
    }
}