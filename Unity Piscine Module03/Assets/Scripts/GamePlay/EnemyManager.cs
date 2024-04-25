using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public int speed;
	public SOEnemy soEnemy;
	public Transform target;
	public Transform[] targets;

	public GameObject enemyHPUIPrefab;
	private Canvas _canvas; 
	private Spawner _spawner;
	private PoolManager _pool;
	private float _timer;
	private GameManager _gameManager;

	private void Awake()
	{
		_spawner = GetComponent<Spawner>();
		_canvas = FindObjectOfType<Canvas>();
		_gameManager = GameManager.Instance;
	}
	private void Start()
	{
		_pool = PoolManager.Instance;
		_pool.CreatePool<EnemyController>(soEnemy.enemyPrefab, 1);
	}

	private float[] _rateUpgrade = {0.2f, 0.3f, 0.4f, 0.5f};
	private int[] _hpUpgrade = {1, 2, 3, 5, 8};

	private void FixedUpdate()
	{
		_timer += Time.fixedDeltaTime;
		if (_timer < soEnemy.rate - _rateUpgrade[Math.Min((int)_timer / 30, _rateUpgrade.Length - 1)])
			return;
		_timer = 0;
		EnemyController enemy = _spawner.Spawn<EnemyController>();
		SOEnemy newEnemy = ScriptableObject.CreateInstance<SOEnemy>();
		newEnemy.DeepCopy(soEnemy);
		newEnemy.maxHP *= _hpUpgrade[Math.Min((int)_gameManager.timer.time / 30, _hpUpgrade.Length - 1)];
		enemy.Init(newEnemy, targets, target, speed);
		
		enemy.gameObject.SetActive(true);
		UIEnemyHP uiEnemyHp = Instantiate(enemyHPUIPrefab, _canvas.transform.GetChild(0)).GetComponent<UIEnemyHP>();
		uiEnemyHp.target = enemy;
	}
}
