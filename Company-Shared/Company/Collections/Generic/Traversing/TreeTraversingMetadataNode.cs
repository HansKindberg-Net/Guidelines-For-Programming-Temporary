namespace Company.Collections.Generic.Traversing
{
	public class TreeTraversingMetadataNode<T> : TreeTraversingNode<T>, ITreeTraversingNode<T>
	{
		#region Constructors

		public TreeTraversingMetadataNode(ITreeNode<T> treeNode) : base(treeNode) {}

		#endregion

		#region Properties

		public virtual bool IsFooter { get; set; }
		public virtual bool IsHeader { get; set; }

		public virtual bool IsItem
		{
			get { return false; }
		}

		public virtual bool IsItemFooter { get; set; }
		public virtual bool IsItemHeader { get; set; }
		public virtual bool IsLevelFooter { get; set; }
		public virtual bool IsLevelHeader { get; set; }

		public virtual bool IsSelected
		{
			get { return this.IsSelectedAncestor || this.IsSelectedItem; }
		}

		public virtual bool IsSelectedAncestor
		{
			get { return false; }
		}

		public virtual bool IsSelectedAncestorFooter { get; set; }
		public virtual bool IsSelectedAncestorHeader { get; set; }

		public virtual bool IsSelectedFooter
		{
			get { return this.IsSelectedAncestorFooter || this.IsSelectedItemFooter; }
		}

		public virtual bool IsSelectedHeader
		{
			get { return this.IsSelectedAncestorHeader || this.IsSelectedItemHeader; }
		}

		public virtual bool IsSelectedItem
		{
			get { return false; }
		}

		public virtual bool IsSelectedItemFooter { get; set; }
		public virtual bool IsSelectedItemHeader { get; set; }

		#endregion
	}
}