using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whot;

namespace WhotTest
{
    [TestClass]
    public class HumanPlayerTest
    {
        [TestMethod]
        public void DropCardsTest()
        {
            //Arrange

            CardFactory cardFactory = new CardFactory();
            Deck deck = new Deck(cardFactory);
            IPlayer player1 = new Player("Henry", deck);

            //Act
            player1.GoMarket();
            player1.GoMarket();
            player1.DropCards();
            var expected = 0;
            var actual = player1.GetCards().Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GoMarketTest()
        {
            //Arrange
            CardFactory cardFactory = new CardFactory();
            Deck deck = new Deck(cardFactory);
            IPlayer player1 = new Player("Ford", deck);

            //Act
            player1.GoMarket();
            player1.GoMarket();
            var expected = 2;
            var actual = player1.GetCards().Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }


    }
}
