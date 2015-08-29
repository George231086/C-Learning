using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnagramGame
{
    class AnagramMaker
    {
        private Random r = new Random();

        public string[] getAnagram(string s)
        {
            var wordLength = s.Length;
            var charArray = s.ToCharArray();
            for (int i = 0; i < wordLength; i++)
            {
               int rPos = i + (int)(r.NextDouble() * (wordLength - i));
               var letter = charArray[rPos];
               charArray[rPos] = charArray[i];
               charArray[i] = letter;
            }
            return new string[] { s, new string(charArray)};
        }
    }
}
