namespace Diese
{
    public abstract class Singleton<TSingleton>
        where TSingleton : class
    {
        static private TSingleton _instance;

        static public TSingleton Instance
        {
            get { return _instance ?? (_instance = default(TSingleton)); }
        }
    }
}