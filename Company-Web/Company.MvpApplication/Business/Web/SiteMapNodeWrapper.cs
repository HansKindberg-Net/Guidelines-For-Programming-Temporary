using System;
using System.Web;

namespace Company.MvpApplication.Business.Web
{
	public class SiteMapNodeWrapper : ISiteMapNode
	{
		#region Fields

		private readonly SiteMapNode _siteMapNode;

		#endregion

		#region Constructors

		public SiteMapNodeWrapper(SiteMapNode siteMapNode)
		{
			if(siteMapNode == null)
				throw new ArgumentNullException("siteMapNode");

			this._siteMapNode = siteMapNode;
		}

		#endregion

		#region Properties

		public virtual string Description
		{
			get { return this.SiteMapNode.Description; }
		}

		protected internal virtual SiteMapNode SiteMapNode
		{
			get { return this._siteMapNode; }
		}

		public virtual string Title
		{
			get { return this.SiteMapNode.Title; }
		}

		public virtual string Url
		{
			get { return this.SiteMapNode.Url; }
		}

		#endregion

		#region Methods

		public static SiteMapNodeWrapper FromSiteMapNode(SiteMapNode siteMapNode)
		{
			return siteMapNode;
		}

		#endregion

		#region Implicit operator

		public static implicit operator SiteMapNodeWrapper(SiteMapNode siteMapNode)
		{
			return siteMapNode != null ? new SiteMapNodeWrapper(siteMapNode) : null;
		}

		#endregion
	}
}