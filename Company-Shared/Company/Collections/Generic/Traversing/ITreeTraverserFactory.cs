using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Company.Collections.Generic.Traversing
{
	public interface ITreeTraverserFactory<T>
	{
		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		IEnumerable<ITreeTraversingNode<T>> Create(ITreeNode<T> rootNode, ITreeNode<T> selectedNode, bool includeRoot, int numberOfLevels, bool expandAllNodes);

		#endregion
	}
}