using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Company.DirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.UnitTests.DirectoryServices
{
	[TestClass]
	public class DirectoryTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		public void ValidateHost_IfTheHostParameterCannotBeParsedToAnUri_ShouldThrowAnArgumentException()
		{
			const string host = "!";
			const string parameterName = "host";
			ArgumentException expectedArgumentException = new ArgumentException(string.Format(CultureInfo.InvariantCulture, "\"{0}\" is an invalid value for {1}.", host, parameterName), parameterName);

			try
			{
				new Directory(null).ValidateHost(host, parameterName);
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.ParamName == parameterName && argumentException.Message == expectedArgumentException.Message && argumentException.InnerException is UriFormatException)
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		public void ValidateHost_IfTheHostParameterContainsIllegalCharacters_ShouldThrowAnArgumentException()
		{
			const string parameterName = "host";
			ArgumentException expectedArgumentException = new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The {0} can not contain the following characters: \"{1}\".", parameterName, string.Join(", ", new[] {'/'})), parameterName);

			try
			{
				new Directory(null).ValidateHost("/", parameterName);
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.ParamName == parameterName && argumentException.Message == expectedArgumentException.Message)
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		public void ValidateHost_IfTheHostParameterIsEmpty_ShouldThrowAnArgumentException()
		{
			const string parameterName = "host";
			ArgumentException expectedArgumentException = new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The {0} can not be empty.", parameterName), parameterName);

			try
			{
				new Directory(null).ValidateHost(string.Empty, parameterName);
			}
			catch(ArgumentException argumentException)
			{
				if(argumentException.ParamName == parameterName && argumentException.Message == expectedArgumentException.Message)
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ValidateHost_IfTheHostParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			const string parameterName = "host";

			try
			{
				new Directory(null).ValidateHost(null, parameterName);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == parameterName)
					throw;
			}
		}

		#endregion
	}
}