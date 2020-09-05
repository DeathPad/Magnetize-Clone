using ProgrammingBatch.Magnetize.Handler;
using System;
using System.Collections;
using UnityEngine;

namespace ProgrammingBatch.Magnetize.Component
{
    public sealed class BoatComponent : MonoBehaviour
    {
        public void SetGameStateHandler(GameStateHandler gameStateHandler, IHandler towerTracker, IHandler boatTracker)
        {
            _gameStateHandler = gameStateHandler;
            _gameStateHandler.HandleEvent -= GameStateChanged;
            _gameStateHandler.HandleEvent += GameStateChanged;

            _towerTracker = towerTracker;
            _towerTracker.HandleEvent -= OnClosestTowerChanged;
            _towerTracker.HandleEvent += OnClosestTowerChanged;

            _boatHandler = boatTracker;
            _boatHandler.HandleEvent -= OnBoatInput;
            _boatHandler.HandleEvent += OnBoatInput;
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        private void OnDestroy()
        {
            try
            {
                _gameStateHandler.HandleEvent -= GameStateChanged;
                _towerTracker.HandleEvent -= OnClosestTowerChanged;
                _boatHandler.HandleEvent -= OnBoatInput;
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("No event to unsubscribe");
            }
        }

        private void GameStateChanged(GameEnum gameEnum)
        {
            _gameEnum = gameEnum;
            switch (_gameEnum)
            {
                case GameEnum.Idle:
                    rb2d.velocity = Vector2.zero;
                    rb2d.angularVelocity = 0;
                    StopAllCoroutines();
                    break;

                case GameEnum.Play:
                    StartCoroutine(BoatMoving());
                    break;

                case GameEnum.Fail:
                case GameEnum.Completed:
                    rb2d.velocity = Vector2.zero;
                    StopAllCoroutines();
                    break;
            }
        }

        private void OnClosestTowerChanged(object value = null)
        {
            if (value == null)
            {
                Debug.Log("closes tower is null");
                return;
            }

            _closestTower = value as TowerComponent;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Wall")
            {
                explode.Play();
                _gameStateHandler.TriggerEvent(GameEnum.Fail);
            }
        }

        private IEnumerator BoatMoving()
        {
            while (_gameEnum == GameEnum.Play)
            {
                yield return null;
                BoatMovement();
            }
        }

        private void BoatMovement()
        {
            if (!_isPressed)
            {
                rb2d.velocity = speed * -transform.up;
                rb2d.angularVelocity = 0;
                return;
            }

            float distance = Vector2.Distance(transform.position, _closestTower.transform.position);

            //Gravitation toward tower
            Vector3 pullDirection = (_closestTower.transform.position - transform.position).normalized;
            float newPullForce = Mathf.Clamp(pullForce / distance, 20, 50);
            rb2d.AddForce(pullDirection * newPullForce);

            rb2d.angularVelocity = -rotateSpeed / distance;
        }

        private void OnBoatInput(object value = null)
        {
            try
            {
                _isPressed = (bool)value;
            } catch(Exception)
            {
                _isPressed = false;
            }
        }

        [SerializeField] private Rigidbody2D rb2d = default;
        [SerializeField] private float speed;
        [SerializeField] private float pullForce;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private AudioSource explode = default;

        private GameStateHandler _gameStateHandler;
        private GameEnum _gameEnum = GameEnum.Idle;

        private IHandler _towerTracker;
        private TowerComponent _closestTower;

        private IHandler _boatHandler;
        private bool _isPressed;
    }
}