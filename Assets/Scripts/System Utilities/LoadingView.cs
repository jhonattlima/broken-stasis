using System;
using JetBrains.Annotations;
using UnityEngine;

public class LoadingView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private static Action _onFadeIn;
    private static Action _onFadeOut;

    private static LoadingView _instance;

    public static LoadingView instance
    {
        get
        {
            return _instance ?? (_instance = InstanceInitialize());
        }
    }
    
    private static LoadingView InstanceInitialize()
    {
        GameObject _loadingViewGameObject = Resources.Load<GameObject>("LoadingView");
        
        _instance = Instantiate(_loadingViewGameObject).GetComponent<LoadingView>();

        DontDestroyOnLoad(_instance);

        return _instance;
    }

    public void InstantBlackScreen()
    {
        _animator.Play("InstantBlackScreen");
    }

    public void FadeIn(Action p_onFinish)
    {
        _onFadeIn = p_onFinish;
        _animator.Play("BlackFadeIn");
    }

    public void FadeOut(Action p_onFinish)
    {
        _onFadeOut = p_onFinish;
        _animator.Play("BlackFadeOut");
    }

    [UsedImplicitly]
    private void FinishedFadeInAnimation()
    {
        _onFadeIn?.Invoke();
        
        _onFadeIn = null;
    }

    [UsedImplicitly]
    private void FinishedFadeOutAnimation()
    {
        _onFadeOut?.Invoke();
        
        _onFadeOut = null;
    }
}
