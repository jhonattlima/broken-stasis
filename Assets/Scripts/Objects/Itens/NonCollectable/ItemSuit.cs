using Audio;
using GameManagers;
using Player;

namespace Interaction
{
    public class ItemSuit : InteractionObjectWithColliders
    {
        public override void Interact()
        {
            GameHudManager.instance.itemCollectedHud.CallNotification("Collected Suit");

            PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM_ON_GROUND);
            AudioManager.instance.Play(AudioNameEnum.ITEM_PICKUP);

            GameStateManager.SetGameState(GameState.CUTSCENE);
        }
    }
}
