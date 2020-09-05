using System;
using UnityEngine;

namespace ProgrammingBatch.Magnetize.Component
{
    public sealed class TowerComponent : MonoBehaviour
    {
        public void SetUpHandler(IHandler trackerHandler)
        {
            _trackerHandler = trackerHandler;
            _trackerHandler.HandleEvent -= OnTrackerChanged;
            _trackerHandler.HandleEvent += OnTrackerChanged;
        }

        public float GetDistance(GameObject boat)
        {
            return Vector3.Distance(transform.position, boat.transform.position);
        }

        private void OnDestroy()
        {
            try
            {
                _trackerHandler.HandleEvent -= OnTrackerChanged;
            } catch(NullReferenceException)
            {
                Debug.Log("No event to unsubscribe");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        private void OnTrackerChanged(object value = null)
        {
            if(value == null)
            {
                return;
            }

            TowerComponent _tower = value as TowerComponent;
            if (_tower.Equals(this))
            {
                spriteRenderer.color = Color.green;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }

        [SerializeField] private int radius = 5;
        [SerializeField] private SpriteRenderer spriteRenderer = default;

        private IHandler _trackerHandler;
    }
}