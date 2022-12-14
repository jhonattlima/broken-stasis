using System;
using UnityEngine;
using UnityEngine.Video;
using Utilities;
using Utilities.VariableManagement;

public enum VideoNameEnum
{
    CUTSCENE_CREDITS,
    CUTSCENE_SPLINTER_RUN,
    STUDIO_LOGO
}

public class VideoController : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    private Action _eventToRun;
    private static VideoController _instance;
    public static VideoController instance { get { return _instance; } }
    
    private GameObject _mainCamera;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayVideo(VideoClip p_video, float p_volume, Action p_eventToRun)
    {
        if(_mainCamera == null)
            _mainCamera = Camera.main.gameObject;

        _videoPlayer = _mainCamera.GetComponent<VideoPlayer>();
        if (_videoPlayer == null) _videoPlayer = _mainCamera.AddComponent<VideoPlayer>();

        _videoPlayer.playOnAwake = false;

        _videoPlayer.clip = p_video;
        _videoPlayer.SetDirectAudioVolume(0, p_volume);

        _videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        _videoPlayer.loopPointReached += HandleVideoEnd;
        _videoPlayer.aspectRatio = VideoAspectRatio.FitVertically; 
        _eventToRun = p_eventToRun;

        InputController.GamePlay.MouseEnabled = false;
        InputController.GamePlay.InputEnabled = false;

        _videoPlayer.Play();
    }

    private void HandleVideoEnd(UnityEngine.Video.VideoPlayer videoPlayer)
    {
        _videoPlayer.Stop();
        _videoPlayer.loopPointReached -= HandleVideoEnd;

        InputController.GamePlay.MouseEnabled = true;
        InputController.GamePlay.InputEnabled = true;

        _eventToRun?.Invoke();
    }
}