using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class AddRemainSeconds : MonoBehaviour
    {
        public void Execute(int seconds)
        {
            Bonfire.RemainSeconds += seconds;
        }
    }
}