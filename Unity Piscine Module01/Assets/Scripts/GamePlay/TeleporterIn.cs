using UnityEngine;

public class TeleporterIn : MonoBehaviour
{
	public Teleporter parent;
	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player"))
			return;
		parent.Teleport(other.transform);
	}
}
