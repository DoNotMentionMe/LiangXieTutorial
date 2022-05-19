using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float HorizontalMovementSpeed;
        [SerializeField] float jumpForce;

        private float mHorizontalInput;

        [Header("==== Jump ====")]
        [HideInInspector] public JumpStates JumpState;
        [SerializeField] Trigger2D groundTrigger;
        [SerializeField] float GravityMultiplier = 2f;
        [SerializeField] float FallMultiplier = 1f;
        [SerializeField] float MinJumpTime = 0.2f;
        [SerializeField] float MaxJumpTime = 0.5f;

        private float mCurrentJumpTime = 0;
        private int CurrentJumpCount = 0;
        private bool mJumpPressed = false;
        bool mCanJump =>
            CurrentJumpCount == 0 && Time.frameCount - LeaveGroundFrame <= 10 ||
            CurrentJumpCount == 0 && groundTrigger.Triggered ||
            CurrentJumpCount > 0 && mDoubleJumpRule.Unlocked && CurrentJumpCount < 2;
        private IBonfireRule mDoubleJumpRule;

        [Header("==== Event ====")]
        [SerializeField] UnityEvent OnLand;
        [SerializeField] UnityEvent OnJump;

        private Rigidbody2D mRigidbody2D;
        private IInputSystem mInputSystem;

        private int LeaveGroundFrame;
        private int JumpPressFrameCount = -1000;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            groundTrigger.OnTriggerEnter.AddListener(() =>
            {
                CurrentJumpCount = 0;

                //落地时判断是否缓存了输入
                if (Time.frameCount - JumpPressFrameCount <= 10)
                {
                    //触发起跳
                    if (mInputSystem.Jump)
                        JumpStart();
                }
            });

            groundTrigger.OnTriggerExit.AddListener(() =>
            {
                //记录主角离开地面时的帧数
                LeaveGroundFrame = Time.frameCount;
            });

            var BonfireSystem = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>();
            mDoubleJumpRule = BonfireSystem.GetRuleByKey(nameof(DoubleJumpRule));

            mInputSystem = ApplePlatformer2D.Interface.GetSystem<IInputSystem>();
        }

        private void OnDestroy()
        {
            mInputSystem = null;
            mDoubleJumpRule = null;
        }

        private void JumpStart()
        {
            mJumpPressed = true;
            OnJump?.Invoke();
            CurrentJumpCount++;

            if (JumpState == JumpStates.NotJump)
            {
                JumpState = JumpStates.MinJump;
                mCurrentJumpTime = 0;
            }
        }

        private void Update()
        {
            if (ApplePlatformer2D.IsGameOver) return;

            mHorizontalInput = mInputSystem.HorizontalInput;

            if (mHorizontalInput * transform.localScale.x < 0)
            {
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }

            if (mInputSystem.JumpDown)
            {
                //记录按下跳跃时的帧数
                JumpPressFrameCount = Time.frameCount;
            }

            if (mInputSystem.JumpDown && mCanJump)
            {
                JumpStart();
            }

            if (mInputSystem.JumpUp)
            {
                mJumpPressed = false;
            }

            if (JumpState != JumpStates.NotJump)
                mCurrentJumpTime += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            //Jump
            if (JumpState == JumpStates.MinJump)
            {
                if (mCurrentJumpTime >= MinJumpTime)
                {
                    JumpState = JumpStates.ControlJump;
                }

                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, jumpForce);
            }
            if (JumpState == JumpStates.ControlJump)
            {
                if (!mJumpPressed || mJumpPressed && mCurrentJumpTime >= MaxJumpTime)
                {
                    JumpState = JumpStates.NotJump;
                }

                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, jumpForce);
            }

            if (mRigidbody2D.velocity.y > 0 && JumpState != JumpStates.NotJump)
            {
                var progress = mCurrentJumpTime / MaxJumpTime;
                float JumpGravityMultiplier = GravityMultiplier;
                if (progress > 0.5f)
                {
                    JumpGravityMultiplier = GravityMultiplier * (1 - progress);
                }
                mRigidbody2D.velocity += Physics2D.gravity * JumpGravityMultiplier * Time.fixedDeltaTime;
            }
            else if (mRigidbody2D.velocity.y < 0)
            {
                mRigidbody2D.velocity += Physics2D.gravity * FallMultiplier * Time.fixedDeltaTime;
            }

            //Move
            mRigidbody2D.velocity = new Vector2(HorizontalMovementSpeed * mHorizontalInput, mRigidbody2D.velocity.y);
        }

        public enum JumpStates
        {
            NotJump,
            MinJump,
            ControlJump,
        }
    }


}