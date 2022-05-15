using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class BonfireOpenRecoverHP : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 30;

        public override string Key { get; } = nameof(BonfireOpenRecoverHP);

        public override string DisplayName { get; protected set; } = "回到火堆恢复血量";

        
    }
}
