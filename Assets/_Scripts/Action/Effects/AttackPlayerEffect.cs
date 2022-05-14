using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class AttackPlayerEffect : MonoBehaviour
    {
        [SerializeField] int HitterVeclocityX = 7;
        [SerializeField] int HitterVeclocityY = 15;

        public void Execute()
        {
            var player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerHit>().Hit();

            AttackPhysicsEffect(this.transform, player.transform);
        }

        void AttackPhysicsEffect(Transform attacker, Transform hitter)
        {
            var direction = hitter.position.x - attacker.position.x;
            var directionNormal = Mathf.Sign(direction);
            hitter.GetComponent<Rigidbody2D>().velocity = new Vector2(directionNormal * HitterVeclocityX, HitterVeclocityY);
        }
    }
}
