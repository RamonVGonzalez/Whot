using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Whot;

namespace WhotTest
{
    [TestClass]
    public class AlgoTest
    {
        [TestMethod]
        public void ChooseCardTest()
        {
            //public int ChooseCard(List<Card> hand)
            //Arrange
            CardFactory cardFactory = new CardFactory();
            Deck deck = new Deck(cardFactory);
            IPlayer player1 = new Player("Henry", deck);
            IPlayer player2 = new Player("Ford", deck);
            WhotPlayAlgo whotPlayAlgo = new WhotPlayAlgo(deck);
            GameManager gameManager = new GameManager(deck,player1, player2);
            
            Card card1 = new Card(CardSymbol.CIRCLE, 2);
            Card card2 = new Card(CardSymbol.TRIANGLE, 2);
            Card card3 = new Card(CardSymbol.TRIANGLE, 1);
            Card card4 = new Card(CardSymbol.SQUARE, 1);
            Card card5 = new Card(CardSymbol.STAR, 1);
            Card card6 = new Card(CardSymbol.STAR, 5);

            Card centreCard = new Card(CardSymbol.CIRCLE, 10);

            List<Card> list = new List<Card>();
            list.Add(card6);
            list.Add(card5);
            list.Add(card4);
            list.Add(card3);
            list.Add(card2);
            list.Add(card1);


            //Act
            deck.SetCentreCard(centreCard);

            var expected0 = 5;
            var expected1 = 4;
            var expected2 = 3;
            var expected3 = 2;
            var expected4 = 1;
            var expected5 = 0;

            var actual0 = whotPlayAlgo.ChooseCard(list);
            deck.SetCentreCard(list[actual0]);
            var actual1 = whotPlayAlgo.ChooseCard(list);
            deck.SetCentreCard(list[actual1]);
            var actual2 = whotPlayAlgo.ChooseCard(list);
            deck.SetCentreCard(list[actual2]);
            var actual3 = whotPlayAlgo.ChooseCard(list);
            deck.SetCentreCard(list[actual3]);
            var actual4 = whotPlayAlgo.ChooseCard(list);
            deck.SetCentreCard(list[actual4]);
            var actual5 = whotPlayAlgo.ChooseCard(list);
            deck.SetCentreCard(list[actual5]);

            //Assert
            Assert.AreEqual(expected0, actual0);
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
            Assert.AreEqual(expected4, actual4);
            Assert.AreEqual(expected5, actual5);
        }
    }
}
