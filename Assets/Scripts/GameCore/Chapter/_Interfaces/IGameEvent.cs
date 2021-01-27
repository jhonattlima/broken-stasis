public interface IGameEvent
{
    GameEventType gameEventType { get; }

    void RunSingleTimeEvents();
    void RunPermanentEvents();
}
