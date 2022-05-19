using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public abstract class AbstractBonfireRule : IBonfireRule
    {

        public abstract int NeedSeconds { get; set; }

        public abstract string Key { get; set; }
        public abstract string DisplayName { get; protected set; }

        public bool Unlocked { get; set; }

        public Func<AbstractBonfireRule, bool> visibleCondition { get; set; } = _ => true;

        public void UnLock()
        {
            Bonfire.RemainSeconds -= NeedSeconds;
            AudioSystem.PlayerUIFeedback();
            OnUnlock();
            Unlocked = true;
        }

        public virtual void OnBonfireGUI()
        {
            if (!visibleCondition(this)) return;

            if (!Unlocked)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label(DisplayName, Styles.Label.Value);
                GUILayout.Label("价格:" + NeedSeconds + "s寿命", Styles.Label.Value);
                GUILayout.FlexibleSpace();
                if (Bonfire.RemainSeconds > NeedSeconds)
                {
                    if (GUILayout.Button("解锁", Styles.Button.Value))
                    {
                        UnLock();
                    }
                }
                else
                {
                    GUILayout.Label("寿命不足", Styles.Label.Value);
                }

                GUILayout.EndHorizontal();
            }
        }

        protected virtual void OnUnlock() { }

        public void OnGUI() { }

        public virtual void OnTopRightGUI() { }

        public void Reset()
        {
            Unlocked = false;
            OnReset();
        }

        protected virtual void OnReset() { }

        public void Save()
        {
            PlayerPrefs.SetInt(Key, Unlocked ? 1 : 0);
            OnSave();
        }

        protected virtual void OnSave() { }

        public IBonfireRule Load()
        {
            Unlocked = PlayerPrefs.GetInt(Key, 0) == 1;
            OnLoad();
            return this;
        }

        protected virtual void OnLoad() { }

    }

    public static class AbstractBonfireRuleExtension
    {
        public static T WithKey<T>(this T self, string key)
            where T : AbstractBonfireRule
        {
            self.Key = key;
            return self;
        }

        public static T SecondsCost<T>(this T self, int needSeconds)
            where T : AbstractBonfireRule
        {
            self.NeedSeconds = needSeconds;
            return self;
        }

        public static T Condition<T>(this T self, Func<T, bool> visibleCondition)
            where T : AbstractBonfireRule
        {
            self.visibleCondition = _ => visibleCondition(self);
            return self;
        }

        public static T AddToRules<T>(this T self, List<IBonfireRule> rules)
            where T : IBonfireRule
        {
            rules.Add(self);
            return self;
        }
    }

}
