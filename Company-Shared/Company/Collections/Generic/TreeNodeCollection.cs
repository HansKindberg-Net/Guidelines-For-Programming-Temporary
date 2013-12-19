using System;
using System.Collections;
using System.Collections.Generic;

namespace Company.Collections.Generic
{
	public class TreeNodeCollection<T> : ITreeNodeCollection<T>
	{
		#region Fields

		private readonly IEqualityComparer<T> _equalityComparer;
		private readonly IList<ITreeNode<T>> _list;
		private readonly ITreeNode<T> _parent;
		private readonly ITreeFactory<T> _treeFactory;

		#endregion

		#region Constructors

		public TreeNodeCollection(ITreeNode<T> parent, ITreeFactory<T> treeFactory, IEqualityComparer<T> equalityComparer)
		{
			if(parent == null)
				throw new ArgumentNullException("parent");

			if(treeFactory == null)
				throw new ArgumentNullException("treeFactory");

			if(equalityComparer == null)
				throw new ArgumentNullException("equalityComparer");

			this._equalityComparer = equalityComparer;
			this._list = new List<ITreeNode<T>>();
			this._parent = parent;
			this._treeFactory = treeFactory;
		}

		#endregion

		#region Properties

		protected internal virtual IEqualityComparer<T> EqualityComparer
		{
			get { return this._equalityComparer; }
		}

		public virtual ITreeNode<T> Parent
		{
			get { return this._parent; }
		}

		protected internal virtual ITreeFactory<T> TreeFactory
		{
			get { return this._treeFactory; }
		}

		#endregion

		#region Methods

		public virtual ITreeNode<T> Add(T value)
		{
			ITreeNode<T> treeNode = this.CreateTreeNode(value);
			this._list.Add(treeNode);
			return treeNode;
		}

		protected internal virtual ITreeNode<T> CreateTreeNode(T value)
		{
			ITreeNode<T> treeNode = this.TreeFactory.CreateTreeNode(this.Parent);
			treeNode.Value = value;
			return treeNode;
		}

		public virtual IEnumerator<ITreeNode<T>> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual int Remove(T value)
		{
			int removedTreeNodes = 0;

			for(int i = this._list.Count - 1; i >= 0; i--)
			{
				if(!this.EqualityComparer.Equals(this._list[i].Value, value))
					continue;

				this._list.RemoveAt(i);
				removedTreeNodes++;
			}

			return removedTreeNodes;
		}

		#endregion
	}
}