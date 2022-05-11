using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HYH;

public class Bonfire : MonoBehaviour
{
    public static float RemainSeconds = 100;
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
        
    }

    private void OnDestroy()
    {
        mBonfireSystem = null;
    }

    private void Update()
    {
        if(ApplePlatformer2D.IsGameOver) return;

        if(RemainSeconds <= 0)
        {
            ApplePlatformer2D.IsGameOver = true;
        }

        if(mPlayerEntered && !mOpenBonfireUI)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                mOpenBonfireUI = true;
                ApplePlatformer2D.OnOpenBonfireUI.Trigger();
            }
        }
        else if(mOpenBonfireUI)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                mOpenBonfireUI = false;
            }
        }

        RemainSeconds -= Time.deltaTime;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 100, 0, 200, 200));
        GUILayout.Label("寿命：" + (int)RemainSeconds + "s", Styles.Label.Value);
        foreach ( var bonfireRule in mBonfireSystem.Rules)
        {
            bonfireRule.OnTopRightGUI();
        }
        GUILayout.EndArea();

        foreach ( var bonfireRule in mBonfireSystem.Rules)
        {
            bonfireRule.OnGUI();
        }

        if(mOpenBonfireUI)
        {
            //TODO:火堆UGUI落实
            GUILayout.Label("火堆操作界面", Styles.Label.Value);

            foreach (var bonfireRule in mBonfireSystem.Rules)
            {
                bonfireRule.OnBonfireGUI();
            }
        }

        if(ApplePlatformer2D.IsGameOver)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
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
                if(GUILayout.Button("重新开始", Styles.BigButton.Value))
                {
                    ApplePlatformer2D.HasContinue = true;
                    ApplePlatformer2D.ResetGameData();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }

                if(GUILayout.Button("回到主页", Styles.BigButton.Value))
                {
                    ApplePlatformer2D.ResetGameData();
                    SceneManager.LoadScene("GameStart");
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.EndArea();
        }
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            KeyTips.enabled = true;
            mPlayerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            KeyTips.enabled = false;
            mPlayerEntered = false;
        }
    }
}
