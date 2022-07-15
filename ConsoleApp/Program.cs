using System;
using System.Text;
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
            var sb = new StringBuilder();
            for (var i = 0; i < 4; i++)
            {
                sb.Append(random.Next(1, 9));
            }

            var secretNumber = sb.ToString();
            return secretNumber;
        }
    }
}