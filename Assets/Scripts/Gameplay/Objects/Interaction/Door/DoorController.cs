using System;
using System.Collections;
using GameManagers;
using UnityEngine;
using Utilities.Audio;

namespace Gameplay.Objects.Interaction
{
    public class DoorController : MonoBehaviour, IInteractionObject
    {
        public bool isLocked { get; private set; }
        public bool isDoorOpen;
        public Action onDoorLocked;

        [SerializeField] private float _doorSpeed = 0.00001f;
        [SerializeField] private float _maxDelayToUseDoor = 2;
        [SerializeField] private MeshRenderer[] _lockIndicatorsMeshRenderer;
        [SerializeField] private Material _lockedDoorMaterial;
        [SerializeField] private Material _unlockedDoorMaterial;

        private Vector3 _targetPosition;
        private Vector3 _doorOpenPosition;
        private Vector3 _doorClosePosition;
        private float _journeyLength;
        private float _startTime;
        private BoxCollider _doorCollider;
        private int _totalLocks;
        private int _currentLockIndex;

        private void Awake()
        {
            _doorCollider = GetComponent<BoxCollider>();

            _doorClosePosition = transform.localPosition;
            _doorOpenPosition = new Vector3(_doorClosePosition.x, _doorClosePosition.y, _doorClosePosition.z + _doorCollider.size.z);
            SetDoorState();

            _totalLocks = _lockIndicatorsMeshRenderer.Length;
            _currentLockIndex = 0;
        }

        public void SetDoorState()
        {
            if (isDoorOpen)
                transform.localPosition = _doorOpenPosition;

            _targetPosition = transform.localPosition;
            _startTime = Time.time;
        }

        public void RunUpdate() { }

        public void RunFixedUpdate()
        {
            float __distCovered = (Time.time - _startTime) * _doorSpeed;
            float __fractionOfJourney = __distCovered / _journeyLength;
            if (!float.IsNaN(__fractionOfJourney))
                transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, __fractionOfJourney);
        }

        public void Interact()
        {
            if (isLocked)
            {
                onDoorLocked?.Invoke();
                return;
            }
            isDoorOpen = !isDoorOpen;
            float __delay = UnityEngine.Random.Range(0f, _maxDelayToUseDoor);
            StopAllCoroutines();

            if (isDoorOpen)
                StartCoroutine(OpenDoor(__delay));
            else
                StartCoroutine(CloseDoor(__delay));
        }

        private IEnumerator OpenDoor(float p_delay)
        {
            AudioManager.instance.Play(AudioNameEnum.DOOR_OPEN, false);

            yield return new WaitForSeconds(p_delay);
            _targetPosition = _doorOpenPosition;
            _journeyLength = Vector3.Distance(transform.localPosition, _doorOpenPosition);
            _startTime = Time.time;
        }

        private IEnumerator CloseDoor(float p_delay)
        {
            AudioManager.instance.Play(AudioNameEnum.DOOR_CLOSE, false);

            yield return new WaitForSeconds(p_delay);
            _targetPosition = _doorClosePosition;
            _journeyLength = Vector3.Distance(transform.localPosition, _doorClosePosition);
            _startTime = Time.time;
        }

        public void LockDoor()
        {
            isLocked = true;
            _currentLockIndex = 0;

            for(int i = 0; i < _totalLocks; i++)
            {
                _lockIndicatorsMeshRenderer[i].material = _lockedDoorMaterial;
            }
        }

        public void UnlockDoorLock()
        {
            _lockIndicatorsMeshRenderer[_currentLockIndex].material = _unlockedDoorMaterial;

            if(_currentLockIndex < _totalLocks - 1)
                _currentLockIndex++;
            else
                isLocked = false;
        }
    }
}