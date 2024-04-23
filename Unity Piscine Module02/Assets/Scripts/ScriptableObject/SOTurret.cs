using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObjects/SOTurret")]
public class SOTurret : ScriptableObject
{ 
	public GameObject bulletPrefab;
	public float rate;
	public float damage;
}
