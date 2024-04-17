using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Core.Singleton<GameManager>
{
	public bool isLive;
	public bool isDead;
	public PlayerController[] players;
	public Outline[] outlines;
	public GameObject uiVictory;
	public GameObject uiDefeat;
	public GameObject displayKeysParent;
	public GameObject displayKeyPrefab;

	protected override void AwakeInit()
	{
		Init();
	}
	private void Init()
	{
		players = FindObjectsOfType<PlayerController>();
		outlines = FindObjectsOfType<Outline>();
		uiVictory.SetActive(false);
		uiDefeat.SetActive(false);
		isLive = true;
		isDead = false;
		Time.timeScale = 1;
	}

	private void Start()
	{
		for (int i = 0; i < players.Length; i++)
		{
			GameObject key = Instantiate(displayKeyPrefab, displayKeysParent.transform, true);
			Image image = key.GetComponent<Image>(); 
			TextMeshProUGUI text = key.GetComponentInChildren<TextMeshProUGUI>(); 
			image.color = text.color = players[i].GetComponent<MeshRenderer>().material.color;
			text.text = "Alpha " + (i + 1);
		}
	}

	private void FixedUpdate()
	{
		foreach (PlayerController player in players)
		{
			if (!player.isDead)
				continue;
			Defeat();
		}
	}

	public void CheckOutlineAligned()
	{
		if (!isLive)
			return;
		bool exit = true;
		foreach (Outline t in outlines)
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
	private void Defeat()
	{
		uiDefeat.SetActive(true);
		Time.timeScale = 0;
		isDead = true;
		isLive = false;
	}

	public void NextStage()
	{
		if (SceneManager.GetActiveScene().name == "Stage1")
			SceneManager.LoadScene("Stage2");
		else if (SceneManager.GetActiveScene().name == "Stage2")
			SceneManager.LoadScene("Stage3");
		else if (SceneManager.GetActiveScene().name == "Stage3")
			SceneManager.LoadScene("Stage4");
		else if (SceneManager.GetActiveScene().name == "Stage4")
			SceneManager.LoadScene("Stage5");
		else
			SceneManager.LoadScene("Stage1");
	}
}
