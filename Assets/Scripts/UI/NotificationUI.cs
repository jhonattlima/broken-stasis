using UnityEngine;
using UnityEngine.UI;
using Utilities;
using VariableManagement;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _notificationText;

    private const string SHOW_ANIMATION = "Show";
    private const string HIDE_ANIMATION = "Hide";

    private float _defaultNotificationDuration;

    private void Awake()
    {
        _defaultNotificationDuration = VariablesManager.uiVariables.defaultNotificationDuration;
    }

    public void ShowNotification(string p_text)
    {
        _notificationText.text = p_text;

        _animator.Play(SHOW_ANIMATION);
    }

    public void ShowAutoHideNotification(string p_text)
    {
        ShowAutoHideNotification(p_text, _defaultNotificationDuration);
    }

    public void ShowAutoHideNotification(string p_text, float p_duration)
    {
        float __duration; 
        
        _notificationText.text = p_text;

        _animator.Play(SHOW_ANIMATION);

        if(p_duration != _defaultNotificationDuration)
            __duration = p_duration;
        else
            __duration = CalculateAutoHideTime(p_text);

        TFWToolKit.Timer(__duration, delegate () 
        {
            _animator.Play(HIDE_ANIMATION); 
        });
    }

    private float CalculateAutoHideTime(string p_text)
    {
        int __wordCount = p_text.Split(' ').Length;
        float __readingSpeed = VariablesManager.uiVariables.defaultReadingWPM / 60f;

        return __wordCount * __readingSpeed;
    }

    public void HideNotification()
    {
        _animator.Play(HIDE_ANIMATION);
    }
}
