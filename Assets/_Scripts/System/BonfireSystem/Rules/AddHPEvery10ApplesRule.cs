using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class AddHPEvery10ApplesRule : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 50;
        public override string Key { get; set; } = nameof(AddHPEvery10ApplesRule);
        public override string DisplayName { get; protected set; } = "每10个苹果恢复1点血量";
    }
}