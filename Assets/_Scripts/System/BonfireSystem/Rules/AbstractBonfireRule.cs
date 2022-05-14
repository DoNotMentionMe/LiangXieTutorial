using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public abstract class AbstractBonfireRule : IBonfireRule
    {
        public abstract int NeedSeconds { get; }

        public abstract string Key { get; }
        public abstract string DisplayName { get; protected set; }

        public bool Unlocked { get; protected set; }

        public virtual void OnBonfireGUI()
        {
            if(!Unlocked)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label(DisplayName, Styles.Label.Value);
                GUILayout.Label("价格:" + NeedSeconds, Styles.Label.Value);
                GUILayout.FlexibleSpace();
                if(Bonfire.RemainSeconds > NeedSeconds)
                {
                    if(GUILayout.Button("解锁", Styles.Button.Value))
                    {
                        Bonfire.RemainSeconds -= NeedSeconds;
                        OnUnlock();
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

        protected virtual void OnUnlock()
        {

        }

        public void OnGUI()
        {

        }

        public virtual void OnTopRightGUI()
        {

        }

        public void Reset()
        {
            Unlocked = false;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(Key, Unlocked ? 1 : 0);
        }

        public IBonfireRule Load()
        {
            Unlocked = PlayerPrefs.GetInt(Key, 0) == 1;
            return this;
        }

    }

}
