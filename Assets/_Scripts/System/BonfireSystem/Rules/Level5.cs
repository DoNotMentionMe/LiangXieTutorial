using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class Level5 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 10;

        public override string Key { get; } = nameof(Level5);
        public override string DisplayName { get; protected set; } = "第五关";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}