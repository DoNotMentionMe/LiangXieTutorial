using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    public class BonfireRuleUnlockedCondition : MonoBehaviour
    {
        [SerializeField] string Key;

        public UnityEvent IsUnlocked = new UnityEvent();

        public void Execute()
        {
            var rule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(Key);

            if(rule.Unlocked)
            {
                IsUnlocked?.Invoke();
            }
        }
    }
}
