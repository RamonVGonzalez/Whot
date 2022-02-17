using System;
using System.Collections.Generic;

namespace Whot
{
    public class WhotPlayAlgo
    {
        Deck deck;
        Card virtualCentreCard;
        List<Card> holder;
        List<Card> winningCards;
        bool winPatternFound;
        public WhotPlayAlgo(Deck deck)
        {
            this.deck = deck;
        }


        public int ChooseCard(List<Card> hand)
        {
            int positionToPlay = -1;
            //check if winning pattern was found already and that the array of winning cards is not empty
            if (winPatternFound && winningCards.Count != 0)
            {
                for (int h = 0; h < hand.Count; h++)
                {
                    if (winningCards[0].Equals(hand[h]))
                    {
                        if (Rule.IsValidMove(winningCards[0], deck.GetCentreCard()))
                        {
                            winningCards.RemoveAt(0);
                            return h;
                        }
                    }
                }
            }
            else
            {
                //if no winning pattern was found
                winPatternFound = false;
                int numberOfSpecialCards = 0;
                int initialNumberOfCardsInHand = hand.Count;
                int highestCardPosition = -1;
                bool playableCard = false;

                //check if you have the special cards needed to win a game
                //set the highest non special card as the default position to play

                for (int i = 0; i < hand.Count; i++)
                {
                    if (hand[i].GetCardNumber() <= 2)
                    {
                        if (Rule.IsValidMove(hand[i], deck.GetCentreCard()))
                        {
                            playableCard = true;
                        }
                        numberOfSpecialCards++;
                    }
                    else
                    {
                        if (Rule.IsValidMove(hand[i], deck.GetCentreCard()))
                        {
                            playableCard = true;
                            if (highestCardPosition == -1 || hand[i].GetCardNumber() > hand[highestCardPosition].GetCardNumber())
                            {
                                highestCardPosition = i;
                            }
                        }
                    }

                }
                positionToPlay = highestCardPosition;

                //checking if we have at least one valid move from special or non-special cards
                if (playableCard)
                {
                    //check if there is a possibility of finding a winning move
                    if (hand.Count <= numberOfSpecialCards + 1)
                    {
                        virtualCentreCard = new Card(deck.GetCentreCard().GetCardSymbol(),
                                                     deck.GetCentreCard().GetCardNumber());
                        holder = new List<Card>();
                        holder.AddRange(hand);
                        winningCards = new List<Card>();
                        Random random = new Random();
                        const int NUMBER_OF_ATTEMPTS = 8;
                        const int NUMBER_OF_ATTEMPTS_FOR_EACH_LEGAL_MOVE = 30;

                        for (int j = 0; j < NUMBER_OF_ATTEMPTS; j++)
                        {
                            for (int k = 0; k < NUMBER_OF_ATTEMPTS_FOR_EACH_LEGAL_MOVE; k++)
                            {
                                int randomCardNum = random.Next(holder.Count);
                                Card choice = holder[randomCardNum];
                                if (Rule.IsValidMove(choice, virtualCentreCard))
                                {
                                    SetVirtualCentreCard(choice);
                                    winningCards.Add(choice);
                                    holder.RemoveAt(randomCardNum);
                                }
                                if (winningCards.Count == initialNumberOfCardsInHand)
                                {
                                    winPatternFound = true;
                                    break;
                                }

                            }

                            if (winPatternFound)
                            {
                                break;
                            }

                            virtualCentreCard = new Card(deck.GetCentreCard().GetCardSymbol(),
                                                         deck.GetCentreCard().GetCardNumber());
                            holder = new List<Card>();
                            holder.AddRange(hand);
                            winningCards = new List<Card>();
                        }

                        if (winPatternFound && winningCards.Count != 0)
                        {
                            for (int h = 0; h < hand.Count; h++)
                            {
                                if (winningCards[0].Equals(hand[h]))
                                {
                                    if (Rule.IsValidMove(winningCards[0], deck.GetCentreCard()))
                                    {
                                        winningCards.RemoveAt(0);
                                        return h;
                                    }
                                }
                            }
                        } 

                    }
                    
                }
                
            }
            return positionToPlay;
        }

        public void SetVirtualCentreCard(Card card)
        {
            virtualCentreCard = card;
        }

        public Card GetVirtualCentreCard()
        {
            return virtualCentreCard;
        }

    }
}
