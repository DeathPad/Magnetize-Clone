using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ProgrammingBatch.Magnetize
{
    public class LevelManager
    {
        public LevelManager()
        {
            _loadedLevels = Resources.LoadAll<TextAsset>("Levels").ToList();
        }

        public TextAsset GetLevel()
        {
            TextAsset _levelAsset = _loadedLevels[0];
            _loadedLevels.Remove(_levelAsset);
            return _levelAsset;
        }

        private List<TextAsset> _loadedLevels = new List<TextAsset>();
    }
}