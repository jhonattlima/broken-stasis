using System.Collections;
using UnityEngine;

namespace Interaction
{
    public class DoorController : MonoBehaviour, IInteractionObject
    {
        [SerializeField] private float _doorSpeed = 0.00001f;
        [SerializeField] private float _maxDelayToUseDoor = 2;
        [SerializeField] private bool _isDoorOpen = false;

        private Vector3 _targetPosition;
        private Vector3 _doorOpenPosition;
        private Vector3 _doorClosePosition;
        private float _journeyLength;
        private float _startTime;

        // If doorModel scale does not match with texture, change _doorOpenPosition attribuition
        private void Awake()
        {
            _doorClosePosition = transform.position;
            _doorOpenPosition = new Vector3(_doorClosePosition.x, _doorClosePosition.y, _doorClosePosition.z + transform.localScale.z * 3);

            if (_isDoorOpen)
                transform.position = _doorOpenPosition;

            _targetPosition = transform.position;
            _startTime = Time.time;
        }

        public void RunUpdate() { }

        public void RunFixedUpdate()
        {
            float __distCovered = (Time.time - _startTime) * _doorSpeed;
            float __fractionOfJourney = __distCovered / _journeyLength;
            if (!float.IsNaN(__fractionOfJourney))
                transform.position = Vector3.Lerp(transform.position, _targetPosition, __fractionOfJourney);
        }

        public void Interact()
        {
            _isDoorOpen = !_isDoorOpen;
            float __delay = Random.Range(0f, _maxDelayToUseDoor);
            StopAllCoroutines();
            if (_isDoorOpen)
                StartCoroutine(OpenDoor(__delay));
            else
                StartCoroutine(CloseDoor(__delay));
        }

        private IEnumerator OpenDoor(float p_delay)
        {
            // Debug.Log("Door is opening after " + delay + "seconds...");
            yield return new WaitForSeconds(p_delay);
            _targetPosition = _doorOpenPosition;
            _journeyLength = Vector3.Distance(transform.position, _doorOpenPosition);
            _startTime = Time.time;
        }

        private IEnumerator CloseDoor(float p_delay)
        {
            // Debug.Log("Door is closing after " + delay + "seconds...");
            yield return new WaitForSeconds(p_delay);
            _targetPosition = _doorClosePosition;
            _journeyLength = Vector3.Distance(transform.position, _doorClosePosition);
            _startTime = Time.time;
        }
    }
}