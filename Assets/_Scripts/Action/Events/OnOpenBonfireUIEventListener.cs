using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using QFramework;


namespace HYH
{
    /// <summary>
    /// <para>可复用，在功能实现的位置挂载</para>
    /// <para>监听ApplePlatformer2D.OnOpenBonfireUI时间</para>
    /// <para>功能：打开bonfire界面时触发的事件</para>
    /// </summary>
    public class OnOpenBonfireUIEventListener : MonoBehaviour
    {
        public UnityEvent OnOpenBonfireUI = new UnityEvent();

        private void Start()
        {
            ApplePlatformer2D.OnOpenBonfireUI.Register(() =>
            {
                var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
                playerModel.HP = playerModel.MaxHP;
                OnOpenBonfireUI?.Invoke();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
