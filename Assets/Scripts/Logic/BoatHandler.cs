using ProgrammingBatch.FlappyBirdClone.Event;
namespace ProgrammingBatch.Magnetize.Handler
{
    public sealed class BoatHandler : IHandler
    {
        public event OnEventHandler HandleEvent;

        public void TriggerEvent(object value = null)
        {
            HandleEvent?.Invoke(value);
        }
    }
}