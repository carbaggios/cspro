namespace Homework4
{
    internal class Player : IDisposable
    {
        private bool _disposed = false;
        public bool AIMode { get; }
        public string Name { get; set; }
        public Deck Deck { get; }

        private bool _isFinished = false;
        public bool IsFinished { get { return _isFinished; } }

        public event Action<Player>? OnFinishedGame;

        public Player() 
            : this(isComputer: false){}

        public Player(bool isComputer)
        {
            AIMode = isComputer;
            Name = isComputer ? "Player2 (Computer)" : "Player1";
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

        /// <summary>
        /// Method <c>FinishGame</c> Setter for IsFinished property
        /// </summary>
        /// <param name="card">The card to taking</param>
        public void FinishGame()
        {
            _isFinished = true;
            OnFinishedGame?.Invoke(this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Name = null;
                }

                _disposed = true;
            }
        }
    }
}
