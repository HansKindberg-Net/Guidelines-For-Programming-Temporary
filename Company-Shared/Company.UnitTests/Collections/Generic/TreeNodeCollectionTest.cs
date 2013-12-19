using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.UnitTests.Collections.Generic
{
	[TestClass]
	public class TreeNodeCollectionTest
	{
		#region Methods

		[TestMethod]
		public void MoreTestsNeeded()
		{
			Assert.Inconclusive("More tests needed.");
		}

		#endregion

		//// Moved from TreeNode, fix the test so it suits in the context of TreeNodeCollection
		//[TestMethod]
		//public void Children_ShouldReturnAllChildrenOfTheTreeNode()
		//{
		//	int numberOfChildren = DateTime.Now.Millisecond;
		//	const string rootValue = "1";
		//	TreeNode<string> root = new TreeNode<string>(rootValue);
		//	for (int i = 1; i <= numberOfChildren; i++)
		//	{
		//		root.Children.Add(rootValue + "." + i.ToString(CultureInfo.InvariantCulture));
		//	}
		//	IEnumerable<ITreeNode<string>> children = root.Children.ToArray();
		//	Assert.AreEqual(numberOfChildren, children.Count());
		//	for (int i = 1; i <= numberOfChildren; i++)
		//	{
		//		Assert.AreEqual(rootValue + "." + i.ToString(CultureInfo.InvariantCulture), children.ElementAt(i - 1).Value);
		//	}
		//}
	}
}