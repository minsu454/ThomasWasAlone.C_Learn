using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Pool
{ 
    public class ObjectPoolContainer : MonoBehaviour
    {
        private static ObjectPoolContainer instance;
        public static ObjectPoolContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("----------ObjectPool----------");
                    instance = obj.AddComponent<ObjectPoolContainer>();
                }
                return instance;
            }
        }

        public Dictionary<string, ObjectPool> objectPoolDic = new Dictionary<string, ObjectPool>();     //오브젝트풀들 담는 Dictionary

        /// <summary>
        /// 오브젝트풀에 오브젝트 생성해주는 함수
        /// </summary>
        public void CreateObjectPool(string poolName, GameObject prefab, int preloadCount, Transform poolTr = null)
        {
            if (objectPoolDic.ContainsKey(poolName))
            {
                Debug.LogError($"Object pool {poolName} is already exists.");
                return;
            }

            ObjectPool pool;
            pool = new ObjectPool(poolName, prefab, poolTr, preloadCount);

            objectPoolDic.Add(poolName, pool);
        }

        /// <summary>
        /// 오브젝트풀에서 오브젝트 가져오는 함수
        /// </summary>
        public GameObject Pop(string poolName)
        {
            if (string.IsNullOrEmpty(poolName))
            {
                return null;
            }

            if (!objectPoolDic.TryGetValue(poolName, out ObjectPool pool))
            {
                return null;
            }

            return pool.GetObject();
        }
        
        /// <summary>
        /// 오브젝트풀에 반환해주는 함수
        /// </summary>
        public void Return(GameObject obj)
        {
            string poolName = obj.name;

            if (!objectPoolDic.TryGetValue(poolName, out ObjectPool pool))
            {
                Debug.LogError($"Cannot found pool Name: {poolName}");
                return;
            }

            obj.gameObject.SetActive(false);
            pool.objectStack.Push(obj);
        }
    }

    public static class Utility {
        /// <summary>
        /// 오브젝트풀에 오브젝트 생성해주는 함수
        /// </summary>
        public static void CreateObjectPool(this GameObject obj, int preloadCount, Transform poolTr = null)
        {
            ObjectPoolContainer.Instance.CreateObjectPool(obj.name, obj, preloadCount, poolTr);
        }

        public static void Return(this GameObject obj) { 
            ObjectPoolContainer.Instance.Return(obj);
        }
    }
}
