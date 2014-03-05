using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Company.Collections.Generic.Traversing
{
	public class TreeTraverserFactory<T> : ITreeTraverserFactory<T>
	{
		#region Methods

		public virtual IEnumerable<ITreeTraversingNode<T>> Create(ITreeNode<T> rootNode, ITreeNode<T> selectedNode, bool includeRoot, int numberOfLevels, bool expandAllNodes)
		{
			if(rootNode == null)
				throw new ArgumentNullException("rootNode");

			var traversingNodes = new List<ITreeTraversingNode<T>>();

			this.Populate(traversingNodes, includeRoot ? new[] {rootNode} : (IEnumerable<ITreeNode<T>>) rootNode.Children, selectedNode, 0, numberOfLevels, expandAllNodes);

			return traversingNodes.ToArray();
		}

		protected internal virtual bool Include(ITreeNode<T> selectedNode, bool expandAllNodes, int numberOfLevels, int level)
		{
			if(level >= numberOfLevels)
				return false;

			if(expandAllNodes)
				return true;

			return level <= 0 || selectedNode != null;
		}

		protected internal virtual bool IncludeNode(ITreeNode<T> treeNode, ITreeNode<T> selectedNode, bool expandAllNodes, int numberOfLevels, int level)
		{
			if(!this.Include(selectedNode, expandAllNodes, numberOfLevels, level))
				return false;

			if(treeNode == null)
				return false;

			if(level > 0)
			{
				if(selectedNode == null)
					return false;

				if(treeNode.Equals(selectedNode))
					return true;

				if(selectedNode.Ancestors.Contains(treeNode))
					return true;

				if(selectedNode.Equals(treeNode.Parent))
					return true;

				if(treeNode.Siblings.Contains(selectedNode))
					return true;

				return selectedNode.Ancestors.SelectMany(ancestor => ancestor.Siblings).Contains(treeNode);
			}

			return true;
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal virtual void Populate(IList<ITreeTraversingNode<T>> traversingNodes, IEnumerable<ITreeNode<T>> items, ITreeNode<T> selectedNode, int level, int numberOfLevels, bool expandAllNodes)
		{
			if(traversingNodes == null)
				throw new ArgumentNullException("traversingNodes");

			if(items == null)
				throw new ArgumentNullException("items");

			if(level < 0 || level == int.MaxValue)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The level must be between 0 and {0}.", int.MaxValue - 1));

			if(numberOfLevels < 0)
				throw new ArgumentException("The number of levels can not be less than zero.", "numberOfLevels");

			// ReSharper disable PossibleMultipleEnumeration

			if(!items.Any())
				return;

			if(!this.Include(selectedNode, expandAllNodes, numberOfLevels, level))
				return;

			var headerNode = new TreeTraversingMetadataNode<T>(items.First()) {IsLevelHeader = true};

			if(level == 0)
				headerNode.IsHeader = true;

			traversingNodes.Add(headerNode);

			foreach(var item in items)
			{
				if(!this.IncludeNode(item, selectedNode, expandAllNodes, numberOfLevels, level))
					continue;

				var itemHeaderNode = new TreeTraversingMetadataNode<T>(item)
				{
					IsItemHeader = true,
					IsSelectedAncestorHeader = selectedNode != null && selectedNode.Ancestors.Contains(item),
					IsSelectedItemHeader = item.Equals(selectedNode)
				};

				traversingNodes.Add(itemHeaderNode);

				var itemNode = new TreeTraversingItemNode<T>(item)
				{
					IsItem = true,
					IsSelectedAncestor = selectedNode != null && selectedNode.Ancestors.Contains(item),
					IsSelectedItem = item.Equals(selectedNode)
				};

				traversingNodes.Add(itemNode);

				this.Populate(traversingNodes, item.Children, selectedNode, level + 1, numberOfLevels, expandAllNodes);

				var itemFooterNode = new TreeTraversingMetadataNode<T>(item)
				{
					IsItemFooter = true,
					IsSelectedAncestorFooter = selectedNode != null && selectedNode.Ancestors.Contains(item),
					IsSelectedItemFooter = item.Equals(selectedNode)
				};

				traversingNodes.Add(itemFooterNode);
			}

			var footerNode = new TreeTraversingMetadataNode<T>(items.Last()) {IsLevelFooter = true};

			if(level == 0)
				footerNode.IsFooter = true;

			traversingNodes.Add(footerNode);

			// ReSharper restore PossibleMultipleEnumeration
		}

		#endregion
	}
}