using System;
using System.Collections.Generic;

namespace Whot
{
    public class GameManager
    {
        IPlayer player1;
        IPlayer player2;

        int player1Score = 0;
        int player2Score = 0;

        MarketSize marketSize;
        private int numOfCardsForEachPlayer;

        Deck deck;
        public GameManager(Deck deck, IPlayer player1, IPlayer player2)
        {
            this.deck = deck;
            this.player1 = player1;
            this.player2 = player2;
        }


        public IPlayer ChooseShuffler()
        {
            if (player1.GetTotalValueofCards() < player2.GetTotalValueofCards())
            {
                Console.WriteLine(Constants.getShufflerMessage, player1.GetNickname(),player2.GetNickname());
                return player1;
            }
            else
            {
                Console.WriteLine(Constants.getShufflerMessage, player2.GetNickname(), player1.GetNickname());
                return player2;
            }
        }

        public void ResetGame()
        {
            player1.DropCards();
            player2.DropCards();
            deck.Reset();
        }


        public bool IsGameWon()
        {
            //call each player's hand to check if it is empty
            if (player1.GetTotalValueofCards() == 0 || player2.GetTotalValueofCards() == 0)
            {
                CheckWinner();
                return true;
            }

            return false;

        }

        public bool IsSpecialCard(IPlayer player, Card card)
        {
            int cardToCollect = 0;
            switch (card.GetCardNumber())
            {
                case 1:
                    break;
                case 2:
                    cardToCollect = 2;
                    break;
                case 14:
                    cardToCollect = 1;
                    break;
                default:
                    return false;
            }

            for (int i = 0; i < cardToCollect; i++)
            {
                if (player.GetNickname() == player1.GetNickname())
                {
                    player1.GoMarket();
                }
                else
                {
                    player2.GoMarket();
                }
            }
            return true;
        }

        public void ShareCardsAndPickCentreCard(int numOfCardsForEachPlayer, MarketSize totalSizeofMarket)
        {
            this.numOfCardsForEachPlayer = numOfCardsForEachPlayer;
            marketSize = totalSizeofMarket;

            ResetGame();

            for (int i = 0; i < numOfCardsForEachPlayer; i++)
            {
                player1.GoMarket();
                player2.GoMarket();
            }

            deck.SetCentreCard(deck.TakeCard());
        }

        public bool IsMarketEmpty()
        {
            int marketSizeNumberValue;
            const int TOTAL_NUM_OF_CARDS = 49;
            int sharedCardsAndCentreCard = (numOfCardsForEachPlayer * 2) + 1;
            int maximumAvailableMarketSize = TOTAL_NUM_OF_CARDS - sharedCardsAndCentreCard;

            switch (marketSize)
            {
                case MarketSize.five:
                    marketSizeNumberValue = Math.Min(maximumAvailableMarketSize, 5);
                    break;
                case MarketSize.ten:
                    marketSizeNumberValue = Math.Min(maximumAvailableMarketSize, 10);
                    break;
                case MarketSize.twenty:
                    marketSizeNumberValue = Math.Min(maximumAvailableMarketSize, 20);
                    break;
                case MarketSize.thirty:
                    marketSizeNumberValue = Math.Min(maximumAvailableMarketSize, 30);
                    break;
                case MarketSize.fullmarket:
                default:
                    marketSizeNumberValue = Math.Min(maximumAvailableMarketSize, 49);
                    break;
            }

            if (deck.GetRemovedCardCount() == sharedCardsAndCentreCard + marketSizeNumberValue) 
            {
                CheckWinner();
                return true;
            }
            return false;

        }
        private IPlayer CheckWinner()
        {
            if (player1.GetTotalValueofCards() < player2.GetTotalValueofCards())
            {
                player1Score++;
                Console.WriteLine("Current Score: {0}:{1}  {2}:{3}",player1.GetNickname(),player1Score,player2.GetNickname(),player2Score);
                return player1;
            }
            player2Score++;
            Console.WriteLine("Current Score: {0}:{1}  {2}:{3}", player1.GetNickname(), player1Score, player2.GetNickname(), player2Score);
            return player2;

        }
        public bool IsGameOver()
        {
            // check if any of the players have more than 1 point 
            return (player1Score > 1 || player2Score > 1);
        }

    }
}
