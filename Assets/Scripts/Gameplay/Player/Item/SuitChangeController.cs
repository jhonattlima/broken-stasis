using System;
using GameManagers;
using UI;
using UnityEngine;
using Utilities;
using Utilities.Audio;
using Utilities.VariableManagement;

namespace Gameplay.Player.Item
{
    public class SuitChangeController
    {
        public void ChangeSuit(PlayerSuitEnum p_playerSuitEnum, Action p_onFaded)
        {
            GameStateManager.SetGameState(GameState.CUTSCENE);

            LoadingView.instance.FadeIn(delegate ()
                {
                    p_onFaded?.Invoke();

                    GameHudManager.instance.notificationHud.ShowText("Collected Suit");
                    GameplayManager.instance.onPlayerSuitChange(p_playerSuitEnum);

                    AudioManager.instance.Play(AudioNameEnum.ITEM_SUIT_PICKUP, false, delegate ()
                    {
                        LoadingView.instance.FadeOut(delegate ()
                            {
                                Debug.Log("Played fadeOut");
                                GameStateManager.SetGameState(GameState.RUNNING);
                            }
                            , VariablesManager.uiVariables.defaultFadeOutSpeed
                        );
                    });
                }
                , VariablesManager.uiVariables.defaultFadeInSpeed
            );
        }
    }
}
