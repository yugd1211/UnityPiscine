using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public SOEnemy soEnemy;
	public Transform target;
	private Spawner _spawner;

	private PoolManager _pool;
	private float _timer;

	private void Awake()
	{
		_spawner = GetComponent<Spawner>();
	}
	private void Start()
	{
		_pool = PoolManager.Instance;
		_pool.CreatePool<EnemyController>(soEnemy.enemyPrefab, 1);
	}

	private void FixedUpdate()
	{
		_timer += Time.fixedDeltaTime;
		if (_timer < soEnemy.rate)
			return;
		_timer = 0;
		EnemyController enemy = _spawner.Spawn<EnemyController>();
		enemy.target = target;
		// enemy.HP
		enemy.gameObject.SetActive(true);
	}
}
