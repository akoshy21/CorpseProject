using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltGoal : MonoBehaviour
{
 // alternate goal code by Annamaria Koshy

    public bool playerOneAtGoal;
    public bool playerTwoAtGoal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent.CompareTag("PlayerOne"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller != null && !controller.dead)
            {
                playerOneAtGoal = true;
            }
        }
        else if (collision.transform.parent.CompareTag("PlayerTwo"))
        {
            PlayerController controller = collision.transform.parent.GetComponentInChildren<PlayerController>();
            if (controller != null && !controller.dead)
            {
                playerTwoAtGoal = true;
            }
        }

        if (playerOneAtGoal && playerTwoAtGoal)
        {
            LevelManager.lm.lvlEnd = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerOne"))
        {
            playerOneAtGoal = false;
        }
        else if (collision.gameObject.CompareTag("PlayerTwo"))
        {
            playerTwoAtGoal = false;
        }
    }
}
