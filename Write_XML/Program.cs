using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Write_XML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(XmlWriter writer= XmlWriter.Create("books.xml"))
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
            }    
        }
    }
}
