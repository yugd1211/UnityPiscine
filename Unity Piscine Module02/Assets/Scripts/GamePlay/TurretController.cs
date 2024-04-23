using TMPro;
using UnityEngine;

public class TurretController : MonoBehaviour
{ 
	public SOTurret turret;
	private PoolManager _pool;
	private Scanner _scanner;
	private Spawner _spawner;
	private float _timer;

	private void Awake()
	{
		_scanner = GetComponent<Scanner>();
		_spawner = GetComponent<Spawner>();
	}
	
	private void Start()
	{
		_pool = PoolManager.Instance;
		_pool.CreatePool<Bullet>(turret.bulletPrefab, 1);
	}

	private void FixedUpdate()
	{
		_timer += Time.fixedDeltaTime;
		if (_timer < turret.rate)
			return;
		_timer = 0;
		if (_scanner.nearestTarget == null)
			return;
		Bullet bullet = _spawner.Spawn<Bullet>();
		bullet.damage = turret.damage;
		bullet.target = _scanner.nearestTarget.position;
		bullet.gameObject.SetActive(true);
	}

}
