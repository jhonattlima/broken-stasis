﻿using System;
using Audio;
using GameManagers;
using Utilities;
using VariableManagement;

public class SuitChangeController
{
    public void ChangeSuit(PlayerSuitEnum p_playerSuitEnum, Action p_onFaded)
    {
        InputController.GamePlay.InputEnabled = false;
        
        LoadingView.instance.FadeIn(delegate ()
            {
                p_onFaded?.Invoke();

                GameHudManager.instance.itemCollectedHud.CallNotification("Collected Suit");
                GameplayManager.instance.onPlayerSuitChange(p_playerSuitEnum);

                AudioManager.instance.Play(AudioNameEnum.SUIT_PICKUP, false, delegate()
                {
                    LoadingView.instance.FadeOut(delegate ()
                        {
                            InputController.GamePlay.InputEnabled = true;
                        }
                        , VariablesManager.uiVariables.defaultFadeOutSpeed
                    );
                });
            }
            , VariablesManager.uiVariables.defaultFadeInSpeed
        );
    }
}
