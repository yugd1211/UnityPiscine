using UnityEngine;

[CreateAssetMenu(fileName = "soEnemy", menuName = "ScriptableObjects/SOEnemy")]
public class SOEnemy : ScriptableObject
{ 
	public GameObject enemyPrefab;
	public float rate;
	public float damage;
}
