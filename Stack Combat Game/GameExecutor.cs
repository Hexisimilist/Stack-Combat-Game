namespace Stack_Combat_Game
{
    public sealed class GameExecutor
    {
        static readonly object InstanceLock = new();

        GameExecutor() { }

        private static GameExecutor _instance;

        public static GameExecutor GetInstance()
        {
            if (_instance == null)
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance ??= new GameExecutor();
                    }
                }
            return _instance;
        }

        public int ExecuteGame(ArmyClass one, ArmyClass two)
        {

            return 0;
        }
    }
}
