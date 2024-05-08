using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
	public int timeScale;
	public float time;
	public TextMeshProUGUI text;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	public void GameOneSpeed()
	{
		timeScale = 1;
	}
	
	public void GameDoubleSpeed()
	{
		timeScale = 2;
	}

	private void Update()
	{
		Time.timeScale = timeScale;
		time += Time.deltaTime;
		text.text = (((int)time) / 60).ToString() + ":" + (((int)time) % 60).ToString();
	}
}
