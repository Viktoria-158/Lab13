using System;

namespace PlantsLibraryVer2
{
    public class JournalEntry
    {
        public string NameCollection { get; set; }
        public string ChangeType { get; set; }
        public string ObjectData { get; set; }

        public JournalEntry(string name, string change, string data)
        {
            NameCollection = name;
            ChangeType = change;
            ObjectData = data;
        }

        public override string ToString()
        {
            return $"Коллекция: {NameCollection}, Изменение: {ChangeType}, Данные: {ObjectData}";
        }
    }
}