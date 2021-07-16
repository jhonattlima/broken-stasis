using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using Utilities.VariableManagement;

public class StudioSceneController : MonoBehaviour
{
    private void Start()
    {
        VideoController.instance.PlayVideo(VariablesManager.videoVariables.studioIntro, VariablesManager.videoVariables.studioIntroVolume, () => SceneManager.LoadScene(ScenesConstants.MENU));
    }
}
