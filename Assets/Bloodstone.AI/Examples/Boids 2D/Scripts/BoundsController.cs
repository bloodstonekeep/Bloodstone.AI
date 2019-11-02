using System;
using UnityEngine;

namespace Bloodstone.AI.Examples.Boids
{
    public class BoundsController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _physicalBlocksBounds;
        [SerializeField]
        private GameObject _teleporterBounds;

        [SerializeField]
        private BoundsMode _mode;

        public BoundsMode Mode
        {
            get => _mode;
            set
            {
                if(_mode == value)
                {
                    return;
                }

                _mode = value;
                ActivateMode();
            }
        }

        // todo: extension to enum
        public BoundsMode ParseMode(bool value)
        {
            return value ? BoundsMode.Teleporter : BoundsMode.PhysicalBlocks;
        }

        private void Awake()
        {
            ActivateMode();
        }

        private void ActivateMode()
        {
            switch(_mode)
            {
                case BoundsMode.PhysicalBlocks:
                    {
                        _physicalBlocksBounds.SetActive(true);
                        _teleporterBounds.SetActive(false);
                    }
                    break;
                case BoundsMode.Teleporter:
                    {
                        _physicalBlocksBounds.SetActive(false);
                        _teleporterBounds.SetActive(true);
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public enum BoundsMode
        {
            PhysicalBlocks = 0,
            Teleporter = 1,
        }
    }
}