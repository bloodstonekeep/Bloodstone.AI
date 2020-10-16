using System.Collections.Generic;
using Bloodstone.AI.Pathfinding;
using UnityEditor;
using UnityEngine;

namespace Bloodstone.AI.Examples
{
    [RequireComponent(typeof(Unit))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider))]
    public class Actor : MonoBehaviour
    {
        [SerializeField]
        private AStar _pathProvider;

        [SerializeField]
        private float _rotationSpeed = 2;
        [SerializeField]
        private float _movementSpeed = 10;

        [SerializeField]
        private float _sightAngle = 60;
        [SerializeField]
        private float _sightRange = 7.5f;

        private Animator _animator;
        private SelectableUnit _unit;
        private BoxCollider _boxCollider;

        private int nextCellIndex = 1;
        private List<WorldCell> _currentPath;
        private WorldSubCell _lastSubCellOccupied;

        private Unit _attackTarget;
        private List<Unit> _hostileUnits = new List<Unit>();

        public void SetDestination(Vector3 newDestination)
        {
            _currentPath = _pathProvider.GetPath(transform.position, newDestination);
            nextCellIndex = 1;
        }

        private void Awake()
        {
            _unit = GetComponent<SelectableUnit>();
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherUnit = other.GetComponent<Unit>();
            if (otherUnit == null)
            {
                return;
            }

            if (_unit.Team != otherUnit.Team)
            {
                otherUnit.DieEvent += OnObservedUnitDie;

                if (other.isTrigger)
                {
                    _hostileUnits.Add(otherUnit);
                    _animator.SetBool("NearCombat", true);
                }
                else
                {
                    _attackTarget = otherUnit;
                    _animator.SetBool("Attacking", true);
                }
            }
        }

        public void OnObservedUnitDie(Unit unit)
        {
            _hostileUnits.Remove(unit);

            if (_attackTarget == unit)
            {
                _attackTarget = _hostileUnits.Count > 0 ? _hostileUnits[0] ?? null : null;
            }

            if (_attackTarget == null)
            {
                _animator.SetBool("Attacking", false);
                _animator.SetBool("NearCombat", _hostileUnits.Count > 0);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var otherUnit = other.GetComponent<Unit>();
            if (otherUnit == null)
            {
                return;
            }

            if (_unit.Team != otherUnit.Team)
            {
                otherUnit.DieEvent -= OnObservedUnitDie;

                if (other.isTrigger)
                {
                    _hostileUnits.Remove(otherUnit);
                    _animator.SetBool("NearCombat", _hostileUnits.Count > 0);
                }
                else
                {
                    _attackTarget = _hostileUnits.Count > 0 ? _hostileUnits[0] ?? null : null;
                    _animator.SetBool("Attacking", false);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var u in _hostileUnits)
            {
                u.DieEvent -= OnObservedUnitDie;
            }
        }

        public void ShootTarget()
        {
            if (_attackTarget != null)
            {
#if DEBUG
                Debug.Log($"Actor: {this.name} shooting to unit: {_attackTarget.name}");
#endif
                _attackTarget.Health -= _unit.Damage;
            }
        }

        private void Update()
        {
            if (_currentPath != null && nextCellIndex < _currentPath.Count)
            {
                _animator.SetFloat("Speed", 50);
                TickPathfollower();
            }
            else
            {
                _animator.SetFloat("Speed", 0);
            }
        }

        private void TickPathfollower()
        {
            var targetCell = _currentPath[nextCellIndex];

            var targetPos = Vector3.zero;
            var subcellIndex = -1;

            if (targetCell.Occupied)
            {
                for (int i = 0; i < targetCell.Subcells.Count; ++i)
                {
                    if (!targetCell.Subcells[i].Occupied)
                    {
                        subcellIndex = i;
                        targetPos = targetCell.Subcells[i].Position;
                    }
                }

                if (subcellIndex == -1)
                {
                    return;
                }
            }
            else
            {
                targetPos = targetCell.Position;
            }

            var direction = targetPos - transform.position;
            var targetRot = Quaternion.LookRotation(direction);
            var newRot = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * _rotationSpeed);

            transform.rotation = newRot;

            var newPos = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * _movementSpeed);
            transform.position = newPos;

            if (Vector3.Distance(newPos, targetPos) < 0.2f)
            {
                var prev = nextCellIndex - 1;
                if (prev >= 0)
                {
                    if (_lastSubCellOccupied != null)
                    {
                        _lastSubCellOccupied.Occupied = false;
                    }
                    else
                    {
                        _currentPath[prev].Occupied = false;
                    }
                }

                if (subcellIndex == -1)
                {
                    targetCell.Occupied = true;
                    _lastSubCellOccupied = null;
                }
                else
                {
                    _lastSubCellOccupied = targetCell.Subcells[subcellIndex];
                    _lastSubCellOccupied.Occupied = true;
                }

                nextCellIndex++;
            }
        }

        private void OnDrawGizmos()
        {
            var center = this.transform.position;
            var halfSightAngle = _sightAngle / 2f;

            var rotateRight = Quaternion.Euler(0, halfSightAngle, 0);
            var rotateLeft = Quaternion.Euler(0, -halfSightAngle, 0);
            var rightDirection = rotateRight * transform.forward;
            rightDirection.Normalize();
            var leftDirection = rotateLeft * transform.forward;
            leftDirection.Normalize();

            Gizmos.color = Color.grey;
            Gizmos.DrawLine(center, center + rightDirection * _sightRange);
            Gizmos.DrawLine(center, center + leftDirection * _sightRange);

            var cross = Vector3.Cross(leftDirection, rightDirection);

            Handles.color = Color.red;
            Handles.DrawWireArc(center, cross, leftDirection * _sightRange, _sightAngle, _sightRange);
        }
    }
}