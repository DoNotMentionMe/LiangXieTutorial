using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    //状态的控制
    public class SimpleEnemy : CharacterController
    {
        [SerializeField] Trigger2D GroundCheck;
        [SerializeField] Trigger2D ForwardCheck;
        [SerializeField] Trigger2D FallCheck;
        [SerializeField] Trigger2D AttackCheck;

        private void Awake()
        {
            var CharacterMovement = GetComponent<CharacterMovement>();

            CharacterMovement.enabled = false;

            GroundCheck.OnTriggerEnter.AddListener(() =>
            {
                //开启移动
                CharacterMovement.enabled = true;
            });

            GroundCheck.OnTriggerExit.AddListener(() =>
            {
                //关闭移动
                CharacterMovement.enabled = false;
            });

            ForwardCheck.OnTriggerEnter.AddListener(() =>
            {
                var Scale = transform.localScale;
                Scale.x *= -1;
                transform.localScale = Scale;
            });

            FallCheck.OnTriggerExit.AddListener(() =>
            {
                var Scale = transform.localScale;
                Scale.x *= -1;
                transform.localScale = Scale;
            });

            AttackCheck.OnTriggerEnterWithCollider.AddListener((collider) =>
            {
                collider.GetComponent<PlayerHit>().Hit();

                AttackPhysicsEffect(transform, collider.transform);
            });
        }

        [SerializeField] int HitterVeclocityX = 7;
        [SerializeField] int HitterVeclocityY = 15;

        void AttackPhysicsEffect(Transform attacker, Transform hitter)
        {
            var direction = hitter.position.x - attacker.position.x;
            var directionNormal = Mathf.Sign(direction);
            hitter.GetComponent<Rigidbody2D>().velocity = new Vector2(directionNormal * HitterVeclocityX, HitterVeclocityY);
        }
    }
}