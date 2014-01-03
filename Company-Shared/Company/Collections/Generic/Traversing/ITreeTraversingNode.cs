namespace Company.Collections.Generic.Traversing
{
	public interface ITreeTraversingNode<T>
	{
		#region Properties

		bool IsFooter { get; }
		bool IsHeader { get; }
		bool IsItem { get; }
		bool IsItemFooter { get; }
		bool IsItemHeader { get; }
		bool IsLevelFooter { get; }
		bool IsLevelHeader { get; }
		bool IsSelected { get; }
		bool IsSelectedAncestor { get; }
		bool IsSelectedAncestorFooter { get; }
		bool IsSelectedAncestorHeader { get; }
		bool IsSelectedFooter { get; }
		bool IsSelectedHeader { get; }
		bool IsSelectedItem { get; }
		bool IsSelectedItemFooter { get; }
		bool IsSelectedItemHeader { get; }
		ITreeNode<T> TreeNode { get; }

		#endregion
	}
}