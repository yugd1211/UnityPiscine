using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public HP HP;
	public Transform target;
	private void Awake()
	{
		HP = GetComponent<HP>();
		HP.DeadAction += Dead;
	}

	private void Dead()
	{
		Debug.Log($"Enemy Dead {gameObject.name}");
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
		if (!target)
			return;
		Vector2 dir = (target.position - transform.position).normalized;
		gameObject.transform.Translate(dir * Time.fixedDeltaTime);
	}
	
}
