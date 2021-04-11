using GameManagers;
using Gameplay.Player.Item;
using UI.EndGamePuzzle;

namespace Gameplay.Objects.Interaction
{
    public class EndGameDoorController : InteractionObjectWithColliders
    {
        private EndGamePuzzleController _endGamePuzzleController;
        private bool _enabled;
        private bool _runningPuzzle;

        // Referência pra porta (para setar o visual de unlocked e de fato abrir a porta)
        // Referência para o Basher/EnemiesManager (para enviar um evento de perseguir player na porta)

        private void Start()
        {
            _enabled = true;
            _runningPuzzle = false;

            _endGamePuzzleController = new EndGamePuzzleController();
            _endGamePuzzleController.onBarCompleted += HandleCurrentBarCompleted;
            _endGamePuzzleController.onAllBarsCompleted += HandlePuzzleCompleted;
        }

        public override void Interact()
        {
            if(_enabled && GameplayManager.instance.inventoryController.inventoryList.Contains(ItemEnum.KEYCARD))
            {
                _runningPuzzle = true;
                // Player entra em animação loop de interação
                _endGamePuzzleController.StartLoading();
            }
        }

        public override void RunFixedUpdate()
        {
            // Player estava no meio do puzzle mas saiu do collider
            if(_runningPuzzle && !_isActive)
            {
                _runningPuzzle = false;
                // Player sai da animação de loop de interação
                _endGamePuzzleController.StopLoading();
            }
        }

        private void HandleCurrentBarCompleted(int p_currentBar)
        {
            // Seta barra correspondente na porta
            // Dispara som de alarme
            // Chama Basher para transform.position desta classe
        }
        
        private void HandlePuzzleCompleted()
        {
            // Desbloqueia porta
            // Toca música da pauleira
            _enabled = false;
        }
    }
}