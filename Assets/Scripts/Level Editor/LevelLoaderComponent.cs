using Newtonsoft.Json;
using ProgrammingBatch.Magnetize.Data;
using System.Collections.Generic;
using UnityEngine;

namespace ProgrammingBatch.Magnetize
{
    public sealed class LevelLoaderComponent : MonoBehaviour
    {
        public void LoadLevel(TextAsset textAsset, string filePath = null)
        {
            LevelData _parsedJson = JsonConvert.DeserializeObject<LevelData>(textAsset.text);
            GenerateLevelObject(_parsedJson);
        }

        private void GenerateLevelObject(LevelData levelData)
        {
            LoadWalls(levelData.walls);
            LoadTowers(levelData.towerPosition);
            LoadPlayer(levelData.player);
            endAt.position = levelData.endAt;
        }

        private void LoadWalls(List<WallData> wallData)
        {
            wallData.ForEach(data =>
            {
                GameObject _wall = Instantiate(wallPrefab, data.position, data.rotation.CreateQuaternion());
                _wall.transform.localScale = data.size;

                _wall.transform.parent = walls.transform;
            });
        }

        private void LoadTowers(List<Vector3> towerData)
        {
            towerData.ForEach(data =>
            {
                GameObject _tower = Instantiate(towerPrefab, data, Quaternion.identity);
                _tower.transform.parent = towers.transform;
            });
        }

        private void LoadPlayer(PlayerData playerData)
        {
            player.transform.position = playerData.position;
            player.transform.rotation = playerData.rotation.CreateQuaternion();
        }

        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private GameObject towerPrefab;
        [Space]
        [SerializeField] private GameObject walls;
        [SerializeField] private GameObject towers;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform endAt;
        //okay, no addressable for now. Idk why i cant find addressable in package manager
     }
}