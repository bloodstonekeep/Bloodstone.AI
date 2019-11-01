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

        private List<Agent> _agents = new List<Agent>();
        private Dictionary<Agent, AISubsystem> _subsystems = new Dictionary<Agent, AISubsystem>();

        private void Update()
        {
            foreach(var agent in _agents)
            {
                _subsystems[agent].Neighborhood = _agents.Where(a => a != agent)
                                                    .Where(a => (a.transform.position - agent.transform.position).sqrMagnitude < agent.PredictionRange * agent.PredictionRange)
                                                    .ToList();
            }
        }

        public void RemoveBoid()
        {
            if(_agents.Count == 0)
            {
                return;
            }

            var lastIndex = _agents.Count - 1;
            var agent = _agents[lastIndex];

            _subsystems.Remove(agent);
            _agents.RemoveAt(lastIndex);

            Destroy(agent.transform.parent.gameObject);
        }

        public void AddBoid()
        {
            var (agent, system) = _boidsFactory.CreateNewBoid();

            _agents.Add(agent);
            _subsystems[agent] = system;
        }

        public void SwitchBorderMode(bool value)
        {
            var newMode = _bounds.ParseMode(value);
            _bounds.Mode = newMode;
        }
    }
}