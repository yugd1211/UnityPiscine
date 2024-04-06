using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Outline : MonoBehaviour
{
	public Character target;
	public bool isAligned
	{
		get => isAligned;
		set
		{
			isAligned = value;
			_gameManager.CheckOutlineAligned();
		}
	}

	private GameManager _gameManager;

	private void Awake()
	{
		isAligned = false;
	}
	private void Start()
	{
		_gameManager = GameManager.Instance;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player") || other.gameObject.GetComponent<PlayerController>().me != target)
			return;
		isAligned = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Player") || other.gameObject.GetComponent<PlayerController>().me != target)
			return;
		isAligned = false;
	}
	
}
