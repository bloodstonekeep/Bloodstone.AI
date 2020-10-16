using System;
using UnityEngine;

namespace Bloodstone.AI.Examples
{
    public class Unit : MonoBehaviour
    {
        //todo: observer
        public event Action<Unit> DieEvent;

        [SerializeField]
        private float _health = 100;
        [SerializeField]
        private byte _team;

        protected bool alive = true;

        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value <= 0)
                {
                    Die();
                    value = 0;
                }

                _health = value;
            }
        }
        public byte Team => _team;

        protected virtual void Die()
        {
            alive = false;

            DieEvent?.Invoke(this);
#if DEBUG
            Debug.Log($"{this.GetInstanceID()} | {this.name} died.");
#endif

            Destroy(this.gameObject);
        }
    }

    public class SelectableUnit : Unit, ISelectableUnit
    {
        [SerializeField]
        private float _damage;

        private Vector3? _targetPosition;

        public float Damage => _damage;

        public ISelectableUnit SelectUnit()
        {
            return this;
        }

        public void MoveToTarget(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        private void Update()
        {
            if (_targetPosition.HasValue)
            {
                var direction = (_targetPosition.Value - transform.position).normalized;

                var targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, _targetPosition.Value, Time.deltaTime);
            }
        }
    }
}