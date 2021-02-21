using System.Collections.Generic;
using Interaction;
using Utilities;

namespace GameManager
{
    public class LevelObjectManager : IFixedUpdateBehaviour, IUpdateBehaviour
    {
        private readonly List<IInteractionObject> _interactionObjectsList;

        public LevelObjectManager(List<IInteractionObject> p_interactionObjectsList)
        {
            _interactionObjectsList = p_interactionObjectsList;
        }

        public void RunFixedUpdate()
        {
            _interactionObjectsList.ForEach(interactionObject => interactionObject.RunFixedUpdate());
        }

        public void RunUpdate()
        {
            _interactionObjectsList.ForEach(interactionObject => interactionObject.RunUpdate());
        }
    }
}