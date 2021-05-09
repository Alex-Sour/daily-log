using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Log
{
    class Program
    {
        static Dictionary<DateTime, string> entries;

        static string Entry()
        {
            Console.WriteLine("Write your entry. Make sure not to use square brackets ([]) or semicolons (;).\n");
            string entry = Console.ReadLine();
            entries.Add(DateTime.Now, entry);

            return "";
        }

        static void SaveEntries()
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (Directory.Exists(path))
            {
                if (!Directory.Exists(Path.Combine(path, "Daily Log")))
                {
                    Directory.CreateDirectory(Path.Combine(path, "Daily Log"));
                }

                string toWrite = "";

                foreach (var entry in entries)
                {
                    toWrite += entry.Key.ToString() + ";" + entry.Value + "\n";
                }


                File.WriteAllText(Path.Combine(path, "Daily Log", "entries.txt"), toWrite);
            }
        }

        static string Fit(string old, string max)
        {
            string give = old;
            while(give.Length < max.Length)
            {
                give += " ";
            }
            return give;
        }

        static string View()
        {
            /*
            var descending = entries.OrderByDescending(d => d.Key);
            IOrderedEnumerable<KeyValuePair<DateTime, string>> ioe = descending;

            Dictionary<DateTime, string> ieo = ioe.ToDictionary(pair => pair.Key, pair => pair.Value);
            */
            SortedDictionary<DateTime, string> ieo = new SortedDictionary<DateTime,string>(entries);

            //foreach (KeyValuePair<DateTime, string> entry in entries)
            foreach (KeyValuePair<DateTime, string> pair in ieo)
            {
                Console.WriteLine(Fit(pair.Key.ToString("yyyy MMMM dd"), "YYYY MMMMMMMMM MMM") + "| " + pair.Value);
            }
            return "";
        }

        static Dictionary<DateTime, string> ParseEntries()
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (Directory.Exists(path))
            {
                if (Directory.Exists(Path.Combine(path, "Daily Log")))
                {
                    if (File.Exists(Path.Combine(path, "Daily Log", "entries.txt")))
                    {
                        string content = File.ReadAllText(Path.Combine(path, "Daily Log", "entries.txt"));
                        string[] lines = content.Split("\n");
                        Dictionary<DateTime, string> d = new Dictionary<DateTime, string>();
                        foreach (string line in lines)
                        {
                            if(line != "")
                            {
                                string[] parts = line.Split(";");
                                DateTime time = DateTime.Parse(parts[0]);
                                string text = parts[1];
                                d.Add(time, text);
                            }
                        }
                        return d;
                    }
                    else
                    {
                        return new Dictionary<DateTime, string>();
                    }
                }
                else
                {
                    return new Dictionary<DateTime, string>();
                }

            }
            else
            {
                return new Dictionary<DateTime, string>();
            }
        }

        static void Main(string[] args)
        {
            entries = ParseEntries();
            
            Console.WriteLine("               Daily Log 0.0.1               ");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("\nWould you like to write a NEW entry or VIEW previous entries?");
            Console.Write("\n");

            string ans = Console.ReadLine();

            while(true)
            {
                

                if (ans.ToLower().Contains("new"))
                {
                    Console.Clear();
                    Console.WriteLine("               Daily Log 0.0.1               ");
                    Console.WriteLine("---------------------------------------------");
                    Console.Write("\n");
                    Entry();
                    SaveEntries();
                    Console.WriteLine("Saved entry to daily log!");
                    
                }

                else if (ans.ToLower().Contains("view"))
                {
                    Console.Clear();
                    Console.WriteLine("               Daily Log 0.0.1               ");
                    Console.WriteLine("---------------------------------------------");
                    Console.Write("\n");
                    View();
                    Console.Write("\n");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("               Daily Log 0.0.1               ");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("\nWould you like to write a NEW entry or VIEW previous entries?");
                    Console.Write("\n");
                }
                ans = Console.ReadLine();
                
            }
            
        }
    }
}
