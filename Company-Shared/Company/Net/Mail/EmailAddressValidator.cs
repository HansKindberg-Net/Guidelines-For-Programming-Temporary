using System.Text.RegularExpressions;

namespace Company.Net.Mail
{
	public class EmailAddressValidator
	{
		#region Fields

		private Regex _validEmailAddressRegex;
		private const RegexOptions _validEmailAddressRegexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline;
		private const string _validEmailAddressRegexString = "^((?>[a-zA-Z\\d!#$%&'*+\\-/=?^_`{|}~]+\\x20*|\"((?=[\\x01-\\x7f])[^\"\\\\]|\\\\[\\x01-\\x7f])*\"\\x20*)*(?<angle><))?((?!\\.)(?>\\.?[a-zA-Z\\d!#$%&'*+\\-/=?^_`{|}~]+)+|\"((?=[\\x01-\\x7f])[^\"\\\\]|\\\\[\\x01-\\x7f])*\")@(((?!-)[a-zA-Z\\d\\-]+(?<!-)\\.)+[a-zA-Z]{2,}|\\[(((?(?<!\\[)\\.)(25[0-5]|2[0-4]\\d|[01]?\\d?\\d)){4}|[a-zA-Z\\d\\-]*[a-zA-Z\\d]:((?=[\\x01-\\x7f])[^\\\\\\[\\]]|\\\\[\\x01-\\x7f])+)\\])(?(angle)>)$";

		#endregion

		#region Properties

		protected internal virtual Regex ValidEmailAddressRegex
		{
			get { return this._validEmailAddressRegex ?? (this._validEmailAddressRegex = new Regex(this.ValidEmailAddressRegexString, this.ValidEmailAddressRegexOptions)); }
		}

		protected internal virtual RegexOptions ValidEmailAddressRegexOptions
		{
			get { return _validEmailAddressRegexOptions; }
		}

		protected internal virtual string ValidEmailAddressRegexString
		{
			get { return _validEmailAddressRegexString; }
		}

		#endregion

		#region Methods

		public virtual bool IsValidEmailAddress(string potentialEmailAddress)
		{
			return potentialEmailAddress != null && this.ValidEmailAddressRegex.IsMatch(potentialEmailAddress);
		}

		#endregion
	}
}