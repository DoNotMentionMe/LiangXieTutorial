using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace HYH
{
    public interface IBonfireSystem : ISystem
    {
        List<IBonfireRule> Rules { get; }

        IBonfireRule GetRuleByKey(string key);
    }

    public class BonfireSystem : AbstractSystem, IBonfireSystem
    {
        protected override void OnInit()
        {
            Rules.Add(new HPBar());
            Rules.Add(new MaxHPPlus1());
            Rules.Add(new BonfireOpenUIRebornEnemy());
            Rules.Add(new BonfireOpenRecoverHP());
            Rules.Add(new Level1());
            Rules.Add(new Level2());
            Rules.Add(new Level3());
            Rules.Add(new Level4());
            Rules.Add(new Level5());
            Rules.Add(new Level6());
        }

        public List<IBonfireRule> Rules { get; } = new List<IBonfireRule>();

        public IBonfireRule GetRuleByKey(string key)
        {
            foreach(var bonfireRule in Rules)
            {
                if(bonfireRule.Key == key)
                {
                    return bonfireRule;
                }
            }

            return null;
        }
    }

}
