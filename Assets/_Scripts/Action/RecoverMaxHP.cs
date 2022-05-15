using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class RecoverMaxHP : MonoBehaviour
    {
        public void Execute()
        {
            var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
            playerModel.HP = playerModel.MaxHP;
        }
    }
}