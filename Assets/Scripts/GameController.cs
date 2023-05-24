using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject gameOverCanvas;
    [SerializeField]
    Transform spawn;

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        player.SetActive(false);
    }

    public void NewGame()
    {
        StarCollector.starCount = 0;
       
        player.transform.position = spawn.position;
        player.SetActive(true );

    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
