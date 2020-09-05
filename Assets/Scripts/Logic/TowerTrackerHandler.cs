using ProgrammingBatch.FlappyBirdClone.Event;
using UnityEngine;

namespace ProgrammingBatch.Magnetize.Handler
{
    public sealed class TowerTrackerHandler : IHandler
    {
        public event OnEventHandler HandleEvent;

        public void TriggerEvent(object value = null)
        {
            HandleEvent?.Invoke(value);
        }
    }
}