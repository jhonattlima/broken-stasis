using System.Collections.Generic;

namespace GameManagers
{
    public static class GameEventManager
    {
        private static List<IGameEvent> _gameEvents;

        public static void SetGameEvents(List<IGameEvent> p_gameEvents)
        {
            _gameEvents = p_gameEvents;
        }

        public static void RunGameEvent(GameEventType p_gameEvent, bool p_runSingleTimeEvents = true)
        {
            foreach(IGameEvent gameEvent in _gameEvents)
            {
                if(p_gameEvent == gameEvent.gameEventType)
                {
                    gameEvent.RunPermanentEvents();

                    if(p_runSingleTimeEvents)
                        gameEvent.RunSingleTimeEvents();
                }
            }
        }
    }
}