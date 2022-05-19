using System.Data;
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
            //第一关
            var level1 = new Level1()
                .SecondsCost(10)
                .Condition(self => !self.Passed)
                .AddToRules(Rules);

            new HPBar()
                .SecondsCost(5)
                .Condition(_ => level1.Passed)
                .AddToRules(Rules);

            new MaxHPPlus1()
                .SecondsCost(5)
                .Condition(_ => level1.Passed)
                .AddToRules(Rules);

            //第二关
            var level2 = new Level2()
                .SecondsCost(10)
                .Condition(self => !self.Passed && level1.Passed)
                .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level2")
                .SecondsCost(5)
                .Condition(_ => level2.Passed)
                .AddToRules(Rules);

            //第三关
            var level3 = new Level3()
                .SecondsCost(10)
                .Condition(self => !self.Passed && level2.Passed)
                .AddToRules(Rules);

            new BonfireOpenRecoverHP()
                .SecondsCost(30)
                .Condition(_ => level3.Passed)
                .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level3")
                .SecondsCost(5)
                .Condition(_ => level3.Passed)
                .AddToRules(Rules);

            //第四关
            var level4 = new Level4()
                .SecondsCost(10)
                .Condition(self => !self.Passed && level3.Passed)
                .AddToRules(Rules);

            new BonfireOpenUIRebornEnemy()
                .SecondsCost(10)
                .Condition(_ => level4.Passed)
                .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level4")
                .SecondsCost(5)
                .Condition(_ => level4.Passed)
                .AddToRules(Rules);

            //第五关
            var level5 = new Level5()
                .SecondsCost(10)
                .Condition(self => !self.Passed && level4.Passed)
                .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level5")
                .SecondsCost(5)
                .Condition(_ => level5.Passed)
                .AddToRules(Rules);

            //第六关
            var level6 = new Level6()
                .SecondsCost(10)
                .Condition(self => !self.Passed && level5.Passed)
                .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level6")
                .SecondsCost(5)
                .Condition(_ => level6.Passed)
                .AddToRules(Rules);

            new AddHPEvery10ApplesRule()
                .Condition(_ => level6.Passed)
                .AddToRules(Rules);

            //第七关
            var level7 = new Level7()
                .SecondsCost(10)
                .Condition(self => !self.Passed && level6.Passed)
                .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level7")
                .SecondsCost(5)
                .Condition(_ => level7.Passed)
                .AddToRules(Rules);

            var doubleJump = new DoubleJumpRule()
                .SecondsCost(5)
                .Condition(_ => level7.Passed)
                .AddToRules(Rules);

            var SimpleGun = new SimpleGunRule()
                .SecondsCost(100)
                .Condition(_ => level7.Passed)
                .AddToRules(Rules);

            var level1_2 = new Level1()
                .WithKey("Level1_2")
                .SecondsCost(10)
                .Condition(self => !self.Passed && doubleJump.Unlocked)
                .AddToRules(Rules);

            var level2_2 = new Level2()
                .WithKey("Level2_2")
                .SecondsCost(10)
                .Condition(self => !self.Passed && level1_2.Passed && doubleJump.Unlocked)
                .AddToRules(Rules);

            var level3_2 = new Level3()
                .WithKey("Level3_2")
                .SecondsCost(10)
                .Condition(self => !self.Passed && level2_2.Passed && SimpleGun.Unlocked)
                .AddToRules(Rules);

            var level4_2 = new Level4()
                .WithKey("Level4_2")
                .SecondsCost(10)
                .Condition(self => !self.Passed && level3_2.Passed && SimpleGun.Unlocked)
                .AddToRules(Rules);

            var level5_2 = new Level5()
                .WithKey("Level5_2")
                .SecondsCost(10)
                .Condition(_ => false)
                //.Condition(self => !self.Passed && level4_2.Passed && doubleJump.Unlocked)
                .AddToRules(Rules);

            var level6_2 = new Level6()
                .WithKey("Level6_2")
                .SecondsCost(10)
                .Condition(self => !self.Passed && level5_2.Passed && doubleJump.Unlocked)
                .AddToRules(Rules);

            var level7_2 = new Level7()
                .WithKey("Level7_2")
                .SecondsCost(10)
                .Condition(self => !self.Passed && level6_2.Passed && doubleJump.Unlocked)
                .AddToRules(Rules);

            new PassAllLevels()
                .Condition(_ => level4_2.Passed)
                .AddToRules(Rules);

            // Rules.Add(new Level3());
            // Rules.Add(new Level4());
            // Rules.Add(new Level5());
            // Rules.Add(new Level6());
            // Rules.Add(new Level7());
            // Rules.Add(new HPBar());
            // Rules.Add(new MaxHPPlus1());
            // Rules.Add(new BonfireOpenUIRebornEnemy());
            // Rules.Add(new BonfireOpenRecoverHP());
            // Rules.Add(new DoubleJumpRule());
        }

        public List<IBonfireRule> Rules { get; } = new List<IBonfireRule>();

        public IBonfireRule GetRuleByKey(string key)
        {
            foreach (var bonfireRule in Rules)
            {
                if (bonfireRule.Key == key)
                {
                    return bonfireRule;
                }
            }

            return null;
        }
    }

}
