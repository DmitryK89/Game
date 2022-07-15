using System;
using System.Collections.Generic;
using Game;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var secretNumber = GenerateSecretNumber();

            var engine = new GameEngine(maxAllowedRetry: 5, secretNumber: secretNumber);

            while (true)
            {
                Console.WriteLine("Введите загаданное четырехзначное число");
                var attempt = Console.ReadLine();
                
                var attemptResult = engine.CheckAttempt(attempt);
                
                if (!attemptResult.InputValid)
                {
                    Console.WriteLine("Введено не четырехзначное число!");
                    continue;
                }

                if (attemptResult.IsWin)
                {
                    Console.WriteLine("Вы угадали слово!");
                    break;
                }
                
                if (attemptResult.IsRetryCounterFull)
                {
                    Console.WriteLine("Попытки закончились, вы проиграли");
                    break;
                }
                
                Console.WriteLine(attemptResult.MatchNumbers);
                Console.WriteLine($"Осталось попыток: {attemptResult.AttemptsLeft}");
            }
        }

        private static string GenerateSecretNumber()
        {
            var random = new Random();
            var eachNumberUnique = false;
            string secretNumber = null;
            while (!eachNumberUnique)
            {
                secretNumber = random.Next(1000, 9999).ToString();
                var hashSet = new HashSet<char>(secretNumber);
                if (hashSet.Count == 4)
                {
                    eachNumberUnique = true;
                }
            }
            return secretNumber;
        }
    }
}