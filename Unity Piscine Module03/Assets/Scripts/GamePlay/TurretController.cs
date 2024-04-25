using TMPro;
using UnityEngine;

public class TurretController : MonoBehaviour
{ 
	public SOTurret turret;
	public bool isReady;
	private PoolManager _pool;
	private Scanner _scanner;
	private Spawner _spawner;
	private float _timer;
	

	private void Awake()
	{
		_scanner = GetComponent<Scanner>();
		_spawner = GetComponent<Spawner>();
		isReady = false;
	}
	
	private void Start()
	{
		_pool = PoolManager.Instance;
		_pool.CreatePool<Bullet>(turret.bulletPrefab, 1);
	}

	private void FixedUpdate()
	{
		if (!isReady)
			return;
		_timer += Time.fixedDeltaTime;
		if (_timer < turret.rate / 2)
			return;
		_timer = 0;
		if (_scanner.nearestTarget == null)
			return;
		Bullet bullet = _spawner.Spawn<Bullet>();
		bullet.Init(turret.damage, 
			_scanner.scanRange, 
			_scanner.nearestTarget);
		bullet.gameObject.SetActive(true);
	}

}
