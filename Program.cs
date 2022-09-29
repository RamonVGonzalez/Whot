using System;

namespace Whot
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            CardFactory cardFactory = new CardFactory();
            Deck deck = new Deck(cardFactory);
            Rule rule = new Rule();
            WhotPlayAlgo whotPlayAlgo = new WhotPlayAlgo(deck);
            int playerTurn;

            //get player1's nickname
            string player1Nickname = "";
            while (string.IsNullOrWhiteSpace(player1Nickname))
            {
                Console.WriteLine(Constants.player1GetNicknameMessage);
                player1Nickname = Console.ReadLine();
            }
            IPlayer humanPlayer = new Player(player1Nickname, deck);

            //get player2's nickname
            string player2Nickname = "Computer";
            if (player1Nickname.Equals(player2Nickname,StringComparison.OrdinalIgnoreCase))
            {
                player2Nickname = "WhotLegend";
            }
            IPlayer comPlayer = new Player(player2Nickname, deck);
            Console.WriteLine(Constants.player2NameMessage, player2Nickname);

            GameManager gameManager = new GameManager(deck, humanPlayer, comPlayer);

            //both players to choose a random card, the player with the lowest card would start the match
            void StartGame()
            {
                //player1
                gameManager.ResetGame();
                Console.WriteLine(Constants.goMarketMessage, humanPlayer.GetNickname());
                while (Console.ReadKey().KeyChar != 'p')
                {
                    Console.WriteLine(Constants.goMarketMessage, humanPlayer.GetNickname());
                }
                humanPlayer.GoMarket();

                foreach (var cardHeld in humanPlayer.GetCards())
                {
                    Console.WriteLine(cardHeld);
                }

                //player2
                comPlayer.GoMarket();
                Console.WriteLine(Constants.comFirstCardPickMessage, comPlayer.GetNickname());

                foreach (var cardHeld in comPlayer.GetCards())
                {
                    Console.WriteLine(cardHeld);
                }

                //choose shuffler and game starter     
                playerTurn = gameManager.ChooseShuffler().Equals(humanPlayer) ? 2 : 1;

                int numberOfCardsToShare = 5;
                MarketSize marketSize = MarketSize.ten;
                if (playerTurn == 2)
                {
                    Console.WriteLine(Constants.shareGameMessage, gameManager.ChooseShuffler().GetNickname());

                    Console.Write(Constants.numberOfCardsToShareMessage);

                    bool validInput = false;

                    while (!validInput)
                    {
                        if (int.TryParse(Console.ReadLine().ToLower(), out numberOfCardsToShare))
                        {
                            validInput = true;
                        }
                    }

                    numberOfCardsToShare = int.Parse(Console.ReadLine().ToLower());
                 
                    int marketSizeOption = 0;
                    while (marketSizeOption<1)
                    {
                        Console.Write(Constants.marketSizeMessage);
                        if (int.TryParse(Console.ReadLine(),out int value))
                        {
                            marketSizeOption = value > 0 && value < 6 ? value : 0;
                        }
                        if (marketSizeOption==0)
                        {
                            Console.WriteLine(Constants.entryErrorMessage);
                        }
                    }
                    marketSize = EnumChecker(marketSizeOption);

                }
                else
                {
                    Random rand = new Random();
                    numberOfCardsToShare = rand.Next(4, 8);
                    marketSize = EnumChecker(rand.Next(1, 5));

                    Console.WriteLine(Constants.comSharingMessage, comPlayer.GetNickname(),numberOfCardsToShare,marketSize);
                    
                }
                    
                gameManager.ShareCardsAndPickCentreCard(numberOfCardsToShare, marketSize);
            }

            MarketSize EnumChecker(int size)
            {
                switch (size)
                {
                    case 1: 
                        return MarketSize.five;
                    case 2:
                        return MarketSize.ten;
                    case 3: 
                        return MarketSize.twenty;
                    case 4: 
                        return MarketSize.thirty;
                    case 5: 
                        return MarketSize.fullmarket;
                    default: 
                        return MarketSize.fullmarket;
                }

            }

            
            do
            {

                StartGame();


                while (true)
                {
                    //Player 1
                    while (playerTurn == 1)
                    {
                        Console.WriteLine(Constants.centreCardMessage + deck.GetCentreCard());
                        Console.WriteLine(Constants.whoIsPlayingMessage, humanPlayer.GetNickname());
                        // player1
                        for (int i = 0; i < humanPlayer.GetCards().Count; i++)
                        {
                            Console.WriteLine(Constants.displayCardsMessage, i + 1, humanPlayer.GetCards()[i]);
                        }
                        Console.WriteLine(Constants.inGameMessage);

                        bool validInput = false;
                        int player1CardPositionToPlay = 0;

                        while (!validInput)
                        {
                            if (int.TryParse(Console.ReadLine().ToLower(), out player1CardPositionToPlay))
                            {
                                validInput = true;
                            }
                        }

                        if (player1CardPositionToPlay == 0)
                        {
                            humanPlayer.GoMarket();
                            playerTurn = 2;
                        }
                        else
                        {
                            bool checkPlay = humanPlayer.Play(player1CardPositionToPlay - 1);
                            if (checkPlay)
                            {
                                if (!gameManager.IsSpecialCard(comPlayer, deck.GetCentreCard()))
                                {
                                    playerTurn = 2;
                                }
                            }

                        }

                    }

                    if (GameEndChecker())
                    {
                        break;
                    }

                    //player 2
                    while (playerTurn == 2)
                    {
                        Console.WriteLine(Constants.centreCardMessage + deck.GetCentreCard());
                        Console.WriteLine(Constants.whoIsPlayingMessage, comPlayer.GetNickname());

                        for (int i = 0; i < comPlayer.GetCards().Count; i++)
                        {
                            Console.WriteLine(Constants.displayCardsMessage, i + 1, comPlayer.GetCards()[i]);
                        }
                        Console.WriteLine(Constants.inGameMessage);
                        Random random = new Random();

                        int player2CardPositionToPlay = whotPlayAlgo.ChooseCard(comPlayer.GetCards());

                        Console.WriteLine(Constants.comCardChoiceMessage, player2CardPositionToPlay + 1, comPlayer.GetNickname());

                        if (player2CardPositionToPlay + 1 == 0)
                        {
                            comPlayer.GoMarket();
                            playerTurn = 1;
                        }
                        else
                        {
                            bool checkPlay = comPlayer.Play(player2CardPositionToPlay);

                            if (checkPlay)
                            {
                                if (!gameManager.IsSpecialCard(humanPlayer, deck.GetCentreCard()))
                                {
                                    playerTurn = 1;
                                }
                            }
                        }
                    }
                    if (GameEndChecker())
                    {
                        break;
                    }

                }

            } while (!gameManager.IsGameOver());

            Console.WriteLine("Game Over!");

            bool GameEndChecker()
            {
                if (gameManager.IsGameWon())
                {
                    Console.WriteLine("Game Won");
                    return true;
                }

                if (gameManager.IsMarketEmpty())
                {
                    Console.WriteLine("Market is Empty");
                    return true;
                }
                return false;
            }
        }

    }
}
