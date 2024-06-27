namespace Homework4
{
    public struct Card
    {
        public string Name { get; set; }
        public Suit Suit { get; set; }

        /// <summary>
        /// The cost of the card 
        /// </summary>
        public int Value { get; set; }
        public int Index { get; set; }
    }

    /// <summary>
    /// The suit of cards: clubs (♣), diamonds (♦), hearts (♥) and spades (♠).
    /// </summary>
    public enum Suit
    {
        /// <summary>
        /// 0 - ♣ - Clubs
        /// </summary>
        Clubs,
        /// <summary>
        /// 1 - ♦ - Diamonds
        /// </summary>
        Diamonds,
        /// <summary>
        /// 2 - ♠ - Spades
        /// </summary>
        Spades,
        /// <summary>
        /// 3 - ♥ - Hearts
        /// </summary>
        Hearts
    }
}
