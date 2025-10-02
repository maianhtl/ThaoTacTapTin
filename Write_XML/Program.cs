using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Write_XML
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*using(XmlWriter writer= XmlWriter.Create("books.xml"))
            {
                String pi = "type=\"text/xsl\" href=\"book/xsl\"";

                writer.WriteProcessingInstruction("xml-stylesheet", pi);

                writer.WriteDocType("catalog", null, null, "<!ENTITY h \"hardcover\">");

                writer.WriteComment("This is the book sample XML");
                writer.WriteStartElement("book");
                writer.WriteAttributeString("ISBN", "9831123212");
                writer.WriteAttributeString("yearpublished", "2002");
                writer.WriteAttributeString("author", "Mahesh Chand");
                writer.WriteElementString("title", "Visual C# Programming");
                writer.WriteElementString("price", "44.95");
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }  */

            List<Book> books = new List<Book>
            {
                new Book { ISBN="9831123212", Title="Visual C# Programming", Author="Mahesh Chand", Price=44.95m, YearPublished=2002 },
                new Book { ISBN="9781484234", Title="Pro Entity Framework Core 1", Author="Adam Freeman", Price=39.99m, YearPublished=2017 }
            };

            SaveToXmlFile(books);

        }

        private static void SaveToXmlFile(List<Book> books)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));

            using (var writer = new StreamWriter("book.xml"))
            {
                serializer.Serialize(writer, books, null);
                writer.Close();
            }   
        }
                    
    }
}

