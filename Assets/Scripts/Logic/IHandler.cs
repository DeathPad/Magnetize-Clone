using ProgrammingBatch.FlappyBirdClone.Event;

namespace ProgrammingBatch.Magnetize
{
    public interface IHandler
    {
        event OnEventHandler HandleEvent;
        void TriggerEvent(object value = null);
    }
}