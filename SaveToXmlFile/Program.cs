using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SaveToXmlFile
{
    public class Program
    {
        static void Main(string[] args)
        {
            var books = new List<Book>
            {
                new Book
                {
                    ISBN = "9831123212",
                    Title = "A Programmer's Guide to ADO .Net using C#",
                    Author = "Mahesh Chand",
                    Price = 44.99,
                    YearPublished = 2002
                },
                new Book
                {
                    ISBN = "9781484234",
                    Title = "Pro Entity Framework Core 1",
                    Author = "Adam Freeman",
                    Price = 44.99,
                    YearPublished = 2018
                }
            };



            SaveToXmlFile(books);

            Console.ReadKey();
        }

        private static void SaveToXmlFile(List<Book> books)
        {
            var serializer = new XmlSerializer(typeof(List<Book>));
            using (var writer = new StreamWriter("books.xml"))
            {
                serializer.Serialize(writer, books, null);
                writer.Close();
            }
        }
    }

    
}
