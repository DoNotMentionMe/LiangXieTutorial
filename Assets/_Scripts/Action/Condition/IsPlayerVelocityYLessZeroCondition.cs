using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    public class IsPlayerVelocityYLessZeroCondition : MonoBehaviour
    {
        [SerializeField] UnityEvent IsLess = new UnityEvent();

        public void Execute()
        {
            if (GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity.y <= 0.1f)
            {
                IsLess?.Invoke();
            }
        }
    }
}