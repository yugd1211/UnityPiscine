using System;
using UnityEngine;

public class HP : MonoBehaviour
{
        public int maxHP = 1;
        public bool IsAlive => currentHp > 0;
        public Action DeadAction;
        public float currentHp { get; private set; }
        
        public void Increment()
        {
            currentHp = Mathf.Clamp(currentHp + 1, 0, maxHP);
        }
        
        public void Decrement(float damage = 1f)
        {
            currentHp = Mathf.Clamp(currentHp - damage, 0, maxHP);
            if (currentHp <= 0)
            {
                Dead();
            }
        }

        private void Dead()
        {
            DeadAction.Invoke();
        }
        
        public void Die()
        {
            while (currentHp > 0) Decrement();
        }

        void Awake()
        {
            currentHp = maxHP;
        }
}