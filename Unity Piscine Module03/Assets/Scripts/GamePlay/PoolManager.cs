using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public partial class PoolManager : MonoBehaviour
{
    private readonly Dictionary<Type, object> _pools = new Dictionary<Type, object>();
    public void CreatePool<T>(GameObject prefab, int initialSize) where T : Component
    {
        if (_pools.ContainsKey(typeof(T)))
        {
            return;
        }

        ObjectPool<T> newPool = new ObjectPool<T>(prefab, initialSize, this.transform);
        _pools.Add(typeof(T), newPool);
    }

    public T Get<T>() where T : Component
    {
        if (_pools.TryGetValue(typeof(T), out object pool))
            return ((ObjectPool<T>)pool).Get();
        Debug.Log($"No pool type = {typeof(T)}.");
        return null;
    }

    public void AllDisable()
    {
        foreach (KeyValuePair<Type, object> poolEntry in _pools)
        {
            MethodInfo disableAllMethod = poolEntry.Value.GetType().GetMethod("DisableAll");
            if (disableAllMethod != null)
            {
                disableAllMethod.Invoke(poolEntry.Value, null);
            }
        }
    }
    
}

public partial class PoolManager
{
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if (_instance)
                return _instance;
            _instance = FindObjectOfType<PoolManager>();
            if (_instance)
                return _instance;
            GameObject obj = new GameObject("PoolManager");
            obj.AddComponent<PoolManager>();
            _instance = obj.GetComponent<PoolManager>();
            return _instance;
        }
    }
}

public class ObjectPool<T> where T : Component
{
    private readonly GameObject _prefab;
    private readonly Transform _parentTransform;
    private readonly List<T> _pool;
    private int _currPos;

    public ObjectPool(GameObject prefab, int initialSize, Transform parentTransform)
    {
        _currPos = 0;
        _prefab = prefab;
        _parentTransform = parentTransform;
        _pool = new List<T>();

        for (int i = 0; i < initialSize; i++)
        {
            AddObject();
        }
    }

    public T Get()
    {
        if (_currPos == _pool.Count)
            _currPos = 0;
        for (; _currPos < _pool.Count; _currPos++)
        {
            if (_pool[_currPos].gameObject.activeSelf)
                continue;
            return _pool[_currPos++];
        }
        AddObject();
        return _pool[_currPos++];
    }

    private void AddObject()
    {
        T newObject = GameObject.Instantiate(_prefab, _parentTransform).GetComponent<T>();
        _pool.Add(newObject);
        newObject.gameObject.SetActive(false);
    }
    
    public void DisableAll()
    {
        foreach (T item in _pool)
        {
            item.gameObject.SetActive(false);
        }
    }
}