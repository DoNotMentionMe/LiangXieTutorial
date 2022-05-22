using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    public class PlayerHit : MonoBehaviour
    {
        public UnityEvent OnHit = new UnityEvent();

        private float mLastHitTime = 0;

        public bool CanHit => Time.time - mLastHitTime > 1f;

        private void Awake()
        {
            mLastHitTime = Time.time;
        }
        public void Hit()
        {
            if (CanHit)
            {
                OnHit?.Invoke();
                mLastHitTime = Time.time;
            }
        }
    }
}