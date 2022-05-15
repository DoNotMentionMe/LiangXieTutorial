using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SimpleFlyEnemy : MonoBehaviour
    {
        [SerializeField] float MovementSpeed = 5;
        [SerializeField] Trigger2D WarningArea;
        [SerializeField] Trigger2D AttackArea;

        private FSM<States> mFSM = new FSM<States>();

        private Rigidbody2D mRigidbody2D;

        public enum States
        {
            Idle,
            FollowPlayer,
        }

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            var playerTransform = GameObject.FindWithTag("Player").transform;

            mFSM.State(States.Idle)
                .OnEnter(() =>
                {
                    mRigidbody2D.velocity = Vector2.zero;
                })
                .OnUpdate(() =>
                {

                });

            mFSM.State(States.FollowPlayer)
                .OnFixedUpdate(() =>
                {
                    var playerPos = playerTransform.position;
                    var enemyPos = transform.position;
                    var direction = (playerPos - enemyPos).normalized;
                    if (direction.x > 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    mRigidbody2D.velocity = direction * MovementSpeed;
                });

            mFSM.StartState(States.Idle);

            WarningArea.OnTriggerEnter.AddListener(() =>
            {
                mFSM.ChangeState(States.FollowPlayer);
            });

            WarningArea.OnTriggerExit.AddListener(() =>
            {
                mFSM.ChangeState(States.Idle);
            });

            AttackArea.OnTriggerEnter.AddListener(() =>
            {
                playerTransform.GetComponent<PlayerHit>().Hit();
            });
        }

        private void FixedUpdate()
        {
            mFSM.FixedUpdate();
        }

        private void OnDestroy()
        {
            mFSM.Clear();
            mFSM = null;
        }
    }
}