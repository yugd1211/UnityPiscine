using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		if (!other.transform.CompareTag("Player"))
			return;
		other.transform.GetComponent<PlayerController>().Die();
	}
}
