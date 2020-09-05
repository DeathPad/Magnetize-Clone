namespace ProgrammingBatch.Magnetize.Core
{
    public sealed class Core
    {
        public static bool IsInitialized { get; private set; }

        public LevelManager LevelManager { get; private set; }

        internal Core()
        {
        }

        internal void Init()
        {
            LoadCoreModules();
            LoadGameModules();
            IsInitialized = true;
        }

        private void LoadCoreModules()
        {
        }

        private void LoadGameModules()
        {
            LevelManager = new LevelManager();
        }
    }
}