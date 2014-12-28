using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomGuess
{
   class Program
   {
      private static Random rnd = new Random();
      private static int[] totalCounts = {0, 0, 0};
      private static int[] latestCounts = { 0, 0, 0 };
      private const int latestSize = 1000;
      private static Queue<int> latestNumbers = new Queue<int>(latestSize);
      private static int totalGuesses = 0;
      private static int correctGuesses = 0;

      static void Main(string[] args)
      {
         int guess = 3;

         for (int i = 0; i < 100000; i++)
         {
            int nextResult = AddRandomNumber();
            if (guess < 3)
            {
               totalGuesses++;
               if (guess == nextResult)
               {
                  Console.WriteLine("Guess {0} was CORRECT.", guess);
                  correctGuesses++;
               }
               else
               {
                  Console.WriteLine("Guess {0} did not match {1}", guess, nextResult);
               }
            }
            if (latestNumbers.Count >= latestSize)
            {
               for (guess = 0; guess < 3; guess++)
               {
                  if (latestCounts[guess] < 300)
                  {
                     Console.WriteLine("Distribution is 0:{0} ({1} total) 1:{2} ({3} total) 2:{4} ({5} total)\nGuessing {6}."
                        , latestCounts[0], totalCounts[0], latestCounts[1], totalCounts[1], latestCounts[2], totalCounts[2], guess);
                     break;
                  }
               }
            }
         }

         Console.WriteLine("Final distribution: 0:{0}, 1:{1}, 2:{2}"
            , totalCounts[0], totalCounts[1], totalCounts[2]);
         Console.WriteLine("Total guesses: {0}; correct guesses: {1}; percent correct: {2}"
            , totalGuesses, correctGuesses, correctGuesses * 100.0 / totalGuesses);
      }

      static int AddRandomNumber()
      {
         int result = rnd.Next(3);
         totalCounts[result]++;
         latestCounts[result]++;
         latestNumbers.Enqueue(result);
         if (latestNumbers.Count > latestSize)
            latestCounts[latestNumbers.Dequeue()]--;
         return result;
      }

   }
}
