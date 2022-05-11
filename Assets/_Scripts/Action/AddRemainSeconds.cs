using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRemainSeconds : MonoBehaviour
{
    public void Execute(int seconds)
    {
        Bonfire.RemainSeconds += seconds;
    }
}
