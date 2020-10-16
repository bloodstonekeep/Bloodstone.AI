using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bloodstone.AI.Examples
{
    public class UnitSelector : MonoBehaviour
    {
        public List<SelectableUnit> LastSelectedUnits { get; set; } = new List<SelectableUnit>();

        [SerializeField]
        private List<SelectableUnit> _selectableUnits;
        [SerializeField]
        private Image _selectorImage;

        private Vector3 _pressPos;
        private Vector3 _lastPos;
        private Vector3 _selectionSizeDelta;

        private void Awake()
        {
            foreach (var u in _selectableUnits)
            {
                u.DieEvent += OnUnitDieEvent;
            }
        }

        private void OnUnitDieEvent(Unit obj)
        {
            var target = (SelectableUnit)obj;

            target.DieEvent -= OnUnitDieEvent;

            LastSelectedUnits.Remove(target);
            _selectableUnits.Remove(target);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleSelectionStart();
            }
            else if (Input.GetMouseButton(0))
            {
                HandleSelection();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                HandleSelectionEnd();
            }
        }

        private void HandleSelectionStart()
        {
            LastSelectedUnits.Clear();
            _selectorImage.gameObject.SetActive(true);

            _pressPos = Input.mousePosition;
            _selectorImage.transform.position = _pressPos;
            _selectionSizeDelta = new Vector2(0, 0);
            _selectorImage.rectTransform.sizeDelta = _selectionSizeDelta;

            _lastPos = _pressPos;
        }

        private void HandleSelection()
        {
            var currPos = Input.mousePosition;
            var delta = currPos - _lastPos;

            _selectionSizeDelta += delta;
            var newPos = _selectionSizeDelta;
            if (newPos.x < 0)
            {
                newPos.x = -newPos.x;
            }

            if (newPos.y < 0)
            {
                newPos.y = -newPos.y;
            }

            _selectorImage.rectTransform.sizeDelta = newPos;
            _selectorImage.transform.position += delta / 2f;

            _lastPos = currPos;
        }

        private void HandleSelectionEnd()
        {
            _lastPos = Input.mousePosition;
            _selectorImage.gameObject.SetActive(false);

            foreach (var unit in _selectableUnits)
            {
                var screenPoint = Camera.main.WorldToScreenPoint(unit.transform.position);

                if (IsPointInsideSelection(screenPoint, _pressPos, _lastPos))
                {
                    unit.SelectUnit();
                    LastSelectedUnits.Add(unit);
                }
            }
        }

        private bool IsPointInsideSelection(Vector3 point, Vector3 start, Vector3 end)
        {
            if (CheckSelectionBasedOnX(point, start, end))
            {
                return true;
            }

            return CheckSelectionBasedOnX(point, end, start);
        }

        private bool CheckSelectionBasedOnX(Vector3 point, Vector3 start, Vector3 end)
        {
            return start.x <= end.x
                && point.x >= start.x
                && point.x <= end.x
                && IsInsideY(point.y, start.y, end.y);
        }

        private bool IsInsideY(float point, float start, float end)
        {
            return (start <= end && point >= start && point <= end)
                    || (start > end && point <= start && point >= end);
        }
    }
}