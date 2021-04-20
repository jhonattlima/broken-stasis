using GameManagers;
using Gameplay.Objects.Interaction;
using Gameplay.Objects.Items;
using UnityEngine;
using Utilities.UI;

namespace CoreEvent.GameEvents
{
    public class GameEvent_GeneratorMinigameComplete : MonoBehaviour, IGameEvent
    {
        [SerializeField] private GameEventTypeEnum _gameEventType;
        [SerializeField] private GeneratorController _generatorController;
        [SerializeField] private ItemFlashlight _itemFlashlight;
        [SerializeField] private GameObject _eventLightBlink;

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
            _eventLightBlink.SetActive(true);
        }

        public void RunSingleTimeEvents()
        {
            RunPermanentEvents();
            GameHudManager.instance.uiDialogHud.StartDialog(DialogEnum.ACT_02_MINIGAME_COMPLETE);
            _hasRun = true;
        }
    }
}
