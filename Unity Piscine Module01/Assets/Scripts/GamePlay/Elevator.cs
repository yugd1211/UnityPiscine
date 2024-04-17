using UnityEngine;


public class Elevator : MonoBehaviour
{
	public GameObject Players;

	public float maxHeight = 10f;
	
	public float speed = 2f;
	public float minHeight = 0f;

	private bool isMovingUp = true;

	private void Awake()
	{
		minHeight = transform.position.y;
	}
	private void FixedUpdate()
	{
		float moveDirection = isMovingUp ? 1f : -1f;
		float moveDistance = speed * Time.deltaTime * moveDirection;

		transform.Translate(0f, moveDistance, 0f);

		float currentHeight = transform.position.y;

		if (currentHeight >= minHeight + maxHeight && isMovingUp)
		{
			isMovingUp = false;
		}
		else if (currentHeight <= minHeight && !isMovingUp)
		{
			isMovingUp = true;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.transform.parent = transform;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.transform.parent = Players.transform;
		}
	}

}
