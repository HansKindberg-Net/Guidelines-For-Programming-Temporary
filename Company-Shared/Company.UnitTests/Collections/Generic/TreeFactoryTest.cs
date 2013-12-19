using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Company.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.UnitTests.Collections.Generic
{
	[TestClass]
	public class TreeFactoryTest
	{
		#region Methods

		private static void CreateTreeNodeCollectionShouldReturnATreeNodeCollectionWhereTheEqualityComparerIsEqualToTheDefaultEqualityComparer<T>()
		{
			TreeNodeCollection<T> treeNodeCollection = (TreeNodeCollection<T>) new TreeFactory<T>().CreateTreeNodeCollection(Mock.Of<ITreeNode<T>>());
			Assert.AreEqual(EqualityComparer<T>.Default, treeNodeCollection.EqualityComparer);
		}

		private static void CreateTreeNodeCollectionShouldReturnATreeNodeCollectionWhereTheParentIsEqualToTheParentParameter<T>(ITreeNode<T> parent)
		{
			Assert.AreEqual(parent, new TreeFactory<T>().CreateTreeNodeCollection(parent).Parent);
		}

		private static void CreateTreeNodeCollectionShouldReturnAnObjectOfTypeTreeNodeCollection<T>()
		{
			Type treeNodeCollectionType = new TreeFactory<T>().CreateTreeNodeCollection(Mock.Of<ITreeNode<T>>()).GetType();
			IEnumerable<Type> genericArguments = treeNodeCollectionType.GetGenericArguments();

			Assert.AreEqual(typeof(TreeNodeCollection<>), treeNodeCollectionType.GetGenericTypeDefinition());
			Assert.AreEqual(1, genericArguments.Count());
			Assert.AreEqual(typeof(T), genericArguments.ElementAt(0));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		public void CreateTreeNodeCollection_IfTheParentParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			int numberOfArgumentNullExceptions = 0;

			try
			{
				new TreeFactory<object>().CreateTreeNodeCollection(null);
			}
			catch(ArgumentNullException)
			{
				numberOfArgumentNullExceptions++;
			}

			try
			{
				new TreeFactory<int>().CreateTreeNodeCollection(null);
			}
			catch(ArgumentNullException)
			{
				numberOfArgumentNullExceptions++;
			}

			// ReSharper disable NotResolvedInText
			if(numberOfArgumentNullExceptions == 2)
				throw new ArgumentNullException("parent");
			// ReSharper restore NotResolvedInText
		}

		[TestMethod]
		public void CreateTreeNodeCollection_ShouldReturnATreeNodeCollectionWhereTheEqualityComparerIsEqualToTheDefaultEqualityComparer()
		{
			CreateTreeNodeCollectionShouldReturnATreeNodeCollectionWhereTheEqualityComparerIsEqualToTheDefaultEqualityComparer<object>();
			CreateTreeNodeCollectionShouldReturnATreeNodeCollectionWhereTheEqualityComparerIsEqualToTheDefaultEqualityComparer<int>();
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		public void CreateTreeNodeCollection_ShouldReturnATreeNodeCollectionWhereTheParentIsEqualToTheParentParameter()
		{
			CreateTreeNodeCollectionShouldReturnATreeNodeCollectionWhereTheParentIsEqualToTheParentParameter(Mock.Of<ITreeNode<object>>());
			CreateTreeNodeCollectionShouldReturnATreeNodeCollectionWhereTheParentIsEqualToTheParentParameter(Mock.Of<ITreeNode<int>>());
		}

		[TestMethod]
		public void CreateTreeNodeCollection_ShouldReturnAnObjectOfTypeTreeNodeCollection()
		{
			CreateTreeNodeCollectionShouldReturnAnObjectOfTypeTreeNodeCollection<object>();
			CreateTreeNodeCollectionShouldReturnAnObjectOfTypeTreeNodeCollection<int>();
		}

		private static void CreateTreeNodeShouldReturnATreeNodeWhereTheParentIsEqualToTheParentParameter<T>(ITreeNode<T> parent)
		{
			Assert.AreEqual(parent, new TreeFactory<T>().CreateTreeNode(parent).Parent);
		}

		private static void CreateTreeNodeShouldReturnATreeNodeWithADefaultValue<T>()
		{
			Assert.AreEqual(default(T), new TreeFactory<T>().CreateTreeNode(null).Value);
		}

		private static void CreateTreeNodeShouldReturnAnObjectOfTypeTreeNode<T>()
		{
			Type treeNodeType = new TreeFactory<T>().CreateTreeNode(null).GetType();
			IEnumerable<Type> genericArguments = treeNodeType.GetGenericArguments();

			Assert.AreEqual(typeof(TreeNode<>), treeNodeType.GetGenericTypeDefinition());
			Assert.AreEqual(1, genericArguments.Count());
			Assert.AreEqual(typeof(T), genericArguments.ElementAt(0));
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ParentIs")]
		public void CreateTreeNode_ShouldReturnATreeNodeWhereTheParentIsEqualToTheParentParameter()
		{
			CreateTreeNodeShouldReturnATreeNodeWhereTheParentIsEqualToTheParentParameter(Mock.Of<ITreeNode<object>>());
			CreateTreeNodeShouldReturnATreeNodeWhereTheParentIsEqualToTheParentParameter<object>(null);
			CreateTreeNodeShouldReturnATreeNodeWhereTheParentIsEqualToTheParentParameter(Mock.Of<ITreeNode<int>>());
		}

		[TestMethod]
		public void CreateTreeNode_ShouldReturnATreeNodeWithADefaultValue()
		{
			CreateTreeNodeShouldReturnATreeNodeWithADefaultValue<object>();
			CreateTreeNodeShouldReturnATreeNodeWithADefaultValue<int>();
		}

		[TestMethod]
		public void CreateTreeNode_ShouldReturnAnObjectOfTypeTreeNode()
		{
			CreateTreeNodeShouldReturnAnObjectOfTypeTreeNode<object>();
			CreateTreeNodeShouldReturnAnObjectOfTypeTreeNode<int>();
		}

		#endregion
	}
}