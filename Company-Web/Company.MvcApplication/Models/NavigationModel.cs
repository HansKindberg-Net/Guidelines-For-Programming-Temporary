using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Company.Collections.Generic;
using Company.Web;

namespace Company.MvcApplication.Models
{
	public class NavigationModel
	{
		#region Fields

		private readonly ISiteMap _siteMap;
		private IEnumerable<ITreeNode<ISiteMapNode>> _siteMapNodes;

		#endregion

		#region Constructors

		public NavigationModel(ISiteMap siteMap)
		{
			if(siteMap == null)
				throw new ArgumentNullException("siteMap");

			this._siteMap = siteMap;
		}

		#endregion

		#region Properties

		protected internal virtual ISiteMap SiteMap
		{
			get { return this._siteMap; }
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual IEnumerable<ITreeNode<ISiteMapNode>> SiteMapNodes
		{
			get { return this._siteMapNodes ?? (this._siteMapNodes = this.SiteMap.Enabled ? this.SiteMap.RootNode.Descendants : new ITreeNode<ISiteMapNode>[0]); }
		}

		#endregion
	}
}