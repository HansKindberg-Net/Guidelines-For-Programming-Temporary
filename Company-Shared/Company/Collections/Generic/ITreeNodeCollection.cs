using System.Collections.Generic;

namespace Company.Collections.Generic
{
	public interface ITreeNodeCollection<T> : IEnumerable<ITreeNode<T>>
	{
		#region Properties

		ITreeNode<T> Parent { get; }

		#endregion

		#region Methods

		ITreeNode<T> Add(T value);
		int Remove(T value);

		#endregion
	}
}