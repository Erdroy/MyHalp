using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyHalp
{
    public static class MyObjectPool
    {
        private struct ObjectEntry
        {
            public bool Allocated;
            public GameObject Object;
        }

        private static GameObject _head;
        private static ObjectEntry[] _objectPool;

        public static void Init(uint objectCount = 2000)
        {
            if(_objectPool != null || _head != null)
                throw new Exception("Can not initialize the MyObjectPool module second time! This can be done only once.");
            
            _head = new GameObject("MyObjectPool HEAD");
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

                    CleanObject(_objectPool[i].Object);
                    _objectPool[i].Object.transform.parent = _head.transform;
                }
            }
        }

        public static GameObject Request(out int index)
        {
            lock (_objectPool)
            {
                for (var i = 0; i < _objectPool.Length; i++)
                {
                    var obj = _objectPool[i];

                    if (!obj.Allocated)
                    {
                        obj.Object.transform.parent = null;
                        obj.Allocated = true;
                        index = i;
                        return obj.Object;
                    }
                }
            }
            index = -1;
            return null;
        }

        public static void Release(int index)
        {
            lock (_objectPool)
            {
                _objectPool[index].Allocated = false;

                if(_objectPool[index].Object)
                    CleanObject(_objectPool[index].Object);
            }
        }

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