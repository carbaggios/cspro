using System.Text;

namespace Homework4
{
    internal class Game
    {
        private Player? _player1;
        private Player? _player2;
        private Deck? _deck;
        public Player Player1 { get { return _player1!; } }
        public Player Player2 { get { return _player2!; } }
        public Deck Deck { get { return _deck!; } }

        private readonly int[] _winCosts = new int[] { 21, 22 };

        public Game(){}

        /// <summary>
        /// The entry point to the Game
        /// </summary>
        public void StartGame()
        {
            _player1 = new Player();
            _player2 = new Player(isComputer: true);
            _deck = new Deck();

            //PrintMainDeck();

            Player1.OnFinishedGame += Player_OnFinishedGame;
            Player2.OnFinishedGame += Player_OnFinishedGame;

            MyConsole.WriteLine("Game \"21\" started".PadRight(50, '.'));

            int firstPlayerNumber;
            firstPlayerNumber = ChoosePlayer();

            if (firstPlayerNumber == 1)
                Play(Player1, Player2);
            else
                Play(Player2, Player1);

            MyConsole.WriteLine("Do you want to play again? Type 'Y' (yes) or 'N' (no) to make your choice");

            string? inputString = MyConsole.ReadLine()?.ToLower();

            if (string.IsNullOrEmpty(inputString) || !(new string[] { "y", "n" }).Contains(inputString!))
            {
                MyConsole.WriteLine("Invalid input command. Please try again");
            }
            else if (inputString!.Contains('y'))
            {
                ClearGame();
                StartGame();
            }

            Console.WriteLine("Game over! Do you want to save the output log file? Type 'Y' (yes) or 'N' (no) to make your choice");

            inputString = Console.ReadLine()?.ToLower();

            if (string.IsNullOrEmpty(inputString) || !(new string[] { "y", "n" }).Contains(inputString!))
            {
                Console.WriteLine("Invalid input command. Please try again");
            }
            else if (inputString!.Contains('y'))
            {
                MyConsole.WriteLine("Game over!");
                FileStream fs = new FileStream($"Homework4_GameLog_{DateTime.Now.Ticks}.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(MyConsole.Output.ToString());
                sw.Close();
            }

            ClearGame();
        }

        private void ClearGame()
        {
            _player1?.Dispose();
            _player2?.Dispose();
            _deck?.Dispose();
        }

        private void Play(Player firstPlayer, Player secondPlayer)
        {
            TakeCards(firstPlayer);
            TakeCards(secondPlayer);

            PlayerPlay(firstPlayer);
            PlayerPlay(secondPlayer);
        }

        private void PlayerPlay(Player currentPlayer)
        {
            if (CheckEndGame())
            {
                PrintWinner();
            }
            else
            {
                PrintPlayerDeck(currentPlayer);

                if (currentPlayer.Deck.Cost >= 21)
                {
                    currentPlayer.FinishGame();
                    return;
                }

                MyConsole.WriteLine($"{currentPlayer.Name}, do you want to take the next card? Type 'Y' (yes) or 'N' (no) to make your choice");
                
                string? inputString = MyConsole.ReadLine()?.ToLower();

                if (string.IsNullOrEmpty(inputString) || !(new string[] { "y", "n" }).Contains(inputString!))
                {
                    MyConsole.WriteLine("Invalid input command. Please try again");
                }
                else if (inputString!.Contains('y'))
                {
                    TakeOneCard(currentPlayer);
                    PlayerPlay(currentPlayer);
                }
                else
                {
                    currentPlayer.FinishGame();
                }
            }
        }

        private void PrintWinner()
        {
            MyConsole.WriteLine();
            PrintPlayerDeck(Player1);
            MyConsole.WriteLine();
            PrintPlayerDeck(Player2);
            MyConsole.WriteLine();

            if (Math.Abs(Player1.Deck.Cost - 21) == Math.Abs(Player2.Deck.Cost - 21))
            {
                if (Player1.Deck.Cost == 22 && Player2.Deck.Cost != 22)
                    PrintWin(Player1);
                else if (Player2.Deck.Cost == 22 && Player1.Deck.Cost != 22)
                    PrintWin(Player2);
                else
                    MyConsole.WriteLine("~~~ Tie game ~~~");
            }
            else if (Math.Abs(Player2.Deck.Cost - 21) > Math.Abs(Player1.Deck.Cost - 21))
            {
                PrintWin(Player1);
            }
            else
            {
                PrintWin(Player2);
            }
        }

        private bool CheckEndGame()
        {
            if (_winCosts.Contains(Player1.Deck.Cost) && _winCosts.Contains(Player2.Deck.Cost))
                return true;
            else if (_winCosts.Contains(Player1.Deck.Cost) && Player2.IsFinished)
                return true;
            else if (_winCosts.Contains(Player2.Deck.Cost) && Player1.IsFinished)
                return true;
            else if (Player1.Deck.Cost >= 21 && Player2.Deck.Cost >= 21)
                return true;
            else if (Player1.IsFinished && Player2.IsFinished)
                return true;

            return false;
        }

        private void TakeCards(Player player)
        {
            player.TakeCard(Deck.List[0]);
            player.TakeCard(Deck.List[1]);
            Deck.List.Remove(Deck.List[0]);
            Deck.List.Remove(Deck.List[1]);
            MyConsole.WriteLine($"{player.Name} took cards");
        }

        private void TakeOneCard(Player player)
        {
            player.TakeCard(Deck.List[0]);
            MyConsole.WriteLine($"{player.Name} took a card {GetCardDescription(Deck.List[0])}");
            Deck.List.Remove(Deck.List[0]);
        }

        private int ChoosePlayer()
        {
            MyConsole.WriteLine("Please choose the player to take cards first (press [1] to select Player1 or [2] to select Player2 (Computer))");
            
            string? inputPlayerNumberString = MyConsole.ReadLine();
            int firstPlayerNumber;

            if (!int.TryParse(inputPlayerNumberString, out firstPlayerNumber) || !(new int[] { 1, 2 }).Contains(firstPlayerNumber))
            {
                MyConsole.WriteLine("Invalid input player number parameter. Please try again");
                ChoosePlayer();
            }

            MyConsole.WriteLine($"Chosen first Player{firstPlayerNumber}");
            return firstPlayerNumber;
        }

        private string GetCardDescription(Card card) =>
            $"{card.Name.PadRight(2, ' ')} {card.Value.ToString().PadRight(2, ' ')} {card.Suit.ToString().PadRight(8, ' ')}";

        private void PrintPlayerDeck(Player player)
        {
            MyConsole.WriteLine($"{player.Name}. TotalCost of cards is [{player.Deck.Cost}]. The deck is:");
            foreach (Card card in player.Deck.List)
            {
                MyConsole.WriteLine($"{player.Deck.List.IndexOf(card) + 1}. {GetCardDescription(card)}");
            }
        }

        private void PrintWin(Player player) => MyConsole.WriteLine($"~~~!!!!! {player.Name} WINS !!!!!~~~");

        private void Player_OnFinishedGame(Player player)
        {
            MyConsole.WriteLine($"{player.Name} has finished the game");

            if (CheckEndGame())
                PrintWinner();
        }

        //Show the main deck list of cards
        private void PrintMainDeck()
        {
            MyConsole.WriteLine();
            MyConsole.WriteLine("The main deck is:");
            foreach (Card card in Deck.List)
            {
                MyConsole.WriteLine($"{Deck.List.IndexOf(card) + 1}. {GetCardDescription(card)}");
            }
        }
    }

    /// <summary>
    /// The logger clas for console
    /// </summary>
    public static class MyConsole
    {
        public static StringBuilder Output = new StringBuilder();

        public static void WriteLine()
        {
            Output.AppendLine();
            Console.WriteLine();
        }

        public static void WriteLine(string text)
        {
            Output.AppendLine(text);
            Console.WriteLine(text);
        }

        public static string? ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
