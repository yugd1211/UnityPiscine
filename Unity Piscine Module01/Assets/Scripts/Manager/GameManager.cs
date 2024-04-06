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
	public int maxStage;
	public PlayerController[] Players;
	public Outline[] Outlines;
	public GameObject uiVictory;
	public GameObject displayKeysParent;
	public GameObject displayKeyPrefab;

	protected override void AwakeInit()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		Init();
	}
	private void Init()
	{
		Players = FindObjectsOfType<PlayerController>();
		uiVictory = FindObjectOfType<WinnerPanel>(true).gameObject;
		uiVictory.SetActive(false);
		isLive = true;
		Time.timeScale = 1;
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

	public void CheckOutlineAligned()
	{
		if (!isLive)
			return;
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
		Debug.Log("Victory!");
		uiVictory.SetActive(true);
		Time.timeScale = 0;
		isLive = false;
	}
	
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("OnSceneLoaded");
		Init();
		// SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void NextStage()
	{
		if (currStage >= maxStage)
		{
			SceneManager.LoadScene(currStage++);
		}
		else
		{
			Debug.Log(" 1stage");
			// Init();
			SceneManager.LoadScene(0);
		}
	}
}
