using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HYH
{
    public class UIGameStart : MonoBehaviour
    {
        private void Start()
        {
            transform.Find("BttnStart").GetComponent<Button>().onClick.AddListener(() =>
            {
                ApplePlatformer2D.ResetGameData();
                ApplePlatformer2D.HasContinue = true;
                SceneManager.LoadScene("Game");
            });

            var BttnContinueButton = transform.Find("BttnContinue").GetComponent<Button>();

            if (ApplePlatformer2D.HasContinue)
            {
                BttnContinueButton.onClick.AddListener(() =>
                {
                    ApplePlatformer2D.ContinueGame();

                    SceneManager.LoadScene("Game");
                });
            }
            else
            {
                BttnContinueButton.gameObject.SetActive(false);
            }

            var bttnVersion = transform.Find("BttnVersion").GetComponent<Button>();

            bttnVersion.GetComponentInChildren<Text>().text = "v" + Application.version;
        }
    }

}
