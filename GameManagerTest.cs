using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whot;

namespace WhotTest
{
    [TestClass]
    public class GameManagerTest
    {
        [TestMethod]
        public void MarketCheckTest()
        {
            //Arrange
            CardFactory cardFactory = new CardFactory();
            Deck deck = new Deck(cardFactory);
            IPlayer player1 = new Player("Henry", deck);
            IPlayer player2 = new Player("Patrick", deck);  
            GameManager gameManager = new GameManager(deck,player1, player2);
            gameManager.ShareCardsAndPickCentreCard(5, MarketSize.five);

            //Act
            var expected = true;

            for (int i = 0; i < 5; i++)
            {
                deck.RemoveCardFromDeck(deck.TakeCard());
            }

            var actual = gameManager.IsMarketEmpty();

            //Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
