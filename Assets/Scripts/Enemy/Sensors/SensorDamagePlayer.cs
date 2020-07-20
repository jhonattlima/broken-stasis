using UnityEngine;

namespace Enemy
{
    public class SensorDamagePlayer : MonoBehaviour
    {
        public bool isTouchingPlayer { private set; get; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameInternalTags.PLAYER))
                isTouchingPlayer = true;
        }

        public void ResetSensorDetection()
        {
            isTouchingPlayer = false;
        }
    }
}
