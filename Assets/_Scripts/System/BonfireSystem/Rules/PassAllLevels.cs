using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HYH
{
    public class PassAllLevels : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 0;
        public override string Key { get; set; } = nameof(PassAllLevels);
        public override string DisplayName { get; protected set; } = "通关";

        protected override void OnUnlock()
        {
            base.OnUnlock();

            SceneManager.LoadScene("GamePass");
        }
    }
}