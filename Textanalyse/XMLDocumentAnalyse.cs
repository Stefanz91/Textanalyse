using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Textanalyse
{
    
    internal class XMLDocumentAnalyse
    {
        private string xmlPath;
        private XmlDocument xmlDoc;
        private XmlNodeList CList;
        private List<string> words;

        public XMLDocumentAnalyse(string path)
        {
            xmlPath = path;
        }

        public bool getXML()
        {
            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                CList = xmlDoc.GetElementsByTagName("w:t");
                ShowText();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
            
        }
        
        public string ShowText()
        {
            string[] text = new string[CList.Count];


            for (int i = 0; i < CList.Count; i++)
            {
                text[i] = CList[i].InnerText.ToString();
            }

            words = new List<string>();
            for (int i = 0; i < text.Length; i++)
            {
                string[] subString = text[i].Split(' ');
                foreach (string s in subString)
                {
                    words.Add(s);
                }
            }

            string saveWord = " ";
            for (int i = 0; i < words.Count; i++)
            {
                if (saveWord != " ")
                {
                    words[i] = saveWord + words[i];
                    saveWord = " ";
                }
                if (words[i].Length < 2)
                {
                    saveWord = words[i];
                    words.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (words[i].Contains('!'))
                    {
                        string[] splitWord = words[i].Split('!');
                        words[i] = splitWord[0];
                        words.Insert(i + 1, "!");
                        i++;
                    }
                    if (words[i].Contains('.'))
                    {
                        string[] splitWord = words[i].Split('.');
                        words[i] = splitWord[0];
                        words.Insert(i + 1, ".");
                        i++;
                    }
                    if (words[i].Contains('?'))
                    {
                        string[] splitWord = words[i].Split('?');
                        words[i] = splitWord[0];
                        words.Insert(i + 1, "?");
                        i++;
                    }
                    if (words[i].Contains(','))
                    {
                        string[] splitWord = words[i].Split(',');
                        words[i] = splitWord[0];
                        words.Insert(i + 1, ",");
                        i++;
                    }
                }
            }

            string returnText = "";
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Length > 1)
                {
                    returnText += " ";
                }
                returnText += words[i];
            }
            return returnText;
        }
        



        private int CountWord(string word)
        {
            int number = 0;
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].ToLower() == word)
                {
                    number++;
                }
            }
            return number;
        }



        public string CountWordsInSentence()
        {
            List<NumberCount> sentenceLengthList = new List<NumberCount>();

            int sentenceLength = 0;
            for (int i = 0; i < words.Count; i++)
            {
                if (!Endmark(words[i]))
                {
                if (words[i] != ",")
                    sentenceLength++;
                }
                else
                {

                if (sentenceLengthList.Count < 2)
                {
                    sentenceLengthList.Add(new NumberCount(sentenceLength));
                }
                else
                {
                    for (int k = 0; k < sentenceLengthList.Count; k++)
                    {
                        if (sentenceLength == sentenceLengthList[k].getNumber())
                        {
                            sentenceLengthList[k].increaseNumberCount();
                            break;
                        }
                        else if (sentenceLength < sentenceLengthList[k].getNumber())
                        {
                            sentenceLengthList.Insert(k, new NumberCount(sentenceLength));
                            break;
                        }
                        else
                        {
                            if (k == sentenceLengthList.Count - 1)
                            {
                                sentenceLengthList.Add(new NumberCount(sentenceLength));
                                break;
                            }
                        }
                    }
                }
                sentenceLength = 0;
            }
        }

            string textToReturn = "";
            foreach (NumberCount z in sentenceLengthList)
            {
                textToReturn += "Satzlänge: " + z.getNumber() + " Häufigkeit: " + z.getNumberCount() +Environment.NewLine;
            }
            return textToReturn;
        }

        private bool Endmark(string word)
        {
            if (word == "!" || word == "." || word == "?")
            return true;
            return false;
        }

    }
}
