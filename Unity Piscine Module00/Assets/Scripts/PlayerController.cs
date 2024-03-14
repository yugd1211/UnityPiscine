using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigid;
    public float moveSpeed = 2f;
    private Coroutine _currCoroutine = null;
    private bool _isMoving = false;
    private bool _isJumping = false;
    private WaitForFixedUpdate _updateCycle;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _updateCycle = new WaitForFixedUpdate();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("PathWay"))
        {
            _isJumping = false;
        }
        if (other.transform.CompareTag("Floor"))
        {
            Debug.Log("Game Over!!!!!");
            gameObject.SetActive(false);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector2>();
        if (context.performed)
            Move(input, true);
        else
            Move(input, false);
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || _isJumping)
            return;
        _isJumping = true;
        _rigid.AddForce(new Vector3(0, 700, 0));
    }


    public void Move(Vector3 input, bool isOn)
    {
        if (isOn)
        {
            if (_currCoroutine != null)
            {
                StopCoroutine(_currCoroutine);
            }
            _currCoroutine = StartCoroutine(PressCoroutine(input));
            _isMoving = true;
        }
        else
        {
            if (_currCoroutine != null && _isMoving)
            {
                StopCoroutine(_currCoroutine);
                _currCoroutine = null;
                _isMoving = false;
            }
        }
    }

    private IEnumerator PressCoroutine(Vector3 input)
    {
        while (true)
        {
            Vector3 convert = new Vector3(input.x, 0, input.y) * (100 * moveSpeed);
            _rigid.velocity = new Vector3(0, _rigid.velocity.y, 0);
            _rigid.AddForce(convert);
            yield return _updateCycle;
        }
    }


}

