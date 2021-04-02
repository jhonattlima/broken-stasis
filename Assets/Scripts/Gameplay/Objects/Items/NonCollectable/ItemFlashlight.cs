using GameManagers;
using Gameplay.Objects.Interaction;
using Gameplay.Player.Motion;
using Utilities.UI;

namespace Gameplay.Objects.Items
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
            if (!_collected)
            {
                if(_enabled)
                {
                    PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM);
                    GameplayManager.instance.onActivatePlayerIllumination(true);
                    _collected = true;
                }
                else
                {
                    GameHudManager.instance.uiDialogHud.InitializeDialog(DialogEnum.ACT_02_FLASHLIGHT_UNAVAILABLE);
                    GameHudManager.instance.uiDialogHud.Show();
                }
            }
        }
    }
}
