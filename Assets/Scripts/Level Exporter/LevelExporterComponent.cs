using Newtonsoft.Json;
using ProgrammingBatch.Magnetize.Data;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ProgrammingBatch.Magnetize.LevelManager
{
    public sealed class LevelExporterComponent : MonoBehaviour
    {
        [SerializeField] private string levelName = default;

        [SerializeField] private GameObject walls = default;
        [SerializeField] private GameObject towers = default;
        [SerializeField] private GameObject player = default;
        [SerializeField] private Transform startAt = default;
        [SerializeField] private Transform endAt = default;

        private void Start()
        {
            LevelData _levelData = new LevelData
            {
                walls = GetWallsData(),
                towerPosition = GetTowersData(),
                player = GetPlayerData(),
                startAt = startAt.position,
                endAt = endAt.position
            };
            ExportToJSON(_levelData);
        }

        private List<WallData> GetWallsData()
        {
            List<WallData> _wallsData = new List<WallData>();
            foreach (Transform _wall in walls.transform)
            {
                WallData _wallData = new WallData()
                {
                    position = _wall.position,
                    rotation = new RotationData(_wall.rotation),
                    size = _wall.localScale
                };
                _wallsData.Add(_wallData);
            }
            return _wallsData;
        }

        private List<Vector3> GetTowersData()
        {
            List<Vector3> _towersData = new List<Vector3>();
            foreach (Transform _tower in towers.transform)
            {
                _towersData.Add(_tower.position);
            }
            return _towersData;
        }

        private PlayerData GetPlayerData()
        {
            PlayerData _playerData = new PlayerData()
            {
                position = player.transform.position,
                rotation = new RotationData(player.transform.rotation)
            };
            return _playerData;
        }

        private void ExportToJSON(LevelData levelData)
        {
            string _serializedLevelData = string.Empty;
            try
            {
                _serializedLevelData = JsonConvert.SerializeObject(levelData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to serialize level. {e}");
            }

            string _pathName = EditorUtility.OpenFolderPanel("Save created level", "Assets", "Levels");
            try
            {
                File.WriteAllText($"{_pathName}/{levelName}.json", _serializedLevelData);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}