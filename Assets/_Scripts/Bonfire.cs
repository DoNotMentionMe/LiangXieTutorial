using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QFramework;

namespace HYH
{
    public class Bonfire : MonoBehaviour
    {
        public static float RemainSeconds = 3000;
        public static float LiveSeconds = 0;
        [SerializeField] MeshRenderer KeyTips;

        private bool mPlayerEntered = false;
        private bool mOpenBonfireUI = false;

        private IBonfireSystem mBonfireSystem;
        private void Awake()
        {
            mBonfireSystem = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>();
        }

        private void Start()
        {
            ApplePlatformer2D.OnOpenBonfireUI.Register(() =>
            {
                ApplePlatformer2D.Save();
            });
        }

        private void OnDestroy()
        {
            mBonfireSystem = null;
        }

        private void Update()
        {
            if (ApplePlatformer2D.IsGameOver) return;

            RemainSeconds -= Time.deltaTime;
            LiveSeconds += Time.deltaTime;

            if (RemainSeconds <= 0)
            {
                ApplePlatformer2D.IsGameOver = true;
            }

            if (mPlayerEntered && !mOpenBonfireUI)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    mOpenBonfireUI = true;
                    ApplePlatformer2D.OnOpenBonfireUI.Trigger();
                    AudioSystem.PlayerUIFeedback();
                }
            }
            else if (mOpenBonfireUI)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    mOpenBonfireUI = false;
                    AudioSystem.PlayerUIFeedback();
                }
            }

        }

        private const int WIDTH = 1024;
        private const int HEIGHT = 768;

        public static void SetDesignResolution(int width, int height)
        {
            var scaleX = Screen.width / width;
            var scaleY = Screen.height / height;
            var scale = Mathf.Max(scaleX, scaleY);

            GUIUtility.ScaleAroundPivot(new Vector2(scale, scale), new Vector2(0, 0));
        }

        private void OnGUI()
        {
            SetDesignResolution(WIDTH, HEIGHT);

            GUILayout.BeginArea(new Rect(WIDTH - 200, 0, 200, 200));
            GUILayout.BeginHorizontal();
            GUILayout.Label("寿命：" + (int)RemainSeconds + "s", Styles.Label.Value);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("已存活：" + (int)LiveSeconds + "s", Styles.Label.Value);
            GUILayout.EndHorizontal();
            foreach (var bonfireRule in mBonfireSystem.Rules)
            {
                bonfireRule.OnTopRightGUI();
            }
            GUILayout.EndArea();

            foreach (var bonfireRule in mBonfireSystem.Rules)
            {
                bonfireRule.OnGUI();
            }

            if (mOpenBonfireUI)
            {
                //TODO:火堆UGUI落实
                var windowPosition = new Rect
                {
                    size = new Vector2(640, 480),
                    center = new Vector2(WIDTH * 0.5f, HEIGHT * 0.5f)
                };

                GUILayout.Window(0, windowPosition, (id) =>
                    {
                        GUILayout.BeginHorizontal();

                        GUILayout.FlexibleSpace();

                        if (GUILayout.Button("x", GUILayout.Width(20)))
                        {
                            mOpenBonfireUI = false;
                        }

                        GUILayout.EndHorizontal();
                        foreach (var bonfireRule in mBonfireSystem.Rules)
                        {
                            bonfireRule.OnBonfireGUI();
                        }
                    },
                    "火堆操作界面");

            }

            #region  游戏结束界面
            if (ApplePlatformer2D.IsGameOver)
            {
                GUILayout.BeginArea(new Rect(0, 0, WIDTH, HEIGHT));
                GUILayout.FlexibleSpace();

                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("游戏结束", Styles.BigLabel.Value);
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(40);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    // if (GUILayout.Button("重新开始", Styles.BigButton.Value))
                    // {
                    //     ApplePlatformer2D.HasContinue = true;
                    //     ApplePlatformer2D.ResetGameData();
                    //     AudioSystem.PlayerUIFeedback();
                    //     SceneManager.LoadScene("Game");
                    // }

                    if (GUILayout.Button("回到主页", Styles.BigButton.Value))
                    {
                        ApplePlatformer2D.ResetGameData();
                        AudioSystem.PlayerUIFeedback();
                        SceneManager.LoadScene("GameStart");
                    }
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
                GUILayout.EndArea();
            }
            #endregion
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                KeyTips.enabled = true;
                mPlayerEntered = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                KeyTips.enabled = false;
                mPlayerEntered = false;
            }
        }
    }
}