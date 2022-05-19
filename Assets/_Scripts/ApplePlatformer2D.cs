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
            Interface.GetModel<IPlayerModel>().CurrentAppleCount = 0;
            foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Reset();
            }

            Interface.GetSystem<ISaveSystem>().Clear();

            Bonfire.RemainSeconds = 100;
            Bonfire.LiveSeconds = 0;
        }

        //Load
        public static void ContinueGame()
        {
            Interface.GetModel<IPlayerModel>().HP = PlayerPrefs.GetInt("HP", 1);
            Interface.GetModel<IPlayerModel>().MaxHP = PlayerPrefs.GetInt("MaxHP", 1);
            Interface.GetModel<IPlayerModel>().CurrentAppleCount = PlayerPrefs.GetInt("CurrentAppleCount", 0);
            Interface.GetSystem<ISaveSystem>().Load();
            Bonfire.RemainSeconds = PlayerPrefs.GetFloat("RemainSeconds", 3000);
            Bonfire.LiveSeconds = PlayerPrefs.GetFloat("LiveSeconds", 0);

            IsGameOver = false;

            foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Load();
            }
        }

        protected override void Init()
        {
            this.RegisterSystem<IBonfireSystem>(new BonfireSystem());
            this.RegisterSystem<ISaveSystem>(new SaveSystem());
            this.RegisterSystem<IInputSystem>(new InputSystem());
            this.RegisterModel<IPlayerModel>(new PlayerModel());
        }

        public static void Save()
        {
            PlayerPrefs.SetInt("HP", Interface.GetModel<IPlayerModel>().HP);
            PlayerPrefs.SetInt("MaxHP", Interface.GetModel<IPlayerModel>().MaxHP);
            PlayerPrefs.SetInt("CurrentAppleCount", Interface.GetModel<IPlayerModel>().CurrentAppleCount);
            PlayerPrefs.SetFloat("RemainSeconds", Bonfire.RemainSeconds);
            PlayerPrefs.SetFloat("LiveSeconds", Bonfire.LiveSeconds);

            foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Save();
            }

            Interface.GetSystem<ISaveSystem>().Save();
        }
    }
}
