using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class ScaleTimeEffect : MonoBehaviour
    {
        public float TimeScale = 0.2f;

        public float Duration = 0.2f;

        public void Execute()
        {
            StartCoroutine(nameof(DoExecute));
        }

        IEnumerator DoExecute()
        {
            Time.timeScale = TimeScale;

            yield return new WaitForSeconds(Duration);

            Time.timeScale = 1;
        }
    }
}