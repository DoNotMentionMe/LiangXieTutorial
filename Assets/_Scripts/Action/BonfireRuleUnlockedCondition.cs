using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    public class BonfireRuleUnlockedCondition : MonoBehaviour
    {
        public UnityEvent IsUnlocked = new UnityEvent();

        [SerializeField] string Key;

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
