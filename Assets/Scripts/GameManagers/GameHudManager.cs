using UI.Dialog;
using UI.Notification;
using UnityEngine;

namespace GameManagers
{
    public class GameHudManager : MonoBehaviour
    {
        public NotificationUI notificationHud;
        public UIDialog uiDialogHud;

        public static GameHudManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Update()
        {
            uiDialogHud.RunUpdate();
        }
    }
}