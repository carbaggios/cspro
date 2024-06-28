using System.Text;

namespace Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Task5();
            Task6();
        }

        /// <summary>
        /// 1. Реверс строки/масиву.Без додаткового масиву.Складність О(n)
        /// </summary>
        static void Task1()
        {
            Console.WriteLine("Task.1..............................................");
            Console.WriteLine("Реверс строки/масиву.Без додаткового масиву.Складність О(n)");

            char[] arr = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Є', 'Ж', 'З', 'И' };

            Console.WriteLine($"Original chars array string is '{new string(arr)}'");

            for (int i = 0; i < arr.Length / 2; i++)
            {
                char c = arr[i];
                arr[i] = arr[arr.Length - 1 - i];
                arr[arr.Length - 1 - i] = c;
            }

            // Show test result
            Console.WriteLine($"Reversed chars array string is '{new string(arr)}'");
        }

        /// <summary>
        /// 2. Фільтрування неприпустимих слів у строці.Має бути саме слова, а не частини слів.
        /// </summary>
        static void Task2()
        {
            Console.WriteLine();
            Console.WriteLine("Task.2..............................................");
            Console.WriteLine("Фільтрування неприпустимих слів у строці.Має бути саме слова, а не частини слів");

            const string censoringWords = "anal,anus,arse,ass,ballsack,balls,bastard,bitch,biatch,bloody,blowjob,blow job,bollock,bollok,boner,boob,bugger,bum,butt,buttplug,clitoris,cock,coon,crap,cunt,damn,dick,dildo,dyke,fag,feck,fellate,fellatio,felching,fuck,f u c k,fudgepacker,fudge packer,flange,Goddamn,God damn,hell,homo,jerk,jizz,knobend,knob end,labia,lmao,lmfao,muff,nigger,nigga,omg,penis,piss,poop,prick,pube,pussy,queer,scrotum,sex,shit,s hit,sh1t,slut,smegma,spunk,tit,tosser,turd,twat,vagina,wank,whore,wtf";
            const string text = "Wtf Contrary bLowjoB to popASSular belief, Bitch Lorem Ipsum is biatch not simply boner bastard random text. It has bollock roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, God damn looked up one of the homo more obscure knob knobend Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes whore from sections 1.10.32 and 1.10.33 of \"de Finibus lmfao Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32. wAnk";

            HashSet<string> censorWords = new HashSet<string>();

            foreach (string s in censoringWords.Split(','))
            {
                censorWords.Add(s);
            }

            StringBuilder sb = new StringBuilder(text);

            foreach (string s in censorWords)
            {
                sb.ReplaceWordsCaseInsensitive(s, new string('*', s.Length));
            }

            // Show test result
            Console.WriteLine($"Censored text is '{sb.ToString()}'");
        }

        /// <summary>
        /// 3. Генератор випадкових символів.На вхід кількість символів, на виході рядок з випадковими символами.
        /// </summary>
        static void Task3()
        {
            Console.WriteLine();
            Console.WriteLine("Task.3..............................................");
            Console.WriteLine("Генератор випадкових символів.На вхід кількість символів, на виході рядок з випадковими символами");

            Console.WriteLine("Enter the number of characters:");
            
            string? text = Console.ReadLine();
            int size;

            if (!int.TryParse(text, out size))
            {
                Console.WriteLine("Invalid input parameter (int)");
                Task3();
            }

            Random random = new Random();
            StringBuilder result = new StringBuilder(size);

            // set bounds to ASCII table
            int startIndex = 33;
            int finishIndex = 126;

            for (int i = 0; i < size; i++)
            {
                result.Append((char)random.Next(startIndex, finishIndex));
            }

            // Show test result
            Console.WriteLine($"Random characters string is '{result.ToString()}'");
        }

        /// <summary>
        /// 4. "Дірка" (пропущене число) у масиві. 
        ///    Масив довжини N у випадковому порядку заповнений цілими числами з діапазону від 0 до N.
        ///    Кожне число зустрічається в масиві не більше одного разу. Знайти відсутнє число (дірку). 
        ///    Є дуже простий та оригінальний спосіб вирішення.Складність алгоритму O(N). 
        ///    Використання додаткової пам'яті, пропорційної довжині масиву не допускається.
        /// </summary>
        static void Task4()
        {
            Console.WriteLine();
            Console.WriteLine("Task.4..............................................");
            Console.WriteLine("Дірка (пропущене число) у масиві");

            int[] ints = { 0, 6, 3, 2, 4, 1};
            int sumNaturals = ints.Length * (ints.Length + 1) / 2;

            // Show test result
            Console.WriteLine($"Missed number is '{sumNaturals - ints.Sum()}'");
        }

        /// <summary>
        /// 5. Найпростіше стиснення ланцюжка ДНК. 
        ///    Ланцюг ДНК у вигляді строки на вхід (кожен нуклеотид представлений символом "A", "C", "G", "T"). 
        ///    Два методи, один для компресії, інший для декомпресії.
        /// </summary>
        static void Task5()
        {
            Console.WriteLine();
            Console.WriteLine("Task.5..............................................");
            Console.WriteLine("Найпростіше стиснення ланцюжка ДНК");

            string dna = "ACGTCGATTAGCAGCTCGTCGATTAGCATAGC";
            byte[] encodeDna = Encode(dna);
            string decodeDna = Decode(encodeDna);

            // Show test result
            Console.WriteLine($"Original dna size (in bytes) is '{System.Text.ASCIIEncoding.Unicode.GetByteCount(dna)}'");
            Console.WriteLine($"Compressed dna size (in bytes) is '{encodeDna.Length}'");
            Console.WriteLine($"Decoded dna string is '{decodeDna}'");
        }

        private static int GetBit(byte b, int bitNumber) => (b >> bitNumber) & 1;

        private static string Decode(byte[] dna)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < dna.Length; i++)
            {
                var b1 = GetBit(dna[i], 0);
                var b2 = GetBit(dna[i], 1);
                var b3 = GetBit(dna[i], 2);
                var b4 = GetBit(dna[i], 3);
                var b5 = GetBit(dna[i], 4);
                var b6 = GetBit(dna[i], 5);
                var b7 = GetBit(dna[i], 6);
                var b8 = GetBit(dna[i], 7);

                char c1 = GetDnaCode((byte)(b8 << 1 | b7));
                char c2 = GetDnaCode((byte)(b6 << 1 | b5));
                char c3 = GetDnaCode((byte)(b4 << 1 | b3));
                char c4 = GetDnaCode((byte)(b2 << 1 | b1));
                
                result.Append(c1);
                result.Append(c2);
                result.Append(c3);
                result.Append(c4);
            }

            return result.ToString();
        }

        private static byte[] Encode(string dna)
        {
            byte[] result = new byte[dna.Length/4];
            int i = 0;

            foreach (string chunk in Enumerable.Range(0, dna.Length / 4).Select(i => dna.Substring(i * 4, 4)))
            {
                byte b1 = GetDnaCode(chunk[0]);
                byte b2 = GetDnaCode(chunk[1]);
                byte b3 = GetDnaCode(chunk[2]);
                byte b4 = GetDnaCode(chunk[3]);

                result[i] = (byte)(b4 | (b3 << 2) | (b2 << 4) | (b1 << 6));

                i++;
            }

            return result;
        }

        private static byte GetDnaCode(char code)
        {
            switch (code)
            {
                case 'A':
                    return 0b00;
                case 'C':
                    return 0b01;
                case 'G':
                    return 0b10;
                case 'T':
                    return 0b11;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static char GetDnaCode(byte code)
        {
            switch (code)
            {
                case 0b00:
                    return 'A';
                case 0b01:
                    return 'C';
                case 0b10:
                    return 'G';
                case 0b11:
                    return 'T';
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 6. Симетричне шифрування.Є строка на вхід, який має бути зашифрований. 
        ///    Ключ можна задати в коді або згенерувати та зберегти.
        ///    Два методи, шифрування та дешифрування.
        /// </summary>
        static void Task6()
        {
            Console.WriteLine();
            Console.WriteLine("Task.6..............................................");
            Console.WriteLine("Симетричне шифрування");

            string input = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            string key = "Hj%khskjhskj@dhJ3HAnb*bdm&n$ujew3736";          

            char[] chars = input.ToCharArray();

            string encrypted = Aes(chars, key);
            string decrypted = Aes(encrypted.ToCharArray(), key);

            // Show test result
            Console.WriteLine($"Encrypted input string is '{encrypted}'");
            Console.WriteLine($"Decrypted input string is '{decrypted}'");
        }

        private static string Aes(char[] input, string key)
        {
            int keyLength = key.Length;

            for (int i = 0; i < input.Length; i++)
            {
                input[i] = (char)(input[i] ^ key[i % keyLength]);
            }

            return new string(input);
        }
    }

    /// <summary>
    /// Extention method ReplaceWordsCaseInsensitive for StringBuilder
    /// </summary>
    public static class SystemTextExtentionMethods
    {
        public static StringBuilder ReplaceWordsCaseInsensitive(this StringBuilder builder, string oldValue, string newValue)
        {
            builder.Append(' ');
            int index = builder.ToString().ToLower().IndexOf((oldValue + ' ').ToLower());
            while (index != -1)
            {
                var token = builder.ToString().Substring(index, oldValue.Length);
                builder.Replace(token, newValue);
                index = builder.ToString().ToLower().IndexOf(oldValue.ToLower());
            }
            return builder.Remove(builder.Length-1, 1);
        }
    }
}