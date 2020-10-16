using Bloodstone.AI.Pathfinding;
using UnityEngine;

namespace Bloodstone.AI.Examples
{
    [RequireComponent(typeof(UnitSelector))]
    public class UnitCommander : MonoBehaviour
    {
        private UnitSelector _unitSelector;

        private void Awake()
        {
            _unitSelector = GetComponent<UnitSelector>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                foreach (var u in _unitSelector.LastSelectedUnits)
                {
                    u.GetComponent<Actor>().SetDestination(hit.point);
                }
            }
        }
    }
}