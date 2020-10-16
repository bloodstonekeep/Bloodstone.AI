using UnityEngine;

namespace Bloodstone.AI.Examples
{
    public class CameraMovement : MonoBehaviour
    {
        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            this.transform.position += new Vector3(horizontal, 0, vertical);
        }
    }
}