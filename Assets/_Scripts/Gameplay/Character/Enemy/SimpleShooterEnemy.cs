using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShooterEnemy : CharacterController
{
    public enum States
    {
        /// <summary>
        /// 落下
        /// </summary>  
        Falling,

        /// <summary>
        /// 巡逻
        /// </summary>
        Patrol,

        /// <summary>
        /// 攻击
        /// </summary>
        Shoot,
    }

    States state = States.Falling;

    [SerializeField] Trigger2D GroundCheck;
    [SerializeField] Trigger2D ForwardCheck;
    [SerializeField] Trigger2D FallCheck;
    [SerializeField] Trigger2D AttackCheck;
    [SerializeField] Trigger2D ShooterArea;
    [SerializeField] GameObject BulletPrefab;

    private void Awake()
    {
        var CharacterMovement = GetComponent<CharacterMovement>();

        CharacterMovement.enabled = false;

        GroundCheck.OnTriggerEnter.AddListener(() =>
        {
            //巡逻状态
            state = States.Patrol;
            //开启移动
            CharacterMovement.enabled = true;
        });

        GroundCheck.OnTriggerExit.AddListener(() =>
        {
            //下落状态
            state = States.Falling;
            //关闭移动
            CharacterMovement.enabled = false;
        });

        ForwardCheck.OnTriggerEnter.AddListener(() =>
        {
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

        ShooterArea.OnTriggerEnter.AddListener(() =>
        {
            state = States.Shoot;
            mPreviousShootTime = Time.time;
            CharacterMovement.enabled = false;
        });

        ShooterArea.OnTriggerExit.AddListener(() =>
        {
            state = States.Patrol;
            CharacterMovement.enabled = true;
        });
    }

    private float mPreviousShootTime;

    private void Update()
    {
        if (state == States.Shoot)
        {
            if (Time.time - mPreviousShootTime > 1f)
            {
                //发射
                var bulletObject = Instantiate(BulletPrefab);
                bulletObject.transform.position = BulletPrefab.transform.position;
                bulletObject.SetActive(true);
                bulletObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * transform.localScale.x * 6;

                mPreviousShootTime = Time.time;
            }
        }
    }
}

