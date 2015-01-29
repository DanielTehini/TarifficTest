using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherTest.Models
{
    public class UserQuote
    {
        public int Id { get; set; }
        public string Author { get; set; }

        public string Quote { get; set; }

        public string Emotion { get; set; }

        public string Source { get; set; }

        public string TransformedQuote { get; set; }

        public DateTime DateCreated { get; set; }

        public override string ToString()
        {
            string[] getSplitWords = Quote.Split(' ');
            string returnSentence = "";
            for (int i = 0; i < getSplitWords.Length; i++)
            {
                string currentWord = getSplitWords[i];
                if (currentWord.Length > 2)
                {
                    currentWord = char.ToUpper(currentWord[0]) + currentWord.Substring(1, currentWord.Length - 2) + char.ToUpper(getSplitWords[i][getSplitWords[i].Length - 1]);
                }
                else
                {
                    currentWord = getSplitWords[i].ToUpper();
                }
                returnSentence += currentWord + " ";
            }
            return returnSentence;
        }
    }
}