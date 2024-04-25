using UnityEngine;

public class BaseCamp : MonoBehaviour
{
	public HP HP;

	private void Awake()
	{
		HP = GetComponent<HP>();
	}
}
