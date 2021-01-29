public interface IGameEvent
{
    GameEventType gameEventType { get; }
    bool hasRun { get; }

    void RunSingleTimeEvents();
    void RunPermanentEvents();
}
