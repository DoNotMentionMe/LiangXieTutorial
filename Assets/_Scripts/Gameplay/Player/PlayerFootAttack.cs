using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootAttack : MonoBehaviour
{
    [SerializeField] Trigger2D FootAttackCHeck;
    [SerializeField] float JumpSpeed = 5;

    private Rigidbody2D mRigidbody2D;

    private void Awake()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        FootAttackCHeck.OnTriggerEnterWithCollider.AddListener((collider) =>
        {
            collider.GetComponent<CharaterHit>().Hit();

            mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed);
        });
    }
}
