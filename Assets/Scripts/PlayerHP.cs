﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public static byte HP = 3;
    bool isAlive;
    PlayerMove food;
    Animator anim;    
    Transform spawn;
    GameController gameController;

    //[SerializeField]
    //TextMeshProUGUI hPtext;
    [SerializeField]
    GameObject gameOverCanvas;
    [SerializeField]
    GameObject hPpanels;

    DBConnection dataBase;

    private void Start()
    {
        anim= GetComponent<Animator>();
        isAlive=true;
        food = GetComponent<PlayerMove>();
        spawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        dataBase = GameObject.Find("DataBaseWork").GetComponent<DBConnection>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -30 && !food.isGround) GameOver(); 
        //hPtext.text = HP.ToString();
        Debug.Log(HP);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            HpMinus(); 
                         

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //Debug.Log("quit");
                //CloseGame();
            }
            
            anim.SetTrigger("Hurt");
           
        }
        if(collision.gameObject.tag == "carrot")
        {
            //  starCollector - yet not only stars conrtoller
            Debug.Log(";dfsghtghftrewgr3ew");
        }
    }

    void HpMinus()
    {
        HP--;            
        if (HP==0)
        {
            isAlive = false;
            
            GameOver();
        }
        hPpanels.transform.GetChild(HP+1).gameObject.SetActive(false);
    }

    
    public void GameOver()
    {
        MySqlSendResults(StarCollector.starCount);

        gameOverCanvas.SetActive(true);
        gameObject.SetActive(false);
        GameObject.Find("score").GetComponent<TextMeshProUGUI>().text += StarCollector.starCount.ToString() + "☼";
    }

    private void MySqlSendResults(int sts)
    {
        MySqlConnection a = dataBase.Connection();
        a.Open();
        string query = $"INSERT INTO `users` (`name`) VALUES ('{StarCollector.starCount}')";
        var cmd = new MySqlCommand(query, a);
        cmd.ExecuteNonQuery();
        //var reader = cmd.ExecuteReader();
        //while (reader.Read())
        //{
        //    string someStringFromColumnZero = reader.GetString(0);
        //    string someStringFromColumnOne = reader.GetString(1);
        //    Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
        //}
        a.Close();
    }

    public void NewGame()
    {
        StarCollector.starCount = 0;
        HP = 3;
        transform.position = spawn.position;
        gameOverCanvas.SetActive(false);
        gameObject.SetActive(true);

        for (int i = 0; i <= HP; i++)
        {
            hPpanels.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void xxx()
    {
        //HpMinus();
        
        StarCollector.starCount = 0; PlayerHP.HP = 3;
        Debug.Log("hp--; tryAgainOnClick");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
