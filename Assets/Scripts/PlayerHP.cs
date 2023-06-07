using JsonQuestion2;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

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

    void TestFormsPosts()
    {
        var playerName = "testName";
        int score = -1;
        var url = @"localhost/games/";

        WWWForm form = new WWWForm();
        form.AddField("playerName", playerName);
        form.AddField("score", score);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Record uploaded successfully!");
            }
            else
            {
                Debug.Log("Error uploading record: " + www.error);
            }
        }
    }
    
    public void GameOver()
    {
        //MySqlSendResults(StarCollector.starCount);
        JsonSendResults(StarCollector.starCount);

        gameOverCanvas.SetActive(true);
        gameObject.SetActive(false);
        GameObject.Find("score").GetComponent<TextMeshProUGUI>().text += StarCollector.starCount.ToString() + "☼";
    }

    private void JsonSendResults(int sts)
    {
        string path = @"http://localhost:81/games/Answers.json";
        string txtResult;

        WebRequest request = WebRequest.Create(path);
        WebResponse response = request.GetResponse();
        var stream = response.GetResponseStream();

        using (StreamReader sr = new StreamReader(stream))
        {
            txtResult = sr.ReadToEnd();
        }

        List<user> a = new() { };
        for (int i = 0; i < sbyte.MaxValue; i++)
        {
            a.Add(new() { name = Random.Range(uint.MinValue, uint.MaxValue).ToString() });
        }
        using (StreamWriter sw = new StreamWriter(stream))
        {
            sw.Write(JsonConvert.SerializeObject(a));
        }

        var jsonResultsList = JsonConvert.DeserializeObject<List<user>>(txtResult) ?? new List<user>();

        
    }

    private void MySqlSendResults(int sts)
    {
        MySqlConnection a = dataBase.Connection();
        a.Open();
        string query = $"INSERT INTO users(name) VALUES ('{StarCollector.starCount}')";
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
