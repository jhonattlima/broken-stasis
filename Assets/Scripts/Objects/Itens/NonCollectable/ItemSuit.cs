using Audio;
using GameManagers;
using Player;

namespace Interaction
{
    public class ItemSuit : InteractionObjectWithColliders
    {
        private SuitChangeController _suitChangeController = new SuitChangeController();

        public override void Interact()
        {

            PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM_ON_GROUND);
            
            // 
            _suitChangeController.ChangeSuit(PlayerSuitEnum.SUIT1);
        }
    }
}
