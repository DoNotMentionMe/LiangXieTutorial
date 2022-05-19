using System;

namespace HYH
{
    public class MaxHPPlus1 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 10;

        public override string Key { get; set; } = nameof(MaxHPPlus1);
        public override string DisplayName { get; protected set; } = "最大血量+1";

        Lazy<IBonfireRule> mHPBarRule = new Lazy<IBonfireRule>(() =>
            //在调用mHPBarRule时执行，GetRuleByKey方法只执行一次，将执行结果返回给mHPBarRule.Value
            ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(nameof(HPBar))
        );

        public override void OnBonfireGUI()
        {
            if (mHPBarRule.Value.Unlocked)
            {
                base.OnBonfireGUI();
            }
        }

        protected override void OnUnlock()
        {
            var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
            playerModel.HP += 1;
            playerModel.MaxHP += 1;
        }

    }
}