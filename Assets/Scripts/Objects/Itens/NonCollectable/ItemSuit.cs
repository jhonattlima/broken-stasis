using Player;
using UnityEngine;

namespace Interaction
{
    public class ItemSuit : InteractionObjectWithColliders
    {
        [SerializeField] private GameObject _suitModel;

        private bool _activated;
        private SuitChangeController _suitChangeController;

        private void Awake()
        {
            _suitChangeController = new SuitChangeController();

            _activated = false;
        }


        public override void Interact()
        {
            Debug.Log("interacted with suit");
            if(!_activated)
            {
                PlayerStatesManager.SetPlayerState(PlayerState.PICK_ITEM_ON_GROUND);
                
                _suitChangeController.ChangeSuit(PlayerSuitEnum.SUIT1, delegate()
                {
                    _suitModel.SetActive(false);
                    _activated = true;
                });
            }
        }
    }
}
