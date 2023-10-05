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
                    for(int n = 0; n <10; n++)
                    {
                        checkNumber(n);
                    }

                    void checkNumber(int number)
                    {
                        if (words[i].StartsWith(number.ToString()))
                        {
                            string splitWord = words[i].Substring(1);
                            words[i] = number.ToString();
                            words.Insert(i + 1, splitWord);
                            i++;
                        }
                    }

                    checkPunctuation('!');
                    checkPunctuation('.');
                    checkPunctuation('?');
                    checkPunctuation(',');

                    void checkPunctuation(char pMark)
                    {
                        if (words[i].Contains(pMark))
                        {
                            string[] splitWord = words[i].Split(pMark);
                            words[i] = splitWord[0];
                            words.Insert(i + 1, pMark.ToString());
                            i++;
                        }
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

        public string CreateWordCountList()
        {
            string wordCountList = "";
            List <CountingClass<string> > wordCount = new List<CountingClass<string>>();
            for (int i = 0; i < words.Count; i++)
            {
                if (!Endmark(words[i]) & (words[i] != ","))
                {
                    if(i == 0)
                    {
                        wordCount.Add(new CountingClass<string>(words[i]));
                    }
                    else
                    {
                        bool foundMatch = false;
                        foreach(CountingClass<string> w in wordCount)
                        {
                            if (words[i].ToLower() == w.getCountedObject())
                            {
                                w.increaseNumberCount();
                                foundMatch = true;
                                break;
                            }
                        }
                        if(!foundMatch)
                            wordCount.Add(new CountingClass<string>(words[i].ToLower()));
                    }
                }
            }
            IEnumerable<CountingClass<string>> wl = wordCount.OrderByDescending(wordCount => wordCount.getObjectCount());
            int count = 0;
            foreach(CountingClass<string> w in wl)
            {
                if(w.getObjectCount() == count)
                {
                    wordCountList += ", " + w.getCountedObject();
                    
                }
                else
                {
                    wordCountList += Environment.NewLine + w.getObjectCount() + " : " + w.getCountedObject();
                    count = w.getObjectCount();
                }
            }

            return wordCountList;
        }


        public string CountWordsInSentence()
        {
            List<CountingClass<int>> sentenceLengthList = new List<CountingClass<int>>();

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
                    sentenceLengthList.Add(new CountingClass<int>(sentenceLength));
                }
                else
                {
                    for (int k = 0; k < sentenceLengthList.Count; k++)
                    {
                        if (sentenceLength == sentenceLengthList[k].getCountedObject())
                        {
                            sentenceLengthList[k].increaseNumberCount();
                            break;
                        }
                        else if (sentenceLength < sentenceLengthList[k].getCountedObject())
                        {
                            sentenceLengthList.Insert(k, new CountingClass<int>(sentenceLength));
                            break;
                        }
                        else
                        {
                            if (k == sentenceLengthList.Count - 1)
                            {
                                sentenceLengthList.Add(new CountingClass<int>(sentenceLength));
                                break;
                            }
                        }
                    }
                }
                sentenceLength = 0;
            }
        }

            string textToReturn = "";
            foreach (CountingClass<int> z in sentenceLengthList)
            {
                textToReturn += "Satzlänge: " + z.getCountedObject() + " Häufigkeit: " + z.getObjectCount() +Environment.NewLine;
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
