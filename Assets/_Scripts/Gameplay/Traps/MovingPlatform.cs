using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class MovingPlatform : MonoBehaviour
    {
        public enum States
        {
            Stop,
            Moving,
        }

        [SerializeField] float MovementSPeed = 5f;
        [SerializeField] float StopSeconds = 2f;
        [SerializeField] Transform Pos1;
        [SerializeField] Transform Pos2;

        private FSM<States> FSM = new FSM<States>();

        private Vector3 mPos1;
        private Vector3 mPos2;

        private Vector3 mToPosition;

        private void Awake()
        {
            mPos1 = Pos1.position;
            mPos2 = Pos2.position;

            mToPosition = mPos2;

            var stopTime = Time.time;

            FSM.State(States.Stop)
                .OnEnter(() => { stopTime = Time.time; })
                .OnUpdate(() =>
                {
                    if (Time.time - stopTime >= StopSeconds)
                    {
                        FSM.ChangeState(States.Moving);
                    }
                });

            FSM.State(States.Moving)
                .OnEnter(() => { mToPosition = mToPosition == mPos2 ? mPos1 : mPos2; })
                .OnUpdate(() =>
                {
                    var currentPosition = transform.position;
                    var moveDirection = (mToPosition - currentPosition).normalized;
                    transform.Translate(moveDirection * MovementSPeed * Time.deltaTime);

                    if (Vector3.Distance(currentPosition, mToPosition) < 0.1f)
                    {
                        FSM.ChangeState(States.Stop);
                    }
                });

            FSM.StartState(States.Stop);
        }

        private void Update()
        {
            FSM.Update();
        }
        private void OnDestroy()
        {
            FSM.Clear();
            FSM = null;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.transform.SetParent(this.transform);
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.transform.SetParent(null);
            }
        }
    }
}
