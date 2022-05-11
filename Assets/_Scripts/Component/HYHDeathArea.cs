using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HYH;
using UnityEngine.SceneManagement;

public class HYHDeathArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();

            playerModel.HP --;

            if(playerModel.HP <= 0)
            {
                ApplePlatformer2D.IsGameOver = true;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
