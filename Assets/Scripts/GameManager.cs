using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public int selectedPlayer = 1;

    public Material boxMat;
    public Material glowMat;

    public ParticleSystem winEffect;
    public Text gameWon;
    public GameObject wonPanel;

    public CameraSwicher cameraSwicher;


    private void Awake()
    {
        // DDOL‚É“o˜^‚·‚é
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (Box box in GetPlayer().GetPossibleBoxes())
        {
            box.GetComponent<MeshRenderer>().material = glowMat;
        }
    }

    public Player GetPlayer()
    {
        return (selectedPlayer == 1) ? player1 : player2;
    }

    public void SwitchPlayer()
    {
        foreach (Box box in GetPlayer().GetPrevieusPossibleBoxes())
        {
            box.GetComponent<MeshRenderer>().material = boxMat;
        }


        selectedPlayer = (selectedPlayer == 1) ? 2 : 1;
        cameraSwicher.switchCamera(selectedPlayer);

        foreach(Box box in GetPlayer().GetPossibleBoxes())
        {
            box.GetComponent<MeshRenderer>().material = glowMat;
        }
    }

    public void GameWin()
    {
        winEffect.Play();
        wonPanel.SetActive(true);
        gameWon.text = ("Game Won!");
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
