using GameManagers;
using UnityEngine;

namespace Interaction
{
    public class ItemSuit : InteractionObjectWithColliders
    {
        public override void Interact()
        {
            GameHudManager.instance.itemCollectedHud.ShowAutoHideNotification("Collected Suit");
        }
    }
}
