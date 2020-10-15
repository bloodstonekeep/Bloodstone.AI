using UnityEngine;

namespace Bloodstone
{
    public class PlayerStateContext : StateContext
    {
        private const float MinimumHealth = 0f;
        private const float MaximumHealth = 1f;

        [SerializeField]
        [Range(MinimumHealth, MaximumHealth)]
        private float _heath = MaximumHealth;

        public float Health
        {
            get => _heath;
            set
            {
                _heath = Mathf.Clamp(_heath, MinimumHealth, MaximumHealth);
            }
        }
    }
}