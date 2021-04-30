using System;
using UnityEngine;

namespace Gameplay.Enemy.Sensors
{
    public class SensorRoom : MonoBehaviour
    {
        public Action<GameObject> onRoomDetected;
        // public Action<Transform> onRoomRemainsDetected;
        // public Action<GameObject> onRoomLeftDetection;

        private void OnTriggerEnter(Collider other)
        {
            // TODO: DETECT COVER ROOM OBJECT
            // if (other.CompareTag(GameInternalTags.PLAYER_SOUND_COLLIDER))
            //     if (onRoomDetected != null) onRoomDetected(other.transform);
        }

        // private void OnTriggerStay(Collider other)
        // {
        //     if (other.CompareTag(GameInternalTags.PLAYER_SOUND_COLLIDER))
        //         if (onRoomRemainsDetected != null) onRoomRemainsDetected(other.transform);
        // }

        // private void OnTriggerExit(Collider other)
        // {
        //     if (other.CompareTag(GameInternalTags.PLAYER_SOUND_COLLIDER))
        //         if (onRoomLeftDetection != null) onRoomLeftDetection(other.transform);
        // }
    }
}
