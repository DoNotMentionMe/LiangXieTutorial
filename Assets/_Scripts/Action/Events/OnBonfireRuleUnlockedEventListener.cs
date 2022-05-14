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

        public UnityEvent OnUnlock = new UnityEvent();

        private void Start()
        {
            var rule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(Key);

            if(rule.Unlocked)
            {
                OnUnlock?.Invoke();
            }

            ApplePlatformer2D.OnBonfireRuleUnlocked.Register((key) => 
            {
                if(key == Key)
                {
                    OnUnlock?.Invoke();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
    
}
