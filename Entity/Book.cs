using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entity
{
	[XmlType("Book")]
	public class Book
	{
		[XmlElement("BookCode")]
		public int BookCode{ set; get; }
		[XmlElement("BookName")]
		public string BookName{ set; get; }
		[XmlElement("Author")]
		public string Author{ set; get; }
		[XmlElement("Price")]
		public decimal Price{ set; get; }
		[XmlElement("PublishDate")]
		public DateTime PublishDate{ set; get; }
		[XmlElement("IsSuit")]
		public bool IsSuit{ set; get; }

		public override string ToString()
		{
			return "[" + BookCode + "," + BookName + "," + Author + "," + Price + "," + PublishDate + "," + IsSuit + "]";
		}
	}

	[XmlRoot("BookRoot")]
	public class BookRoot
	{
		[XmlArray("BookList")]
		public List<Book> Books{ set; get; }
	}
}
