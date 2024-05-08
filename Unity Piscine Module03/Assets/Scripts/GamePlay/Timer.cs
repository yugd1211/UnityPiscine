using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
	public int timeScale;
	public float time;
	public TextMeshProUGUI text;
	private int _prevTime;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
		_prevTime = 1;
	}

	public void GameOneSpeed()
	{
		timeScale = 1;
	}
	public void GamePause()
	{
		_prevTime = timeScale; 
		timeScale = 0;
	}

	public void GameResume()
	{
		if (_prevTime == 0)
			_prevTime = 1;
		timeScale = _prevTime;
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
