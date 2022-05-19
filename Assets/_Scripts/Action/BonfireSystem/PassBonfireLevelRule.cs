using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class PassBonfireLevelRule : MonoBehaviour
    {
        [SerializeField] string LevelName;

        public void Execute()
        {
            var levelRule = ApplePlatformer2D.Interface
                .GetSystem<IBonfireSystem>()
                .GetRuleByKey(LevelName) as AbstractBonfireLevelRule;

            levelRule.Passed = true;
        }
    }
}