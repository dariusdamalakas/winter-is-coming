using NUnit.Framework;

namespace WinterIsComing.Server.Tests
{
    [TestFixture]
    class GameCommandFactory_NewGame_Tests
    {
        private readonly GameCommandFactory gameCommandFactory = new GameCommandFactory(); 

        [TestCase("START john_snow", "john_snow", null)]
        [TestCase("START john_snow season_1", "john_snow", "season_1")]
        [Test]
        public void ValidCommands(string input, string playerName, string boardName)
        {
            // Given
            // When
            var cmd = gameCommandFactory.BuildFrom(input) as StartGameCommand;

            // Then
            Assert.That(cmd.PlayerName, Is.EqualTo(playerName));
            Assert.That(cmd.BoardName, Is.EqualTo(boardName));
        }

        [TestCase("START_john_snow")] // no space after "start"
        [TestCase("STart john_snow")] // START not uppercase
        [TestCase("START john_snow board1 board2")] // too many params

        // TODO add input validation
        //[TestCase("START { ")] // invalid character. 
        //[TestCase("START ! ")] // invalid character
        //[TestCase("START ? ")] // invalid character
        [Test]
        public void InvalidCommands(string input)
        {
            // Given
            // When
            // Then
            Assert.Throws<InvalidGameCommandException>(() => gameCommandFactory.BuildFrom(input));

            // TODO 
            // var excception = Assert.Throws<InvalidGameCommandException>(() => gameCommandFactory.BuildFrom(input));
            // Assert.That(exception.Message, Is.EqualTo("invalid_command")); // Should assert on specific exceptions
        }
    }
}
