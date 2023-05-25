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
    GameObject prefBtn;
    List<GameObject> btns = new();
    List<bool> buttons = new();
    Question currQ;

    [SerializeField]
    TextMeshProUGUI starsText;
    GameObject hPpanels;
    public GameObject questionPanel;
    public GUIStyle questionMenuStyle;

    private void Start()
    {
        hPpanels = GameObject.Find("PanelplayerHP");
        GetQuss();
        //questionPanel = GameObject.FindWithTag("QuestionPanel");
        questionPanel.SetActive(false);
    }

    void Update()
    {
        Debug.LogWarning($"{Environment.CurrentDirectory}");
        starsText.text = starCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
                Destroy(collision.gameObject);
                break;                

            default:
                break;
        }
    }
    #region ThenTheLivingWillEnvyTheDead

    //  event
    private void StartMegaQuest()
    {
        // child list 0 - preview 1 - questtxt 2 - answers

        //GameObject panel = GameObject.FindWithTag("QuestionPanel");
        questionPanel.SetActive(true);
        puse.isQuestActive = true;
        Time.timeScale = 0;

        UnityEngine.Cursor.visible = true;

        buttons.Clear();

        currQ = GetQus();
        questText.text = currQ.Text;

        //  game object buttons
        CreateButtons();

        //OnGUI();
    }

    //  ~ onGui
    void CreateButtons()
    {
        ClearListFromScene(btns);

        float tempHeight = 0.0f;

        for (int i = 0; i < currQ.Answers.Count; i++)
        {
            var a = Instantiate(prefBtn, answersPanel.transform);
            tempHeight += a.GetComponent<RectTransform>().rect.height;
            a.GetComponent<Button>().onClick.AddListener(() => EndQuestionActivity());
            a.GetComponentInChildren<TextMeshProUGUI>().text = currQ.Answers[i].Text;
            a.transform.position = new Vector3(prefBtn.transform.position.x, prefBtn.transform.position.y + tempHeight, prefBtn.transform.position.z);
            btns.Add(a);
        }

    }

    //  for game obj btns
    void ClearListFromScene(List<GameObject> list)
    {
        foreach (GameObject item in list)
        {
            Destroy(item);
        }
    }

    #region RemnantsOfPast

    void OldCreateButton()
    {
        GameObject x = new("testBtn", typeof(UnityEngine.UI.Image), typeof(UnityEngine.UI.Button), typeof(UnityEngine.UI.LayoutElement));
        x.transform.SetParent(answersPanel.transform);
        x.GetComponent<RectTransform>().localScale = new Vector3(1, 2);

        GameObject xText = new("textTxt", typeof(TextMeshProUGUI));
        xText.transform.SetParent(answersPanel.transform);
        xText.GetComponent<TextMeshProUGUI>().text = "te te te st te st te ststttt ees sst tt";
        xText.GetComponent<RectTransform>().localScale = new Vector3(2, 1);
    }

    private void OnGUI()
    {
        //if(GUI.Button(new(100,100,100,100),"testSTYLE",questionMenuStyle))
        //{
        //    Debug.Log("style style");
        //}

        if (false & puse.isQuestActive)
        {
            UnityEngine.Cursor.visible = true;

            try
            {
                //var x = new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 150f, 45f);
                //var a = GUI.Button(x, "correct answer TEST");
                //var b = GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "false 222222");

                var temp = answersPanel.GetComponent<RectTransform>().rect;

                float tempX = Screen.width / 2;
                float tempY = Screen.height * 0.67f;
                float tempW = Screen.height * 0.02f;
                float tempH = Screen.height * 0.05f;

                //  clone (copy, duplicate) CreateButtons()
                for (int i = 0; i < currQ.Answers.Count ; i++)
                {
                    ////  he is run if change display resolution
                    //var curAnsBtn = GUI.Button(new Rect(tempW2, tempH2 + 25 * i, 100f, 50f), currQ.Answers[i].Text);
                    //           ~ 0.03f per 1 char
                    var curAnsBtn = GUI.Button(new Rect(tempX, tempY + Screen.height * 0.05f * i, tempW * ((float)currQ.Answers[i].Text.Length<10?11:(float)currQ.Answers[i].Text.Length>30? (float)currQ.Answers[i].Text.Length * 0.5f : 20), tempH), currQ.Answers[i].Text);
                    if (curAnsBtn)
                    {
                        EndQuestionActivity(currQ.Answers[i].IsCorrect);
                    }
                    buttons.Add(curAnsBtn);
                }

                //List<bool> listAb = new() { a, b };

                //for(int i = 0; i < 4; i++)
                //{
                //    listAb.Add(GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 75f - (50f)*i, 150f, 45f), $"{i}"));
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
    }
    public void EndQuestionActivity(bool isCorrectAnswer)
    {
        if(isCorrectAnswer) starCount++;
        EndQuestionActivity();
    }


    #endregion

    public void EndQuestionActivity()
    {

        Debug.Log("QuestionPanel");
        Time.timeScale = 1;
        UnityEngine.Cursor.visible = false;
        questionPanel.SetActive(!questionPanel.active);
        puse.isQuestActive = false;
        UnityEngine.Cursor.visible = false;
        //throw new NullReferenceException();
    }


    //  all qss
    void GetQuss()
    {
        //  Assembly.GetExecutingAssembly() - dll, .procPath - exe
        //int lastSlash = Environment.CurrentDirectory.LastIndexOf(@"\") + 1;
        //string path2 = Environment.CurrentDirectory.Substring(0, lastSlash) + "Questions.json";
        string path2 = Environment.CurrentDirectory + @"\Questions.json";
        questions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(path2)) ?? new List<Question>();

        ////MessageBox.Show(string.Join('—', list.Select(x=>x.Answers.Select(x=>x.Text))));
        //list.Add(quest);
        //var convertedJson = JsonConvert.SerializeObject(list);
        //File.WriteAllText(path2, convertedJson);
    }
    //  current
    Question GetQus()
    {
        Question cQ = new();
        if (questions.Count > 0)
        {
            cQ = questions[UnityEngine.Random.Range(0, questions.Count - 1)];
            //questText.text = currQ.Text;
        }

        return cQ;
    }

    #endregion
}
