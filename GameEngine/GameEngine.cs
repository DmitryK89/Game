using System.Collections.Generic;

namespace Game
{
    public sealed class GameEngine
    {
        private int _retryCounter;
        private readonly int _maxAllowedRetry;
        private readonly string _secretNumber;
        private const string Bull = "BULL";
        private const string Cow = "COW";

        public GameEngine(int maxAllowedRetry, string secretNumber)
        {
            _maxAllowedRetry = maxAllowedRetry;
            _secretNumber = secretNumber;
        }

        public AttemptResult CheckAttempt(string userInput)
        {
            var result = new AttemptResult()
            {
                InputValid = true
            };

            if (userInput == null || userInput.Length != 4)
            {
                result.InputValid = false;
            }

            CheckRetryAttempt(result);

            CheckIfUserWin(userInput, result);
            
            CheckEachNumberMatch(userInput, result);

            return result;
        }

        private void CheckRetryAttempt(AttemptResult result)
        {
            _retryCounter++;
            result.AttemptsLeft = _maxAllowedRetry - _retryCounter;
            if (_retryCounter == _maxAllowedRetry)
            {
                result.IsRetryCounterFull = true;
            }
        }

        private void CheckEachNumberMatch(string userInput, AttemptResult result)
        {
            var results = new List<string>();
            var hashSet = new HashSet<char>(_secretNumber);
            for (var i = 0; i < _secretNumber.Length; i++)
            {
                if (_secretNumber[i] == userInput[i])
                {
                    results.Add($"{Bull} {userInput[i]}");
                }
                else if (hashSet.Contains(userInput[i]))
                {
                    results.Add($"{Cow} {userInput[i]}");
                }
            }

            result.MatchNumbers = string.Join(", ", results);
        }

        private void CheckIfUserWin(string userInput, AttemptResult result)
        {
            if (string.Equals(_secretNumber, userInput))
            {
                result.IsWin = true;
            }
        }
    }
}