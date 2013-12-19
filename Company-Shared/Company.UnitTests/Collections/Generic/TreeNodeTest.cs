using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Company.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.UnitTests.Collections.Generic
{
	[TestClass]
	public class TreeNodeTest
	{
		#region Methods

		[TestMethod]
		public void Ancestors_ShouldNeverReturnNull()
		{
			Assert.IsNotNull(new TreeNode<object>(Mock.Of<ITreeFactory<object>>()).Ancestors.ToArray());
		}

		[TestMethod]
		public void Ancestors_ShouldReturnAllAncestorsOfTheTreeNode()
		{
			int numberOfLevels = DateTime.Now.Millisecond + 1;
			const string rootValue = "1";
			const string childAppendValue = ".1";

			TreeNode<string> root = new TreeNode<string>(rootValue, CreateTreeFactory<string>());
			ITreeNode<string> leaf = root;

			for(int i = 1; i < numberOfLevels; i++)
			{
				leaf = leaf.Children.Add(leaf.Value + childAppendValue);
			}

			IEnumerable<ITreeNode<string>> ancestors = leaf.Ancestors.ToArray();

			Assert.AreEqual(numberOfLevels - 1, ancestors.Count());

			string value = rootValue;

			foreach(ITreeNode<string> ancestor in ancestors.Reverse())
			{
				Assert.AreEqual(value, ancestor.Value);
				value += childAppendValue;
			}
		}

		[TestMethod]
		public void Children_ShouldReturnTheTreeNodeCollectionCreatedByTheTreeFactory()
		{
			var treeNodeCollection = Mock.Of<ITreeNodeCollection<object>>();
			var treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns(treeNodeCollection);

			Assert.AreEqual(treeNodeCollection, new TreeNode<object>(treeFactoryMock.Object).Children);

			treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns((ITreeNodeCollection<object>) null);

			Assert.IsNull(new TreeNode<object>(treeFactoryMock.Object).Children);
		}

		private static TreeNode<string> CreateTree(int numberOfLevels, int numberOfChildren)
		{
			if(numberOfLevels < 1)
				throw new ArgumentException("The number of levels can not be less than 1.");

			TreeNode<string> root = new TreeNode<string>(null, "1", CreateTreeFactory<string>());

			PopulateTree(root, 1, numberOfLevels, numberOfChildren);

			return root;
		}

		private static ITreeFactory<T> CreateTreeFactory<T>()
		{
			return new TreeFactory<T>();
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "descendants")]
		public void Descendants_IfChildrenIsNull_ShouldThrowANullReferenceException()
		{
			var treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns((ITreeNodeCollection<object>) null);

			TreeNode<object> treeNode = new TreeNode<object>(treeFactoryMock.Object);

			Assert.IsNull(treeNode.Children);

			var descendants = treeNode.Descendants.ToArray();
		}

		[TestMethod]
		public void Descendants_ShouldReturnAllDescendantsOfTheTreeNode()
		{
			int numberOfLevels = DateTime.Now.Second%5 + 1;
			int numberOfChildren = numberOfLevels*2;

			TreeNode<string> tree = CreateTree(numberOfLevels, numberOfChildren);

			IEnumerable<ITreeNode<string>> descendants = tree.Descendants.ToArray();

			int expectedDescendantsCount = 0;
			int addend = 1;

			for(int i = 1; i <= numberOfLevels - 1; i++)
			{
				addend = addend*numberOfChildren;
				expectedDescendantsCount += addend;
			}

			Assert.AreEqual(expectedDescendantsCount, descendants.Count());
		}

		[TestMethod]
		public void Descendants_ShouldReturnDescendantsInHierarchicOrder()
		{
			const int numberOfLevels = 3;
			const int numberOfChildren = 4;

			TreeNode<string> tree = CreateTree(numberOfLevels, numberOfChildren);

			IEnumerable<ITreeNode<string>> descendants = tree.Descendants.ToArray();

			Assert.AreEqual("1.1", descendants.ElementAt(0).Value);
			Assert.AreEqual("1.1.1", descendants.ElementAt(1).Value);
			Assert.AreEqual("1.1.2", descendants.ElementAt(2).Value);
			Assert.AreEqual("1.1.3", descendants.ElementAt(3).Value);
			Assert.AreEqual("1.1.4", descendants.ElementAt(4).Value);
			Assert.AreEqual("1.2", descendants.ElementAt(5).Value);
			Assert.AreEqual("1.2.1", descendants.ElementAt(6).Value);
			Assert.AreEqual("1.2.2", descendants.ElementAt(7).Value);
			Assert.AreEqual("1.2.3", descendants.ElementAt(8).Value);
			Assert.AreEqual("1.2.4", descendants.ElementAt(9).Value);
			Assert.AreEqual("1.3", descendants.ElementAt(10).Value);
			Assert.AreEqual("1.3.1", descendants.ElementAt(11).Value);
			Assert.AreEqual("1.3.2", descendants.ElementAt(12).Value);
			Assert.AreEqual("1.3.3", descendants.ElementAt(13).Value);
			Assert.AreEqual("1.3.4", descendants.ElementAt(14).Value);
			Assert.AreEqual("1.4", descendants.ElementAt(15).Value);
			Assert.AreEqual("1.4.1", descendants.ElementAt(16).Value);
			Assert.AreEqual("1.4.2", descendants.ElementAt(17).Value);
			Assert.AreEqual("1.4.3", descendants.ElementAt(18).Value);
			Assert.AreEqual("1.4.4", descendants.ElementAt(19).Value);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "isFirstSibling")]
		public void IsFirstSibling_IfChildrenOfTheParentIsNull_ShouldThrowAnArgumentNullException()
		{
			var treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns((ITreeNodeCollection<object>) null);

			TreeNode<object> treeNode = new TreeNode<object>(new TreeNode<object>(null, treeFactoryMock.Object), treeFactoryMock.Object);

			Assert.IsNotNull(treeNode.Parent);

			Assert.IsNull(treeNode.Parent.Children);

			var isFirstSibling = treeNode.IsFirstSibling;
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		public void IsFirstSibling_IfTheParentIsNull_ShouldReturnTrue()
		{
			ITreeNode<object> treeNode = new TreeNode<object>(null, Mock.Of<ITreeFactory<object>>());

			Assert.IsNull(treeNode.Parent);

			Assert.IsTrue(treeNode.IsFirstSibling);
		}

		[TestMethod]
		public void IsFirstSibling_ShouldReturnTrueIfTheTreeNodeIsFirstWithinTheChildrenOfTheParent()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;

			TreeNode<string> root = CreateTree(2, numberOfChildren);

			ITreeNode<string> selectedTreeNode = root.Children.First();

			Assert.IsTrue(selectedTreeNode.IsFirstSibling);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "isLastSibling")]
		public void IsLastSibling_IfChildrenOfTheParentIsNull_ShouldThrowAnArgumentNullException()
		{
			var treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns((ITreeNodeCollection<object>) null);

			TreeNode<object> treeNode = new TreeNode<object>(new TreeNode<object>(null, treeFactoryMock.Object), treeFactoryMock.Object);

			Assert.IsNotNull(treeNode.Parent);

			Assert.IsNull(treeNode.Parent.Children);

			var isLastSibling = treeNode.IsLastSibling;
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		public void IsLastSibling_IfTheParentIsNull_ShouldReturnTrue()
		{
			ITreeNode<object> treeNode = new TreeNode<object>(null, Mock.Of<ITreeFactory<object>>());

			Assert.IsNull(treeNode.Parent);

			Assert.IsTrue(treeNode.IsLastSibling);
		}

		[TestMethod]
		public void IsLastSibling_ShouldReturnTrueIfTheTreeNodeIsLastWithinTheChildrenOfTheParent()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;

			TreeNode<string> root = CreateTree(2, numberOfChildren);

			ITreeNode<string> selectedTreeNode = root.Children.Last();

			Assert.IsTrue(selectedTreeNode.IsLastSibling);
		}

		[TestMethod]
		public void IsLeaf_IfChildrenIsEmpty_ShouldReturnTrue()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;
			int selectedTreeNodeIndex = numberOfChildren%50;

			if(selectedTreeNodeIndex == numberOfChildren)
				selectedTreeNodeIndex = selectedTreeNodeIndex - 1;

			TreeNode<string> root = CreateTree(2, numberOfChildren);

			ITreeNode<string> selectedTreeNode = root.Children.ElementAt(selectedTreeNodeIndex);

			Assert.IsFalse(selectedTreeNode.Children.Any());

			Assert.IsTrue(selectedTreeNode.IsLeaf);
		}

		[TestMethod]
		public void IsLeaf_IfChildrenIsNotEmpty_ShouldReturnFalse()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;
			int selectedTreeNodeIndex = numberOfChildren%50;

			if(selectedTreeNodeIndex == numberOfChildren)
				selectedTreeNodeIndex = selectedTreeNodeIndex - 1;

			TreeNode<string> root = CreateTree(3, numberOfChildren);

			ITreeNode<string> selectedTreeNode = root.Children.ElementAt(selectedTreeNodeIndex);

			Assert.IsTrue(selectedTreeNode.Children.Any());

			Assert.IsFalse(selectedTreeNode.IsLeaf);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "isLeaf")]
		public void IsLeaf_IfChildrenIsNull_ShouldThrowAnArgumentNullException()
		{
			var treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns((ITreeNodeCollection<object>) null);

			TreeNode<object> treeNode = new TreeNode<object>(treeFactoryMock.Object);

			Assert.IsNull(treeNode.Children);

			var isLeaf = treeNode.IsLeaf;
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "nextSibling")]
		public void NextSibling_IfChildrenOfTheParentIsNull_ShouldThrowAnArgumentNullException()
		{
			var treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns((ITreeNodeCollection<object>) null);

			TreeNode<object> treeNode = new TreeNode<object>(new TreeNode<object>(null, treeFactoryMock.Object), treeFactoryMock.Object);

			Assert.IsNotNull(treeNode.Parent);

			Assert.IsNull(treeNode.Parent.Children);

			var nextSibling = treeNode.NextSibling;
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		public void NextSibling_IfTheParentIsNull_ShouldReturnNull()
		{
			ITreeNode<object> treeNode = new TreeNode<object>(null, Mock.Of<ITreeFactory<object>>());

			Assert.IsNull(treeNode.Parent);

			Assert.IsNull(treeNode.NextSibling);
		}

		[TestMethod]
		public void NextSibling_IfTheTreeNodeIsNotTheLastSibling_ShouldReturnTheTreeNodeAfterTheTreeNodeWithinTheChildrenOfTheParent()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;
			int selectedTreeNodeIndex = numberOfChildren%50;

			if(selectedTreeNodeIndex == numberOfChildren)
				selectedTreeNodeIndex = selectedTreeNodeIndex - 1;

			var root = CreateTree(2, numberOfChildren + 1);

			var selectedTreeNode = root.Children.ElementAt(selectedTreeNodeIndex);

			var nextSibling = root.Children.ElementAt(selectedTreeNodeIndex + 1);

			Assert.AreEqual(nextSibling, selectedTreeNode.NextSibling);
		}

		[TestMethod]
		public void NextSibling_IfTheTreeNodeIsTheLastSibling_ShouldReturnNull()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;
			int selectedTreeNodeIndex = numberOfChildren - 1;

			var root = CreateTree(2, numberOfChildren);

			var selectedTreeNode = root.Children.ElementAt(selectedTreeNodeIndex);

			Assert.IsNull(selectedTreeNode.NextSibling);
		}

		private static void PopulateTree(ITreeNode<string> parent, int level, int numberOfLevels, int numberOfChildren)
		{
			if(parent == null)
				throw new ArgumentNullException("parent");

			if(level < 1)
				return;

			if(level >= numberOfLevels)
				return;

			for(int i = 1; i <= numberOfChildren; i++)
			{
				ITreeNode<string> child = parent.Children.Add(parent.Value + "." + i.ToString(CultureInfo.InvariantCulture));
				PopulateTree(child, level + 1, numberOfLevels, numberOfChildren);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "previousSibling")]
		public void PreviousSibling_IfChildrenOfTheParentIsNull_ShouldThrowAnArgumentNullException()
		{
			var treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns((ITreeNodeCollection<object>) null);

			TreeNode<object> treeNode = new TreeNode<object>(new TreeNode<object>(null, treeFactoryMock.Object), treeFactoryMock.Object);

			Assert.IsNotNull(treeNode.Parent);

			Assert.IsNull(treeNode.Parent.Children);

			var previousSibling = treeNode.PreviousSibling;
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		public void PreviousSibling_IfTheParentIsNull_ShouldReturnNull()
		{
			ITreeNode<object> treeNode = new TreeNode<object>(null, Mock.Of<ITreeFactory<object>>());

			Assert.IsNull(treeNode.Parent);

			Assert.IsNull(treeNode.PreviousSibling);
		}

		[TestMethod]
		public void PreviousSibling_IfTheTreeNodeIsNotTheFirstSibling_ShouldReturnTheTreeNodeBeforeTheTreeNodeWithinTheChildrenOfTheParent()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;
			int selectedTreeNodeIndex = numberOfChildren%50;

			if(selectedTreeNodeIndex == numberOfChildren)
				selectedTreeNodeIndex = selectedTreeNodeIndex - 1;

			if(selectedTreeNodeIndex == 0)
				selectedTreeNodeIndex = 1;

			var root = CreateTree(2, numberOfChildren);

			var selectedTreeNode = root.Children.ElementAt(selectedTreeNodeIndex);

			var previousSibling = root.Children.ElementAt(selectedTreeNodeIndex - 1);

			Assert.AreEqual(previousSibling, selectedTreeNode.PreviousSibling);
		}

		[TestMethod]
		public void PreviousSibling_IfTheTreeNodeIsTheFirstSibling_ShouldReturnNull()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;
			const int selectedTreeNodeIndex = 0;

			var root = CreateTree(2, numberOfChildren);

			var selectedTreeNode = root.Children.ElementAt(selectedTreeNodeIndex);

			Assert.IsNull(selectedTreeNode.PreviousSibling);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "siblingIndex")]
		public void SiblingIndex_IfChildrenOfTheParentIsNull_ShouldThrowAnArgumentNullException()
		{
			var treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns((ITreeNodeCollection<object>) null);

			TreeNode<object> treeNode = new TreeNode<object>(new TreeNode<object>(null, treeFactoryMock.Object), treeFactoryMock.Object);

			Assert.IsNotNull(treeNode.Parent);

			Assert.IsNull(treeNode.Parent.Children);

			var siblingIndex = treeNode.SiblingIndex;
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		public void SiblingIndex_IfTheParentIsNull_ShouldReturnZero()
		{
			ITreeNode<object> treeNode = new TreeNode<object>(null, Mock.Of<ITreeFactory<object>>());

			Assert.IsNull(treeNode.Parent);

			Assert.AreEqual(0, treeNode.SiblingIndex);
		}

		[TestMethod]
		public void SiblingIndex_ShouldReturnTheIndexOfTheTreeNodeWithinTheChildrenOfTheParent()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;
			int selectedTreeNodeIndex = numberOfChildren%50;

			if(selectedTreeNodeIndex == numberOfChildren)
				selectedTreeNodeIndex = selectedTreeNodeIndex - 1;

			TreeNode<string> root = CreateTree(2, numberOfChildren);

			ITreeNode<string> selectedTreeNode = root.Children.ElementAt(selectedTreeNodeIndex);

			Assert.AreEqual(selectedTreeNodeIndex, selectedTreeNode.SiblingIndex);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "siblings")]
		public void Siblings_IfChildrenOfTheParentIsNull_ShouldThrowAnArgumentNullException()
		{
			var treeFactoryMock = new Mock<ITreeFactory<object>>();
			treeFactoryMock.Setup(treeFactory => treeFactory.CreateTreeNodeCollection(It.IsAny<ITreeNode<object>>())).Returns((ITreeNodeCollection<object>) null);

			TreeNode<object> treeNode = new TreeNode<object>(new TreeNode<object>(null, treeFactoryMock.Object), treeFactoryMock.Object);

			Assert.IsNotNull(treeNode.Parent);

			Assert.IsNull(treeNode.Parent.Children);

			var siblings = treeNode.Siblings.ToArray();
		}

		[TestMethod]
		public void Siblings_ShouldReturnTheChildrenOfTheParentExpectTheTreeNodeItself()
		{
			int numberOfChildren = DateTime.Now.Millisecond + 1;
			int selectedTreeNodeIndex = numberOfChildren%50;

			if(selectedTreeNodeIndex == numberOfChildren)
				selectedTreeNodeIndex = selectedTreeNodeIndex - 1;

			TreeNode<string> root = CreateTree(2, numberOfChildren);

			ITreeNode<string> selectedTreeNode = root.Children.ElementAt(selectedTreeNodeIndex);

			IEnumerable<ITreeNode<string>> siblings = selectedTreeNode.Siblings.ToArray();

			Assert.AreEqual(numberOfChildren - 1, siblings.Count());

			Assert.IsNull(siblings.FirstOrDefault(sibling => sibling.Value.Equals(selectedTreeNode.Value)));
		}

		#endregion
	}
}