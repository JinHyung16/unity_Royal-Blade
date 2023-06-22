using HughGeneric;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Hugh.PoolSystem
{
    public class PoolManager : Singleton<PoolManager>
    {
        protected override void OnAwake()
        {
            //base.OnAwake();
            Pooling();
        }

        private Dictionary<PoolableType, Stack<PoolObject>> poolDictionary = new Dictionary<PoolableType, Stack<PoolObject>>();

        /// <summary>
        /// Dictionary�� PoolabeType�� ������ ���� �ʱ�ȭ�ؼ� ������ Ȯ���صд�.
        /// </summary>
        private void Pooling()
        {
            foreach ( PoolableType type in Enum.GetValues(typeof(PoolableType)) )
            {
                poolDictionary.Add(type, new Stack<PoolObject>());
            }

        }

        private PoolObject CreatePoolObject(PoolableType tpye, string name)
        {
            GameObject obj = Resources.Load(PrefabPath(tpye) + name, typeof(GameObject)) as GameObject;

            if ( obj == null )
            {
                return null;
            }

            obj = Instantiate(obj);

            if ( obj.TryGetComponent<PoolObject>(out PoolObject poolObj) )
            {
                poolObj.Name = name;
                return poolObj;
            }
            else
            {
                poolObj = obj.AddComponent<PoolObject>();
                poolObj.Name = name;
                return poolObj;
            }
        }

        /// <summary>
        /// Pooling�� ������Ʈ�� ������ȭ ���� Resources ���� �� �����ص� ��ġ�� �о�´�.
        /// </summary>
        /// <param name="type"> pooling�� ������Ʈ Ÿ��</param>
        /// <returns> Resources���� �� �ش� prefab ��� ��ȯ</returns>
        private string PrefabPath(PoolableType type)
        {
            switch ( type )
            {
                case PoolableType.None:
                    break;
                case PoolableType.BasicCat:
                    return "Prefab/Enemy/";
                case PoolableType.RareCat:
                    return "Prefab/Enemy/";
            }
            return "Prefab/";
        }

        public GameObject GetPrefab(PoolableType type, string name)
        {

            return null;
        }

        public void DespawnObject(PoolableType _type, GameObject obj)
        {
            if ( obj.TryGetComponent<PoolObject>(out PoolObject poolObj) )
            {
            }
        }

    }

    public enum PoolableType
    {
        None = 0,

        BasicCat,
        RareCat,
    }
}
