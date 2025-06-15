using System;

namespace PlantsLibraryVer2
{
    public class Point<T> where T : IInit, ICloneable, new()
    {
        public T? Data { get; set; }
        public Point<T>? Next { get; set; }
        public Point<T>? Prev { get; set; }

        public Point()
        {
            Data = default;
            Next = null;
            Prev = null;
        }

        public Point(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }

        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }
    }
}