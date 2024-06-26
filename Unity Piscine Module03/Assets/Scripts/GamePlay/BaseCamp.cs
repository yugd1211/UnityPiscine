using UnityEngine;

public class BaseCamp : MonoBehaviour
{
	public HP HP;

	private void Awake()
	{
		HP = GetComponent<HP>();
		HP.DeadAction += Pause;
	}

	private void Pause()
	{
		Time.timeScale = 0;
	}
}
