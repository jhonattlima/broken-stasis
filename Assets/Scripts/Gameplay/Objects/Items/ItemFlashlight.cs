using CoreEvent.GameEvents;
using GameManagers;
using Gameplay.Objects.Interaction;
using Gameplay.Player.Item;
using Gameplay.Player.Motion;
using UI.ToolTip;
using UnityEngine;
using Utilities;
using Utilities.Audio;
using Utilities.UI;

namespace Gameplay.Objects.Items
{
    public class ItemFlashlight : InteractionObjectWithColliders
    {
        [SerializeField] private ToolTip _lightToolTip;

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

        public void SetCollected(bool p_collected)
        {
            _collected = p_collected;
        }

        public override void Interact()
        {
            if (!_collected)
            {
                if (_enabled)
                {
                    _lightToolTip.InteractToolTip();

                    PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM);
                    AudioManager.instance.Play(AudioNameEnum.ITEM_LANTERN_PICKUP, false, delegate ()
                    {
                        GameHudManager.instance.notificationHud.ShowText("Collected flashlight batteries");
                        GameplayManager.instance.onPlayerCollectedItem(ItemEnum.FLASHLIGHT_BATTERY);
                        GameEventManager.RunGameEvent(GameEventTypeEnum.GENERATOR_EXPLOSION);
                        _collected = true;
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
