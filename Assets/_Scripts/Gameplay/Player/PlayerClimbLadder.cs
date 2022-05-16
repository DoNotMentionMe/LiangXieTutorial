using System;
using UnityEngine;

namespace HYH
{
    public class PlayerClimbLadder : MonoBehaviour
    {
        public enum States
        {
            NotClimb,
            Climb
        }

        private bool mCanClimb = false;

        private FSM<States> mFSM = new FSM<States>();

        private void Awake()
        {
            var playerMovement = GetComponent<PlayerMovement>();
            var rigidbody = GetComponent<Rigidbody2D>();
            var currentGravity = rigidbody.gravityScale;

            mFSM.State(States.NotClimb)
                .OnEnter(() =>
                {
                    playerMovement.enabled = true;
                    rigidbody.gravityScale = currentGravity;
                })
                .OnUpdate(() =>
                {
                    if (mCanClimb && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D)))
                    {
                        mFSM.ChangeState(States.Climb);
                    }
                });

            mFSM.State(States.Climb)
                .OnEnter(() =>
                {
                    playerMovement.enabled = false;
                    rigidbody.gravityScale = 0;
                })
                .OnUpdate(() =>
                {
                    var verticalMovement = Input.GetAxis("Vertical");
                    var horizontalMovement = Input.GetAxis("Horizontal");

                    rigidbody.velocity = new Vector2(horizontalMovement, verticalMovement) * 5;
                });

            mFSM.StartState(States.NotClimb);
        }

        private void Update()
        {
            mFSM.Update();
        }

        private void OnDestroy()
        {
            mFSM.Clear();
            mFSM = null;
        }

        public void CanClimb()
        {
            mCanClimb = true;
        }

        public void CantClimb()
        {
            mCanClimb = false;
            mFSM.ChangeState(States.NotClimb);
        }
    }
}