using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Events;

namespace HYH
{
    /// <summary>
    /// Key规则解锁的监听器
    /// </summary>
    public class OnBonfireRuleUnlockedEventListener : MonoBehaviour
    {
        [SerializeField] string Key;

        [SerializeField] List<string> Keys = new List<string>();

        public UnityEvent OnUnlock = new UnityEvent();

        private void Start()
        {
            Keys.Add(Key);

            foreach (var key in Keys)
            {
                var rule = ApplePlatformer2D.Interface
                    .GetSystem<IBonfireSystem>()
                    .GetRuleByKey(key);

                if (rule.Unlocked)
                {
                    OnUnlock?.Invoke();
                }
            }


            ApplePlatformer2D.OnBonfireRuleUnlocked.Register((key) =>
            {
                if (Keys.Contains(key))
                {
                    OnUnlock?.Invoke();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }

}
