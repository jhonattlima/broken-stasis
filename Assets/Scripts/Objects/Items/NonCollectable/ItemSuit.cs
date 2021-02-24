using CoreEvent.GameEvents;
using GameManagers;
using Objects.Interaction;
using Player.Motion;

namespace Objects.Items
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
            if (!_activated)
            {
                PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM_ON_GROUND);

                GameEventManager.RunGameEvent(GameEventTypeEnum.DRESS_PLAYER);

                _activated = true;
            }
        }
    }
}
