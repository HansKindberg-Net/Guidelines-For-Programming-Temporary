namespace Company.Collections.Generic
{
	public interface ITreeFactory<T>
	{
		#region Methods

		ITreeNode<T> CreateTreeNode(ITreeNode<T> parent);
		ITreeNodeCollection<T> CreateTreeNodeCollection(ITreeNode<T> parent);

		#endregion
	}
}