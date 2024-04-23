using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public GameObject UIDead;
	public GameObject UIHP;

	private BaseCamp _baseCamp;

	private void Start()
	{
		_baseCamp = FindObjectOfType<BaseCamp>();
	}
	public void Dead()
	{
		UIDead.SetActive(true);
	}

	public void UIUpdate()
	{
		UIHP.GetComponent<TextMeshProUGUI>().text = $"HP : {_baseCamp.HP.currentHp}";
		if (!_baseCamp.HP.IsAlive)
			Dead();
	}

	private void FixedUpdate()
	{
		UIUpdate();
	}
}
