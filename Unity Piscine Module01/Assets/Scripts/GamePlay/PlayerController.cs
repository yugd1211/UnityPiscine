using System.Collections;
using UnityEngine;

public enum Character
{
    Thomas,
    John,
    Claire,
}

public class PlayerController : MonoBehaviour
{
    [Header("Inspector Allocate")]
    public Character me;
    
    [Header("Player Info")]
    public int moveSpeed;
    
    private Rigidbody _rigid;
    private Coroutine _currCoroutine; 
    public bool isJumping;
    public bool isDead;
    private GameManager _gameManager;
    private readonly WaitForFixedUpdate _updateCycle = new WaitForFixedUpdate();

    private void Awake()    
    {
        _rigid = GetComponent<Rigidbody>();
        isJumping = false;
        _currCoroutine = null;
        isDead = false;
    }
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= -11) 
            Die();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!_gameManager.isLive)
            return;
        if (other.transform.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
    }

    public void Jump()
    {
        if (!_gameManager.isLive)
            return;
        if (isJumping)
            return;
        _rigid.velocity = new Vector3(_rigid.velocity.x, moveSpeed, 0);
        isJumping = true;
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
