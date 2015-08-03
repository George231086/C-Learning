using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnagramGame
{
    class AnagramMaker
    {

        public static void modHeapsAlgo(int n, char[] word, ref int permNo, ref string anagram)
        {
            if (permNo == 0)
                return;
            else if (n == 1)
            {
                permNo--;
                if (permNo == 0)
                {
                    string wordStr = "";
                    foreach (char c in word)
                    {
                        wordStr += c;
                    }
                    anagram = wordStr;
                }

            }
            else
            {
                for (int i = 0; i < n - 1; i++)
                {
                    modHeapsAlgo(n - 1, word, ref permNo, ref anagram);
                    if (n % 2 == 0)
                    {
                        char temp = word[n - 1];
                        word[n - 1] = word[i];
                        word[i] = temp;
                    }
                    else
                    {
                        char temp = word[n - 1];
                        word[n - 1] = word[0];
                        word[0] = temp;
                    }
                }

                modHeapsAlgo(n - 1, word, ref permNo, ref anagram);
            }

        }

        public static int factorial(int i)
        {
            if (i <= 1)
                return 1;

            return i * factorial(i - 1);
        }


        public string[] getAnagram(string s)
        {
            int wordLength = s.Length;
            int i1;
            if (wordLength <= 7)
                i1 = factorial(wordLength) / 4;
            else
                i1 = factorial(7) / 4;
            int i2 = 3 * i1;

            Random r = new Random();
            int permNo = 0;
            permNo = r.Next(i1, i2);
            string anagram = "";
            modHeapsAlgo(wordLength, s.ToCharArray(), ref permNo, ref anagram);
            return new string[] { s, anagram };
        }

    }
}
