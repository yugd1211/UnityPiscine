using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObjects/SOTurret")]
public class SOTurret : ScriptableObject
{ 
	public GameObject turretPrefab;
	public GameObject bulletPrefab;
	public float rate;
	public int damage;
	public int energy;
}
