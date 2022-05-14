using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SetVelocity2D : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2d;

        public Vector2 vector2;
        
        public void Execute()
        {
            Rigidbody2d.velocity = vector2;
        }
    }
    
}
