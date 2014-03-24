using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Company.Build.Tasks.Html
{
	public class DefaultHtmlFormatter : IHtmlFormatter
	{
		#region Methods

		protected internal virtual XmlWriterSettings CreateXmlWriterSettings(Encoding encoding)
		{
			return new XmlWriterSettings
			{
				ConformanceLevel = ConformanceLevel.Auto,
				Encoding = encoding,
				Indent = true,
				IndentChars = "\t",
				OmitXmlDeclaration = true
			};
		}

		protected internal virtual void FixDocumentTypeIfNecessary(XDocument xmlDocument)
		{
			if(xmlDocument == null)
				throw new ArgumentNullException("xmlDocument");

			// To avoid [] at the end of the !DOCTYPE element.
			if(xmlDocument.DocumentType != null && xmlDocument.DocumentType.InternalSubset != null && xmlDocument.DocumentType.InternalSubset.Length == 0)
				xmlDocument.DocumentType.InternalSubset = null;
		}

		protected internal virtual string FixDocumentTypeLineIfNecessary(string html)
		{
			if(string.IsNullOrEmpty(html))
				return html;

			var firstNewLineIndex = html.IndexOf(Environment.NewLine, StringComparison.OrdinalIgnoreCase);

			if(firstNewLineIndex < 0)
				return html;

			var firstLine = html.Substring(0, firstNewLineIndex).Replace(" >", ">");

			return firstLine + html.Substring(firstNewLineIndex);
		}

		public virtual string Format(string html, Encoding encoding)
		{
			var xDocument = XDocument.Parse(html);

			this.FixDocumentTypeIfNecessary(xDocument);

			var stringBuilder = new StringBuilder();

			using(var xmlWriter = XmlWriter.Create(stringBuilder, this.CreateXmlWriterSettings(encoding)))
			{
				xDocument.WriteTo(xmlWriter);
			}

			var formattedHtml = stringBuilder.ToString();

			if(xDocument.DocumentType != null)
				formattedHtml = this.FixDocumentTypeLineIfNecessary(formattedHtml);

			return formattedHtml;
		}

		#endregion
	}
}