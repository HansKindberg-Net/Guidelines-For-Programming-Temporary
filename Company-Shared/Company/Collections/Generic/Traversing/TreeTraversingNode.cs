using System;

namespace Company.Collections.Generic.Traversing
{
	public abstract class TreeTraversingNode<T>
	{
		#region Fields

		private readonly ITreeNode<T> _treeNode;

		#endregion

		#region Constructors

		protected TreeTraversingNode(ITreeNode<T> treeNode)
		{
			if(treeNode == null)
				throw new ArgumentNullException("treeNode");

			this._treeNode = treeNode;
		}

		#endregion

		#region Properties

		public virtual ITreeNode<T> TreeNode
		{
			get { return this._treeNode; }
		}

		#endregion
	}
}