using System.Numerics;

namespace Homework4
{
    internal class Game
    {
        private Player _player1;
        private Player _player2;
        private Deck _deck;

        private readonly int[] _winCosts = new int[] { 21, 22 };

        public Game() 
        {
            _player1 = new Player();
            _player2 = new Player(isComputer: true);
            _deck = new Deck();
        }

        public void Play()
        {
            Console.WriteLine("Game \"21\" started".PadRight(50, '.'));

            int firstPlayerNumber;
            firstPlayerNumber = ChoosePlayer();

            if (firstPlayerNumber == 1)
            {
                TakeCards(_player1);
                TakeCards(_player2);
            }
            else
            {
                TakeCards(_player2);
                TakeCards(_player1);
            }


            /*
            Console.WriteLine($"{_player1.Name}. TotalCost of cards is [{_player1.Deck.Cost}]. The deck is:");
            foreach (Card card in _player1.Deck.List)
            {
                Console.WriteLine($"{_player1.Deck.List.IndexOf(card) + 1}. {card.Name.PadRight(2, ' ')} {card.Value.ToString().PadRight(2, ' ')} {card.Suit.ToString().PadRight(8, ' ')}");
            }

            Console.WriteLine($"{_player2.Name}. TotalCost of cards is [{_player2.Deck.Cost}]. The deck is:");
            foreach (Card card in _player2.Deck.List)
            {
                Console.WriteLine($"{_player2.Deck.List.IndexOf(card) + 1}. {card.Name.PadRight(2, ' ')} {card.Value.ToString().PadRight(2, ' ')} {card.Suit.ToString().PadRight(8, ' ')}");
            }

            Console.WriteLine();
            Console.WriteLine($"{_player1.Name}, do you want to take another card? Press 'Yes' or 'No' to make your choice");
            */
        }

        private bool CheckWinner(ref string message)
        {
            if (_winCosts.Contains(_player1.Deck.Cost) && _winCosts.Contains(_player2.Deck.Cost))
            {
                Console.WriteLine("Drawn game");
                return true;
            }
            else if (_winCosts.Contains(_player1.Deck.Cost) || _winCosts.Contains(_player2.Deck.Cost))
            {
                Console.WriteLine("PlayerN wins");
                return true;
            }
            return false;
        }

        private void TakeCards(Player player)
        {
            player.TakeCard(_deck.List[0]);
            player.TakeCard(_deck.List[1]);
            _deck.List.Remove(_deck.List[0]);
            _deck.List.Remove(_deck.List[1]);
            Console.WriteLine($"Player{player.Name} took a cards");
        }

        private int ChoosePlayer()
        {
            Console.WriteLine("Please choose the player to take a cards first (press [1] for Player1 (You) or [2] for Player2 (Computer))");
            
            string inputPlayerNumberString = Console.ReadLine();
            int firstPlayerNumber;

            if (!int.TryParse(inputPlayerNumberString, out firstPlayerNumber) || !(new int[] { 1, 2 }).Contains(firstPlayerNumber))
            {
                Console.WriteLine("Invalid input player number parameter. Please try again");
                ChoosePlayer();
            }

            Console.WriteLine($"Chosen first Player{firstPlayerNumber}");
            return firstPlayerNumber;
        }
    }
}
