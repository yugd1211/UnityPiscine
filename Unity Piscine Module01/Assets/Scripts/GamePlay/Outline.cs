using UnityEngine;
using UnityEngine.Serialization;

public class Outline : MonoBehaviour
{
	public Character target; 
	public bool isAligned;

	private void Awake()
	{
		isAligned = false;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player") || other.gameObject.GetComponent<PlayerController>().me != target)
			return;
		isAligned = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Player") || other.gameObject.GetComponent<PlayerController>().me != target)
			return;
		isAligned = false;
	}
	
}
