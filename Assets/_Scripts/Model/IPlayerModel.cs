using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace HYH
{
    public interface IPlayerModel : IModel
    {
        int MaxHP { get; set; }
        int HP { get; set; }
    }

    public class PlayerModel : AbstractModel, IPlayerModel
    {
        protected override void OnInit()
        {
            
        }

        public int MaxHP { get; set; } = 1;
        public int HP { get; set; } = 1;
    }
}
