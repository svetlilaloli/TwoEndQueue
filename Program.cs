using System;

namespace TwoEndQueue
{
    public class Program
    {
        public static void Main()
        {
            var queue = new TwoEndQueue<int>();

            for (int i = 0; i < 5; i++)
            {
                queue.EnqueueLeft(0 - i);
                queue.EnqueueRight(i);
            }

            Console.WriteLine(string.Join(" ", queue));
            Console.WriteLine(queue.Size);

            while (!queue.Empty)
            {
                Console.WriteLine(queue.Front);
                queue.DequeueLeft();
                Console.WriteLine(queue.Back);
                queue.DequeueRight();
            }
        }
    }
}
