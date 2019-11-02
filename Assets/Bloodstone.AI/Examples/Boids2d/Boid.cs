using UnityEngine;

namespace Bloodstone.AI.Examples.Boids
{
    public class Boid : MonoBehaviour
    {
        private const string SpawnAnimationName = "Spawn";
        private readonly int _spawnAnimationHash;

        [SerializeField]
        private Agent _agent;

        [SerializeField]
        private ParticleSystem _dieParticles;
        [SerializeField]
        private Animator _animator;

        public Agent Agent => _agent;

        public Boid()
        {
            _spawnAnimationHash = Animator.StringToHash(SpawnAnimationName);
        }

        public void Die()
        {
            _dieParticles.time = 0;
            _dieParticles.Play();

            _dieParticles.transform.SetParent(null);
            Destroy(_dieParticles.gameObject, _dieParticles.main.duration);

            Destroy(gameObject);
        }

        private void Start()
        {
            _animator.keepAnimatorControllerStateOnDisable = true;
            _animator.Play(_spawnAnimationHash);
        }
    }
}