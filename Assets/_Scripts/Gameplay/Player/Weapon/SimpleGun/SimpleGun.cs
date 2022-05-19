using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SimpleGun : Weapon
    {
        /// <summary>
        /// 子弹模板
        /// </summary>
        public SimpleBullet BulletTemplate;

        public override void ShootDown()
        {
            var bullet = Instantiate(BulletTemplate);
            bullet.transform.position = BulletTemplate.transform.position;
            bullet.gameObject.SetActive(true);
        }

        public override void Shoot()
        {

        }

        public override void ShootUp()
        {

        }
    }
}