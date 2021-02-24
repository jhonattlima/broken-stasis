using Utilities;

namespace Objects.Interaction
{
    public interface IInteractionObject : IUpdateBehaviour, IFixedUpdateBehaviour
    {
        void Interact();
    }
}
