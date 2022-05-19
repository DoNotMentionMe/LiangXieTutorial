using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SetPlayerVelocity2D : MonoBehaviour
    {
        public UnityEngine.Vector2 velocity;

        public void Execute()
        {
            GameObject.FindWithTag("Player")
                .GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}