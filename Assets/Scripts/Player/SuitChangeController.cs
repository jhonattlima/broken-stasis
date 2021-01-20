using System;
using Audio;
using GameManagers;
using Utilities;

public class SuitChangeController
{
    private const float FADE_IN_ANIMATOR_SPEED = 0.5f;
    private const float FADE_OUT_ANIMATOR_SPEED = 0.1f;

    public void ChangeSuit(PlayerSuitEnum p_playerSuitEnum, Action p_onFaded)
    {
        InputController.GamePlay.InputEnabled = false;
        
        
        LoadingView.instance.FadeIn(delegate ()
            {
                p_onFaded?.Invoke();

                GameHudManager.instance.itemCollectedHud.CallNotification("Collected Suit");
                GameplayManager.instance.onPlayerSuitChange(p_playerSuitEnum);

                AudioManager.instance.Play(AudioNameEnum.ITEM_PICKUP);
                // Esperar o som parar de tocar

                // BUG: Fade out não está respeitando o speed do animator para tocar a animação
                LoadingView.instance.FadeOut(delegate ()
                    {
                        InputController.GamePlay.InputEnabled = true;
                    }
                    , FADE_OUT_ANIMATOR_SPEED
                );
            }
            , FADE_IN_ANIMATOR_SPEED
        );
    }
}
