using UnityEngine;

namespace HYH
{
    public abstract class AbstractBonfireLevelRule : AbstractBonfireRule
    {
        public bool Passed { get; set; } = false;

        protected override void OnReset()
        {
            Passed = false;
        }

        protected override void OnSave()
        {
            PlayerPrefs.SetInt(Key + ":Passed", Passed ? 1 : 0);
        }
        protected override void OnLoad()
        {
            Passed = PlayerPrefs.GetInt(Key + ":Passed", 0) == 1;
        }

    }
}