using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector3 initPosition;
	public float endDistance;
	public float speed;

	public void Init(float endDis)
	{
		endDistance = endDis;
		initPosition = transform.position;
	}
	public void Init(float endDis, float speed)
	{
		endDistance = endDis;
		this.speed = speed;
		initPosition = transform.position;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.CompareTag("Player"))
		{
			other.transform.GetComponent<PlayerController>().Die();
			Destroy(other.gameObject);
		}
		else if (other.transform.CompareTag("Pillar"))
			Destroy(gameObject);
	}

	private void FixedUpdate()
	{
		Vector3 position = transform.position;
		float x = Mathf.Abs(initPosition.x - position.x);
		float y = Mathf.Abs(initPosition.y - position.y);
		float z = Mathf.Abs(initPosition.z - position.z);
		if (x + y + z >= endDistance)
			Destroy(gameObject); 
		transform.Translate(Vector3.forward * (speed * Time.fixedDeltaTime));
	}
}
