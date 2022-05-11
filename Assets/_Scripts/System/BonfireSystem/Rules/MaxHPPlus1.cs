using System;
using System.Collections;
using System.Collections.Generic;
using HYH;
using UnityEngine;

public class MaxHPPlus1 : IBonfireRule
{
    public int NeedSeconds { get; } = 10;

    public string Key { get; } = nameof(MaxHPPlus1);

    public bool Unlocked { get; private set; }

    Lazy<IBonfireRule> mHPBarRule = new Lazy<IBonfireRule>(() => 
        //在调用mHPBarRule时执行，GetRuleByKey方法只执行一次，将执行结果返回给mHPBarRule.Value
        ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(nameof(HPBar))
    );

    public void OnBonfireGUI()
    {
        if(!Unlocked && mHPBarRule.Value.Unlocked)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("最大血量+1", Styles.Label.Value);
            GUILayout.Label("价格:" + NeedSeconds, Styles.Label.Value);
            GUILayout.FlexibleSpace();
            if(Bonfire.RemainSeconds > NeedSeconds)
            {
                if(GUILayout.Button("解锁", Styles.Button.Value))
                {
                    Bonfire.RemainSeconds -= NeedSeconds;
                    var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
                    playerModel.HP += 1;
                    playerModel.MaxHP += 1;
                    Unlocked = true;
                }
            }
            else
            {
                GUILayout.Label("寿命不足", Styles.Label.Value);
            }

            GUILayout.EndHorizontal();
        }
    }

    public void OnGUI()
    {
        
    }

    public void OnTopRightGUI()
    {
        
    }

    public void Reset()
    {
        Unlocked = false;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(nameof(MaxHPPlus1), Unlocked ? 1 : 0);
    }

    public IBonfireRule Load()
    {
        Unlocked = PlayerPrefs.GetInt(nameof(MaxHPPlus1), 0) == 1;
        return this;
    }
}
