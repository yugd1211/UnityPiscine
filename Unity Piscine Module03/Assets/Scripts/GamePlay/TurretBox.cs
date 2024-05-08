using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretBox : MonoBehaviour
{
	public Color fullColor;
	private SpriteRenderer _spriter; 
	public GameObject turret;

	private void Awake()
	{
		_spriter = GetComponent<SpriteRenderer>();
	}
	
	public void DisplayTurret(GameObject turretPrefab)
	{
		turret = Instantiate(turretPrefab, transform.position, quaternion.identity, transform);
		turret.GetComponent<Scanner>().DisplayScanRange();
		_spriter.color = fullColor;
	}

	public void ConfirmTurretPlacement()
	{
		gameObject.layer = 0;
		turret.GetComponent<TurretController>().isReady = true;
		turret.GetComponent<Scanner>().HideScanRange();
	}
	
	public void DestroyTurret()
	{
		gameObject.layer = 8;
		_spriter.color = Color.white;
		Destroy(turret);
	}
}
