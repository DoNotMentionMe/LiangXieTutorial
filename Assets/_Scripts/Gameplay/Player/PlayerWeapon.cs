using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace HYH
{
    public class PlayerWeapon : MonoBehaviour
    {
        private IInputSystem mInputSystem;
        private IBonfireRule mSimpleGunRule;

        private void Awake()
        {
            mInputSystem = ApplePlatformer2D.Interface
                .GetSystem<IInputSystem>();

            mSimpleGunRule = ApplePlatformer2D.Interface
                .GetSystem<IBonfireSystem>()
                .GetRuleByKey(nameof(SimpleGunRule));

            if (mSimpleGunRule.Unlocked)
            {
                CurrentWeapon = transform.GetComponentInChildren<SimpleGun>();
            }
            else
            {
                CurrentWeapon = null;
            }

            ApplePlatformer2D.OnBonfireRuleUnlocked.Register((Key) =>
            {
                if (Key == mSimpleGunRule.Key)
                {
                    CurrentWeapon = transform.GetComponentInChildren<SimpleGun>();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnDestroy()
        {
            mInputSystem = null;
            mSimpleGunRule = null;
        }
        /// <summary>
        /// 当前武器
        /// </summary>
        private Weapon CurrentWeapon;

        /// <summary>
        /// 使用武器
        /// </summary>
        private void Update()
        {
            if (mInputSystem.ShootDown)
            {
                CurrentWeapon?.ShootDown();
            }

            if (mInputSystem.Shoot)
            {
                CurrentWeapon?.Shoot();
            }

            if (mInputSystem.ShootUp)
            {
                CurrentWeapon?.ShootUp();
            }
        }
    }
}