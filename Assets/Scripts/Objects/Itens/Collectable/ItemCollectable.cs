using UnityEngine;
using Audio;
using GameManager;

namespace Interaction
{
    public class ItemCollectable : InteractionObjectWithColliders
    {
        [SerializeField] ItemHeightEnum itemHeight;

        public override void Interact()
        {
            AudioManager.instance.Play(AudioNameEnum.ITEM_PICKUP);
            if (itemHeight.Equals(ItemHeightEnum.GROUND))
            {
                Debug.Log("Picked collectable item on ground.");
            }
            else if (itemHeight.Equals(ItemHeightEnum.HIGH))
            {
                Debug.Log("Picked collectable item on high place.");
            }
        }
    }
}
