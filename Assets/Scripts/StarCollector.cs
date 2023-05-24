using JsonQuestion2;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarCollector : MonoBehaviour
{
    public static int starCount = 0;
    static List<Question> questions = new();

    [SerializeField]
    TextMeshProUGUI questText;
    [SerializeField]
    GameObject answersPanel;
    [SerializeField]
    GameObject oldBtn;
    List<GameObject> btns = new();
    Question currQ;

    [SerializeField]
    TextMeshProUGUI starsText;
    GameObject hPpanels;
    public GameObject questionPanel;
    //public GUIStyle questionMenuStyle;

    private void Start()
    {
        hPpanels = GameObject.Find("PanelplayerHP");
        btns.Add(oldBtn);
        GetQuss();
        //questionPanel = GameObject.FindWithTag("QuestionPanel");
        //questionPanel.SetActive(false);
    }

    void Update()
    {
        starsText.text = starCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Star")
        {
            
        }
        if(collision.tag == "carrot" & PlayerHP.HP<3)
        {


        }
        switch (collision.tag)
        {
            case "Star":
                starCount++;
                Destroy(collision.gameObject);
                break;

            case "carrot":
                if(PlayerHP.HP<3)
                {
                    Debug.Log("fsdjtewrgfo");
                    PlayerHP.HP++;
                    hPpanels.transform.GetChild(PlayerHP.HP).gameObject.SetActive(true);
                    Destroy(collision.gameObject);
                }

                break;

            case "StarQ":
                StartMegaQuest();
                break;

            default:
                break;
        }
    }
    #region ThenTheLivingWillEnvyTheDead

    private void StartMegaQuest()
    {
        // child list 0 - preview 1 - questtxt 2 - answers

        //GameObject panel = GameObject.FindWithTag("QuestionPanel");
        questionPanel.SetActive(true);
        puse.isQuestActive = true;
        Time.timeScale = 0;

        GetQus();
        CreateButtons();

        //OnGUI();
    }

    //  ~ onGui
    void CreateButtons()
    {
        float tempHeight = 0.0f;

        for (int i = 0; i < currQ.Answers.Count; i++)
        {
            var a = Instantiate(btns[i], answersPanel.transform);
            tempHeight += a.GetComponent<RectTransform>().rect.height;
            a.transform.position = new Vector3(btns[i].transform.position.x, btns[i].transform.position.y + tempHeight, btns[i].transform.position.z);
            btns.Add(a);
            a.GetComponentInChildren<TextMeshProUGUI>().text = currQ.Answers[i].Text;
        }
    }
    void OldCreateButton()
    {
        GameObject x = new("testBtn", typeof(Image), typeof(Button), typeof(LayoutElement));
        x.transform.SetParent(answersPanel.transform);
        x.GetComponent<RectTransform>().localScale = new Vector3(1, 2);

        GameObject xText = new("textTxt", typeof(TextMeshProUGUI));
        xText.transform.SetParent(answersPanel.transform);
        xText.GetComponent<TextMeshProUGUI>().text = "te te te st te st te ststttt ees sst tt";
        xText.GetComponent<RectTransform>().localScale = new Vector3(2, 1);
    }

    private void OnGUI()
    {
        Cursor.visible = true;

        if (puse.isQuestActive)
        {
            //  clone (copy, duplicate) CreateButtons()

            var x = new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 150f, 45f);
            var a = GUI.Button(x, "correct answer TEST");
            var b = GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "false 222222");

            List<bool> buttons = new();

            var temp = answersPanel.GetComponent<RectTransform>().rect;
            float tempW2 = temp.width / 2;
            float tempH2 = temp.height / 2;

            for (int i = 0; i < currQ.Answers.Count-1; i++)
            {
                buttons.Add(GUI.Button(new Rect(tempW2, tempH2, 150f, 100f), currQ.Answers[i].Text));
            }

            //List<bool> listAb = new() { a, b };

            //for(int i = 0; i < 4; i++)
            //{
            //    listAb.Add(GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 75f - (50f)*i, 150f, 45f), $"{i}"));
            //}

            if (a)
            {
                Debug.Log("QuestionPanel");
                Time.timeScale = 1;
                Cursor.visible = false;
                questionPanel.SetActive(!questionPanel.active);
                puse.isQuestActive = false;
            }
        }

    }
    //  current
    void GetQuss()
    {
        //  Assembly.GetExecutingAssembly() - dll, .procPath - exe
        //int lastSlash = Environment.CurrentDirectory.LastIndexOf(@"\") + 1;
        //string path2 = Environment.CurrentDirectory.Substring(0, lastSlash) + "test1.json";
        string path2 = Environment.CurrentDirectory + @"\test1.json";
        questions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(path2)) ?? new List<Question>();

        ////MessageBox.Show(string.Join('�', list.Select(x=>x.Answers.Select(x=>x.Text))));
        //list.Add(quest);
        //var convertedJson = JsonConvert.SerializeObject(list);
        //File.WriteAllText(path2, convertedJson);
    }
    //  all qss
    void GetQus()
    {
        if (questions.Count > 0)
        {
            currQ = questions[UnityEngine.Random.Range(0, questions.Count - 1)];
            questText.text = currQ.Text;
        }
    }

    #endregion
}