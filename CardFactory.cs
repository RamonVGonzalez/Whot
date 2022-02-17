using System.Collections.Generic;

namespace Whot
{
    public class CardFactory
    {
        private List<Card> allCards;
        public CardFactory()
        {
            allCards = new List<Card>();
            CreateCards();
        }

        private void CreateCards()
        {
            int highestCard = 14;
            for (int i = 1; i <= highestCard; i++)
            {
                if (i != 6 && i != 9)
                {
                    Card circle = new Card(CardSymbol.CIRCLE, i);
                    Card triangle = new Card(CardSymbol.TRIANGLE, i);
                    allCards.Add(circle);
                    allCards.Add(triangle);

                    if (i != 4 && i != 8 && i != 12)
                    {
                        Card cross = new Card(CardSymbol.CROSS, i);
                        Card square = new Card(CardSymbol.SQUARE, i);
                        allCards.Add(cross);
                        allCards.Add(square);

                    }

                    if (i < 9)
                    {
                        Card star = new Card(CardSymbol.STAR, i);
                        allCards.Add(star);
                    }
                }
            }
        }
        public List<Card> GetCards()
        {
            return allCards;
        }
    }
}
