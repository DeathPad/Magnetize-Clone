using ProgrammingBatch.Magnetize.Handler;
using System.Collections;
using UnityEngine;

namespace ProgrammingBatch.Magnetize.Component
{
    public sealed class InputComponent : MonoBehaviour
    {
        public void SetUp(GameStateHandler gameStateHandler, IHandler boatHandler)
        {
            _gameStateHandler = gameStateHandler;
            _gameStateHandler.HandleEvent -= GameStateChanged;
            _gameStateHandler.HandleEvent += GameStateChanged;

            _boatHandler = boatHandler;
        }

        private void Update()
        {
            
        }

        private void GameStateChanged(GameEnum gameEnum)
        {
            _gameEnum = gameEnum;
            switch(_gameEnum)
            {
                case GameEnum.Idle:
                    StartCoroutine(CheckInput2());
                    break;

                case GameEnum.Play:
                    StartCoroutine(CheckInput());
                    break;

                case GameEnum.Fail:
                case GameEnum.Completed:
                    StopAllCoroutines();
                    break;
            }
        }

        private IEnumerator CheckInput()
        {
            while (_gameEnum == GameEnum.Play)
            {
                yield return null;

                if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                {
                    _boatHandler.TriggerEvent(true);
                }

                if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
                {
                    _boatHandler.TriggerEvent(false);
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _gameStateHandler.TriggerEvent(GameEnum.Idle);
                }
            }
        }

        private IEnumerator CheckInput2()
        {
            while(_gameEnum == GameEnum.Idle)
            {
                yield return null;

                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("A");
                    _gameStateHandler.TriggerEvent(GameEnum.Play);
                }
            }
        }

        private GameStateHandler _gameStateHandler;
        private GameEnum _gameEnum;

        private IHandler _boatHandler;
    }
}