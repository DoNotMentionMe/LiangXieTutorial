using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    public class OnDelaySeconds : MonoBehaviour
    {
        [SerializeField] float Seconds = 1;

        public UnityEvent OnDelayFinish = new UnityEvent();

        IEnumerator Start()
        {
            yield return new WaitForSeconds(Seconds);

            OnDelayFinish?.Invoke();
        }
    }

}
