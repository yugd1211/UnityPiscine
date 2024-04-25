using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int damage;
	public int speed;
	public Transform target;
	private bool isColliding; // 충돌 중인지를 확인하는 플래그

	private Vector3 _initPosition;

	public void Init(int damage, float range, Transform target)
	{
		this.damage = damage;
		this.target = target;
		_initPosition = transform.position;
	}
	

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (isColliding) return;

		if (!other.transform.CompareTag("Enemy"))
			return;

		isColliding = true;

		EnemyController enemy = other.transform.GetComponent<EnemyController>();
		enemy.HP.Decrement(damage);

		gameObject.SetActive(false);
	}
	
	private void FixedUpdate()
	{
		isColliding = false;
		if (transform.position == target.position)
		{
			gameObject.SetActive(false);
		}
		transform.Translate((target.position - transform.position) * (speed * Time.fixedDeltaTime));
	}
}
