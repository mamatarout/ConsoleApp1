using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string strFilePathPath = @"C:\Test\wordlist.txt";
            //Read all words in the file
            string[] arrWords = File.ReadAllLines(strFilePathPath);

            // string[] arrWords = { "cat", "cats", "catsdogcats", "catxdogcatsrat", "dog", "dogcatsdog", "hippopotamuses", "rat", "ratcatdogcat","ab", "ac" };

            //Create object of the class
            clsConcat objConcat = new clsConcat();
            List<String> lst = objConcat.findAllConcatWords(arrWords);
            //Retrieve two longest numbers
            List<String> finallist = lst.OrderByDescending(x => x.Length).Take(2).ToList();

            Console.WriteLine("The longest word in the file that can be constructed by concatenating copies of shorter words also found in the file:  " + finallist[0].ToString());
            Console.WriteLine("2nd longest word in the file that can be constructed by concatenating copies of shorter words also found in the file:  " + finallist[1].ToString());
            Console.WriteLine("Total count of how many of the words in the list can be constructed of other words in the list:  " + lst.Count);
            Console.ReadLine();



        }

    }
    public class clsConcat
    {

        public List<String> findAllConcatWords(String[] words)
        {
            int intMinWordLen = 0;
            int intMaxWordLen = 0;
            int intWordlen = 0;
            int minConcatWordLen = 0;
            //Create HashSet to store words for seearch purpose
            HashSet<string> hashSetWords = new HashSet<string>();

            //Populate HashSetWord and assign intMinWordLen, intMaxWordLen
            foreach (String word in words)
            {
                intWordlen = word.Count();

                if (intWordlen < intMinWordLen || intMinWordLen == 0)
                {
                    intMinWordLen = intWordlen;
                }

                if (intMaxWordLen < intWordlen)
                {
                    intMaxWordLen = intWordlen;
                }

                hashSetWords.Add(word);
            }

            //List to store possible words have concatinated words
            List<String> lstResult = new List<string>();
            minConcatWordLen = intMinWordLen + intMinWordLen;
            foreach (String word in words)
            {
                intWordlen = word.Count();
                if (intWordlen < minConcatWordLen) continue;
                if (isWordExist(word, 0, hashSetWords, intMinWordLen, intMaxWordLen, 0))
                {
                    lstResult.Add(word);

                }
            }

            return lstResult;
        }

        bool isWordExist(String strWord, int intStartIndex, HashSet<string> hashSetWords, int intMinWordLen, int intMaxWordLen, int intCounter)
        {

            string strSubWord = null;
            if (intStartIndex == strWord.Count())
            {
                if (intCounter > 1)
                    return true;
                else
                    return false;
            }

            for (int i = intMinWordLen; i < intMaxWordLen; i++)
            {
                if (intStartIndex + i > strWord.Count()) break;
                strSubWord = strWord.Substring(intStartIndex, i);
                //if sub string contains in hashSetWord then call recursive function
                if (hashSetWords.Contains(strSubWord))
                {
                    if (isWordExist(strWord, intStartIndex + i, hashSetWords, intMinWordLen, intMaxWordLen, intCounter + 1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}