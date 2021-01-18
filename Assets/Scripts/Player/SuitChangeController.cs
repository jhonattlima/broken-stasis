using System.Collections;
using System.Collections.Generic;
using Audio;
using GameManagers;
using UnityEngine;
using Utilities;

public class SuitChangeController
{
    private const float FADE_IN_ANIMATOR_SPEED = 0.5f;
    private const float FADE_OUT_ANIMATOR_SPEED = 0.1f;

    public void ChangeSuit(PlayerSuitEnum p_playerSuitEnum)
    {
        InputController.GamePlay.InputEnabled = false;
        LoadingView.instance.FadeIn(
            delegate ()
            {
                GameplayManager.instance.ChangePlayerSuit(p_playerSuitEnum);
                Debug.Log("FINISHED FADE IN");

                AudioManager.instance.Play(AudioNameEnum.ITEM_PICKUP);
                // Esperar o som parar de tocar

                // BUG: Fade out não está respeitando o speed do animator para tocar a animação
                LoadingView.instance.FadeOut(delegate ()
                    {
                        InputController.GamePlay.InputEnabled = true;
                        GameHudManager.instance.itemCollectedHud.CallNotification("Collected Suit");
                        Debug.Log("FINISHED FADE OUT");
                    }
                    , FADE_OUT_ANIMATOR_SPEED);
            }
            , FADE_IN_ANIMATOR_SPEED);
    }
}
