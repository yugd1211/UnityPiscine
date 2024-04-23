using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float damage;
	public Vector3 target;

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!other.transform.CompareTag("Enemy"))
			return;
		EnemyController enemy = other.transform.GetComponent<EnemyController>();
		enemy.HP.Decrement(damage);
		gameObject.SetActive(false);
	}
	private void FixedUpdate()
	{
		Vector3 transformPositionRounded = new Vector3(
			Mathf.Round(transform.position.x * 10f) / 10f,
			Mathf.Round(transform.position.y * 10f) / 10f,
			Mathf.Round(transform.position.z * 10f) / 10f);
		Vector3 targetRounded = new Vector3(
			Mathf.Round(target.x * 10f) / 10f,
			Mathf.Round(target.y * 10f) / 10f,
			Mathf.Round(target.z * 10f) / 10f);
		if (transformPositionRounded == targetRounded)
		{
			gameObject.SetActive(false);
		}
		// if (transform.position == target)
		// 	gameObject.SetActive(false);
		Vector2 dir = (target - transform.position).normalized;
		gameObject.transform.Translate(dir * Time.fixedDeltaTime);
	}
}
