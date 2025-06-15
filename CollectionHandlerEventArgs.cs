using System;

namespace PlantsLibraryVer2
{
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string NameCollection { get; set; }
        public string ChangeCollection { get; set; }
        public object Obj { get; set; }

        public CollectionHandlerEventArgs(string name, string change, object obj)
        {
            NameCollection = name;
            ChangeCollection = change;
            Obj = obj;
        }
    }
}