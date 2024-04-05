using Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameManager : Singleton<GameManager>
{
	public bool isLive;
	public int currStage;
	public GameObject[] Players;
	public Outline[] Outlines;
	public GameObject uiVictory;
	public GameObject displayKeysParent;
	public GameObject displayKeyPrefab;

	protected override void AwakeInit()
	{
		Debug.Log(SceneManager.sceneCount);
		isLive = true;
		currStage = 0;
	}
	
	private void Start()
	{
		for (int i = 0; i < Players.Length; i++)
		{
			GameObject key = Instantiate(displayKeyPrefab, displayKeysParent.transform, true);
			Image image = key.GetComponent<Image>(); 
			TextMeshProUGUI text = key.GetComponentInChildren<TextMeshProUGUI>(); 
			image.color = text.color = Players[i].GetComponent<MeshRenderer>().material.color;
			text.text = "Alpha " + (i + 1);
		}
	}
	private void FixedUpdate()
	{
		bool exit = true;
		foreach (Outline t in Outlines)
		{
			if (!t.isAligned) exit = false;
		}
		if (!exit)
			return;
		Victory();
	}

	private void Victory()
	{
		uiVictory.SetActive(true);
		Time.timeScale = 0;
		isLive = false;
	}

	public void nextStage()
	{
		if (SceneManager.sceneCount < currStage)
			SceneManager.LoadScene(currStage++);
		else
			Debug.Log("GAME END");
	}
}
