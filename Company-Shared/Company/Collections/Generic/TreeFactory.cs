using System.Collections.Generic;

namespace Company.Collections.Generic
{
	public class TreeFactory<T> : ITreeFactory<T>
	{
		#region Methods

		public virtual ITreeNode<T> CreateTreeNode(ITreeNode<T> parent)
		{
			return new TreeNode<T>(parent, this);
		}

		public virtual ITreeNodeCollection<T> CreateTreeNodeCollection(ITreeNode<T> parent)
		{
			return new TreeNodeCollection<T>(parent, this, EqualityComparer<T>.Default);
		}

		#endregion
	}
}