using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    public class CharacterHP : MonoBehaviour
    {
        [SerializeField] float HP = 1;
        [SerializeField] float MaxHP = 1;

        //对外提供死亡事件
        public UnityEvent OnDeath = new UnityEvent();
        //对外提供血量变更事件
        public UnityEvent<float, float, float> OnHPChanged = new UnityEvent<float, float, float>();

        public void MinusHP(float minusHP)
        {
            HP -= minusHP;

            if (HP <= 0)
            {
                HP = 0;
                OnDeath?.Invoke();
            }

            OnHPChanged?.Invoke(HP, MaxHP, -minusHP);
        }
    }
}