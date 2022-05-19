using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SimpleBullet : MonoBehaviour
    {
        [SerializeField] float bulletLiveTime = 5f;

        private WaitForSeconds waitForBulletLiveTime;

        private void Awake()
        {
            waitForBulletLiveTime = new WaitForSeconds(bulletLiveTime);
        }

        private IEnumerator Start()
        {
            var rigidBody2D = GetComponent<Rigidbody2D>();
            var player = GameObject.FindWithTag("Player");
            rigidBody2D.velocity = Vector2.right * 10 * player.transform.localScale.x;

            yield return waitForBulletLiveTime;

            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var enemyLayer = LayerMask.GetMask("Enemy");
            var groundLayer = LayerMask.GetMask("Ground");

            if (LayerMaskUtility.Contains(enemyLayer, col.collider.gameObject.layer))
            {
                col.collider.GetComponent<CharaterHit>().Hit();
                Destroy(gameObject);
            }
            else if (LayerMaskUtility.Contains(groundLayer, col.collider.gameObject.layer))
            {
                Destroy(gameObject);
            }
        }
    }
}