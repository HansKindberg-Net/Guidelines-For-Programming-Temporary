using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Company.Build.Tasks.Markdown
{
	[CLSCompliant(false)]
	public class MarkdownTransformer : IMarkdownTransformer
	{
		#region Fields

		private static IEnumerable<string> _headings;
		private readonly MarkdownSharp.Markdown _markdown;

		#endregion

		#region Constructors

		public MarkdownTransformer() : this(new MarkdownTransformerOptions()) {}

		public MarkdownTransformer(IMarkdownTransformerOptions markdownTransformerOptions)
		{
			if(markdownTransformerOptions == null)
				throw new ArgumentNullException("markdownTransformerOptions");

			this._markdown = new MarkdownSharp.Markdown(markdownTransformerOptions);
		}

		#endregion

		#region Properties

		protected internal virtual IEnumerable<string> Headings
		{
			get
			{
				if(_headings == null)
				{
					var headings = new List<string>();

					for(int i = 1; i < 10; i++)
					{
						headings.Add("h" + i.ToString(CultureInfo.InvariantCulture));
					}

					_headings = headings.ToArray();
				}

				return _headings;
			}
		}

		protected internal virtual MarkdownSharp.Markdown Markdown
		{
			get { return this._markdown; }
		}

		public virtual IMarkdownTransformerOptions Options
		{
			get { return (IMarkdownTransformerOptions) this.Markdown.Options; }
		}

		public virtual string Version
		{
			get { return this.Markdown.Version; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Html identifiers should be lower case.")]
		protected internal virtual string CreateIdentifierValueFromText(string text)
		{
			return (text ?? string.Empty).Replace(" ", "-").ToLower(CultureInfo.InvariantCulture);
		}

		protected internal virtual string GenerateHeadingIdentifiers(string html)
		{
			if(!string.IsNullOrEmpty(html))
			{
				var xmlDocument = XDocument.Parse("<root>" + html + "</root>");

				foreach(var heading in xmlDocument.Descendants().Where(this.IsHeading))
				{
					var headingXml = heading.ToString();

					heading.Add(new XAttribute("id", this.CreateIdentifierValueFromText(heading.Value)));

					html = html.Replace(headingXml, heading.ToString());
				}
			}

			return html;
		}

		protected internal virtual bool IsHeading(XElement xmlElement)
		{
			return xmlElement != null && this.Headings.Contains(xmlElement.Name.LocalName);
		}

		public virtual string TransformToHtml(string markdown)
		{
			string html = this.Markdown.Transform(markdown);

			if(!this.Options.GenerateHeadingIdentifiers)
				return html;

			html = this.GenerateHeadingIdentifiers(html);

			return html;
		}

		#endregion
	}
}