
using CoreEvent.GameEvents;
using GameManagers;

namespace Objects.Interaction
{
    public class GeneratorController : InteractionObjectWithColliders
    {
        private bool _enabled;

        private void Awake()
        {
            _enabled = false;
        }

        public void SetEnabled(bool p_enabled)
        {
            _enabled = p_enabled;
        }

        private void Start()
        {
            GameHudManager.instance.minigameHud.onMinigameSuccess = HandleMinigameSuccess;
            GameHudManager.instance.minigameHud.onMinigameFailed = HandleMinigameFailed;
        }

        public override void Interact()
        {
            if(_enabled)
                GameHudManager.instance.minigameHud.ShowMinigame();
        }

        private void HandleMinigameSuccess()
        {
            GameEventManager.RunGameEvent(GameEventTypeEnum.COMPLETE_MINIGAME);
        }

        private void HandleMinigameFailed()
        {

        }
    }
}
