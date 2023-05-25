using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JsonQuestion2
{
    internal class Answer : MonoBehaviour
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Answer() { }

        public Answer(string txt, bool corr)
        {
            Text = txt;
            IsCorrect = corr;
        }

    }
}
