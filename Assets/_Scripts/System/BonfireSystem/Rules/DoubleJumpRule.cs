using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class DoubleJumpRule : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 100;

        public override string Key { get; } = nameof(DoubleJumpRule);

        public override string DisplayName { get; protected set; } = "二段跳";
    }
}