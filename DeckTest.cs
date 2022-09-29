using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Whot;

namespace WhotTest
{
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void RemoveCardFromDeckTest()
        {
            //Arrange
            CardFactory cardFactory = new CardFactory();
            Deck deck = new Deck(cardFactory);
            Card card1 = new Card(CardSymbol.CIRCLE, 1);
            Card card2 = new Card(CardSymbol.STAR, 2);


            //Act
            var expected1 = true;
            var expected2 = 47;
            var actual1 = deck.RemoveCardFromDeck(card1);
            deck.RemoveCardFromDeck(card2);
            var actual2 = deck.GetMarket().Count;

            //Assert
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod]
        public void TakeCardfromDeckTest()
        {
            //Arrange
            CardFactory cardFactory = new CardFactory();
            Deck deck = new Deck(cardFactory);

            //Act
            deck.TakeCard();
            deck.TakeCard();
            deck.TakeCard();
            var expected = 46;
            var actual = deck.GetMarket().Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ResetDeckTest()
        {
            //Arrange
            CardFactory cardFactory = new CardFactory();
            Deck deck = new Deck(cardFactory);

            //Act
            deck.TakeCard();
            deck.TakeCard();
            deck.TakeCard();
            deck.Reset();
            var expected = 49;
            var actual = deck.GetMarket().Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRemovedCardCountTest()
        {
            //Arrange
            CardFactory cardFactory = new CardFactory();
            Deck deck = new Deck(cardFactory);
            IPlayer player1 = new Player("Henry", deck);
            IPlayer player2 = new Player("Patrick", deck);
            GameManager gameManager = new GameManager(deck, player1, player2);
            gameManager.ShareCardsAndPickCentreCard(5, MarketSize.five);

            //Act
            var expected = 16;

            for (int i = 0; i < 5; i++)
            {
                deck.RemoveCardFromDeck(deck.TakeCard());
            }

            var actual = deck.GetRemovedCardCount();

            //Assert
            Assert.AreEqual(expected, actual);
        }


    }
}
