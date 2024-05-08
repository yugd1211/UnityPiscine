using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
	public int timeScale;
	public float time;
	public TextMeshProUGUI text;
	private int prevTime;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	public void GameOneSpeed()
	{
		timeScale = 1;
	}
	public void GamePause()
	{
		prevTime = timeScale; 
		timeScale = 0;
	}

	public void GameResume()
	{
		timeScale = prevTime;
		prevTime = 0;
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
