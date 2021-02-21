using UnityEngine;

namespace Utilities
{
    public static class InputController
    {
        public static class GamePlay
        {
            public static bool InputEnabled { get; set; }

            private static Vector3 _defaultMousePosition = Vector3.zero;

            public static bool Run()
            {
                if (InputEnabled)
                    return Input.GetButton("Run");
                else
                    return false;
            }

            public static bool Crouch()
            {
                if (InputEnabled)
                    return Input.GetButton("Crouch");
                else
                    return false;
            }

            public static bool Interact()
            {
                if (InputEnabled)
                    return Input.GetButtonDown("Interact");
                else
                    return false;
            }

            public static Vector3 MousePosition()
            {
                if (InputEnabled)
                {
                    _defaultMousePosition = Input.mousePosition;

                    return Input.mousePosition;
                }
                else
                    return _defaultMousePosition;
            }

            public static Vector3 NavigationAxis()
            {
                if (InputEnabled)
                    return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
                else
                    return Vector3.zero;
            }
        }
    }
}

