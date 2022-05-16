using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SetSpriteColor : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Color color;

        public void Execute()
        {
            spriteRenderer.color = color;
        }
    }
}
