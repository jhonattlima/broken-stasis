using System.Collections.Generic;
using System.Linq;
using CoreEvent.GameEvents;
using GameManagers;
using Objects.Interaction;
using UI;
using UnityEngine;
using Utilities;
using Utilities.VariableManagement;

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
            LoadingView.instance.FadeOut(delegate ()
            {
                InputController.GamePlay.InputEnabled = true;
            });

            _doorController.isLocked = true;
            _doorController.onDoorLocked = delegate ()
            {
                GameHudManager.instance.itemCollectedHud.CallNotification(VariablesManager.textAIVariables.ACT1_DOOR_LOCKED_MESSAGE);
            };
        }

        public void ChapterEnd()
        {
            Debug.Log("FINISHED CHAPTER 1");
        }
    }
}
