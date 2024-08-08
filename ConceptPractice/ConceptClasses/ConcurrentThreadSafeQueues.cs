using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptPractice.ConceptClasses
{
    public class ConcurrentThreadSafeQueues
    {
        private static Mutex mutex;
        List<int> queue;
        public ConcurrentThreadSafeQueues()
        {
            mutex = new Mutex();
            queue = new List<int>();
        }

        public void Enqueue(int n)
        {
            //lock (queue) 
            //{
            //    queue.Add(n);

            // }

            //Monitor.Enter(queue);
            // try
            // {
            //     queue.Add(n);
            // }
            // finally { Monitor.Exit(queue); }


            mutex.WaitOne();
            try
            {
                queue.Add(n);
            }
            finally { mutex.ReleaseMutex(); }

        }

        public int Dequeue() 
        {
            lock (queue)
            {
                if (queue.Count == 0)
                    throw new Exception("Queue is empty");

                int value = queue[0];
                queue.RemoveAt(0);
                return value;
            }
        }

        public int Count { 
            get { 
                lock(queue)
                return queue.Count; } 
        }

    }
}
