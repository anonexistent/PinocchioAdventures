using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField]
    GameObject WinCanvas; //для связи с окном победы

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "finish" && StarCollector.starCount>3)
        {
            WinCanvas.SetActive(true);
            gameObject.SetActive(false);

        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("level2");
    }

    public void GameExit()
    {
        Application.Quit(); 
    }
}
