using MySql.Data.MySqlClient;
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

    private void Start()
    {
        anim= GetComponent<Animator>();
        isAlive=true;
        food = GetComponent<PlayerMove>();
        spawn = GameObject.FindGameObjectWithTag("Respawn").transform;
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

            MySqlNewScore();

            GameOver();
        }
        hPpanels.transform.GetChild(HP+1).gameObject.SetActive(false);
    }

    
    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        gameObject.SetActive(false);
        GameObject.Find("score").GetComponent<TextMeshProUGUI>().text += StarCollector.starCount.ToString() + "☼";
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

    //  TEST mySql


    public void MySqlNewScore()
    {
        var dbCon = DBConnection.Instance();
        dbCon.Server = "127.0.0.1";
        dbCon.DatabaseName = "test";
        dbCon.UserName = "root";
        dbCon.Password = "";
        if (dbCon.IsConnect())
        {
            //suppose col0 and col1 are defined as VARCHAR in the DB
            string query = $"INSERT INTO `users` (`name`) VALUES ('{StarCollector.starCount}')";
            var cmd = new MySqlCommand(query, dbCon.Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string someStringFromColumnZero = reader.GetString(0);
                string someStringFromColumnOne = reader.GetString(1);
                Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
            }
            dbCon.Close();
        }
    }

    public void xxx()
    {
        //HpMinus();
        
        StarCollector.starCount = 0; PlayerHP.HP = 3;
        Debug.Log("hp--; tryAgainOnClick");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

public class DBConnection
{
    private DBConnection()
    {
    }

    public string Server { get; set; }
    public string DatabaseName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public MySqlConnection Connection { get; set; }

    private static DBConnection _instance = null;
    public static DBConnection Instance()
    {
        if (_instance == null)
            _instance = new DBConnection();
        return _instance;
    }

    public bool IsConnect()
    {
        if (Connection == null)
        {
            if (String.IsNullOrEmpty(DatabaseName))
                return false;
            string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
            Connection = new MySqlConnection(connstring);
            Connection.Open();
        }

        return true;
    }

    public void Close()
    {
        Connection.Close();
    }
}