using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Net;

namespace Company.DirectoryServices
{
	public class Directory : IDirectory
	{
		#region Fields

		private string _distinguishedName;
		private string _host;
		private string _hostUrl;
		private static readonly IEnumerable<char> _illegalHostCharacters = new[] {'/'};
		private readonly NetworkCredential _networkCredential;
		private int? _port;
		private string _rootPath;
		private Scheme _scheme;

		#endregion

		#region Constructors

		public Directory()
		{
			this._networkCredential = new NetworkCredential();
		}

		public Directory(string host) : this()
		{
			this._host = host;
		}

		public Directory(string host, Scheme scheme) : this(host)
		{
			this._scheme = scheme;
		}

		#endregion

		#region Properties

		public virtual AuthenticationTypes? AuthenticationType { get; set; }

		public virtual string DistinguishedName
		{
			get { return this._distinguishedName; }
			set
			{
				this.Initialized = false;
				this._distinguishedName = value;
			}
		}

		protected internal virtual string Host
		{
			get
			{
				this.ValidateHost(this._host, "host");
				return this._host;
			}
			set
			{
				this.ValidateHost(value, "value");
				this.Initialized = false;
				this._host = value;
			}
		}

		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
		public virtual string HostUrl
		{
			get
			{
				this.EnsureInitialization();
				return this._hostUrl;
			}
		}

		protected internal virtual IEnumerable<char> IllegalHostCharacters
		{
			get { return _illegalHostCharacters; }
		}

		protected internal virtual bool Initialized { get; set; }

		protected internal virtual NetworkCredential NetworkCredential
		{
			get { return this._networkCredential; }
		}

		[SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly")]
		public virtual string Password
		{
			set { this.NetworkCredential.Password = value; }
		}

		public virtual int? Port
		{
			get { return this._port; }
			set
			{
				this.Initialized = false;
				this._port = value;
			}
		}

		protected internal virtual string RootPath
		{
			get
			{
				this.EnsureInitialization();
				return this._rootPath;
			}
		}

		public virtual Scheme Scheme
		{
			get { return this._scheme; }
			set
			{
				this.Initialized = false;
				this._scheme = value;
			}
		}

		public virtual string UserName
		{
			get { return this.NetworkCredential.UserName; }
			set { this.NetworkCredential.UserName = value; }
		}

		#endregion

		#region Methods

		protected internal virtual void EnsureInitialization()
		{
			if(this.Initialized)
				return;

			this._hostUrl = this.Scheme + "://" + this.Host;

			if(this.Port.HasValue)
				this._hostUrl += ":" + this.Port.Value.ToString(CultureInfo.InvariantCulture);

			if(!this._hostUrl.EndsWith("/", StringComparison.Ordinal))
				this._hostUrl += "/";

			this._rootPath = this._hostUrl + this.DistinguishedName;

			this.Initialized = true;
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose must be handled by the caller, IDirectoryEntry.Dispose().")]
		protected internal virtual DirectoryEntry GetConcreteDirectoryEntry(string path)
		{
			DirectoryEntry directoryEntry = new DirectoryEntry(path, this.UserName, this.NetworkCredential.Password);

			if(this.AuthenticationType.HasValue)
				directoryEntry.AuthenticationType = this.AuthenticationType.Value;

			return directoryEntry;
		}

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected internal virtual DirectoryEntry GetConcreteRoot()
		{
			return this.GetConcreteDirectoryEntry(this.RootPath);
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose must be handled by the caller, IDirectoryEntry.Dispose().")]
		public virtual IDirectoryEntry GetDirectoryEntry(string path)
		{
			return new DirectoryEntryWrapper(this.GetConcreteDirectoryEntry(path));
		}

		public virtual string GetPath(string distinguishedName)
		{
			return this.HostUrl + distinguishedName;
		}

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose must be handled by the caller, IDirectoryEntry.Dispose().")]
		public virtual IDirectoryEntry GetRoot()
		{
			return this.GetDirectoryEntry(this.RootPath);
		}

		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Uri")]
		protected internal virtual void ValidateHost(string host, string parameterName)
		{
			if(host == null)
				throw new ArgumentNullException(parameterName);

			if(host.Length == 0)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The {0} can not be empty.", parameterName), parameterName);

			IEnumerable<char> illegalHostCharacters = this.IllegalHostCharacters ?? new char[0];

			foreach(var illegalHostCharacter in illegalHostCharacters.Where(host.Contains))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The {0} can not contain the following characters: \"{1}\".", parameterName, string.Join(", ", illegalHostCharacter)), parameterName);
			}

			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new Uri("LDAP://" + host);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(Exception exception)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "\"{0}\" is an invalid value for {1}.", host, parameterName), parameterName, exception);
			}
		}

		#endregion
	}
}