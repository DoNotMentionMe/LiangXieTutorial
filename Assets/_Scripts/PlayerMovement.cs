using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float HorizontalMovementSpeed;
    public float jumpForce;

    [SerializeField] float JumpUpGravity = 3;
    [SerializeField] float FallDownGravity = 6;
    [SerializeField] UnityEvent OnLand;
    [SerializeField] UnityEvent OnJump;
    
    private Rigidbody2D mRigidbody2D;

    private void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.K) && mCollisionObjectCount > 0)
        {
            OnJump?.Invoke();
            mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, jumpForce);
        }

        mRigidbody2D.velocity = new Vector2(HorizontalMovementSpeed * horizontal, mRigidbody2D.velocity.y);

        if(mRigidbody2D.velocity.y > 0)
        {
            mRigidbody2D.gravityScale = JumpUpGravity;
        }
        else
        {
            mRigidbody2D.gravityScale = FallDownGravity;
        }
    }

    [SerializeField] private int mCollisionObjectCount = 0;

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnLand?.Invoke();
        mCollisionObjectCount++;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        mCollisionObjectCount--;
    }
}
