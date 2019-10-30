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

        private void BottomTriggerOccuredEvent(Transform targetTransform)
        {
            var newPosition = targetTransform.position;
            newPosition.y = _topTrigger.transform.position.y - _topTrigger.transform.localScale.y;
            targetTransform.position = newPosition;
        }

        private void RightTriggerOccuredEvent(Transform targetTransform)
        {
            var newPosition = targetTransform.position;
            newPosition.x = _leftTrigger.transform.position.x + _leftTrigger.transform.localScale.x;
            targetTransform.position = newPosition;
        }

        private void LeftTriggerOccuredEvent(Transform targetTransform)
        {
            var newPosition = targetTransform.position;
            newPosition.x = _rightTrigger.transform.position.x - _rightTrigger.transform.localScale.x;
            targetTransform.position = newPosition;
        }

        private void TopTriggerOccuredEvent(Transform targetTransform)
        {
            var newPosition = targetTransform.position;
            newPosition.y = _bottomTrigger.transform.position.y + _bottomTrigger.transform.localScale.y;
            targetTransform.position = newPosition;
        }
    }
}