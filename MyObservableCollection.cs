using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PlantsLibraryVer2
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class MyObservableCollection<T> : MyCollection<T> where T : IInit, ICloneable, new()
    {
        public string Name { get; set; }

        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        public MyObservableCollection() : base() { Name = "ObservableCollection"; }
        public MyObservableCollection(int size) : base(size) { Name = "ObservableCollection"; }
        public MyObservableCollection(string name) : base() { Name = name; }
        public MyObservableCollection(string name, int size) : base(size) { Name = name; }

        protected virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionCountChanged?.Invoke(source, args);
        }

        protected virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionReferenceChanged?.Invoke(source, args);
        }

        public override void Add(T item)
        {
            base.Add(item);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(Name, "add", item));
        }

        public override bool Remove(T item)
        {
            bool result = base.Remove(item);
            if (result)
            {
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(Name, "remove", item));
            }
            return result;
        }

        public override void RemoveAt(int index)
        {
            if (index < 0 || index >= Count) return;

            T item = this[index];
            base.RemoveAt(index);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(Name, "remove", item));
        }

        public override T this[int index]
        {
            get => base[index];
            set
            {
                T oldItem = base[index];
                base[index] = value;
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(Name, "changed", value));
            }
        }
    }
}