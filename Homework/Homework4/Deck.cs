namespace Homework4
{
    public class Deck
    {
        public List<Card> List { get; set; }

        public Deck() 
        {
            List = CreateList(shuffle: true);
        }

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

        public void Shuffle()
        {
            List<Card> deck = List;
            Shuffle(ref deck);
        }

        public void Arrange() => 
            List = List.OrderBy(s => (s.Index, s.Suit)).ToList();

        private void Shuffle(ref List<Card> cards)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            //Random r = new Random(new Random().Next(int.MaxValue));

            for (int n = cards.Count - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);

                Card temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
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
}
