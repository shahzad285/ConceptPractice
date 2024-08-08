using ConceptPractice.ConceptClasses;
using System.Diagnostics.Metrics;

Console.WriteLine("Hello, World!");

////Concurrent queue implementation test
//ConcurrentThreadSafeQueues concurrentThreadSafeQueues = new ConcurrentThreadSafeQueues();
//Parallel.For(0, 10000, i =>
//{
//    concurrentThreadSafeQueues.Enqueue(i);
//});
//Console.WriteLine(concurrentThreadSafeQueues.Count);
//int count= concurrentThreadSafeQueues.Count;
//for (int i = 0; i < count; i++)
//    Console.WriteLine(concurrentThreadSafeQueues.Dequeue());

//Console.WriteLine(concurrentThreadSafeQueues.Count);


//Multi threading test
//Check prime numbers count without multithreading
int num= MultiThreading.CheckPrimeNumbersWithoutThreading(10000000);
Console.WriteLine("Total prime numbers count without threading "+num);

 num = MultiThreading.CheckPrimeNumbersWithThreading(10000000);
Console.WriteLine("Total prime numbers count with threading " + num);

num = MultiThreading.CheckPrimeNumbersWithThreadingWithoutBatching(10000000);
Console.WriteLine("Total prime numbers count with threading without batching " + num);


