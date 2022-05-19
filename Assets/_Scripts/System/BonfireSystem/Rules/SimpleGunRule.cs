using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SimpleGunRule : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 100;
        public override string Key { get; set; } = nameof(SimpleGunRule);
        public override string DisplayName { get; protected set; } = "简单手枪";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(nameof(SimpleGunRule));
        }
    }
}