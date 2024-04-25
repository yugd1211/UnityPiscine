using UnityEngine;

public class EnemyController : MonoBehaviour
{ 
	public int bulletSpeed;
	public HP HP;
	public Transform target;
	public Transform[] targets;
	public SOEnemy soEnemy;
	
	private int _pathCount;
	private void Awake()
	{
		HP = GetComponent<HP>();
		HP.DeadAction += Dead;
		HP.DeadAction += () =>
		{
			GameManager.Instance.IncrementEnergy(soEnemy.energy);
		};
	}

	public void Init(SOEnemy so, Transform[] targets, Transform target, int speed)
	{
		soEnemy = so;
		this.targets = targets;
		this.target = target;
		bulletSpeed = speed;
		HP.maxHP = so.maxHP;
		HP.Init();
	}

	private void OnEnable()
	{
		_pathCount = 0;
	}

	private void Dead()
	{
		GameManager.Instance.IncrementKill();
		gameObject.SetActive(false);
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!other.transform.CompareTag("BaseCamp"))
			return;
		other.transform.GetComponent<BaseCamp>().HP.Decrement();
		gameObject.SetActive(false);
	}
	
	private void FixedUpdate()
	{
		if (targets.Length != 0)
		{
			transform.position = Vector2.MoveTowards(transform.position, targets[_pathCount].position, Time.fixedDeltaTime * bulletSpeed);
			if (transform.position == targets[_pathCount].position)
				_pathCount++;
		}
		else if (target)
		{
			Vector2 dir = (target.position - transform.position).normalized;
			gameObject.transform.Translate(dir * Time.fixedDeltaTime);
		}
	}
	
}
