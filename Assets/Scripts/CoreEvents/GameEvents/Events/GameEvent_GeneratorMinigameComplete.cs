using GameManagers;
using Gameplay.Objects.Interaction;
using Gameplay.Objects.Items;
using UnityEngine;

namespace CoreEvent.GameEvents
{
    public class GameEvent_GeneratorMinigameComplete : MonoBehaviour, IGameEvent
    {
        [SerializeField] private GameEventTypeEnum _gameEventType;
        [SerializeField] private GeneratorController _generatorController;
        [SerializeField] private ItemFlashlight _itemFlashlight;

        public GameEventTypeEnum gameEventType
        {
            get
            {
                return _gameEventType;
            }
        }

        private bool _hasRun;
        public bool hasRun
        {
            get
            {
                return _hasRun;
            }
        }

        private void Awake()
        {
            _hasRun = false;    
        }

        public void RunPermanentEvents()
        {
            _generatorController.SetEnabled(false);
            _itemFlashlight.SetEnabled(true);
        }

        public void RunSingleTimeEvents()
        {
            RunPermanentEvents();
            GameHudManager.instance.notificationHud.ShowText("Lantern batteries now available in Dressing Room");
            _hasRun = true;
        }
    }
}
