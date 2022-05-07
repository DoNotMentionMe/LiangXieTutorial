using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float HorizontalMovementSpeed;
    public float jumpForce;


    [Header("==== Jump ====")]
    [SerializeField] Trigger2D groundTrigger;
    [SerializeField] float GravityMultiplier = 2f;
    [SerializeField] float FallMultiplier = 1f;
    [SerializeField] float MinJumpTime = 0.2f;
    [SerializeField] float MaxJumpTime = 0.5f;
    [Header("==== Event ====")]
    [SerializeField] UnityEvent OnLand;
    [SerializeField] UnityEvent OnJump;
    
    private float mHorizontalInput;
    private float mCurrentJumpTime = 0;
    private bool mJumpPressed = false;
    private Rigidbody2D mRigidbody2D;
    private JumpStates JumpState;

    private void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mHorizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.K) && groundTrigger.Triggered)
        {
            mJumpPressed = true;
            OnJump?.Invoke();
            
            if(JumpState == JumpStates.NotJump)
            {
                JumpState = JumpStates.MinJump;
                mCurrentJumpTime = 0;
            }
        }

        if(Input.GetKeyUp(KeyCode.K))
        {
            mJumpPressed = false;
        }

        if(JumpState != JumpStates.NotJump)
            mCurrentJumpTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //Jump
        if(JumpState == JumpStates.MinJump)
        {
            if(mCurrentJumpTime >= MinJumpTime)
            {
                JumpState = JumpStates.ControlJump;
            }

            mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, jumpForce);
        }
        if(JumpState == JumpStates.ControlJump)
        {
            if(!mJumpPressed || mJumpPressed && mCurrentJumpTime >= MaxJumpTime)
            {
                JumpState = JumpStates.NotJump;
            }

            mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, jumpForce);
        }

        if(mRigidbody2D.velocity.y > 0 && JumpState != JumpStates.NotJump)
        {
            var progress = mCurrentJumpTime / MaxJumpTime;
            float JumpGravityMultiplier = GravityMultiplier;
            if(progress > 0.5f)
            {
                JumpGravityMultiplier = GravityMultiplier * (1 - progress);
            }
            mRigidbody2D.velocity += Physics2D.gravity * JumpGravityMultiplier * Time.fixedDeltaTime;
        }
        else if(mRigidbody2D.velocity.y < 0)
        {
            mRigidbody2D.velocity += Physics2D.gravity * FallMultiplier * Time.fixedDeltaTime;
        }

        //Move
        mRigidbody2D.velocity = new Vector2(HorizontalMovementSpeed * mHorizontalInput, mRigidbody2D.velocity.y);
    }

}

public enum JumpStates
{
    NotJump,
    MinJump,
    ControlJump,
}
