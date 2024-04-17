using UnityEngine;

public class Teleporter : MonoBehaviour
{
	public GameObject In;
	public GameObject Out;

	public void Teleport(Transform trans)
	{
		trans.position = Out.transform.position;
	}
}
