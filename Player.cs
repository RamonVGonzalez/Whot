using System.Collections.Generic;

namespace Whot
{
    public class Player : IPlayer
    {
        string playerNickname;
        List<Card> hand;
        Deck deck;

        public Player(string playerNickname, Deck deck)
        {
            this.playerNickname = playerNickname;
            hand = new List<Card>();
            this.deck = deck;
            
        }

        public string GetNickname()
        {
            return playerNickname;
        }
        public void DropCards()
        {
            hand.Clear();
        }

        public List<Card> GetCards()
        {
            return hand;
        }

        public int GetTotalValueofCards()
        {
            int totalCardValue = 0;

            foreach (var card in hand)
            {
                if (card.GetCardSymbol() == CardSymbol.STAR)
                {
                    totalCardValue += card.GetCardNumber();
                }
                totalCardValue += card.GetCardNumber();            
            }
            return totalCardValue;
        }

        public void GoMarket()
        {
            hand.Add(deck.TakeCard());
        }

        public virtual bool Play(int cardPosition)
        {
            if (cardPosition<hand.Count)
            {
                if (Rule.IsValidMove(hand[cardPosition],deck.GetCentreCard()))
                {
                    deck.SetCentreCard(hand[cardPosition]);
                    hand.RemoveAt(cardPosition); 
                    return true;
                }

            }
            return false;
        }

    }
}
