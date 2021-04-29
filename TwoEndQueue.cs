using System.Collections;
using System.Collections.Generic;

namespace TwoEndQueue
{
    public class TwoEndQueue<T> : IEnumerable<T>
    {
        private const int MinCapacity = 4;
        private T[] buffer;
        private int startIndex;
        private int size;
        private int endIndex
        {
            get
            {
                int index = startIndex + size;
                if (index >= Capacity)
                {
                    index -= Capacity;
                }
                return index;
            }
            set { }
        }
        public T Front { get { return buffer[startIndex]; } }
        public T Back { get { return buffer[PrevIndex(endIndex)]; } }
        public int Size { get { return size; } }
        public int Capacity { get { return buffer.Length; } }
        public bool Empty { get { return size == 0; } }
        public TwoEndQueue()
        {
            buffer = null;
            startIndex = 0;
            size = 0;
        }
        public void EnqueueRight(T value)
        {
            MakeSpaceForOneMoreIfNeeded();

            buffer[endIndex] = value;
            size++;
        }
        public void EnqueueLeft(T value)
        {
            MakeSpaceForOneMoreIfNeeded();

            startIndex = PrevIndex(startIndex);
            buffer[startIndex] = value;
            size++;
        }
        public void DequeueLeft()
        {
            buffer[startIndex] = default(T);
            startIndex = NextIndex(startIndex);
            size--;
        }
        public void DequeueRight()
        {
            endIndex = PrevIndex(endIndex);
            buffer[endIndex] = default(T);
            size--;
        }
        private void Reserve(int newSize)
        {
            if (newSize < size)
            {
                return;
            }
            var newBuffer = new T[newSize];
            for (int i = 0, j = startIndex; i < size; i++, j = NextIndex(j))
            {
                newBuffer[i] = buffer[j];
            }
            startIndex = 0;
            buffer = newBuffer;
        }
        private int NextIndex(int index)
        {
            index++;
            if (index == buffer.Length)
            {
                index = 0;
            }
            return index;
        }
        private int PrevIndex(int index)
        {
            if (index == 0)
            {
                index = buffer.Length;
            }
            index--;
            return index;
        }
        private void MakeSpaceForOneMoreIfNeeded()
        {
            if (buffer == null)
            {
                buffer = new T[MinCapacity];

            }
            else if (size == buffer.Length)
            {
                Reserve(size * 2);
            }
        }
        public T this[int index]
        {
            get
            {
                return buffer[RealIndex(index)];
            }
            set
            {
                buffer[RealIndex(index)] = value;
            }
        }
        private int RealIndex(int index)
        {
            int realIndex = startIndex + index;
            if (realIndex >= Capacity)
            {
                realIndex -= Capacity;
            }
            return realIndex;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = startIndex, j = 0; j < size; i = NextIndex(i), j ++)
            {
                yield return buffer[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
