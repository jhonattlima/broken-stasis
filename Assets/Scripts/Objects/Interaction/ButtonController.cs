using UnityEngine;

namespace Interaction
{
    public class ButtonController : MonoBehaviour, IInteractionObject
    {
        [SerializeField] private GameObject _interactionObject;
        private bool _isButtonActive = false;

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
            if (_isButtonActive)
                if (Input.GetButtonDown("Action"))
                    _interactionObject.GetComponent<IInteractionObject>().Interact();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                _isButtonActive = true;
                // Debug.Log("Player is close to the door");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                _isButtonActive = false;
                // Debug.Log("Player is out of door range");
            }
        }

        void OnGUI()
        {
            if (_isButtonActive)
            {
                var position = Camera.main.WorldToScreenPoint(transform.localPosition);
                GUI.skin.box.wordWrap = true;
                GUI.Box(new Rect(position.x, position.y, 100, 25), "Interact Controller");
            }
        }

    }
}
