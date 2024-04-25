using UnityEngine;

[CreateAssetMenu(fileName = "soEnemy", menuName = "ScriptableObjects/SOEnemy")]
public class SOEnemy : ScriptableObject
{ 
	public GameObject enemyPrefab;
	public float rate;
	public int damage;
	public int energy;
	public int maxHP;

	public void DeepCopy(SOEnemy copySource)
	{
		enemyPrefab = copySource.enemyPrefab;
		rate = copySource.rate;
		damage = copySource.damage;
		energy = copySource.energy;
		maxHP = copySource.maxHP;
	}
}
