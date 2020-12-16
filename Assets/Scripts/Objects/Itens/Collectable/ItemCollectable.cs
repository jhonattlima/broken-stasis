using UnityEngine;
using Audio;

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
            AudioManager.instance.Play(AudioNameEnum.ITEM_PICKUP);
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
