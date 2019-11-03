using UnityEngine;

namespace Bloodstone.AI.Examples.Boids
{
    public class BoundsTeleporter : MonoBehaviour
    {
        [SerializeField]
        private TeleporterTrigger _topTrigger;
        [SerializeField]
        private TeleporterTrigger _leftTrigger;
        [SerializeField]
        private TeleporterTrigger _rightTrigger;
        [SerializeField]
        private TeleporterTrigger _bottomTrigger;

        private void OnEnable()
        {
            AttachTriggers();
        }

        private void OnDisable()
        {
            DetachTriggers();
        }

        private void AttachTriggers()
        {
            _topTrigger.TriggerOccuredEvent += TopTriggerOccuredEvent;
            _leftTrigger.TriggerOccuredEvent += LeftTriggerOccuredEvent;
            _rightTrigger.TriggerOccuredEvent += RightTriggerOccuredEvent;
            _bottomTrigger.TriggerOccuredEvent += BottomTriggerOccuredEvent;
        }

        private void DetachTriggers()
        {
            _topTrigger.TriggerOccuredEvent -= TopTriggerOccuredEvent;
            _leftTrigger.TriggerOccuredEvent -= LeftTriggerOccuredEvent;
            _rightTrigger.TriggerOccuredEvent -= RightTriggerOccuredEvent;
            _bottomTrigger.TriggerOccuredEvent -= BottomTriggerOccuredEvent;
        }

        private void Teleport(Transform target, Vector3 position)
        {
            target.gameObject.SetActive(false);

            target.position = position;

            target.gameObject.SetActive(true);
        }

        private void BottomTriggerOccuredEvent(Transform target)
        {
            var newPosition = target.position;
            newPosition.y = _topTrigger.transform.position.y - _topTrigger.transform.localScale.y;

            Teleport(target, newPosition);
        }

        private void RightTriggerOccuredEvent(Transform target)
        {
            var newPosition = target.position;
            newPosition.x = _leftTrigger.transform.position.x + _leftTrigger.transform.localScale.x;

            Teleport(target, newPosition);
        }

        private void LeftTriggerOccuredEvent(Transform target)
        {
            var newPosition = target.position;
            newPosition.x = _rightTrigger.transform.position.x - _rightTrigger.transform.localScale.x;

            Teleport(target, newPosition);
        }

        private void TopTriggerOccuredEvent(Transform target)
        {
            var newPosition = target.position;
            newPosition.y = _bottomTrigger.transform.position.y + _bottomTrigger.transform.localScale.y;

            Teleport(target, newPosition);
        }
    }
}