using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarQuest : MonoBehaviour
{
    struct Quest
    {
        public int id;
        public string text;
        public List<string> answers;

        public Quest(int id, string txt, List<string> ans)
        {
            this.id = id;
            text = txt;
            answers = ans;
        }
    }

    List<Quest> quests;
}
