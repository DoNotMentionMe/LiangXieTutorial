using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SetTargetFrameRate : MonoBehaviour
    {
        [SerializeField] int FPS = 60;

        public void Execute()
        {
            Application.targetFrameRate = FPS;
        }
    }
}