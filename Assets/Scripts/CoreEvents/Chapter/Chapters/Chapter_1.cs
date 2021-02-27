using System.Collections.Generic;
using System.Linq;
using CoreEvent.GameEvents;
using GameManagers;
using Objects.Interaction;
using UI;
using UnityEngine;
using Utilities;
using Utilities.UI;

namespace CoreEvent.Chapters
{
    public class Chapter_1 : MonoBehaviour, IChapter
    {
        [SerializeField] private ChapterTypeEnum _chapterType;
        [SerializeField] private DoorController _doorController;

        public ChapterTypeEnum chapterType
        {
            get
            {
                return _chapterType;
            }
        }

        public List<IGameEvent> gameEvents
        {
            get
            {
                return gameObject.GetComponentsInChildren<IGameEvent>().ToList();
            }
        }

        public void ChapterStart()
        {
            GameHudManager.instance.uiDialogHud.InitializeDialog(DialogEnum.ACT_01_WAKE_UP_MESSAGE);
            
            LoadingView.instance.FadeOut(delegate ()
            {
                InputController.UI.InputEnabled = true;

                GameHudManager.instance.uiDialogHud.Show();
            });

            _doorController.isLocked = true;
            _doorController.onDoorLocked = delegate ()
            {
                GameHudManager.instance.uiDialogHud.InitializeDialog(DialogEnum.ACT_01_LOCKED_DOOR);
                GameHudManager.instance.uiDialogHud.Show();
            };
        }

        public void ChapterEnd()
        {
            Debug.Log("FINISHED CHAPTER 1");
        }
    }
}
