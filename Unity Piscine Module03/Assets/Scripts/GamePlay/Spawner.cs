using System;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{ 
	public Transform spawnPos;
	private PoolManager _pool;
	private float _rate;

	private void Start()
	{
		_pool = PoolManager.Instance;
	}
	
	public T Spawn<T>() where T : Component
	{
		T obj = _pool.Get<T>();
		obj.transform.position = spawnPos.position;
		return obj;
	}
}
