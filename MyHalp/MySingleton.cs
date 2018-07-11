// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

namespace MyHalp
{
    /// <inheritdoc />
    /// <summary>
    /// Creates instance when needed.
    /// Can be used for managers and anything else which needs to be easily accessed.
    /// </summary>
    /// <typeparam name="T">The derived type of object.</typeparam>
    public class MySingleton<T> : MyComponent where T : MyComponent
    {
        private static T _instance;

        /// <summary>
        /// Destroys the instance.
        /// </summary>
        public static void Destroy()
        {
            Destroy(_instance);
            _instance = null;
        }

        public static T Instance => _instance ?? (_instance = MyInstancer.Create<T>());
    }
}
