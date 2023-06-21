using JsonQuestion2;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
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
    Animator animator;
    [Range(0.0f, 2.0f)]
    public float a = 1;

    private void Start()
    {
        var b = UnityWebRequest.Get(@"http://localhost:5000/api/1");
        b.SendWebRequest();

        Debug.Log(">>" + b.downloadProgress);

        hPpanels = GameObject.Find("PanelplayerHP");
        animator = questionPanel.GetComponent<Animator>();
        GetQuss();
        //questionPanel = GameObject.FindWithTag("QuestionPanel");
        questionPanel.SetActive(false); 
        questionPanel.LeanScale(Vector3.zero, 0.5f);

        Time.timeScale = a;
    }

    void Update()
    {
        starsText.text = starCount.ToString();
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("QuestionPanelStart")) Debug.Log("anim anim anim anim");
        //if (animator.runtimeAnimatorController.animationClips[0].)
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
                    Debug.Log("taked carrot");
                    PlayerHP.HP++;
                    hPpanels.transform.GetChild(PlayerHP.HP).gameObject.SetActive(true);
                    Destroy(collision.gameObject);
                }
                break;

            case "StarQ":
                StartCoroutine(PreStart(collision));
                Debug.Log("mega quest begins...");

                StartMegaQuest();
                collision.gameObject.GetComponent<Animator>().SetTrigger("Taking");
                Destroy(collision.gameObject);
                break;

            case "finish":
                //StartCoroutine(PreStart(collision));
                Debug.Log("hourse");
                break;

            default:
                break;
        }
    }
    #region ThenTheLivingWillEnvyTheDead

    //  taking star animation
    IEnumerator PreStart(Collider2D col)
    {
        yield return col.transform.LeanScale(Vector2.one*8,2f);
        //yield return new WaitForSeconds(2.1f);
        //yield return col.transform.LeanScale(Vector2.zero, 2f).setEasePunch();
    }

    //  event
    private void StartMegaQuest()
    {        
        // child list 0 - preview 1 - questtxt 2 - answers

        //GameObject panel = GameObject.FindWithTag("QuestionPanel");
        questionPanel.SetActive(true);

        StartCoroutine(Open());

        //questionPanel.LeanScale(Vector2.one, 0.3f);
        //if (!animator.IsInTransition(0))
        //{

        //}            
        puse.isQuestActive = true;            

            UnityEngine.Cursor.visible = true;

            buttons.Clear();

            currQ = GetQus();
            questText.text = currQ.Text;
        
        //  game object buttons
        CreateButtons();

            //OnGUI();

    }

    //  two pieces
    IEnumerator Open()
    {
        yield return questionPanel.LeanScale(Vector2.one, 0.3f);
        yield return new WaitForSeconds(0.32f);
        Time.timeScale = 0;
        
    }

    //  ~ onGui
    void CreateButtons()
    {
        float tempHeight = 0.0f;

        for (int i = 0; i < currQ.Answers.Count; i++)
        {
            var a = Instantiate(prefBtn, answersPanel.transform);
            tempHeight += a.GetComponent<RectTransform>().rect.height;
            //  circa event button1_Click(sender, e)
            a.GetComponent<Button>().onClick.AddListener(() => 
            {
                EndQuestionActivity();
                Debug.Log("answer clicked");
                //CheckAnswer();
            });
            a.GetComponentInChildren<TextMeshProUGUI>().text = currQ.Answers[i].Text;
            a.GetComponent<Answer>().IsCorrect = currQ.Answers[i].IsCorrect;
            a.transform.position = new Vector3(prefBtn.transform.position.x, prefBtn.transform.position.y + tempHeight, prefBtn.transform.position.z);
            btns.Add(a);
        }

    }

    //  for game obj btns
    IEnumerator ClearListFromScene(List<GameObject> list)
    {
        foreach (GameObject item in list)
        {
            Destroy(item);
        }
        list.Clear();
        yield return null;
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

    public void JsonWebRequest(string path)
    {
        StartCoroutine(JsonWorkInNewFonrmat(path));
    }

    IEnumerator JsonWorkInNewFonrmat(string path)
    {
        //using (UnityWebRequest wr = new UnityWebRequest(@"127.0.01/games"))
        //{
        //    yield return wr.SendWebRequest();

        //    if (wr.result != UnityWebRequest.Result.Success)
        //    {
        //        Debug.Log(wr.error);
        //    }
        //    else
        //    {
        //        Debug.Log("Form upload complete!");
        //    }
        //}

        UnityWebRequest wr = UnityWebRequest.Get(path);
        yield return wr.SendWebRequest();

        if (wr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wr.error);
        }
        else
        {
            //      ↓↓ jsonString ↓↓
            Debug.Log("json file has been goted");
            questions = JsonConvert.DeserializeObject<List<Question>>(wr.downloadHandler.text) ?? new List<Question>();
        }

    }

    public void EndQuestionActivity()
    {
        foreach (var btn in btns)
        {
        //    var isTemp = btn.GetComponent<QuestionButton>().IsPressed;
            if(btn.GetComponent<QuestionButton>().answerText == currQ.Answers.Where(x=>x.IsCorrect).Select(x=>x.Text).FirstOrDefault()) starCount++;
        //    if (isTemp) Debug.Log(txtTemp);
        }

        Time.timeScale = 1;
        UnityEngine.Cursor.visible = false;
        //StartCoroutine(Close());
        //questionPanel.SetActive(!questionPanel.active);        
        UnityEngine.Cursor.visible = false;
        puse.isQuestActive = false;
        //throw new NullReferenceException();

        StartCoroutine(ClearBtns1());
    }

    IEnumerator ClearBtns1()
    {
        yield return null;
        yield return StartCoroutine(ClearButns2());
    }

    IEnumerator ClearButns2()
    {
        yield return StartCoroutine(Close());
        yield return StartCoroutine(ClearListFromScene(btns));
    }

    IEnumerator Close()
    {
        questionPanel.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
        yield return new WaitForSeconds(0.51f); 
        //questionPanel.SetActive(!questionPanel.active);

    }

    void CheckAnswer(PointerEventData btnInfo)
    {
        Debug.Log(btnInfo.button);
    }

    //  all qss
    void GetQuss()
    {
        //  Assembly.GetExecutingAssembly() - dll, .procPath - exe
        //int lastSlash = Environment.CurrentDirectory.LastIndexOf(@"\") + 1;
        //string path2 = Environment.CurrentDirectory.Substring(0, lastSlash) + "Questions.json";

        //string path2 = Path.Combine(Environment.CurrentDirectory, "Questions.json");
        JsonWebRequest(@"http://localhost:81/games/Questions.json");

        //Debug.Log(path2);
        //questions = JsonConvert.DeserializeObject<List<Question>>(path2) ?? new List<Question>();

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
