using System.Collections.Generic;
using UnityEngine;

    public class ObjectPool<T> where T : Behaviour
    {
        private readonly T _prefab;
        private readonly int _count;

        private List<T> _list;

        public ObjectPool(T prefab, int count)
        {
            _prefab = prefab;
            _count = count;
            _list = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                ReturntoPool(Object.Instantiate(prefab));
            }
        }

        public T GetOrNull()
        {
            var index = _list.Count - 1;
            var pooledObject = _list[index];
            _list.RemoveAt(index);

            pooledObject.gameObject.SetActive(true);

            return pooledObject;
        }


        public T GetForce()
        {
            var index = _list.Count - 1;
            T pooledObject;
            if (index >= 0) {
                pooledObject = _list[index];
                _list.RemoveAt(index);
            }
            else
            {
                pooledObject = Object.Instantiate(_prefab);
            }


            pooledObject.gameObject.SetActive(true);

            return pooledObject;
        }

        public bool TryGet(out T pooledObject)
        {
            if (_list.Count == 0)
            {
                pooledObject = null;
                return false;
            }

            var index = _list.Count - 1;
            pooledObject = _list[index];
            _list.RemoveAt(index);

            pooledObject.gameObject.SetActive(true);

            return pooledObject;
        }


        public void ReturntoPool(T pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
            _list.Add(pooledObject);
            Debug.Log("Returned");
        }
    }