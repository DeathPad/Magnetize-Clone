using ProgrammingBatch.Magnetize.Event;

namespace ProgrammingBatch.Magnetize.Handler
{
    public sealed class GameStateHandler
    {
        public event GameEventHandler HandleEvent;

        public void TriggerEvent(GameEnum gameEnum)
        {
            HandleEvent?.Invoke(gameEnum);
        }
    }
}