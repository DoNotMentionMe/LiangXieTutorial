using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class BonfireOpenUIRebornEnemy : IBonfireRule
    {
        public int NeedSeconds { get; } = 5;

        public string Key { get; } = nameof(BonfireOpenUIRebornEnemy);
        
        public bool Unlocked { get; private set; }

        

        public void OnBonfireGUI()
        {
            if(!Unlocked)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("打开火堆敌人重生", Styles.Label.Value);
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
            PlayerPrefs.SetInt(nameof(BonfireOpenUIRebornEnemy), Unlocked ? 1 : 0);
        }

        public IBonfireRule Load()
        {
            Unlocked = PlayerPrefs.GetInt(nameof(BonfireOpenUIRebornEnemy), 0) == 1;
            return this;
        }
    }
}
