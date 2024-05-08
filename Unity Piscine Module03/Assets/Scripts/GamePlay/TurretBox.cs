using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TurretBox : MonoBehaviour
{
	public Color fullColor;
	private SpriteRenderer _spriter;
	private GameObject _turret;

	private void Awake()
	{
		_spriter = GetComponent<SpriteRenderer>();
	}
	
	public void DisplayTurret(GameObject turretPrefab)
	{
		_turret = Instantiate(turretPrefab, transform.position, quaternion.identity, transform);
		_turret.GetComponent<Scanner>().DisplayScanRange();
		_spriter.color = fullColor;
	}

	public void ConfirmTurretPlacement()
	{
		gameObject.layer = 0;
		_turret.GetComponent<TurretController>().isReady = true;
		_turret.GetComponent<Scanner>().HideScanRange();
	}
	
	public void DestroyTurret()
	{
		gameObject.layer = 8;
		_spriter.color = Color.white;
		Destroy(_turret);
	}
}
