using ProgrammingBatch.Magnetize.Handler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProgrammingBatch.Magnetize.Component
{
    public sealed class TowerTrackerComponent : MonoBehaviour
    {
        public void SetUp(GameStateHandler gameStateHandler, IHandler trackerEvent, List<TowerComponent> towerComponents)
        {
            _gameStateHandler = gameStateHandler;
            _gameStateHandler.HandleEvent -= GameStateChanged;
            _gameStateHandler.HandleEvent += GameStateChanged;

            _trackerHandler = trackerEvent;

            _towerList = towerComponents;

        }

        private void OnDestroy()
        {
            try
            {
                _gameStateHandler.HandleEvent -= GameStateChanged;
            } catch(Exception)
            {
                Debug.Log("no event to unsubscribe");
            }
        }

        private void GameStateChanged(GameEnum gameEnum)
        {
            _gameEnum = gameEnum;
            switch(_gameEnum)
            {
                case GameEnum.Idle:
                    break;

                case GameEnum.Play:
                    StartCoroutine(TrackingCoroutine());
                    break;

                case GameEnum.Completed:
                case GameEnum.Fail:
                    StopAllCoroutines();
                    break;
            }
        }

        private IEnumerator TrackingCoroutine()
        {
            while(_gameEnum == GameEnum.Play)
            {
                yield return null;

                float _closestDistance = float.MaxValue;
                TowerComponent _closestTower = null;
                _towerList.ForEach(tower =>
                {
                    float _distance = tower.GetDistance(boat);
                    if(_distance < _closestDistance)
                    {
                        _closestDistance = _distance;
                        _closestTower = tower;
                    }
                });

                if(_closestTower != null && _closestTower != _currentClosestTower)
                {
                    _trackerHandler.TriggerEvent(_closestTower);
                    _currentClosestTower = _closestTower;
                }
            }
        }

        [SerializeField] private GameObject boat = default;

        private List<TowerComponent> _towerList = new List<TowerComponent>();

        private GameStateHandler _gameStateHandler;
        private GameEnum _gameEnum;

        private IHandler _trackerHandler;

        private TowerComponent _currentClosestTower;
    }
}