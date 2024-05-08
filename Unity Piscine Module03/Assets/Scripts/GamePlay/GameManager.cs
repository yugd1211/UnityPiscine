using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
	[SerializeField] private int kill;
	[SerializeField] private int energy;
	private int _energyConsumed;
	public Timer timer;
	public int HP => baseCamp.HP.currentHp;
	public BaseCamp baseCamp;
	public int initEnergy;
	public int endTime;
	public int Energy 
	{ 
		get => energy;
		private set => energy = value;
	}
	
	private void Awake()
	{
		energy = initEnergy;
	}

	private void Start()
	{
		baseCamp = FindObjectOfType<BaseCamp>();
	}

	private void GameOver()
	{
		PlayerPrefs.SetInt("kill", kill);
		PlayerPrefs.SetInt("energy", _energyConsumed);
		PlayerPrefs.SetInt("hp", HP);
		PlayerPrefs.SetInt("time", (int)timer.time);
		PlayerPrefs.SetString("currentScene", SceneManager.GetActiveScene().name);
		CalculateRanking();
		SceneManager.LoadScene("Score");
	}

	private void CalculateRanking()
	{
		int rank = 0;
		if (HP > 0)
		{
			if (kill > 100)
				rank++;
			if (kill > 200)
				rank++;
			if (HP == baseCamp.HP.maxHP)
				rank++;
			if (_energyConsumed < 200)
				rank++;
			if (energy < 100)
				rank++;
		}
		PlayerPrefs.SetInt("rank", rank);
	}

	private void Update()
	{
		if (timer.time >= endTime)
			GameOver();
		if (HP <= 0)
			GameOver();
	}

	public void Pause()
	{
		timer.timeScale = 0;
	}
	
	public void Resume()
	{
		timer.timeScale = 1;
	}

	public void IncrementEnergy(int amount = 1)
	{
		energy += amount;
	}
	
	public void IncrementKill()
	{
		kill++;
	}
	
	public bool DecrementEnergy(int amount)
	{
		if (energy < amount)
			return false;
		energy -= amount;
		_energyConsumed += amount;
		return true;
	}
}

// Singleton
public partial class GameManager
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance)
				return _instance;
			_instance = FindObjectOfType<GameManager>();
			if (_instance)
				return _instance;
			GameObject obj = new GameObject("GameManager");
			_instance = obj.GetComponent<GameManager>();
			return _instance;
		}
	}
}
