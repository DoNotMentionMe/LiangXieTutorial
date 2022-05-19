using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HYH
{
    public class UIGamePass : MonoBehaviour
    {
        private void Start()
        {
            transform.Find("BttnBack").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameStart");
            });

            transform.Find("LiveTime").GetComponent<Text>()
                .text = "通关时长:" + (int)Bonfire.LiveSeconds + "s";

            //删掉存档
            ApplePlatformer2D.HasContinue = false;
        }
    }
}