using UnityEngine;

namespace Interaction
{
    enum ItemHeight
    {
        GROUND,
        HIGH
    }

    public class ItemCollectable : InteractionObjectWithColliders
    {
        [SerializeField] ItemHeight itemHeight;

        public override void Interact()
        {
            if (itemHeight.Equals(ItemHeight.GROUND))
            {
                Debug.Log("Picked collectable item on ground.");
            }
            else if (itemHeight.Equals(ItemHeight.HIGH))
            {
                Debug.Log("Picked collectable item on high place.");
            }
        }
    }
}
