using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class Level4 : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 10;

        public override string Key { get; set; } = nameof(Level4);
        public override string DisplayName { get; protected set; } = "第四关";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}
