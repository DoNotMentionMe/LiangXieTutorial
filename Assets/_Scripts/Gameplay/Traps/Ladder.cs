using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class Ladder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerClimbLadder>().CanClimb();
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerClimbLadder>().CantClimb();
            }
        }
    }
}