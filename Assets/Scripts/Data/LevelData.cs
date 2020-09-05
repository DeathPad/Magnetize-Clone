using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ProgrammingBatch.Magnetize.Data
{
    public sealed class LevelData
    {
        public List<WallData> walls;
        public List<Vector3> towerPosition;
        public PlayerData player;
        [JsonIgnore]
        public Vector3 startAt;
        public Vector3 endAt;
    }
}