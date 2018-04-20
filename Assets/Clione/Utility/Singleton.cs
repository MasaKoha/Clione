namespace Clione.Utility
{
    public class Singleton<T> where T : class, new()
    {
        public static T Instance { get; } = new T();

        protected Singleton()
        {
        }
    }
}