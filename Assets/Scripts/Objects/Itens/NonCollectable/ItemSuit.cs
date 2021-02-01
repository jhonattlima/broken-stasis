using GameManagers;
using Player;
using UnityEngine;

namespace Interaction
{
    public class ItemSuit : InteractionObjectWithColliders
    {
        private bool _activated;

        private void Awake()
        {
            _activated = false;
        }


        public override void Interact()
        {
            if(!_activated)
            {
                PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM_ON_GROUND);
                
                GameEventManager.RunGameEvent(GameEventType.DRESS_PLAYER);

                _activated = true;
            }
        }
    }
}
