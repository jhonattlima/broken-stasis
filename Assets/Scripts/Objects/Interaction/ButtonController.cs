using Player;
using UnityEngine;
using Utilities;

namespace Interaction
{
    public class ButtonController : MonoBehaviour, IInteractionObject
    {
        [SerializeField] private GameObject _interactionObject;
        public bool _isButtonActive = false;

        private void Awake()
        {
            if (_interactionObject == null)
                throw new MissingComponentException("Interaction object not found!");
        }

        public void RunUpdate()
        {
            Interact();
        }

        public void RunFixedUpdate() { }

        public void Interact()
        {
            if (_isButtonActive && InputController.GamePlay.Interact())
            {
                PlayerStatesManager.SetPlayerState(PlayerState.PRESS_BUTTON);
                _interactionObject.GetComponent<IInteractionObject>().Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
                _isButtonActive = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
                _isButtonActive = false;
        }
    }
}
