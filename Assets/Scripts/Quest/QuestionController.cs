using JsonQuestion2;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    static List<Question> questions = new();

    [SerializeField]
    TextMeshProUGUI questText;
    [SerializeField]
    GameObject answersPanel;
    [SerializeField]
    GameObject oldBtn;
    List<GameObject> btns = new();
    Question currQ;

    void Start()
    {
        btns.Add(oldBtn);
        GetQuss();
        CreateButtons();
    }

    void Update()
    {

    }

    void GetQuss()
    {
        //  Assembly.GetExecutingAssembly() - dll, .procPath - exe
        //int lastSlash = Environment.CurrentDirectory.LastIndexOf(@"\") + 1;
        //string path2 = Environment.CurrentDirectory.Substring(0, lastSlash) + "test1.json";
        string path2 = Environment.CurrentDirectory + @"\test1.json";
        questions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(path2)) ?? new List<Question>();

        ////MessageBox.Show(string.Join('—', list.Select(x=>x.Answers.Select(x=>x.Text))));
        //list.Add(quest);
        //var convertedJson = JsonConvert.SerializeObject(list);
        //File.WriteAllText(path2, convertedJson);

        if (questions.Count > 0)
        {
            currQ = questions[UnityEngine.Random.Range(0, questions.Count - 1)];
            questText.text = currQ.Text;
        }

        
    }

    void CreateButtons()
    {
        for (int i = 0; i < currQ.Answers.Count; i++)
        {
            var a = Instantiate(btns[i], answersPanel.transform);
            var tempHeight = a.GetComponent<RectTransform>().rect.height;
            a.transform.position = new Vector3(btns[i].transform.position.x, btns[i].transform.position.y + tempHeight, btns[i].transform.position.z);
            btns.Add(a);
            a.GetComponentInChildren<TextMeshProUGUI>().text = currQ.Answers[i].Text;
        }
    }

    void OldCreateButton()
    {
        GameObject x = new("testBtn", typeof(Image), typeof(Button), typeof(LayoutElement));
        x.transform.SetParent(answersPanel.transform);
        x.GetComponent<RectTransform>().localScale = new Vector3(1,2);

        GameObject xText = new("textTxt", typeof(TextMeshProUGUI));
        xText.transform.SetParent(answersPanel.transform);
        xText.GetComponent<TextMeshProUGUI>().text = "te te te st te st te ststttt ees sst tt";
        xText.GetComponent<RectTransform>().localScale = new Vector3(2,1);
    }
}
