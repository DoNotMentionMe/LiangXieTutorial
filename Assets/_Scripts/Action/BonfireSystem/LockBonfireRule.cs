using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class LockBonfireRule : MonoBehaviour
    {
        public string RuleName;

        public void Execute()
        {
            var rule = ApplePlatformer2D.Interface
                .GetSystem<IBonfireSystem>()
                .GetRuleByKey(RuleName);

            rule.Unlocked = false;
        }
    }
}