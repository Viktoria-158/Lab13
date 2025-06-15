using System;
using System.Collections;
using System.Collections.Generic;

namespace PlantsLibraryVer2
{
    public class MyCollection<T> : IList<T>, IEnumerable<T>, ICloneable where T : IInit, ICloneable, new()
    {
        private Point<T>? begin;
        private Point<T>? end;

        public Point<T>? Begin => begin;
        public Point<T>? End => end;

        public int Count
        {
            get
            {
                int count = 0;
                Point<T>? current = begin;
                while (current != null)
                {
                    count++;
                    current = current.Next;
                }
                return count;
            }
        }

        public bool IsReadOnly => false;

        public MyCollection()
        {
            begin = null;
            end = null;
        }

        public MyCollection(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Размер не может быть отрицательным");

            for (int i = 0; i < size; i++)
            {
                T item = new T();
                item.RandomInit();
                Add(item);
            }
        }

        public MyCollection(MyCollection<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.begin == null) return;

            begin = new Point<T>((T)other.begin.Data.Clone());
            Point<T>? currentOther = other.begin.Next;
            Point<T>? currentThis = begin;

            while (currentOther != null)
            {
                Point<T> newPoint = new Point<T>((T)currentOther.Data.Clone());
                currentThis.Next = newPoint;
                newPoint.Prev = currentThis;
                currentThis = newPoint;
                currentOther = currentOther.Next;
            }
            end = currentThis;
        }

        public virtual T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                Point<T>? current = begin;
                for (int i = 0; i < index; i++)
                {
                    current = current?.Next;
                }
                return current!.Data;
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                Point<T>? current = begin;
                for (int i = 0; i < index; i++)
                {
                    current = current?.Next;
                }
                current!.Data = value;
            }
        }

        public virtual void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Point<T> newPoint = new Point<T>(item);
            if (begin == null)
            {
                begin = newPoint;
                end = newPoint;
            }
            else
            {
                end!.Next = newPoint;
                newPoint.Prev = end;
                end = newPoint;
            }
        }

        public void Clear()
        {
            begin = null;
            end = null;
        }

        public bool Contains(T item)
        {
            if (item == null)
                return false;

            Point<T>? current = begin;
            while (current != null)
            {
                if (current.Data.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Массив недостаточно длинный");

            Point<T>? current = begin;
            while (current != null)
            {
                array[arrayIndex++] = current.Data;
                current = current.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Point<T>? current = begin;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public int IndexOf(T item)
        {
            if (item == null)
                return -1;

            int index = 0;
            Point<T>? current = begin;
            while (current != null)
            {
                if (current.Data.Equals(item))
                    return index;
                current = current.Next;
                index++;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (index == 0)
            {
                Point<T> newPoint = new Point<T>(item); // Объявление для index == 0
                newPoint.Next = begin;
                if (begin != null)
                    begin.Prev = newPoint;
                begin = newPoint;
                if (end == null)
                    end = newPoint;
                return;
            }

            if (index == Count)
            {
                Add(item);
                return;
            }

            Point<T>? current = begin;
            for (int i = 0; i < index - 1; i++)
            {
                current = current?.Next;
            }

            Point<T> insertionPoint = new Point<T>(item);
            insertionPoint.Next = current!.Next;
            insertionPoint.Prev = current;
            current.Next!.Prev = insertionPoint;
            current.Next = insertionPoint;
        }

        public virtual bool Remove(T item)
        {
            if (item == null || begin == null)
                return false;

            if (begin.Data.Equals(item))
            {
                begin = begin.Next;
                if (begin != null)
                    begin.Prev = null;
                else
                    end = null;
                return true;
            }

            Point<T>? current = begin.Next;
            while (current != null && !current.Data.Equals(item))
            {
                current = current.Next;
            }

            if (current == null) return false;

            if (current.Next != null)
                current.Next.Prev = current.Prev;
            else
                end = current.Prev;

            current.Prev!.Next = current.Next;
            return true;
        }

        public virtual void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
            {
                begin = begin!.Next;
                if (begin != null)
                    begin.Prev = null;
                else
                    end = null;
                return;
            }

            Point<T>? current = begin;
            for (int i = 0; i < index; i++)
            {
                current = current?.Next;
            }

            current!.Prev!.Next = current.Next;
            if (current.Next != null)
                current.Next.Prev = current.Prev;
            else
                end = current.Prev;
        }

        public object Clone()
        {
            return new MyCollection<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void PrintCollection()
        {
            if (begin == null)
            {
                Console.WriteLine("Коллекция пуста");
                return;
            }

            Point<T>? current = begin;
            int i = 1;
            while (current != null)
            {
                Console.WriteLine($"{i}. {current.Data}");
                current = current.Next;
                i++;
            }
        }
    }
}