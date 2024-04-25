using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HP : MonoBehaviour
{
    public int maxHP = 1;
    public bool IsAlive => currentHp > 0;
    public Action DeadAction;
    public int currentHp { get; private set; }
    
    public void Increment()
    {
        currentHp = Mathf.Clamp(currentHp + 1, 0, maxHP);
    }
    
    public void Decrement(int damage = 1)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHP);
        if (currentHp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        DeadAction?.Invoke();
    }
    
    public void Die()
    {
        while (currentHp > 0) Decrement();
    }

    public void Init()
    {
        currentHp = maxHP;
    }

    // private void Awake()
    // {
    //     Init();
    // }

    private void OnEnable()
    {
        Init();
    }
}