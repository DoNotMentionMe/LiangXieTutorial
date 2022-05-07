using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Player;

    private void Update()
    {
        var camPos = transform.position;
        camPos.x = Player.position.x;
        camPos.y = Player.position.y + 2;
        transform.position = camPos;
    }
}
