using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using QFramework;


namespace HYH
{
    /// <summary>
    /// 监听ApplePlatformer2D.OnOpenBonfireUI时间
    /// 功能：打开bonfire界面时触发的事件
    /// </summary>
    public class OnOpenBonfireUIEventListener : MonoBehaviour
    {
        public UnityEvent OnOpenBonfireUI = new UnityEvent();

        private void Start()
        {
            ApplePlatformer2D.OnOpenBonfireUI.Register(() =>
            {
                OnOpenBonfireUI?.Invoke();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
