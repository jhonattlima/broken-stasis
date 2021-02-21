
namespace CoreEvent
{
    public interface IGameEvent
    {
        GameEventTypeEnum gameEventType { get; }
        bool hasRun { get; }

        void RunSingleTimeEvents();
        void RunPermanentEvents();
    }
}
