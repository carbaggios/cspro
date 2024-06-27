namespace Homework4
{
    internal class Player
    {
        public bool AIMode { get; }
        public string Name { get; set; }
        public Deck Deck { get; }
        
        public Player()
        {
            AIMode = false;
            Name = "Player1";
            Deck = new Deck(DeckType.User);
        }
        public Player(string name)
        {
            AIMode = false;
            Name = name;
            Deck = new Deck(DeckType.User);
        }

        public Player(bool isComputer)
        {
            AIMode = isComputer;
            Name = "Player2 (Computer)";
            Deck = new Deck(DeckType.User);
        }

        /// <summary>
        /// Method <c>TakeCard</c> Adding a card to the user deck from the main deck
        /// </summary>
        /// <param name="card">The card to taking</param>
        public void TakeCard(Card card)
        {
            Deck.List.Add(card);
        }
    }
}
