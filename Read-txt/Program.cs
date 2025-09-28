using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
namespace Read_txt
{
    public class Program
    {

        static void WriteReadText(string fileName, string[] text)
        {
            File.WriteAllLines(fileName, text);

            foreach (string line in File.ReadAllLines(fileName))
            {
                Console.WriteLine(line);
            }

        }

        static void Main(string[] args)
        {
            string[] text = { "line 1", "line 2", "line 3" };
            string fileName = "fileText.txt";

            WriteReadText(fileName, text);
            
            Console.ReadKey();
        }
        
    }
}
