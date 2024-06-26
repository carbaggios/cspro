namespace Homework4
{
    public struct Card
    {
        public string Name { get; set; }
        public Suit Suit { get; set; }
        public int Value { get; set; }
        public int Index { get; set; }
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Spades,
        Hearts
    }
}
