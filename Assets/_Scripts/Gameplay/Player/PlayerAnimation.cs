using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Rigidbody2D mRigidbody2D;
    PlayerMovement mPlayerMovement;

    private void Awake()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
        mPlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        var velocity = mRigidbody2D.velocity;
        var xParameter = 0.2f * Mathf.Abs(velocity.x) / mPlayerMovement.HorizontalMovementSpeed;
        if(Mathf.Abs(velocity.y) > 0.1) xParameter = 0;
        var scale = new Vector3(1f + xParameter, 1f, 1f);

        transform.localScale = scale;
    }
}
