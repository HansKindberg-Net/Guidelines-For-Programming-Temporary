namespace Company.Collections.Generic.Traversing
{
	public class TreeTraversingItemNode<T> : TreeTraversingNode<T>, ITreeTraversingNode<T>
	{
		#region Constructors

		public TreeTraversingItemNode(ITreeNode<T> treeNode) : base(treeNode) {}

		#endregion

		#region Properties

		public virtual bool IsFooter
		{
			get { return false; }
		}

		public virtual bool IsHeader
		{
			get { return false; }
		}

		public virtual bool IsItem { get; set; }

		public virtual bool IsItemFooter
		{
			get { return false; }
		}

		public virtual bool IsItemHeader
		{
			get { return false; }
		}

		public virtual bool IsLevelFooter
		{
			get { return false; }
		}

		public virtual bool IsLevelHeader
		{
			get { return false; }
		}

		public virtual bool IsSelected
		{
			get { return false; }
		}

		public virtual bool IsSelectedAncestor { get; set; }

		public virtual bool IsSelectedAncestorFooter
		{
			get { return false; }
		}

		public virtual bool IsSelectedAncestorHeader
		{
			get { return false; }
		}

		public virtual bool IsSelectedFooter
		{
			get { return false; }
		}

		public virtual bool IsSelectedHeader
		{
			get { return false; }
		}

		public virtual bool IsSelectedItem { get; set; }

		public virtual bool IsSelectedItemFooter
		{
			get { return false; }
		}

		public virtual bool IsSelectedItemHeader
		{
			get { return false; }
		}

		#endregion
	}
}