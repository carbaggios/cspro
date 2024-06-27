namespace Homework4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Згенерувати впорядковану колоду карт
            Console.WriteLine("1. Згенерувати впорядковану колоду карт".PadRight(50, '.'));            
            Deck deck = new Deck(false);
            
            foreach (Card card in deck.List)
            {
                Console.WriteLine($"{deck.List.IndexOf(card) + 1}. {card.Name} {card.Value} {card.Suit}");
            }

            // 2. Перемішати колоду карт
            Console.WriteLine();
            Console.WriteLine("2. Перемішати колоду карт".PadRight(50, '.'));
            deck.Shuffle();

            foreach (Card card in deck.List)
            {
                Console.WriteLine($"{deck.List.IndexOf(card) + 1}. {card.Name} {card.Value} {card.Suit}");
            }

            // 3. Знайти позиції всіх тузів у колоді
            Console.WriteLine();
            Console.WriteLine("3. Знайти позиції всіх тузів у колоді".PadRight(50, '.'));

            foreach (Card card in deck.List.Where(a => a.Name == "A"))
            {
                Console.WriteLine($"{card.Name} {card.Value.ToString().PadRight(2, ' ')} {card.Suit.ToString().PadRight(8,' ')} found in position {deck.List.IndexOf(card) + 1}");
            }

            // 4. Перемістити всі пікові карти на початок колоди
            Console.WriteLine();
            Console.WriteLine("4. Перемістити всі пікові карти на початок колоди".PadRight(50, '.'));

            deck.List = deck.List
                .OrderBy(s => (s.Suit == Suit.Spades ? 0 : deck.List.IndexOf(s) + 9))
                .ToList();

            foreach (Card card in deck.List)
            {
                Console.WriteLine($"{deck.List.IndexOf(card) + 1}. {card.Name} {card.Value} {card.Suit}");
            }

            // 5. Відсортувати колоду
            Console.WriteLine();
            Console.WriteLine("5. Відсортувати колоду".PadRight(50, '.'));

            deck.Arrange();

            foreach (Card card in deck.List)
            {
                Console.WriteLine($"{deck.List.IndexOf(card) + 1}. {card.Name} {card.Value} {card.Suit}");
            }

            //6. Створіть консольну програму для карткової гри «21» з простими правилами
            //   1. у грі 36 карт
            //   2. вартість карток в окулярах: Туз - 11 очок; Король – 4 очки; Леді / Дама - 3 бали; Джек / Валет – 2 очки; Інші карти за їх номіналом
            //   3. 2 гравці(ви та комп'ютер)
            //   4. мета гри - набрати 21 очко
            //   5. спочатку ви повинні ввести, хто отримує перші картки
            //   6. ви та комп'ютер отримуєте 2 карти відразу
            //   7. після цього кожен із вас вирішить, чого ви хочете ? отримати ще одну карту або перестати отримувати карти(залежить від того, хто першим почав гру)
            //   8. якщо один з вас набрав 21 очко або 2 тузи, гра закінчена і виграє гравець з 21 очком або 2 тузами
            //   9. якщо один із гравців набирає більше 21 очка, гра закінчується, але в кінці раунду
            //   10. якщо у вас обох більше 21 очка гра закінчена та виграє гравець з меншою кількістю очок
            //   11. має бути можливість продовжувати грати
            //   12. повинна статистика за результатами всіх зіграних ігор
            Console.WriteLine();
            Console.WriteLine("6. Створіть консольну програму для карткової гри «21» з простими правилами".PadRight(50, '.'));

            Game game = new Game();
            game.Play();
        }
    }
}
