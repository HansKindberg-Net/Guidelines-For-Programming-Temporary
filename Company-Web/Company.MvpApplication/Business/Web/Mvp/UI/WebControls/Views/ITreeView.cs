using System;
using System.ComponentModel;
using System.Web.UI;
using Company.Collections.Generic;
using Company.MvpApplication.Business.Mvp.Views;
using Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Models;

namespace Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Views
{
	public interface ITreeView<TModel, T> : IView<TModel> where TModel : TreeModel<T>
	{
		#region Events

		event EventHandler<CancelEventArgs> CreatingChildControls;
		event EventHandler<CancelEventArgs> DataBindingChildren;
		event EventHandler DataBound;
		event EventHandler<CancelEventArgs> EnsuringChildControls;
		event EventHandler PreRender;

		#endregion

		#region Properties

		ControlCollection Controls { get; }
		ITreeNode<T> Current { get; }
		bool DataBindAutomatically { get; }
		bool ExpandAllNodes { get; }
		ITemplate FooterTemplate { get; }
		ITemplate HeaderTemplate { get; }
		bool IncludeRoot { get; }
		ITemplate ItemFooterTemplate { get; }
		ITemplate ItemHeaderTemplate { get; }
		ITemplate ItemTemplate { get; }
		ITemplate LevelFooterTemplate { get; }
		ITemplate LevelHeaderTemplate { get; }
		int? NumberOfLevels { get; }
		ITreeNode<T> Root { get; }
		ITemplate SelectedAncestorHeaderTemplate { get; }
		ITemplate SelectedAncestorTemplate { get; }
		ITemplate SelectedItemHeaderTemplate { get; }
		ITemplate SelectedItemTemplate { get; }
		bool Visible { get; }

		#endregion

		#region Methods

		void DataBind();
		void EnsureChildControlsAreCreated();

		#endregion
	}
}