using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading;
using System.Linq;
using System.Text;

namespace Homework13
{
    abstract class ThreadProcessorBase
    {
        protected readonly Thread[] _threads;
        protected readonly int[] _array;
        protected readonly char[] _charArray;

        public ThreadProcessorBase(int[] array, int threadCount)
        {
            _threads = new Thread[threadCount];
            _array = array;
        }

        public ThreadProcessorBase(char[] array, int threadCount)
        {
            _threads = new Thread[threadCount];
            _charArray = array;
        }

        public virtual void Run()
        {
            for (int i = 0; i < _threads.Length; i++)
            {
                _threads[i] = new Thread(Process!);
            }

            for (int i = 0; i < _threads.Length; i++)
            {
                _threads[i].Start(i);
            }

            for (int i = 0; i < _threads.Length; i++)
            {
                _threads[i].Join();
            }
        }

        protected virtual Span<int> GetSpan(int threadNumber)
        {
            var itemsByThread = _array.Length / _threads.Length;

            return threadNumber == _threads.Length
                               ? _array[(threadNumber * itemsByThread)..]
                               : _array.AsSpan(threadNumber * itemsByThread, itemsByThread);
        }

        protected virtual Span<char> GetCharSpan(int threadNumber)
        {
            var itemsByThread = _charArray.Length / _threads.Length;

            return threadNumber == _threads.Length
                               ? _charArray[(threadNumber * itemsByThread)..]
                               : _charArray.AsSpan(threadNumber * itemsByThread, itemsByThread);
        }

        protected abstract void Process(object threadNumber);
    }

    abstract class RandomThreadProcessor : ThreadProcessorBase
    {
        protected readonly Random[] _randoms;

        protected RandomThreadProcessor(int[] array, int threadCount)
            : base(array, threadCount)
        {
            var random = new Random();
            _randoms = new Random[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                _randoms[i] = new Random(random.Next());
            }
        }

        protected RandomThreadProcessor(char[] array, int threadCount)
            : base(array, threadCount)
        {
            var random = new Random();
            _randoms = new Random[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                _randoms[i] = new Random(random.Next());
            }
        }
    }

    class RandomIntThreadProcessor : RandomThreadProcessor
    {
        public RandomIntThreadProcessor(int[] array, int threadCount)
            : base(array, threadCount)
        {
        }

        protected override void Process(object threadNumber)
        {
            var num = (int)threadNumber;
            var span = GetSpan(num);
            var random = _randoms[num];

            for (int i = 0; i < span.Length; i++)
            {
                span[i] = random.Next();
            }
        }
    }

    class MinThreadProcessor : ThreadProcessorBase
    {
        public long[] Results { get; }
        public MinThreadProcessor(int[] array, int threadCount)
            : base(array, threadCount)
        {
            Results = new long[threadCount];
        }

        protected override void Process(object threadNumber)
        {
            var num = (int)threadNumber;
            var span = GetSpan(num);

            Results[num] = span.ToArray().Min();
        }
    }

    class MaxThreadProcessor : ThreadProcessorBase
    {
        public long[] Results { get; }
        public MaxThreadProcessor(int[] array, int threadCount)
            : base(array, threadCount)
        {
            Results = new long[threadCount];
        }

        protected override void Process(object threadNumber)
        {
            var num = (int)threadNumber;
            var span = GetSpan(num);

            Results[num] = span.ToArray().Max();
        }
    }

    class SumThreadProcessor : ThreadProcessorBase
    {
        public long[] Results { get; }
        public SumThreadProcessor(int[] array, int threadCount)
            : base(array, threadCount)
        {
            Results = new long[threadCount];
        }

        protected override void Process(object threadNumber)
        {
            var num = (int)threadNumber;
            var span = GetSpan(num);

            for (int i = 0; i < span.Length; i++)
            {
                Results[num] += span[i];
            }
        }
    }

    class AvgThreadProcessor : ThreadProcessorBase
    {
        public double[] Results { get; }
        public AvgThreadProcessor(int[] array, int threadCount)
            : base(array, threadCount)
        {
            Results = new double[threadCount];
        }

        protected override void Process(object threadNumber)
        {
            var num = (int)threadNumber;
            var span = GetSpan(num);

            Results[num] = span.ToArray().Average();
        }
    }

    class CopyThreadProcessor : ThreadProcessorBase
    {
        private int _copyFrom;
        private int _copyTo;

        public int[] Results { get; }
        public CopyThreadProcessor(int[] array, int threadCount, int copyFrom, int copyTo)
            : base(array, threadCount)
        {
            int[] check = [0, array.Length];
            if (copyFrom >= copyTo && check.Contains(copyFrom) && check.Contains(copyTo))
                throw new ArgumentException();

            _copyFrom = copyFrom;
            _copyTo = copyTo;

            Results = new int[copyTo - copyFrom + 1];
        }

