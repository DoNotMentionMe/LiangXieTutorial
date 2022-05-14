using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class BonfireOpenUIRebornEnemy : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 5;

        public override string Key { get; } = nameof(BonfireOpenUIRebornEnemy);
        public override string DisplayName { get; protected set; } = "打开火堆敌人重生";

    }
}
