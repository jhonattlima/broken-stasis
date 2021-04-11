﻿using GameManagers;
using Gameplay.Player.Item;
using Gameplay.Player.Motion;
using UI.EndGamePuzzle;
using UnityEngine;

namespace Gameplay.Objects.Interaction
{
    public class EndGameDoorController : InteractionObjectWithColliders
    {
        private EndGamePuzzleController _endGamePuzzleController;
        private bool _enabled;
        private bool _runningPuzzle;
        [SerializeField] private DoorController _door;

        // TODO: Substituir Depois por uma porta com os 3 indicadores

        // Referência para o Basher/EnemiesManager (para enviar um evento de perseguir player na porta)

        private void Start()
        {
            _enabled = true;
            _runningPuzzle = false;

            _door.LockDoor();

            _endGamePuzzleController = new EndGamePuzzleController();
            _endGamePuzzleController.onBarCompleted = HandleCurrentBarCompleted;
            _endGamePuzzleController.onAllBarsCompleted = HandlePuzzleCompleted;
        }

        public override void Interact()
        {
            if(!_runningPuzzle && _enabled && GameplayManager.instance.inventoryController.inventoryList.Contains(ItemEnum.KEYCARD))
            {
                _runningPuzzle = true;
                PlayerStatesManager.SetPlayerState(PlayerState.INTERACT_WITH_ENDLEVEL_DOOR);
                _endGamePuzzleController.StartLoading();
            }
        }

        public override void RunFixedUpdate()
        {
            // Player estava no meio do puzzle mas saiu do collider
            if(_runningPuzzle && !_isActive)
            {
                _runningPuzzle = false;
                PlayerStatesManager.SetPlayerState(PlayerState.EXITED_ENDLEVEL_DOOR_AREA);
                _endGamePuzzleController.StopLoading();
            }
        }

        private void HandleCurrentBarCompleted(int p_currentBar)
        {
            UnityEngine.Debug.Log("Bar " + p_currentBar + " completed");
            _door.UnlockDoorLock();
            // Dispara som de alarme
            // Chama Basher para transform.position desta classe
        }
        
        private void HandlePuzzleCompleted()
        {
            UnityEngine.Debug.Log("All bars completed");

            _runningPuzzle = false;
            PlayerStatesManager.SetPlayerState(PlayerState.EXITED_ENDLEVEL_DOOR_AREA);
            GameHudManager.instance.endGameUI.HideUI();
            _door.Interact();
            // Toca música da pauleira
            _enabled = false;
        }
    }
}