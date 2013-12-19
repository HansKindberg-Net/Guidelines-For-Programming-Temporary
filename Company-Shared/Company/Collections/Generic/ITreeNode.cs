using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Company.Collections.Generic
{
	public interface ITreeNode<T>
	{
		#region Properties

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		IEnumerable<ITreeNode<T>> Ancestors { get; }

		ITreeNodeCollection<T> Children { get; }

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		IEnumerable<ITreeNode<T>> Descendants { get; }

		bool IsFirstSibling { get; }
		bool IsLastSibling { get; }
		bool IsLeaf { get; }
		int Level { get; }
		ITreeNode<T> NextSibling { get; }
		ITreeNode<T> Parent { get; }
		ITreeNode<T> PreviousSibling { get; }
		ITreeNode<T> Root { get; }
		int SiblingIndex { get; }

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		IEnumerable<ITreeNode<T>> Siblings { get; }

		T Value { get; set; }

		#endregion
	}
}