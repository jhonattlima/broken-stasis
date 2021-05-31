
using CoreEvent.GameEvents;
using GameManagers;
using UnityEngine;

namespace Gameplay.Objects.Interaction
{
    public class ControlPanelController : InteractionObjectWithColliders
    {
        public override void Interact()
        {
            GameEventManager.RunGameEvent(GameEventTypeEnum.CUTSCENE_CREDITS);
        }
    }
}
