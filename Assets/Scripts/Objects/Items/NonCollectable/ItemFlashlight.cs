using Objects.Interaction;
using Player.Motion;

namespace Objects.Items
{
    public class ItemFlashlight : InteractionObjectWithColliders
    {
        private bool _collected;
        private bool _enabled;

        private void Awake()
        {
            _collected = false;
            _enabled = false;
        }

        public void SetEnabled(bool p_enabled)
        {
            _enabled = p_enabled;
        }

        public override void Interact()
        {
            if (!_collected && _enabled)
            {
                PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM);

                _collected = true;
            }
        }
    }
}
