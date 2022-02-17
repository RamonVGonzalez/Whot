using System;
using System.Collections.Generic;

namespace Whot
{
    public class Deck
    {
        List<Card> market;
        private Card currentCentreCard;
        private int removedCardCount;
        public Deck(CardFactory cardFactory)
        {
            market = cardFactory.GetCards();
            removedCardCount = 0;
        }

        public void SetCentreCard(Card card)
        {
            currentCentreCard = card;
            RemoveCardFromDeck(card);
        }

        public Card GetCentreCard()
        {
            return currentCentreCard;
        }

        public int GetRemovedCardCount()
        {
            return removedCardCount;
        }

        public bool RemoveCardFromDeck(Card card)
        {
            for (int i = 0; i < market.Count; i++)
            {
                if (card.Equals(market[i]))
                {
                    market.RemoveAt(i);
                    removedCardCount++;
                    return true;
                }

            }
            return false;
        }

        public Card TakeCard()
        {
            Random random = new Random();
            int cardPosition = random.Next(0, market.Count);
            Card card = market[cardPosition];
            market.RemoveAt(cardPosition);
            removedCardCount++;
            return card;
        }


        public void Reset()
        {
            market = new CardFactory().GetCards();
            removedCardCount = 0;
        }

        public List<Card> GetMarket()
        {
            return market;
        }

    }
    public enum MarketSize
    {
        five, ten, twenty, thirty, fullmarket
    }
}
