using System;
using JetBrains.Annotations;
using UnityEngine;

public class LoadingView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public static LoadingView instance;
    
    private static Action _onFadeIn;
    private static Action _onFadeOut;

    private void Awake()
    {      
        if(instance == null)
            instance = this;
        
        DontDestroyOnLoad(this);
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
