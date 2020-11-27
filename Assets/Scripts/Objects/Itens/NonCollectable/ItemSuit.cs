using UnityEngine;

namespace Interaction
{
    public class ItemSuit : InteractionObjectWithColliders
    {
        public override void Interact()
        {
            Debug.Log("Picked suit.");
        }
    }
}
