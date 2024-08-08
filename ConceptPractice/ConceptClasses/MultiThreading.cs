using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConceptPractice.ConceptClasses
{
    public class MultiThreading
    {

        #region without threading
        public static int primeNumberCount = 0;
        public static int nextNumber = 0;
        public static int maxNumber = 0;
        private readonly static Object obj = new object();
        public static int CheckPrimeNumbersWithoutThreading(int x)
        {
            int primeNumberCount = 0;
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 1; i <= x; i++)
            {
                if (CheckPrime(i))
                {
                    primeNumberCount++;
                }
            }
            sw.Stop();
            Console.WriteLine("Total time taken in milliseconds " + sw.ElapsedMilliseconds);
            return primeNumberCount;
        }
        #endregion

        #region With threading in batch
        public static int CheckPrimeNumbersWithThreading(int x)
        {
            int primeNumberCount = 0;
            int threadCount = 10;
            int batchSize = x / threadCount;

            int[] results = new int[threadCount];
            Thread[] threads = new Thread[threadCount];
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < threadCount; i++)
            {
                int currentIndex = i;
                int start = currentIndex * batchSize + 1;
                int end = (currentIndex == threadCount - 1) ? x : start + batchSize - 1;

                threads[currentIndex] = new Thread(() =>
                {
                    results[currentIndex] = BatchExecution(currentIndex + 1, start, end);
                });
                threads[currentIndex].Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            sw.Stop();

            Console.WriteLine("Total time taken in milliseconds " + sw.ElapsedMilliseconds);
            foreach (var t in results)
            {
                primeNumberCount += t;
            }
            return primeNumberCount;
        }

        public static int BatchExecution(int bn, int sn, int ln)
        {
            Stopwatch sw = Stopwatch.StartNew();
            int totalPrimeNumbers = 0;
            for (int i = sn; i <= ln; i++)
            {
                if (CheckPrime(i))
                {
                    totalPrimeNumbers++;
                }
            }
            sw.Stop();
            Console.WriteLine("Time taken in milliseconds by batch number " + (bn) + " " + sw.ElapsedMilliseconds);
            return totalPrimeNumbers;

        }
        #endregion

        #region Threadig without batching 
        public static int CheckPrimeNumbersWithThreadingWithoutBatching(int x)
        {
            maxNumber = x;
            int threadCount = 10;
            Thread[] threads = new Thread[threadCount];
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < threadCount; i++)
            {
                int currentNumber = i ;
                threads[currentNumber] = new Thread(()=>DoWork(currentNumber + 1));
                threads[currentNumber].Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            sw.Stop();

            Console.WriteLine("Total time taken in milliseconds " + sw.ElapsedMilliseconds);

            return primeNumberCount;
        }

        public static void DoWork(int i)
        {
            Stopwatch sw = Stopwatch.StartNew();
            while (true)
            {
                int number;
                lock (obj)
                {
                    if (maxNumber < nextNumber)
                        break;
                    number = nextNumber++;
                    if (CheckPrime(number))
                        primeNumberCount++;
                }
            }
            sw.Stop();
            Console.WriteLine("Total time taken for thread "+i +" "+ sw.ElapsedMilliseconds);
        }

        #endregion

        #region Check prime method
        public static bool CheckPrime(int x)
        {
            if (x < 2)
                return false;
            if (x == 2 || x == 3)
                return true;
            if (x % 2 == 0)
                return false;
            for (int i = 3; i <= Math.Sqrt(x); i += 2)
            {
                if (x % i == 0)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
