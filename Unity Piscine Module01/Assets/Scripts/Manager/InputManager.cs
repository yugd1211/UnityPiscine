using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
	[Header("Inspector Allocate")]
	public PlayerController[] players; 
	public CinemachineVirtualCamera[] cameras;
	
	private int _currCharacter = -1;
	private GameManager _gameManager;
	private Vector3 _nextVec;

	private void Start()
	{
		_gameManager = GameManager.Instance;
		players = _gameManager.Players;
		cameras = new CinemachineVirtualCamera[_gameManager.Players.Length];
		for (int i = 0; i < _gameManager.Players.Length; i++)
		{
			cameras[i] = _gameManager.Players[i].GetComponentInChildren<CinemachineVirtualCamera>();
		}
	}
	public void OnMove(InputAction.CallbackContext context)
	{
		if (!_gameManager.isLive)
			return;
		if (_currCharacter < 0)
			return;
		if (!context.performed)
		{
			players[_currCharacter].Move(Vector3.zero, false);
			return;
		}
		Vector2 inputVec = context.ReadValue<Vector2>();
		players[_currCharacter].Move(new Vector3(inputVec.x, 0, 0), true);
	}
	public void OnJump(InputAction.CallbackContext context)
	{
		if (!_gameManager.isLive)
			return;
		if (_currCharacter < 0)
			return;
		if (!context.performed)
			return;
		players[_currCharacter].Jump();
	}
	public void OnChange(InputAction.CallbackContext context)
	{
		if (!_gameManager.isLive)
			return;
		if (!context.performed)
			return;
		int next = int.Parse(context.control.name);
		if (next > players.Length)
			return;
		_currCharacter = next - 1;
		foreach (var camera in cameras)
		{
			camera.Priority = 1;
		}
		cameras[_currCharacter].Priority = 2;
	}
	public void OnReset(InputAction.CallbackContext context)
	{
		if (!_gameManager.isLive)
			return;
		if (!context.performed)
			return;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void OnNextStage(InputAction.CallbackContext context)
	{
		if (_gameManager.isLive)
			return;
		if (!context.performed)
			return;
		_gameManager.NextStage();
	}
}