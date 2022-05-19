using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SetPlayerMovementNotJump : MonoBehaviour
    {
        public void Execute()
        {
            var player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerMovement>().JumpState = PlayerMovement.JumpStates.NotJump;
        }
    }
}