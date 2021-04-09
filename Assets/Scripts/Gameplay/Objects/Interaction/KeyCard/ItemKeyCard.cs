
using GameManagers;
using Gameplay.Player.Item;
using Gameplay.Player.Motion;

namespace Gameplay.Objects.Interaction
{
    public class ItemKeyCard : InteractionObjectWithColliders
    {       
        public override void Interact()
        {
            PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM_ON_GROUND);
            GameplayManager.instance.onPlayerCollectedItem(ItemEnum.KEYCARD);
            
            this.gameObject.SetActive(false);
        }
    }
}