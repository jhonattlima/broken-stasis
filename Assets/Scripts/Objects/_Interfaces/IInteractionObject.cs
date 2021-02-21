using Utilities;

namespace Interaction
{
    public interface IInteractionObject : IUpdateBehaviour, IFixedUpdateBehaviour
    {
        void Interact();
    }
}
