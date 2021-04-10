
using GameManagers;
using Gameplay.Player.Item;
using Gameplay.Player.Motion;
using UnityEngine;

namespace Gameplay.Objects.Interaction
{
    public class ItemKeyCard : InteractionObjectWithColliders
    {
        [SerializeField] private GameObject _modelGameObject;

        private bool _enabled;

        private void Awake()
        {
            _enabled = false;
            _modelGameObject.SetActive(_enabled);
        }

        public void SetEnabled(bool p_enabled)
        {
            _enabled = p_enabled;
            _modelGameObject.SetActive(_enabled);
        }

        public override void Interact()
        {
            if(_enabled)
            {
                PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM_ON_GROUND);
                GameplayManager.instance.onPlayerCollectedItem(ItemEnum.KEYCARD);

                SetEnabled(false);
            }
        }
    }
}