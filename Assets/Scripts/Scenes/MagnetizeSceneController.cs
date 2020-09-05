using ProgrammingBatch.Magnetize.Component;
using ProgrammingBatch.Magnetize.Handler;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProgrammingBatch.Magnetize.Core
{
    public class MagnetizeSceneController : SceneController
    {
        public static Core GameCore { get; private set; }

        protected override void Start()
        {
            if(!Core.IsInitialized)
            {
                GameCore = new Core();
                GameCore.Init();
            }
            
            base.Start();
        }

        public override void OnStartInitialize()
        {
            _gameStateHandler = new GameStateHandler();
            _towerTracker = new TowerTrackerHandler();
            _boatTracker = new BoatHandler();

        }

        public override void InitializeSceneComponents() 
        {
            
            bool _isLoaded = GenerateLevel();
            if(!_isLoaded)
            {
                return;
            }

            List<TowerComponent> _towers = new List<TowerComponent>();
            foreach(Transform _tower in towers.transform)
            {
                TowerComponent _tComponent = _tower.GetComponent<TowerComponent>();
                _tComponent.SetUpHandler(_towerTracker);
                _towers.Add(_tComponent);
            }

            boatComponent.SetGameStateHandler(_gameStateHandler, _towerTracker, _boatTracker);
            towerTrackerComponent.SetUp(_gameStateHandler, _towerTracker, _towers);
            inputComponent.SetUp(_gameStateHandler, _boatTracker);
        }

        /// <summary>
        /// Show disable visual effect in Introduction function or disable unused scripts
        /// </summary>
        public override void StartCompleted() 
        {
            _gameStateHandler.TriggerEvent(GameEnum.Play);
        }

        private bool GenerateLevel()
        {
            bool _isLoaded = false;

            TextAsset _levelAsset = null;
            try
            {
                _levelAsset = GameCore.LevelManager.GetLevel();
                levelLoader.LoadLevel(_levelAsset);
                _isLoaded = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.Log("You have been completed all available levels");
            }

            return _isLoaded;
        }

        [Space]
        [SerializeField] private LevelLoaderComponent levelLoader = default;
        [SerializeField] private BoatComponent boatComponent = default;
        [SerializeField] private TowerTrackerComponent towerTrackerComponent = default;
        [SerializeField] private InputComponent inputComponent = default;
        [Space]
        [SerializeField] private GameObject towers = default;

        private GameStateHandler _gameStateHandler;
        private IHandler _towerTracker;
        private IHandler _boatTracker;
    }
}