using System;
using System.Collections;
using System.Collections.Generic;
using HYH;
using UnityEngine;

public class HPBar : IBonfireRule
{
    Lazy<IPlayerModel> mPlayerModel = new Lazy<IPlayerModel>(() => ApplePlatformer2D.Interface.GetModel<IPlayerModel>());

    public int NeedSeconds { get; } = 30;
    public string Key { get; } = nameof(HPBar);
    public bool Unlocked { get; private set; }


    public void OnBonfireGUI()
    {
        if(!Unlocked)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("血量条", Styles.Label.Value);
            GUILayout.Label("价格:" + NeedSeconds, Styles.Label.Value);
            GUILayout.FlexibleSpace();
            if(Bonfire.RemainSeconds > NeedSeconds)
            {
                if(GUILayout.Button("解锁", Styles.Button.Value))
                {
                    Bonfire.RemainSeconds -= NeedSeconds;
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

    public void OnTopRightGUI()
    {
        if(Unlocked)
        {
            GUILayout.Label($"血量:{mPlayerModel.Value.HP}/{mPlayerModel.Value.MaxHP}", Styles.Label.Value);
        }
    }
    public void OnGUI()
    {
        
    }

    public void Reset()
    {
        Unlocked = false;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(nameof(HPBar), Unlocked ? 1 : 0);
    }

    public IBonfireRule Load()
    {
        Unlocked = PlayerPrefs.GetInt(nameof(HPBar), 0) == 1;
        return this;
    }
}
