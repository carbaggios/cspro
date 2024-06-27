namespace Homework4
{
    public class Deck
    {
        /// <summary>
        /// Contains the list of cards
        /// </summary>
        public List<Card> List { get; set; }

        /// <summary>
        /// The total cost of the deck of cards
        /// </summary>
        public int Cost { get { return List.Sum(s => s.Value); } }

        /// <summary>
        /// Create the list of cards in shuffle mode
        /// </summary>
        public Deck() 
        {
            List = CreateList(shuffle: true);
        }

        /// <summary>
        /// Create the list of cards in deckType mode
        /// </summary>
        /// <param name="deckType">Specifies the creation method for main or user mode of deck</param>
        public Deck(DeckType deckType)
        {
            if (deckType == DeckType.Main)
                List = CreateList(shuffle: true);
            else
                List = new List<Card>();
        }

        /// <summary>
        /// Create the list of cards using the shuffle parameter
        /// </summary>
        /// <param name="shuffle">Specifies the creation method for sorting mode: false - arrange the elements, true - shuffle the elements in the deck of a sequence in random mode</param>
        public Deck(bool shuffle)
        {
            List = CreateList(shuffle);
        }

        private List<Card> CreateList(bool shuffle = false)
        {
            List<Card> cards = new List<Card>();
            for (int i = 6; i <= 14; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    cards.Add(BuildCard(i, suit));
                }
            }

            if (shuffle)
                Shuffle(ref cards);

            return cards;
        }

        /// <summary>
        /// Method <c>Shuffle</c> Shuffle the elements in the deck of a sequence in random mode
        /// </summary>
        public void Shuffle()
        {
            List<Card> deck = List;
            Shuffle(ref deck);
        }

        /// <summary>
        /// Method <c>Arrange</c> Sorts the elements in the deck of a sequence in ascending order according to card index and suit
        /// </summary>
        public void Arrange() => 
            List = List.OrderBy(s => (s.Index, s.Suit)).ToList();

        private void Shuffle(ref List<Card> cards)
        {
            Random r = new Random((int)DateTime.Now.Ticks);

            for (int n = cards.Count - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);

                Card tmp = cards[n];
                cards[n] = cards[k];
                cards[k] = tmp;
            }
        }

        private Card BuildCard(int index, Suit suit)
        {
            int value = -1;
            string name = string.Empty;

            if (index >= 6 && index <= 10)
            {
                value = index;
                name = index.ToString();
            }
            else if (index == 11)
            {
                value = 2;
                name = "J";
            }
            else if (index == 12)
            {
                value = 3;
                name = "Q";
            }
            else if (index == 13)
            {
                value = 4;
                name = "K";
            }
            else if (index == 14)
            {
                value = 11;
                name = "A";
            }

            return new Card()
            {
                Suit = suit,
                Value = value,
                Name = name,
                Index = index
            };
        }
    }

    /// <summary>
    /// The type of deck
    /// </summary>
    public enum DeckType
    {
        /// <summary>
        /// Play deck
        /// </summary>
        Main,
        /// <summary>
        /// User deck
        /// </summary>
        User
    }
}
