using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Interaction
{
    public abstract class InteractionObjectWithColliders : MonoBehaviour, IInteractionObject
    {
        protected bool _isActive = false;

        public virtual void Interact() { }

        public virtual void RunFixedUpdate() { }

        public virtual void RunUpdate()
        {
            if (_isActive && InputController.GamePlay.Interact())
            {
                Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("PlayerInteractor"))
                _isActive = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("PlayerInteractor"))
                _isActive = false;
        }
    }
}
