using CoreEvent.Chapters;
using JetBrains.Annotations;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;
using Utilities.VariableManagement;

namespace UI.MainMenu
{
    public class SlotsScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _slotsTexts;

        private void Start()
        {
            SetSlotsTexts();
        }

        [UsedImplicitly]
        public void LoadSlot1()
        {
            LoadSlot(1);
        }

        [UsedImplicitly]
        public void LoadSlot2()
        {
            LoadSlot(2);
        }

        [UsedImplicitly]
        public void LoadSlot3()
        {
            LoadSlot(3);
        }

        private void LoadSlot(int p_slot)
        {
            foreach(Button __button in gameObject.GetComponentsInChildren<Button>())
                __button.interactable = false;

            if(SaveGameManager.instance.HasSaveSlot(p_slot))
                SaveGameManager.instance.LoadSlot(p_slot);
            else
                SaveGameManager.instance.NewSlot(p_slot);
            
            LoadingView.instance.FadeIn(delegate ()
            {
                SceneManager.LoadScene(ScenesConstants.GAME);
                // LoadingView.instance.FadeOut(null, VariablesManager.uiVariables.defaultFadeOutSpeed);
            }, VariablesManager.uiVariables.defaultFadeInSpeed * 2f);
        }

        private void SetSlotsTexts()
        {
            for(int i = 0; i < 3; i++)
            {
                if(SaveGameManager.instance.HasSaveSlot(i+1))
                {
                    SaveGameManager.instance.LoadSlot(i+1);

                    _slotsTexts[i].text = "CONTINUE - " + GetMissionText(SaveGameManager.instance.currentGameSaveData.chapter);
                }
            }
        }

        private string GetMissionText(ChapterTypeEnum p_chapter)
        {
            if(p_chapter == ChapterTypeEnum.CHAPTER_1)
                return "[1] THE AWAKENING";
            else if(p_chapter == ChapterTypeEnum.CHAPTER_2)
                return "[2] SEARCHING FOR ANSWERS";
            else if(p_chapter == ChapterTypeEnum.CHAPTER_3)
                return "[3] FIRST CONTACT";
            else
                return "UNKNOWN CHAPTER";
        }
    }
}
