using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class BgScroll : MonoBehaviour
    {
        /// <summary>
        /// 偏移值的比率，可以通过这个数值来控制背景的滚动速度
        /// </summary>
        public float Ratio = 1.1f;

        /// <summary>
        /// 背景滚动一般依据相机来驱动
        /// </summary>
        public Camera Camera;

        /// <summary>
        /// 要控制的材质
        /// </summary>
        private Material mMaterial;

        private void Awake()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            mMaterial = spriteRenderer.material;
        }

        private void Update()
        {
            var position = Camera.transform.position;

            mMaterial.SetVector("OffsetXY", new Vector4(position.x * Ratio, position.y * Ratio, 0, 0));
        }
    }
}