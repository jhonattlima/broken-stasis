using UnityEngine;
using VariableManagement;


namespace Player
{
    public class PlayerMovement : IFixedUpdateBehaviour
    {
        #region PRIVATE READONLY FIELDS
        private readonly CharacterController _charController;
        private readonly Transform _playerTransform;
        #endregion PRIVATE READONLY FIELDS

        #region PRIVATE FIELDS
        private Vector3 _previousPosition;
        #endregion PRIVATE FIELDS

        private bool _movingSideways;
        private bool _movingBackward;
        private float _currentSpeed;

        public PlayerMovement(CharacterController p_charController,
                                Transform p_playerTransform)
        {
            _charController = p_charController;
            _playerTransform = p_playerTransform;

            _movingSideways = false;
            _movingBackward = false;
            PlayerStatesManager.onStateChanged += HandleStateChanged;
        }

        public void RunFixedUpdate()
        {
            if (GameStateManager.currentState != GameState.RUNNING) return;
            
            CheckWalkingBackward();
            SetMovingState();

            HandleMovement();
            HandleDirection();
        }

        private void HandleStateChanged(PlayerState p_playerState)
        {
            switch (p_playerState)
            {
                case PlayerState.RUNNING_FORWARD:
                case PlayerState.RUNNING_SIDEWAYS:
                    _currentSpeed = VariablesManager.playerVariables.regularSpeed * VariablesManager.playerVariables.runningSpeedMultiplier;
                    break;
                case PlayerState.WALKING_BACKWARD:
                    _currentSpeed = VariablesManager.playerVariables.regularSpeed * VariablesManager.playerVariables.backwardSpeedMultiplier;
                    break;
                default:
                    _currentSpeed = VariablesManager.playerVariables.regularSpeed;
                    break;
            }
        }


        private void HandleMovement()
        {
            Vector3 __moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            _charController.SimpleMove(__moveDirection.normalized * _currentSpeed * Time.deltaTime);
        }

        private void HandleDirection()
        {
            Vector2 __positionOnScreen = Camera.main.WorldToViewportPoint(_playerTransform.position);
            Vector2 __mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float __angle = Mathf.Atan2(__positionOnScreen.y - __mouseOnScreen.y, __positionOnScreen.x - __mouseOnScreen.x) * Mathf.Rad2Deg;

            _playerTransform.rotation = Quaternion.Euler(new Vector3(0f, -__angle, 0f));
        }

        #region  CHECKERS
        private void SetMovingState()
        {
            if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (_movingSideways) PlayerStatesManager.SetPlayerState(PlayerState.RUNNING_SIDEWAYS);
                    else if (_movingBackward) PlayerStatesManager.SetPlayerState(PlayerState.WALKING_BACKWARD);
                    else PlayerStatesManager.SetPlayerState(PlayerState.RUNNING_FORWARD);
                }
                else
                {
                    if (_movingSideways) PlayerStatesManager.SetPlayerState(PlayerState.WALKING_SIDEWAYS);
                    else if (_movingBackward) PlayerStatesManager.SetPlayerState(PlayerState.WALKING_BACKWARD);
                    else PlayerStatesManager.SetPlayerState(PlayerState.WALKING_FORWARD);
                }
            }
            else
                PlayerStatesManager.SetPlayerState(PlayerState.STATIC);
        }

        private void CheckWalkingBackward()
        {
            Vector3 direction = (_playerTransform.position - _previousPosition).normalized;
            direction = Quaternion.AngleAxis(-90, Vector3.down) * direction;
            float dotProduct = Vector3.Dot(direction, _playerTransform.forward.normalized);

            if (dotProduct > 0.5)
            {
                // Debug.Log("forward");
                _movingBackward = false;
                _movingSideways = false;
            }
            else if (dotProduct < 0)
            {
                // Debug.Log("Backward");
                _movingSideways = false;
                _movingBackward = true;
            }
            else if (dotProduct != 0)
            {
                // Debug.Log("Side");
                _movingSideways = true;
                _movingBackward = false;
            }

            _previousPosition = _playerTransform.position;
        }
        #endregion CHECKERS
    }
}