using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum Character
{
    Thomas,
    Chris,
    John,
    Claire,
    Laura,
    James,
    Sarah,
}

public class PlayerController : MonoBehaviour
{
    [Header("Inspector Allocate")]
    public Character me;
    
    [Header("Player Info")]
    public int moveSpeed;
    
    private Rigidbody _rigid;
    private Coroutine _currCoroutine;
    private bool _isJumping;
    private GameManager _gameManager;
    private readonly WaitForFixedUpdate _updateCycle = new WaitForFixedUpdate();

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _isJumping = false;
        _currCoroutine = null;
    }
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_gameManager.isLive)
            return;
        if (other.transform.CompareTag("Floor"))
        {
            
            _isJumping = false;
        }
    }

    public void Jump()
    {
        if (!_gameManager.isLive)
            return;
        if (_isJumping)
            return;
        _rigid.velocity = new Vector3(_rigid.velocity.x, moveSpeed, 0);
        _isJumping = true;
    }
    
    public void Move(Vector3 input, bool isOn)
    {
        if (!_gameManager.isLive)
            return;
        if (isOn)
        {
            if (_currCoroutine != null)
            {
                StopCoroutine(_currCoroutine);
            }
            _currCoroutine = StartCoroutine(PressCoroutine(input));
        }
        else
        {
            if (_currCoroutine == null)
                return;
            StopCoroutine(_currCoroutine);
            _currCoroutine = null;
        }
    }

    private IEnumerator PressCoroutine(Vector3 input)
    {
        while (true)
        {
            _rigid.velocity = new Vector3(input.x * moveSpeed, _rigid.velocity.y, 0); 
            yield return _updateCycle;
        }
    }
}
