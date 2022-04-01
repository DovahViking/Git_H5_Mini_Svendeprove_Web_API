namespace H5_Svendeprove_Web_API.Singletons
{
    public class Singleton_Game
    {
        /// <summary>
        /// USE OF "Second version - simple thread-safety" FROM https://csharpindepth.com/articles/singleton
        /// </summary>

        private static Singleton_Game instance = null;
        private static readonly object padlock = new object();

        public List<int[]> pool_array = new List<int[]>();
        public int user_id;

        Singleton_Game()
        {
        }

        public static Singleton_Game init
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton_Game();
                    }
                    return instance;
                }
            }
        }

    }
}
