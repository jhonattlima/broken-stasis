
using GameManagers;

namespace Objects.Interaction
{
    public class GeneratorController : InteractionObjectWithColliders
    {
        private void Start()
        {
            GameHudManager.instance.minigameHud.onMinigameSuccess = HandleMinigameSuccess;
            GameHudManager.instance.minigameHud.onMinigameFailed = HandleMinigameFailed;
        }

        public override void Interact()
        {
            GameHudManager.instance.minigameHud.ShowMinigame();
        }

        private void HandleMinigameSuccess()
        {
            // chama game event success
        }

        private void HandleMinigameFailed()
        {

        }
    }
}
