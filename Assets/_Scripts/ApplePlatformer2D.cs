using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace HYH
{
    /// <summary>
    /// 总架构
    /// </summary>
    public class ApplePlatformer2D : Architecture<ApplePlatformer2D>
    {
        public static EasyEvent OnOpenBonfireUI = new EasyEvent();
        public static EasyEvent<string> OnBonfireRuleUnlocked = new EasyEvent<string>();

        private static bool mIsGameOver = false;
        public static bool IsGameOver
        {
            get => mIsGameOver;
            set 
            {
                if(value)
                {
                    HasContinue = false;
                }

                mIsGameOver = value;
            }
        }

        /// <summary>
        /// 判断什么时候可以继续游戏
        /// </summary>
        public static bool HasContinue
        {
            get => PlayerPrefs.GetInt(nameof(HasContinue), 0) == 1;
            set => PlayerPrefs.SetInt(nameof(HasContinue), value ? 1 : 0);
        }
        /// <summary>
        /// 重置数据
        /// </summary>
        public static void ResetGameData()
        {
            IsGameOver = false;

            Interface.GetModel<IPlayerModel>().HP = 1;
            Interface.GetModel<IPlayerModel>().MaxHP = 1;
            foreach(var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Reset();
            }

            Interface.GetSystem<ISaveSystem>().Clear();

            Bonfire.RemainSeconds = 100;
        }

        //Load
        public static void ContinueGame()
        {
            Interface.GetModel<IPlayerModel>().HP = PlayerPrefs.GetInt("HP", 1);
            Interface.GetModel<IPlayerModel>().MaxHP = PlayerPrefs.GetInt("MaxHP", 1);
            Interface.GetSystem<ISaveSystem>().Load();
            Bonfire.RemainSeconds = PlayerPrefs.GetFloat("RemainSeconds", 100);

            foreach(var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Load();
            }
        }

        protected override void Init()
        {
            this.RegisterSystem<IBonfireSystem>(new BonfireSystem());
            this.RegisterSystem<ISaveSystem>(new SaveSystem());
            this.RegisterModel<IPlayerModel>(new PlayerModel());

            GlobalMonoEvents.OnApplicationQuitEvent.Register(() =>
            {
                //Save
                PlayerPrefs.SetInt("HP", Interface.GetModel<IPlayerModel>().HP);
                PlayerPrefs.SetInt("MaxHP", Interface.GetModel<IPlayerModel>().MaxHP);
                PlayerPrefs.SetFloat("RemainSeconds", Bonfire.RemainSeconds);

                foreach(var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
                {
                    bonfireRule.Save();
                }

                Interface.GetSystem<ISaveSystem>().Save();
            });
        }
    }
}
