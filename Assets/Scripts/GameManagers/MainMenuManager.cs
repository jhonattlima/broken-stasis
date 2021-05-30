using GameManagers;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities.Audio;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _buttonLoadGame;

    private void Awake()
    {
        AudioManager.instance.Play(AudioNameEnum.SOUND_TRACK_INTRO, true);
        HandleLoadGameButtonState();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        SaveGameManager.LoadGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void HandleLoadGameButtonState()
    {
        if(SaveGameManager.HasLoadFile()) return;

        _buttonLoadGame.GetComponent<Button>().enabled = false;
        GrayOutImage(_buttonLoadGame.GetComponent<Image>());
    }

    private void GrayOutImage(Image p_image)
    {
        var __newColor = new Color();
        __newColor.a = 0.2f;
        __newColor.b = p_image.color.b;
        __newColor.g = p_image.color.g;
        __newColor.r = p_image.color.r;
        p_image.color = __newColor;
    }
}
