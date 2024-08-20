using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Homework11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new List<object>() 
            {
                "Hello",
                new Book() 
                { 
                    Author = "Terry Pratchett", 
                    Name = "Guards! Guards!", 
                    Pages = 810 
                },
                new List<int>() {4, 6, 8, 2},
                new string[] {"Hello inside array"},
                new Film() 
                { 
                    Author = "Martin Scorsese", 
                    Name= "The Departed", 
                    Actors = new List<Actor>() 
                    {
                        new Actor() { Name = "Jack Nickolson", Birthdate = new DateTime(1937, 4, 22)},
                        new Actor() { Name = "Leonardo DiCaprio", Birthdate = new DateTime(1974, 11, 11)},
                        new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)}
                    }
                },
                new Film() 
                { 
                    Author = "Gus Van Sant", 
                    Name = "Good Will Hunting", 
                    Actors = new List<Actor>() 
                    {
                        new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)},
                        new Actor() { Name = "Robin Williams", Birthdate = new DateTime(1951, 8, 11)},
                    }
                },
                new Book() 
                { 
                    Author = "Stephen King", 
                    Name="Finders Keepers", 
                    Pages = 200
                },
                "Leonardo DiCaprio"
            };

            Console.WriteLine("1. Виведіть усі елементи, крім ArtObjects:");
            Console.WriteLine(string.Join(',', data.Where(x => x.GetType().BaseType != typeof(ArtObject))));
            Console.WriteLine();

            Console.WriteLine("2. Виведіть імена всіх акторів:");
            Console.WriteLine(
                string.Join(',', 
                    data.Where(x => x.GetType() == typeof(Film))
                        .Select(x => ((Film)x).Actors)
                        .SelectMany(x => x)
                        .Select(x => x.Name)
                        .Distinct()
                    )
                );
            Console.WriteLine();

            Console.WriteLine("3. Виведіть кількість акторів, які народилися в серпні:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Film))
                        .Select(x => ((Film)x).Actors)
                        .SelectMany(x => x)
                        .Where(x => x.Birthdate.Month == 8)
                        .Count()
                    )
                );
            Console.WriteLine();

            Console.WriteLine("4. Виведіть два найстаріших імена акторів:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Film))
                        .Select(x => ((Film)x).Actors)
                        .SelectMany(x => x)
                        .OrderBy(x => x.Birthdate)
                        .Take(2)
                        .Select(x => x.Name)
                    )
                );
            Console.WriteLine();

            Console.WriteLine("5. Вивести кількість книг на авторів:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Book))
                        .GroupBy(x => ((Book)x).Author)
                        .Select(x => new { Author = x.Key, Count = x.Count() })
                    )
                );
            Console.WriteLine();

            Console.WriteLine("6. Виведіть кількість книг на одного автора та фільмів на одного режисера:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Book))
                        .GroupBy(x => ((Book)x).Author)
                        .Select(x => new { Author = x.Key, Count = x.Count() })
                        .Union(
                        data.Where(x => x.GetType() == typeof(Film))
                            .GroupBy(x => ((Film)x).Author)
                            .Select(x => new { Author = x.Key, Count = x.Count() })
                        )
                    )
                );
            Console.WriteLine();

            Console.WriteLine("7. Виведіть, скільки різних букв використано в іменах усіх акторів:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Film))
                        .Select(x => ((Film)x).Actors)
                        .SelectMany(x => x)
                        .SelectMany(x => x.Name.ToLower().ToArray().Where(c => c != ' '))
                        .Distinct()
                    )
                );
            Console.WriteLine();

            Console.WriteLine("8. Виведіть назви всіх книг, упорядковані за іменами авторів і кількістю сторінок:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Book))
                        .Select(x => new { Author = ((Book)x).Author, Pages = ((Book)x).Pages, Name =((Book)x).Name })
                        .OrderBy(x => x.Author)
                        .ThenBy(x => x.Pages)
                    )
                );
            Console.WriteLine();

            Console.WriteLine("9. Виведіть ім'я актора та всі фільми за участю цього актора:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Film))
                    .SelectMany(x => ((Film)x).Actors, (film, actor) => new { ActorName = actor.Name, FilmName = ((Film)film).Name })
                    )
                );
            Console.WriteLine();

            Console.WriteLine("10. Виведіть суму загальної кількості сторінок у всіх книгах і всі значення int у всіх послідовностях у даних:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Book))
                        .Sum(x => ((Book)x).Pages)
                    )
                );
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(List<int>))
                    .Select(x => ((List<int>)x).Count)
                    )
                );
            Console.WriteLine();

            Console.WriteLine("11. Отримати словник з ключем - автор книги, значенням - список авторських книг:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Book))
                        .Select(x => new { Author = ((Book)x).Author, Books = new List<Book> { (Book)x } })
                        .ToDictionary(Key => Key.Author, Value => Value.Books)
                    )
                );
            Console.WriteLine();

            Console.WriteLine("12. Вивести всі фільми \"Метт Деймон\", за винятком фільмів з акторами, імена яких представлені в даних у вигляді рядків:");
            Console.WriteLine(
                string.Join(',',
                    data.Where(x => x.GetType() == typeof(Film))
                    .SelectMany(x => ((Film)x).Actors, (film, actor) => new { ActorName = actor.Name, Film = ((Film)film) })
                    .Where(x => x.ActorName == "Matt Damon" 
                        && !x.Film.Actors.Any(a => data.Where(x => x.GetType() == typeof(string)).Select(x => x).Contains(a.Name))
                        )
                    .Select(x => new { Actor = x.ActorName, Film = x.Film.Name })
                    )
                );
            Console.WriteLine();
        }
    }

    class Actor
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }

    abstract class ArtObject
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }

    class Film : ArtObject
    {
        public int Length { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
    }

    class Book : ArtObject
    {
        public int Pages { get; set; }
    }
}
