using GameManagers;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Utilities;
using Utilities.Audio;
using Utilities.UI;
using Utilities.VariableManagement;
namespace CoreEvent.GameEvents
{
    public class GameEvent_CreditsCutScene : MonoBehaviour, IGameEvent
    {
        [SerializeField] private GameEventTypeEnum _gameEventType;
        [SerializeField] private GameObject _mainCamera;

        private bool _hasRun;

        public GameEventTypeEnum gameEventType { get { return _gameEventType; } }
        public bool hasRun { get { return _hasRun; } }

        private void Awake()
        {
            _hasRun = false;
        }

        public void RunPermanentEvents() { }

        public void RunSingleTimeEvents()
        {
            _hasRun = true;

            GameHudManager.instance.uiDialogHud.StartDialog(DialogEnum.ACT_03_CONTROL_PANEL_GREETINGS, delegate ()
            {              
                AudioManager.instance.Stop(AudioNameEnum.PLAYER_HEARTBEAT);
                GameHudManager.instance.damageUI.ResetHud();
                
                VideoController.instance.PlayVideo(VariablesManager.videoVariables.endCredits, VariablesManager.videoVariables.endCreditsVolume, delegate()
                {
                    LoadingView.instance.InstantBlackScreen();
                    SceneManager.LoadScene(ScenesConstants.MENU);
                }, delegate()
                {
                    InputController.GamePlay.InputEnabled = false;
                    LoadingView.instance.ClearedScreen();
                });                  
            }
            , false);
        }
    }
}
