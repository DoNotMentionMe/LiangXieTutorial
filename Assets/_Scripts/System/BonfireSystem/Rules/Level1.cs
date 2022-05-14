using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class Level1 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 10;

        public override string Key { get; } = nameof(Level1);
        public override string DisplayName { get; protected set; } = "第一关";


        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }

    }
}
