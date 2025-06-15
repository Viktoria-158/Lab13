using System;
using System.Collections.Generic;

namespace PlantsLibraryVer2
{
    public class Journal
    {
        private List<JournalEntry> journal = new List<JournalEntry>();

        public void Add(JournalEntry entry)
        {
            journal.Add(entry);
        }

        public void PrintJournal()
        {
            Console.WriteLine("Журнал изменений:");
            foreach (var entry in journal)
            {
                Console.WriteLine(entry);
            }
        }

        public override string ToString()
        {
            string result = "Журнал изменений:\n";
            foreach (var entry in journal)
            {
                result += entry.ToString() + "\n";
            }
            return result;
        }
    }
}