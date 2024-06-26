namespace Homework4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck(false);
            foreach (Card card in deck.List)
            {
                Console.WriteLine($"{card.Name} {card.Value} {card.Suit}");
            }

            Console.WriteLine();

            deck.Shuffle();
            foreach (Card card in deck.List)
            {
                Console.WriteLine($"{card.Name} {card.Value} {card.Suit}");
            }

            Console.WriteLine();

            deck.Arrange();
            foreach (Card card in deck.List)
            {
                Console.WriteLine($"{card.Name} {card.Value} {card.Suit}");
            }
        }
    }
}
