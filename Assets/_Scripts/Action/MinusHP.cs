using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HYH;
using UnityEngine.Events;

public class MinusHP : MonoBehaviour
{
    public UnityEvent OnDeath;

    public void Excute(int damage)
    {
        var model = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
        
        model.HP -= damage;
        if(model.HP <= 0)
        {
            //OnDeath?.Invoke();

            ApplePlatformer2D.IsGameOver = true;
        }
    }
}
