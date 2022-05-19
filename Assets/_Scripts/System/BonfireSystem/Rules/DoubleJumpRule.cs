using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class DoubleJumpRule : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 100;

        public override string Key { get; set; } = nameof(DoubleJumpRule);

        public override string DisplayName { get; protected set; } = "二段跳";
    }
}