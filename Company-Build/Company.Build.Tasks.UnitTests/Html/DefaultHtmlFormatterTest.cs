using System;
using System.Text;
using System.Xml;
using Company.Build.Tasks.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Build.Tasks.UnitTests.Html
{
	[TestClass]
	public class DefaultHtmlFormatterTest
	{
		#region Methods

		[TestMethod]
		public void CreateXmlWriterSettings_Test()
		{
			var randomEncoding = Encoding.GetEncodings()[DateTime.Now.Second].GetEncoding();

			var xmlWriterSettings = new DefaultHtmlFormatter().CreateXmlWriterSettings(randomEncoding);

			Assert.IsTrue(xmlWriterSettings.CheckCharacters);
			Assert.IsFalse(xmlWriterSettings.CloseOutput);
			Assert.AreEqual(ConformanceLevel.Auto, xmlWriterSettings.ConformanceLevel);
			Assert.IsFalse(xmlWriterSettings.DoNotEscapeUriAttributes);
			Assert.AreEqual(randomEncoding, xmlWriterSettings.Encoding);
			Assert.IsTrue(xmlWriterSettings.Indent);
			Assert.AreEqual("\t", xmlWriterSettings.IndentChars);
			Assert.AreEqual(Environment.NewLine, xmlWriterSettings.NewLineChars);
			Assert.AreEqual(NewLineHandling.Replace, xmlWriterSettings.NewLineHandling);
			Assert.IsFalse(xmlWriterSettings.NewLineOnAttributes);
			Assert.IsTrue(xmlWriterSettings.OmitXmlDeclaration);
			Assert.AreEqual(XmlOutputMethod.Xml, xmlWriterSettings.OutputMethod);
		}

		[TestMethod]
		public void Format_ShouldIndentEachLevelWithATab()
		{
			string expectedResult = "<!DOCTYPE html>" + Environment.NewLine + "<html>" + Environment.NewLine + "\t<body>" + Environment.NewLine + "\t\t<h1>Heading</h1>" + Environment.NewLine + "\t\t<p>First line.</p>" + Environment.NewLine + "\t\t<p>Second line.</p>" + Environment.NewLine + "\t\t<p>Third line.</p>" + Environment.NewLine + "\t</body>" + Environment.NewLine + "</html>";

			Assert.AreEqual(expectedResult, new DefaultHtmlFormatter().Format("<!DOCTYPE html><html><body><h1>Heading</h1><p>First line.</p><p>Second line.</p><p>Third line.</p></body></html>", Encoding.UTF8));
		}

		#endregion
	}
}