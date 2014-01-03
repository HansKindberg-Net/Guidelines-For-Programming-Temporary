using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Company.Collections.Generic.Traversing;
using Company.Web;

namespace Company.MvcApplication.Models
{
	public class LayoutModel
	{
		#region Fields

		private readonly ISiteMap _siteMap;
		private readonly ITreeTraverserFactory<ISiteMapNode> _treeTraverserFactory;

		#endregion

		#region Constructors

		public LayoutModel(ISiteMap siteMap, ITreeTraverserFactory<ISiteMapNode> treeTraverserFactory)
		{
			if(siteMap == null)
				throw new ArgumentNullException("siteMap");

			if(treeTraverserFactory == null)
				throw new ArgumentNullException("treeTraverserFactory");

			this._siteMap = siteMap;
			this._treeTraverserFactory = treeTraverserFactory;
		}

		#endregion

		#region Properties

		protected internal virtual ISiteMap SiteMap
		{
			get { return this._siteMap; }
		}

		protected internal virtual ITreeTraverserFactory<ISiteMapNode> TreeTraverserFactory
		{
			get { return this._treeTraverserFactory; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual IEnumerable<ITreeTraversingNode<ISiteMapNode>> GetSiteMapNodes(bool includeRoot, int? numberOfLevels, bool expandAllNodes)
		{
			return this.SiteMap.Enabled ? this.TreeTraverserFactory.Create(this.SiteMap.RootNode, this.SiteMap.CurrentNode, includeRoot, numberOfLevels.HasValue ? numberOfLevels.Value : int.MaxValue, expandAllNodes) : new ITreeTraversingNode<ISiteMapNode>[0];
		}

		#endregion
	}
}