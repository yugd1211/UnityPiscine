using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHUD : MonoBehaviour
{
	
	public enum HUDType
	{
		HP,
		Energy,
	}

	private TextMeshProUGUI _text;
	private GameManager _gameManager;
	public HUDType type;

	private void Awake()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		_gameManager = GameManager.Instance;
	}

	private void Update()
	{
		if (type == HUDType.HP)
		{
			_text.text = "체력 : " + _gameManager.HP;
		}
		else if (type == HUDType.Energy)
		{
			_text.text = "에너지 : " + _gameManager.Energy;
		}
	}
}
