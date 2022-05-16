using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    public class SimpleFollowEnemy : MonoBehaviour
    {
        public enum States
        {
            Idle,
            Warning,
            Following,
        }

        [SerializeField] float followSpeed = 7;
        [SerializeField] Trigger2D GroundCheck;
        [SerializeField] Trigger2D ForwardCheck;
        [SerializeField] Trigger2D FallCheck;
        [SerializeField] Trigger2D AttackCheck;
        [SerializeField] Trigger2D WarningCheck;
        [SerializeField] UnityEvent OnIdle = new UnityEvent();
        [SerializeField] UnityEvent OnWarning = new UnityEvent();
        [SerializeField] UnityEvent OnFollowing = new UnityEvent();

        private float stateDuration = 0;
        private FSM<States> mFSM = new FSM<States>();

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
                mFSM.ChangeState(States.Idle);
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

            WarningCheck.OnTriggerEnter.AddListener(() =>
            {
                mFSM.ChangeState(States.Warning);
            });

            WarningCheck.OnTriggerExit.AddListener(() =>
            {
                mFSM.ChangeState(States.Idle);
            });

            mFSM.State(States.Idle)
                .OnEnter(() =>
                {
                    OnIdle?.Invoke();
                    CharacterMovement.enabled = true;
                    CharacterMovement.HorizontalMovementSpeed = 3;
                });

            mFSM.State(States.Warning)
                .OnEnter(() =>
                {
                    OnWarning?.Invoke();
                    CharacterMovement.enabled = false;
                    stateDuration = 0;
                })
                .OnUpdate(() =>
                {
                    if (stateDuration > 2f)
                    {
                        mFSM.ChangeState(States.Following);
                    }
                    stateDuration += Time.deltaTime;
                });

            var playerGameObject = GameObject.FindWithTag("Player");

            mFSM.State(States.Following)
                .OnEnter(() =>
                {
                    OnFollowing?.Invoke();
                    CharacterMovement.enabled = true;
                    CharacterMovement.HorizontalMovementSpeed = followSpeed;
                })
                .OnFixedUpdate(() =>
                {
                    var playerPos = playerGameObject.transform.position;
                    var enemyPos = transform.position;
                    var moveDirection = playerPos - enemyPos;
                    if (moveDirection.x > 0)
                    {
                        var scale = transform.localScale;
                        scale.x = 1;
                        transform.localScale = scale;
                    }
                    else
                    {
                        var scale = transform.localScale;
                        scale.x = -1;
                        transform.localScale = scale;
                    }

                })
                .OnExit(() => { });

            mFSM.StartState(States.Idle);
        }

        private void Update()
        {
            mFSM.Update();
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