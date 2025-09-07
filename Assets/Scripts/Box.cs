using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{
    private GameManager gameManager;
    private Player player;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        player = gameManager.GetPlayer();

        if (player.GetPossibleBoxes().Contains(this))
        {
            if (this.tag == "finalBox")
            {
                player.MoveToPosition(transform);
                gameManager.GameWin();
            }
            else
            {
                player.MoveToPosition(transform);
                gameManager.SwitchPlayer();
            }
        }

        if (FindObjectOfType<CameraSwicher>().isMoving)
            return;

        player = gameManager.GetPlayer();

        if (player.GetPossibleBoxes().Contains(this))
        {
            player.MoveToPosition(transform);
            gameManager.SwitchPlayer();
        }
    }
}
