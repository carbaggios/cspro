namespace Homework10
{
    using System.Linq;
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Виведіть усі числа від 10 до 50 через кому:");
            Console.WriteLine(string.Join(',', Enumerable.Range(10, 41)));
            Console.WriteLine();

            Console.WriteLine("2. Виведіть лише ті числа від 10 до 50, які можна поділити на 3:");
            Console.WriteLine(string.Join(',', Enumerable.Range(10, 41).Where(x => x % 3 == 0)));
            Console.WriteLine();

            Console.WriteLine("3. Виведіть слово \"Linq\" 10 разів:");
            Console.WriteLine(string.Join(',', Enumerable.Repeat("Linq", 10)));
            Console.WriteLine();

            Console.WriteLine("4. Вивести всі слова з буквою «a» в рядку «aaa;abb;ccc;dap»:");
            Console.WriteLine(string.Join(',', new string("aaa;abb;ccc;dap").Split(';').Where(x => x.Contains('a'))));
            Console.WriteLine();

            Console.WriteLine("5. Виведіть кількість літер «a» у словах з цією літерою в рядку «aaa;abb;ccc;dap» через кому:");
            Console.WriteLine(string.Join(',', new string("aaa;abb;ccc;dap").Split(';').Where(x => x.Contains('a')).Select(x => x.ToArray().Count(c => c.Equals('a')))));
            Console.WriteLine();

            Console.WriteLine("6. Вивести true, якщо слово \"abb\" існує в рядку \"aaa;xabbx;abb;ccc;dap\", інакше false:");
            Console.WriteLine(string.Join(',', new string("aaa;xabbx;abb;ccc;dap").Split(';').Select(x => x.Contains("abb"))));
            Console.WriteLine();

            Console.WriteLine("7. Отримати найдовше слово в рядку \"aaa;xabbx;abb;ccc;dap\":");
            Console.WriteLine(new string("aaa;xabbx;abb;ccc;dap").Split(';').MaxBy(x => x.Length));
            Console.WriteLine();

            Console.WriteLine("8. Обчислити середню довжину слова в рядку \"aaa;xabbx;abb;ccc;dap\":");
            Console.WriteLine(new string("aaa;xabbx;abb;ccc;dap").Split(';').Average(x => x.Length));
            Console.WriteLine();

            Console.WriteLine("9. Вивести найкоротше слово в рядку \"aaa;xabbx;abb;ccc;dap;zh\" у зворотному порядку.");
            Console.WriteLine(string.Concat(new string("aaa;xabbx;abb;ccc;dap;zh").Split(';').MinBy(x => x.Length)!.Reverse()));
            Console.WriteLine();

            Console.WriteLine("10. Вивести true, якщо в першому слові, яке починається з \"aa\", усі літери \"b\"(За винятком \"aa\"), інакше false \"baaa;aabb;aaa;xabbx;abb;ccc;dap;zh\"");
            Console.WriteLine(string.Join(',', new string("baaa;aabb;aaa;xabbx;abb;ccc;dap;zh").Split(';').Select(x => x.Substring(0,2) == "aa" && x.Replace("aa", "bb") == new string('b',x.Length))));
            Console.WriteLine();

            Console.WriteLine("11. Вивести останнє слово в послідовності, за винятком перших двох елементів, які закінчуються на \"bb\"(використовуйте послідовність із 10 завдання) \"baaa;aabb;aaa;xabbx;abb;ccc;dap;zh\"");
            Console.WriteLine(string.Join(',', new string("baaa;aabb;aaa;xabbx;abb;ccc;dap;zh").Split(';').Last(x => x.EndsWith("bb"))));
            Console.WriteLine();
        }
    }
}
