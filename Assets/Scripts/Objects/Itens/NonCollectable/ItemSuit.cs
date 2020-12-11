using GameManagers;
using Player;
using UnityEngine;

namespace Interaction
{
    public class ItemSuit : InteractionObjectWithColliders
    {
        public override void Interact()
        {
            GameHudManager.instance.itemCollectedHud.CallNotification("Collected Suit");

            PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM_ON_GROUND);

            GameStateManager.SetGameState(GameState.CUTSCENE);
        }
    }
}
