using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Company.Collections.Generic.Traversing
{
	public class TreeTraverserFactory<T> : ITreeTraverserFactory<T>
	{
		//protected internal virtual void CorrectTreeTraversingNodeLevel(TreeTraversingNode<T> treeTraversingNode, bool includeRoot)
		//{
		//	if(treeTraversingNode == null)
		//		throw new ArgumentNullException("treeTraversingNode");
		//	if(!includeRoot)
		//		treeTraversingNode.Level--;
		//}
		//[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		//public virtual IEnumerable<ITreeTraversingNode<T>> Create(ITreeNode<T> rootNode, ITreeNode<T> selectedNode, bool includeRoot, int numberOfLevels, bool expandAllNodes)
		//{
		//	if(rootNode == null)
		//		throw new ArgumentNullException("rootNode");
		//	if(numberOfLevels < 0)
		//		throw new ArgumentException("The number of levels can not be less than zero.", "numberOfLevels");
		//	if(numberOfLevels < 1)
		//		return new ITreeTraversingNode<T>[0];
		//	var nodeList = new List<ITreeNode<T>>(rootNode.Descendants);
		//	if(includeRoot)
		//		nodeList.Insert(0, rootNode);
		//	int maximumLevel = includeRoot ? numberOfLevels - 1 : numberOfLevels;
		//	var nodes = nodeList.Where(treeNode => treeNode.Level <= maximumLevel && (expandAllNodes || (selectedNode == null && treeNode.Level <= (includeRoot ? 0 : 1)) || (selectedNode != null && (selectedNode.Equals(treeNode) || selectedNode.Ancestors.Contains(treeNode) || selectedNode.Equals(treeNode.Parent))))).ToArray();
		//	var traversingNodes = new List<ITreeTraversingNode<T>>();
		//	for(int i = 0; i < nodes.Count(); i++)
		//	{
		//		var currentNode = nodes.ElementAt(i);
		//		if(i == 0)
		//		{
		//			var header = this.CreateTreeTraversingMetadataNode(rootNode, includeRoot);
		//			header.IsHeader = true;
		//			header.IsLevelHeader = true;
		//			traversingNodes.Add(header);
		//		}
		//		else
		//		{
		//			var previousNode = nodes.ElementAt(i - 1);
		//			if(previousNode.Level < currentNode.Level)
		//			{
		//				var levelHeader = this.CreateTreeTraversingMetadataNode(previousNode, includeRoot);
		//				levelHeader.IsLevelHeader = true;
		//				traversingNodes.Add(levelHeader);
		//			}
		//		}
		//		var itemHeader = this.CreateTreeTraversingMetadataNode(currentNode, includeRoot);
		//		itemHeader.IsItemHeader = true;
		//		itemHeader.IsSelectedAncestorHeader = currentNode.Ancestors.Contains(selectedNode);
		//		itemHeader.IsSelectedItemHeader = currentNode.Equals(selectedNode);
		//		traversingNodes.Add(itemHeader);
		//		var item = this.CreateTreeTraversingItemNode(currentNode, includeRoot);
		//		item.IsItem = true;
		//		item.IsSelectedAncestor = currentNode.Ancestors.Contains(selectedNode);
		//		item.IsSelectedItem = currentNode.Equals(selectedNode);
		//		traversingNodes.Add(item);
		//		var itemFooter = this.CreateTreeTraversingMetadataNode(currentNode, includeRoot);
		//		itemFooter.IsItemFooter = true;
		//		itemFooter.IsSelectedAncestorFooter = currentNode.Ancestors.Contains(selectedNode);
		//		itemFooter.IsSelectedItemFooter = currentNode.Equals(selectedNode);
		//		traversingNodes.Add(itemFooter);
		//		if(i == nodes.Count() - 1)
		//		{
		//			var treeTraversingMetadataNode = this.CreateTreeTraversingMetadataNode(rootNode, includeRoot);
		//			treeTraversingMetadataNode.IsFooter = true;
		//			treeTraversingMetadataNode.IsLevelFooter = true;
		//			traversingNodes.Add(treeTraversingMetadataNode);
		//		}
		//		else
		//		{
		//			var nextNode = nodes.ElementAt(i + 1);
		//			if(nextNode.Level < currentNode.Level)
		//			{
		//				var treeTraversingMetadataNode = this.CreateTreeTraversingMetadataNode(nextNode, includeRoot);
		//				treeTraversingMetadataNode.IsLevelFooter = true;
		//				traversingNodes.Add(treeTraversingMetadataNode);
		//			}
		//		}
		//	}
		//	return traversingNodes.ToArray();
		//}
		//protected internal virtual TreeTraversingItemNode<T> CreateTreeTraversingItemNode(ITreeNode<T> treeNode, bool includeRoot)
		//{
		//	if(treeNode == null)
		//		throw new ArgumentNullException("treeNode");
		//	var treeTraversingItemNode = new TreeTraversingItemNode<T>(treeNode);
		//	this.CorrectTreeTraversingNodeLevel(treeTraversingItemNode, includeRoot);
		//	return treeTraversingItemNode;
		//}
		//protected internal virtual TreeTraversingMetadataNode<T> CreateTreeTraversingMetadataNode(ITreeNode<T> treeNode, bool includeRoot)
		//{
		//	if(treeNode == null)
		//		throw new ArgumentNullException("treeNode");
		//	var treeTraversingMetadataNode = new TreeTraversingMetadataNode<T>(treeNode);
		//	this.CorrectTreeTraversingNodeLevel(treeTraversingMetadataNode, includeRoot);
		//	return treeTraversingMetadataNode;
		//}
		//[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]

		#region Methods

		public virtual IEnumerable<ITreeTraversingNode<T>> Create(ITreeNode<T> rootNode, ITreeNode<T> selectedNode, bool includeRoot, int numberOfLevels, bool expandAllNodes)
		{
			if(rootNode == null)
				throw new ArgumentNullException("rootNode");

			var traversingNodes = new List<ITreeTraversingNode<T>>();

			this.Populate(traversingNodes, includeRoot ? new[] {rootNode} : (IEnumerable<ITreeNode<T>>) rootNode.Children, selectedNode, 0, numberOfLevels, expandAllNodes);

			return traversingNodes.ToArray();
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

			if(level >= numberOfLevels)
				return;

			if(!expandAllNodes && level > 0 && selectedNode == null)
				return;

			var headerNode = new TreeTraversingMetadataNode<T>(items.First()) {IsLevelHeader = true};

			if(level == 0)
				headerNode.IsHeader = true;

			traversingNodes.Add(headerNode);

			foreach(var item in items)
			{
				if(!expandAllNodes && level > 0 && selectedNode != null && !selectedNode.Equals(item) && !selectedNode.Ancestors.Contains(item) && !selectedNode.Equals(item.Parent))
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

		//protected internal abstract Control CreateTreeNodeContainer(ITreeNode<T> treeNode);
		//protected internal virtual bool IncludeLevel(int level, int numberOfLevels)
		//{
		//	if (!this.View.NumberOfLevels.HasValue)
		//		return true;
		//	if (this.View.NumberOfLevels.Value < 1)
		//		return false;
		//	return level < this.View.NumberOfLevels.Value;
		//}
	}
}