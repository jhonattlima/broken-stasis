using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Utilities;
using Utilities.Dialog;
using Utilities.UI;
using Utilities.VariableManagement;

namespace UI.Dialog
{
    public class UIDialog : MonoBehaviour, IUIDialogText, IUpdateBehaviour
    {
        [SerializeField] private Animator _hudAnimator;
        [SerializeField] private TextMeshProUGUI _speakerText;
        [SerializeField] private TextMeshProUGUI _dialogText;
        [SerializeField] private DialogBoxUIAnimationEventHandler _dialogEventHandler;

        private const string SHOW_DIALOG_HUD_ANIMATION = "Show";
        private const string HIDE_DIALOG_HUD_ANIMATION = "Hide";

        private const string ENABLE_SKIP_TEXT_ANIMATION = "Enable";
        private const string DISABLE_SKIP_TEXT_ANIMATION = "Disable";

        private static DialogsPopulator _dialogLibraryPopulator;
        private static AIDialogScriptableObject _dialogsLibraryAsset;

        private Queue<DialogTextUnit> _conversationQueue = new Queue<DialogTextUnit>();
        private string _currentDialogText = "";
        private bool _visible = false;
        private Action _dialogEndCallback;

        public void StartDialog(DialogEnum p_dialogName, Action p_onDialogEnd = null)
        {
            _dialogEndCallback = p_onDialogEnd;

            InitializeDialog(p_dialogName);
            Show();
        }
        
        private void InitializeDialog(DialogEnum p_dialogName)
        {
            _conversationQueue.Clear();

            _speakerText.text = "";
            _dialogText.text = "";

            _dialogLibraryPopulator = new DialogsPopulator();
            _dialogLibraryPopulator.InitializeDialogLibrary();

            _dialogsLibraryAsset = Resources.Load<AIDialogScriptableObject>("AIDialogs");

            foreach(DialogConversationUnit __dialogConversation in _dialogsLibraryAsset.GameDialogs)
            {
                if(p_dialogName.ToString() == __dialogConversation.dialogName)
                {
                    foreach (DialogTextUnit __conversationUnit in __dialogConversation.conversation.conversationTexts)
                    {
                        _conversationQueue.Enqueue(__conversationUnit);
                    }
                    break;
                }
            }
        }

        private void Show()
        {
            if(!_visible)
            {
                _hudAnimator.Play(SHOW_DIALOG_HUD_ANIMATION);
                _visible = true;
            }
            else
                DisplayNextDialog();
        }
        
        [UsedImplicitly]
        private void StartShowText()
        {            
            DisplayNextDialog();

            InputController.UI.InputEnabled = true;
            InputController.GamePlay.InputEnabled = false;
        }

        [UsedImplicitly]
        private void DisablingHud()
        {
            InputController.GamePlay.InputEnabled = true;
            _visible = false;
        }

        private void DisplayNextDialog()
        {
            StopAllCoroutines();

            _dialogText.text = _currentDialogText;

            if (_conversationQueue.Count == 0)
            {
                EndDialog();
                return;
            }

            DialogTextUnit __conversationUnit = _conversationQueue.Dequeue();
            StartCoroutine(TypeDialog(__conversationUnit));
        }

        private IEnumerator TypeDialog (DialogTextUnit p_conversationUnit)
        {
            _speakerText.text = p_conversationUnit.speaker.ToString();
            _dialogText.text = "";

            _currentDialogText = p_conversationUnit.text;

            foreach (char __letter in _currentDialogText.ToCharArray())
            {
                _dialogText.text += __letter;
                yield return null;
            }
        }

        private void EndDialog()
        {
            _hudAnimator.Play(HIDE_DIALOG_HUD_ANIMATION);

            // TODO: Set callback to be called on animation end (like AnimationEventHandlers)
            _dialogEndCallback?.Invoke();
        }

        public void RunUpdate()
        {
            if(InputController.UI.SkipDialog())
                DisplayNextDialog();
        }
    }
}
