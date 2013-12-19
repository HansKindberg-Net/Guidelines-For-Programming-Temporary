using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.Collections.Generic
{
	public class TreeNode<T> : ITreeNode<T>
	{
		#region Fields

		private readonly ITreeNodeCollection<T> _children;
		private ITreeNode<T> _parent;
		private T _value;

		#endregion

		#region Constructors

		public TreeNode(ITreeFactory<T> treeFactory) : this(null, treeFactory) {}
		public TreeNode(T value, ITreeFactory<T> treeFactory) : this(null, value, treeFactory) {}
		public TreeNode(ITreeNode<T> parent, ITreeFactory<T> treeFactory) : this(parent, default(T), treeFactory) {}

		public TreeNode(ITreeNode<T> parent, T value, ITreeFactory<T> treeFactory)
		{
			if(treeFactory == null)
				throw new ArgumentNullException("treeFactory");

			this._children = treeFactory.CreateTreeNodeCollection(this);
			this._parent = parent;
			this._value = value;
		}

		#endregion

		#region Properties

		public virtual IEnumerable<ITreeNode<T>> Ancestors
		{
			get
			{
				List<ITreeNode<T>> ancestors = new List<ITreeNode<T>>();

				ITreeNode<T> parent = this.Parent;

				while(parent != null)
				{
					ancestors.Add(parent);

					parent = parent.Parent;
				}

				return ancestors.ToArray();
			}
		}

		public virtual ITreeNodeCollection<T> Children
		{
			get { return this._children; }
		}

		public virtual IEnumerable<ITreeNode<T>> Descendants
		{
			get
			{
				foreach(ITreeNode<T> child in this.Children)
				{
					yield return child;

					foreach(ITreeNode<T> descendant in child.Descendants)
					{
						yield return descendant;
					}
				}
			}
		}

		public virtual bool IsFirstSibling
		{
			get { return this.Parent == null || this.Equals(this.Parent.Children.First()); }
		}

		public virtual bool IsLastSibling
		{
			get { return this.Parent == null || this.Equals(this.Parent.Children.Last()); }
		}

		public virtual bool IsLeaf
		{
			get { return !this.Children.Any(); }
		}

		public virtual int Level
		{
			get { return this.Ancestors.Count(); }
		}

		public virtual ITreeNode<T> NextSibling
		{
			get
			{
				if(this.Parent == null)
					return null;

				var siblingIndex = this.SiblingIndex;

				return siblingIndex < this.Parent.Children.Count() - 1 ? this.Parent.Children.ElementAt(siblingIndex + 1) : null;
			}
		}

		public virtual ITreeNode<T> Parent
		{
			get { return this._parent; }
			set { this._parent = value; }
		}

		public virtual ITreeNode<T> PreviousSibling
		{
			get
			{
				if(this.Parent == null)
					return null;

				var siblingIndex = this.SiblingIndex;

				return siblingIndex > 0 ? this.Parent.Children.ElementAt(siblingIndex - 1) : null;
			}
		}

		public virtual ITreeNode<T> Root
		{
			get
			{
				ITreeNode<T> root = this;

				while(root.Parent != null)
				{
					root = root.Parent;
				}

				return root;
			}
		}

		public virtual int SiblingIndex
		{
			get
			{
				if(this.Parent == null)
					return 0;

				for(int i = 0; i < this.Parent.Children.Count(); i++)
				{
					if(this == this.Parent.Children.ElementAt(i))
						return i;
				}

				throw new InvalidOperationException("The tree-node must be included in the children of the parent.");
			}
		}

		public virtual IEnumerable<ITreeNode<T>> Siblings
		{
			get
			{
				List<ITreeNode<T>> siblings = new List<ITreeNode<T>>();

				if(this.Parent != null)
					siblings.AddRange(this.Parent.Children.Where(childOfParent => this != childOfParent));

				return siblings.ToArray();
			}
		}

		public virtual T Value
		{
			get { return this._value; }
			set { this._value = value; }
		}

		#endregion
	}
}