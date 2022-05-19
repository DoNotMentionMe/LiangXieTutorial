using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    /// <summary>
    /// 尝试恢复血量
    /// 因为bonfireRuleUnlockedCondition所以这里不需要判断
    /// </summary>
    public class AddAppleCount4RecoverHP : MonoBehaviour
    {
        public UnityEvent OnHpAdd = new UnityEvent();

        public void Execute()
        {
            var PlayerModel = ApplePlatformer2D.Interface
                .GetModel<IPlayerModel>();

            PlayerModel.CurrentAppleCount++;

            if (PlayerModel.CurrentAppleCount >= 10)
            {
                OnHpAdd?.Invoke();
                PlayerModel.CurrentAppleCount -= 10;
                PlayerModel.HP = Mathf.Min(PlayerModel.HP + 1, PlayerModel.MaxHP);
            }
        }
    }
}