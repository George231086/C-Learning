using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramGame
{
    class AnagramGame
    {
        public List<string> getWordList(string wordListLocation)
        {
            List<string> words = new List<string>();

            using (StreamReader s = new StreamReader(wordListLocation))
                {
                    string line;
                    while ((line = s.ReadLine()) != null)
                    {
                        words.Add(line.Trim());
                    }
                }
            return words;
        }

        public void playGame()
        {
            string loc = "C:\\Users\\George\\Desktop\\wordsEn.txt";
            List<string> words = new List<string>();
            try
            {
                words = getWordList(loc);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find word list.");
                Environment.Exit(1);
            }


            Console.WriteLine("Let the game commence!");

            string response = "";
            int noCorrect = 0;
            while (!response.Equals("q"))
            {
                AnagramMaker a = new AnagramMaker();
                Random r = new Random();
                int wordPos = r.Next(words.Count);
                string[] anaWords = a.getAnagram(words.ElementAt(wordPos));
                
                while (!response.Equals("n") || !response.Equals("q"))
                {
                    Console.WriteLine("Anagram is: " + anaWords[1] + "\nMake your guess, type n for next word or q to quit!");
                    response = Console.ReadLine();
                    if (response.Equals(anaWords[0]))
                    {
                        Console.WriteLine("You got it!");
                        noCorrect++;
                        break;
                    }
                    else if (response.Equals("n"))
                    {
                        Console.WriteLine("The word was: " + anaWords[0]);
                        break;
                    }
                    else if (response.Equals("q"))
                        break;
                }
            }
            Console.WriteLine("You got " + noCorrect + " correct!");
        }
        
        static void Main(string[] args)
        {
            new AnagramGame().playGame();
        }
    }

}
