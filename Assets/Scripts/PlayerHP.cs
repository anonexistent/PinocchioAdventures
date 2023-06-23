using JsonQuestion2;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
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
        Cursor.visible = false;

        anim= GetComponent<Animator>();
        isAlive=true;
        food = GetComponent<PlayerMove>();
        spawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        //dataBase = GameObject.Find("DataBaseWork").GetComponent<DBConnection>();
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
        //StartCoroutine(
        //JsonSendResults2(StarCollector.starCount);
        JavaScript.RequestJs();

        Cursor.visible = true;
        gameOverCanvas.SetActive(true);
        gameObject.SetActive(false);
        GameObject.Find("score").GetComponent<TextMeshProUGUI>().text += StarCollector.starCount.ToString() + "☼";
    }

    void JsonSendResults(int sts)
    {
        //string path = @"http://localhost:81/games/";
        string path = @"D:\xampp\htdocs\games\Answers.json";
        string txtResult;

        WebRequest request = WebRequest.Create(path);
        WebResponse response = request.GetResponse();
        //yield return response.GetResponseStream();

        //FileStream s = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

        var stream = response.GetResponseStream();

        //var list = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(path2)) ?? new List<Question>();

        List<UserProfile> list;

        //using (StreamReader sr = new StreamReader(s))
        //{
        //    //txtResult = sr.ReadToEnd();
        //    list = JsonConvert.DeserializeObject<List<UserProfile>>(sr.ReadToEnd()) ?? new List<UserProfile>();
        //}

        list = JsonConvert.DeserializeObject<List<UserProfile>>(File.ReadAllText(path)) ?? new List<UserProfile>();

        Debug.Log("json got");
        //var newUserPro = JsonConvert.DeserializeObject<List<UserProfile>>(txtResult) ?? new List<UserProfile>();
        //for (int i = 0; i < sbyte.MaxValue; i++)
        //{
        //    newUserPro.Add(new() { name = Random.Range(uint.MinValue, uint.MaxValue).ToString() });
        //}

        list.Add(new() { id=UnityEngine.Random.Range(sbyte.MinValue, sbyte.MaxValue), name = sts.ToString() });
        Debug.Log("create new for json");
        //using (StreamWriter sw = new StreamWriter(
        //    //stream
        //    path
        //    ))
        //{
        //    var temp = JsonConvert.SerializeObject(newUserPro, Formatting.Indented);
        //    sw.Write(temp);
        //}
        var temp = JsonConvert.SerializeObject(list);
        Debug.Log("new for json serilization");
        //File.WriteAllText(path, temp, encoding: System.Text.Encoding.UTF8);

        using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Open, FileAccess.Read)))
        {
            sw.Write(temp);
        }
        //File.WriteAllText(path, temp);

        Debug.Log("writing");

        response.Close();

        //yield return null;
    }

    void JsonSendResults2(int sts)
    {
        #region WorkingButNotInTheBuild

        //string path = @"D:\xampp\htdocs\games\Answers.json";
        //string tempPath;

        //UserProfile newUserPro = new() { id = UnityEngine.Random.Range(ushort.MinValue, ushort.MaxValue), name = sts.ToString() };
        ////string jsonUser = JsonUtility.ToJson(newUserPro);
        ////string jsonU = JsonConvert.SerializeObject(newUserPro);
        //string allUsers;

        ////try
        ////{

        //string x =
        //    //Assembly.GetEntryAssembly().Location
        //    Application.dataPath
        //    ;
        //Debug.Log("====\n" + Application.platform.ToString() + $"\n{x}" + "\n====");

        //path =
        //    //Path.Combine(Assembly.GetExecutingAssembly().Location.Split(@"\"))
        //    GetAnswersLocation()
        //    ;

        ////  this line needs to be trimmed
        //int lastSlash = path.LastIndexOf(@"\") + 1;
        //int preLastSlash = path.Substring(0, lastSlash).LastIndexOf(@"\") + 1;
        //path = path.Substring(0, preLastSlash) + "Answers.json";
        ////Debug.Log(path);
        //allUsers = File.ReadAllText(path);
        ////}
        ////catch (Exception)
        ////{
        ////    throw;
        ////}

        //var usersList = JsonConvert.DeserializeObject<List<UserProfile>>(allUsers) ?? new List<UserProfile>();

        //usersList.Add(newUserPro);

        //string newJson = JsonConvert.SerializeObject(usersList);

        //Debug.Log(newJson);

        //File.WriteAllText(path, newJson);

        #endregion
    }

    string GetAnswersLocation()
    {
        string r = Assembly.GetExecutingAssembly().Location;

        var a = r.IndexOf("demo007")+7;
        var aa = r.IndexOf("PinocchioAdventures") + ("PinocchioAdventures").Length;

        if(Application.platform.ToString()== "WindowsEditor")
        {
            return r.Substring(0, aa) + "\\Answers.json";
        }
        else
        {
            return r.Substring(0, a) + "\\Answers.json";
        }
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