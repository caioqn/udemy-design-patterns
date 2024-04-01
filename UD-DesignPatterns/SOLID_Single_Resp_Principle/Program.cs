using System.Diagnostics;
using static System.Console;


namespace SOLID_Single_Resp_Principle
{
    /// <summary>
    /// A class should have one and only one reason to change, meaning that a class should have only one job.
    /// </summary>
    public class Journal
    {
        private readonly List<string> entries = new();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string? ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        /* Avoid inserting too much responsibility to a class, it should only handle what it's created for
        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }

        public static Journal Load(string filename)
        {

        }

        public void Load(Uri uri)
        {

        }
        */
    }

    /// <summary>
    /// Better approach, to not violate the SingleResp.
    /// </summary>
    public class Persistance
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if(overwrite || !File.Exists(filename)) File.WriteAllText(filename, j.ToString());
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("asdf today");
            j.AddEntry("asdf another day");
            WriteLine(j.ToString());

            var p = new Persistance();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(j, filename, true);
            var psi = new ProcessStartInfo(filename)
            {
                UseShellExecute = true,
            };
            Process.Start(psi);
        }
    }
}