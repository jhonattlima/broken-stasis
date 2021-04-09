using CoreEvent.GameEvents;
using GameManagers;
using Gameplay.Objects.Interaction;
using Gameplay.Player.Motion;
using Utilities.Audio;
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
                if (_enabled)
                {
                    PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM);
                    AudioManager.instance.Play(AudioNameEnum.ITEM_LANTERN_PICKUP, false, delegate ()
                    {
                        GameHudManager.instance.notificationHud.ShowText("Collected flashlight batteries (Toogle: press [F]", 5);
                        _collected = true;
                        GameplayManager.instance.onActivatePlayerIllumination(true);
                        GameEventManager.RunGameEvent(GameEventTypeEnum.GENERATOR_EXPLOSION);
                    });
                }
                else
                {
                    AudioManager.instance.Play(AudioNameEnum.ITEM_LANTERN_PICKUP_DENIED);
                    GameHudManager.instance.uiDialogHud.StartDialog(DialogEnum.ACT_02_FLASHLIGHT_UNAVAILABLE);
                }
            }
        }
    }
}
