using UI.Dialog;
using UI.EndGamePuzzle;
using UI.Minigame;
using UI.Notification;
using UI.Options;
using UnityEngine;

namespace GameManagers
{
    public class GameHudManager : MonoBehaviour
    {
        public NotificationUI notificationHud;
        public UIDialog uiDialogHud;
        public MinigameUI minigameHud;
        public EndGameUI endGameUI;
        public DamageUI _damageUI;
        public UIOptions _optionsUI;
        public UIAreYouSure _areyouSureUI;

        public static GameHudManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Update()
        {
            if(GameStateManager.currentState != GameState.RUNNING) return;
            uiDialogHud.RunUpdate();
        }
    }
}