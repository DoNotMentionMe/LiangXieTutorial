using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    public class OnStart : MonoBehaviour
    {
        [SerializeField] UnityEvent OnStartEvent = new UnityEvent();

        [SerializeField] bool CallOnceInGlobal = false;

        private static bool mCalled = false;

        private void Start()
        {
            if (CallOnceInGlobal)
            {
                if (!mCalled)
                {
                    OnStartEvent?.Invoke();
                    mCalled = true;
                }
            }
            else
            {
                OnStartEvent?.Invoke();
            }
        }
    }
}