using System.ComponentModel;
using System.Web.UI;
using Company.Collections.Generic;
using Company.MvpApplication.Business.Web.Mvp.UI.Views;
using Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Models;

namespace Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Views
{
	[ParseChildren(true)]
	public abstract class TreeView<TModel, T> : Control<TModel>, INamingContainer, INotifyPropertyChanged, ITreeView<TModel, T> where TModel : TreeModel<T>
	{
		#region Fields

		private const string _currentViewStateName = "Current";
		private const string _dataBindAutomaticallyViewStateName = "DataBindAutomatically";
		private const string _dataCategory = "Data";
		private const string _includeRootViewStateName = "IncludeRoot";
		private const string _informationCategory = "Information";
		private const string _numberOfLevelsViewStateName = "NumberOfLevels";
		private const string _rootViewStateName = "Root";

		#endregion

		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Properties

		[Bindable(true), Category(_dataCategory), DefaultValue(null)]
		public virtual ITreeNode<T> Current
		{
			get { return (ITreeNode<T>) this.ViewState[_currentViewStateName]; }
			set
			{
				this.ViewState[_currentViewStateName] = value;
				this.OnPropertyChanged(_currentViewStateName);
			}
		}

		[Bindable(true), Category(_informationCategory), DefaultValue(false)]
		public virtual bool DataBindAutomatically
		{
			get { return ((bool?) this.ViewState[_dataBindAutomaticallyViewStateName] ?? false as bool?).Value; }
			set
			{
				this.ViewState[_dataBindAutomaticallyViewStateName] = value;
				this.OnPropertyChanged(_dataBindAutomaticallyViewStateName);
			}
		}

		public virtual ITemplate FooterTemplate { get; set; }

		protected internal virtual bool HasPropertyChangedEvents
		{
			get { return this.PropertyChanged != null; }
		}

		public virtual ITemplate HeaderTemplate { get; set; }

		[Bindable(true), Category(_informationCategory), DefaultValue(true)]
		public virtual bool IncludeRoot
		{
			get { return ((bool?) this.ViewState[_includeRootViewStateName] ?? true as bool?).Value; }
			set
			{
				this.ViewState[_includeRootViewStateName] = value;
				this.OnPropertyChanged(_includeRootViewStateName);
			}
		}

		public virtual ITemplate ItemFooterTemplate { get; set; }
		public virtual ITemplate ItemHeaderTemplate { get; set; }
		public virtual ITemplate ItemTemplate { get; set; }
		public virtual ITemplate LevelFooterTemplate { get; set; }
		public virtual ITemplate LevelHeaderTemplate { get; set; }

		[Bindable(true), Category(_informationCategory), DefaultValue(null)]
		public virtual int? NumberOfLevels
		{
			get { return (int?) this.ViewState[_numberOfLevelsViewStateName]; }
			set
			{
				this.ViewState[_numberOfLevelsViewStateName] = value;
				this.OnPropertyChanged(_numberOfLevelsViewStateName);
			}
		}

		[Bindable(true), Category(_dataCategory), DefaultValue(null)]
		public virtual ITreeNode<T> Root
		{
			get { return (ITreeNode<T>) this.ViewState[_rootViewStateName]; }
			set
			{
				this.ViewState[_rootViewStateName] = value;
				this.OnPropertyChanged(_rootViewStateName);
			}
		}

		public virtual ITemplate SelectedAncestorHeaderTemplate { get; set; }
		public virtual ITemplate SelectedAncestorTemplate { get; set; }
		public virtual ITemplate SelectedItemHeaderTemplate { get; set; }
		public virtual ITemplate SelectedItemTemplate { get; set; }

		#endregion

		#region Eventhandlers

		protected internal virtual void OnPropertyChanged(string propertyName)
		{
			if(this.HasPropertyChangedEvents)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}