        protected override void Process(object threadNumber)
        {
            var num = (int)threadNumber;
            var span = GetSpan(num);

            int lastIndex = (num + 1) * span.Length - 1;
            int firstIndex = lastIndex + 1 - span.Length;

            if ((_copyFrom >= firstIndex && _copyFrom <= lastIndex) || (_copyTo >= firstIndex))
            {
                int startSpanIndex = _copyFrom - firstIndex < 0 ? 0 : _copyFrom - firstIndex;
                int finishSpanIndex = lastIndex - _copyTo < 0 ? span.Length - 1 : _copyTo - span.Length * num;

                for (int i = startSpanIndex; i <= finishSpanIndex; i++)
                {
                    Results[i + firstIndex - _copyFrom] = span[i];
                }
            }
        }
    }

    class FrequencyThreadProcessor : RandomThreadProcessor
    {
        // set bounds to ASCII table
        int _startIndex = 32;
        int _finishIndex = 126;

        public FrequencyThreadProcessor(char[] array, int threadCount)
            : base(array, threadCount)
        {
        }

        protected override void Process(object threadNumber)
        {
            var num = (int)threadNumber;
            var span = GetCharSpan(num);
            var random = _randoms[num];

            for (int i = 0; i < span.Length; i++)
            {
                span[i] = (char)random.Next(_startIndex, _finishIndex);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[100_000_000];
            int threadCount = 1;

            Console.WriteLine($"Введіть кількість потоків для виконання завдань:");
            int.TryParse(Console.ReadLine(), out threadCount);
            Console.WriteLine($"Обрано кількість потоків: {threadCount}");
            Console.WriteLine();

            Console.WriteLine($"Генерація випадкового масиву");
            var sw = Stopwatch.StartNew();
            var randomProc = new RandomIntThreadProcessor(arr, threadCount);
            randomProc.Run();
            sw.Stop();
            Console.WriteLine($"Elapsed - {sw.Elapsed.ToString()}");
            Console.WriteLine();

            Console.WriteLine($"Мін. в масиві");
            sw.Restart();
            var minProc = new MinThreadProcessor(arr, threadCount);
            minProc.Run();
            sw.Stop();
            var minArrayValue = minProc.Results.Min();
            Console.WriteLine($"Elapsed - {sw.Elapsed.ToString()} - Value = {minArrayValue}");
            Console.WriteLine();

            Console.WriteLine($"Макс. в масиві");
            sw.Restart();
            var maxProc = new MaxThreadProcessor(arr, threadCount);
            maxProc.Run();
            sw.Stop();
            var maxArrayValue = maxProc.Results.Max();
            Console.WriteLine($"Elapsed - {sw.Elapsed.ToString()} - Value = {maxArrayValue}");
            Console.WriteLine();

            Console.WriteLine($"Сума елементів масиву");
            sw.Restart();
            var sumProc = new SumThreadProcessor(arr, threadCount);
            sumProc.Run();
            sw.Stop();
            var sumArrayValue = sumProc.Results.Sum();
            Console.WriteLine($"Elapsed - {sw.Elapsed.ToString()} - Value = {sumArrayValue}");
            Console.WriteLine();

            Console.WriteLine($"Середнє в масиві");
            sw.Restart();
            var avgProc = new AvgThreadProcessor(arr, threadCount);
            avgProc.Run();
            sw.Stop();
            var avgArrayValue = avgProc.Results.Sum();
            Console.WriteLine($"Elapsed - {sw.Elapsed.ToString()} - Value = {avgArrayValue}");
            Console.WriteLine();

            Console.WriteLine($"Копіювати частину масиву");
            sw.Restart();
            var copyProc = new CopyThreadProcessor(arr, threadCount, copyFrom: 11_452_587, copyTo: 66_721_217);
            copyProc.Run();
            sw.Stop();
            var copyArrayValue = copyProc.Results;
            Console.WriteLine($"Elapsed - {sw.Elapsed.ToString()} - Copied array count = {copyArrayValue.Count()}");
            Console.WriteLine();

            /*
            // ASCII 32 - 126
            var charArr = new char[10_000_000];
            Console.WriteLine($"Частотний словник символів у якійсь довгій книзі/тексті");
            Console.WriteLine($"Генерація випадкового масиву символів");
            sw.Restart();
            var frequencyProc = new FrequencyThreadProcessor(charArr, threadCount);
            frequencyProc.Run();
            sw.Stop();
            Console.WriteLine($"Elapsed - {sw.Elapsed.ToString()} - Array count = {charArr.Count()}");
            Console.WriteLine();
            */

            /*          
            Частотний словник слів у якійсь довгій книзі/тексті
            */
        }
    }
}
