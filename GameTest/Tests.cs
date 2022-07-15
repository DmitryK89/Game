using Game;
using NUnit.Framework;

namespace GameTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InputMatchSecretNumber()
        {
            // Arrange
            var engine = new GameEngine(maxAllowedRetry: 3, secretNumber: "1234");
            // Act
            var attemptResult = engine.CheckAttempt("1234");
            // Assert
            Assert.IsTrue(attemptResult.IsWin);
        }
        
        [Test]
        public void RetryCounterFull()
        {
            // Arrange
            var engine = new GameEngine(maxAllowedRetry: 3, secretNumber: "1234");
            // Act
            var attemptResult1 = engine.CheckAttempt("1111");
            var attemptResult2 = engine.CheckAttempt("2222");
            var attemptResult3 = engine.CheckAttempt("3333");
            // Assert
            Assert.IsTrue(attemptResult3.IsRetryCounterFull);
        }
        
        [Test]
        public void CheckHowManyAttemptLeft()
        {
            // Arrange
            var engine = new GameEngine(maxAllowedRetry: 3, secretNumber: "1234");
            // Act
            var attemptResult1 = engine.CheckAttempt("1111");
            var attemptResult2 = engine.CheckAttempt("2222");
            var attemptResult3 = engine.CheckAttempt("3333");
            // Assert
            Assert.AreEqual(attemptResult1.AttemptsLeft, 2);
            Assert.AreEqual(attemptResult2.AttemptsLeft, 1);
            Assert.AreEqual(attemptResult3.AttemptsLeft, 0);
        }
        
        [Test]
        public void OneDigitMatchInTheSamePosition()
        {
            // Arrange
            var engine = new GameEngine(maxAllowedRetry: 3, secretNumber: "1234");
            // Act
            var attemptResult3 = engine.CheckAttempt("1777");
            // Assert
            Assert.AreEqual(attemptResult3.MatchNumbers, "BULL 1");
        }
        [Test]
        public void TwoDigitMatchInTheSamePosition()
        {
            // Arrange
            var engine = new GameEngine(maxAllowedRetry: 3, secretNumber: "1234");
            // Act
            var attemptResult3 = engine.CheckAttempt("1277");
            // Assert
            Assert.AreEqual(attemptResult3.MatchNumbers, "BULL 1, BULL 2");
        }
        
        [Test]
        public void OneDigitMatchOnOtherPosition()
        {
            // Arrange
            var engine = new GameEngine(maxAllowedRetry: 3, secretNumber: "1234");
            // Act
            var attemptResult3 = engine.CheckAttempt("4777");
            // Assert
            Assert.AreEqual(attemptResult3.MatchNumbers, "COW 4");
        }
        
        [Test]
        public void OneMatchOnTheSamePositionAnotherMatchOnOtherPosition()
        {
            // Arrange
            var engine = new GameEngine(maxAllowedRetry: 3, secretNumber: "1234");
            // Act
            var attemptResult3 = engine.CheckAttempt("1477");
            // Assert
            Assert.AreEqual(attemptResult3.MatchNumbers, "BULL 1, COW 4");
        }
    }
}