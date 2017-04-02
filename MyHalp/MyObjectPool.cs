// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using UnityEngine;
using Object = UnityEngine.Object;

namespace MyHalp
{
    /// <summary>
    /// MyObjectPool class, allows to really fast acquire gameobject for temporary use.
    /// </summary>
    public static class MyObjectPool
    {
        private struct ObjectEntry
        {
            public bool Allocated;
            public GameObject Object;
        }

        private static GameObject _head;
        private static ObjectEntry[] _objectPool;

        /// <summary>
        /// Initialize the object pool with given object count in the pool.
        /// </summary>
        /// <param name="objectCount">The amout of object to be created for the pooling.</param>
        public static void Init(uint objectCount = 2000)
        {
            if (_objectPool != null || _head != null)
            {
                Debug.LogError("Can not initialize the MyObjectPool module second time! This can be done only once.");
                return;
            }

            _head = new GameObject("MyObjectPool");

            // do not destroy on scene change
            Object.DontDestroyOnLoad(_head);

            _objectPool = new ObjectEntry[objectCount];

            lock (_objectPool)
            {
                for (var i = 0; i < _objectPool.Length; i ++)
                {
                    _objectPool[i] = new ObjectEntry
                    {
                        Allocated = false,
                        Object = new GameObject("pooledGameObject")
                    };

                    // do not destroy on scene change
                    Object.DontDestroyOnLoad(_objectPool[i].Object);

                    CleanObject(_objectPool[i].Object);
                    _objectPool[i].Object.transform.parent = _head.transform;
                }
            }
        }

        /// <summary>
        /// Require gameobject.
        /// </summary>
        /// <param name="index">The output index which will be required to release the object.</param>
        /// <returns>The acquired gameobject.</returns>
        public static GameObject Request(out int index)
        {
            lock (_objectPool)
            {
                for (var i = 0; i < _objectPool.Length; i++)
                {
                    var obj = _objectPool[i];

                    if (!obj.Object)
                    {
                        // wut the developer is doin with my childs?
                        Debug.LogWarning("Found destroyed MyObjectPool object, this should never happen!");
                        continue;
                    }

                    if (!obj.Allocated)
                    {
                        // allocate
                        _objectPool[i].Allocated = true;

                        // reset objects parent
                        obj.Object.transform.parent = null;

                        // set the index
                        index = i;

                        // done
                        return obj.Object;
                    }
                }
            }

            // no free objects
            index = -1;
            return null;
        }

        /// <summary>
        /// Release gameobject with given index.
        /// </summary>
        /// <param name="index"></param>
        public static void Release(int index)
        {
            lock (_objectPool)
            {
                _objectPool[index].Allocated = false;

                if(_objectPool[index].Object)
                    CleanObject(_objectPool[index].Object);
            }
        }

        // private
        private static void CleanObject(GameObject go)
        {
            var got = go.transform;

            // clean childrens if needed
            if (got.childCount > 0)
            {
                for (var i = 0; i < got.childCount; i++)
                {
                    Object.Destroy(got.GetChild(i).gameObject);
                }
            }

            // clean components
            var components = got.GetComponents(typeof(Component));

            foreach (var component in components)
            {
                if (!(component is Transform))
                {
                    Object.Destroy(component);
                }
            }

            // clean transform
            got.position = Vector3.zero;
            got.rotation = Quaternion.identity;
            got.localScale = Vector3.one;
            got.parent = _head.transform;
        }
    }
}