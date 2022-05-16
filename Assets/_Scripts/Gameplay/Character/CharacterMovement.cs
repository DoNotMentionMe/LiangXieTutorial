using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float HorizontalMovementSpeed = 3;

    private Rigidbody2D mRigidbody2D;

    private void Awake()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mRigidbody2D.velocity = new Vector2(transform.localScale.x * HorizontalMovementSpeed, mRigidbody2D.velocity.y);
    }
}
