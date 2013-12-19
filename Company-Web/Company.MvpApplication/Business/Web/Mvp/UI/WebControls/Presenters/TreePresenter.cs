using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Company.Collections.Generic;
using Company.MvpApplication.Business.Mvp.Models;
using Company.MvpApplication.Business.Mvp.Presenters;
using Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Models;
using Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Views;

namespace Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Presenters
{
	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	public abstract class TreePresenter<TView, TModel, T> : Presenter<TView> where TView : class, ITreeView<TModel, T> where TModel : TreeModel<T>
	{
		#region Constructors

		protected TreePresenter(TView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.CreatingChildControls += (sender, e) => this.OnViewCreatingChildControls(e);
			this.View.DataBindingChildren += (sender, e) => this.OnViewDataBindingChildren(e);
			this.View.DataBound += (sender, e) => this.OnViewDataBound(e);
			this.View.EnsuringChildControls += (sender, e) => this.OnViewEnsuringChildControls(e);
			this.View.PreRender += (sender, e) => this.OnViewPreRender(e);
		}

		#endregion

		#region Eventhandlers

		#endregion

		#region Properties

		protected internal virtual bool EnsureChildControls { get; set; }
		protected internal virtual bool ViewIsDataBound { get; set; }

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "template")]
		protected internal virtual void AddTemplate(Control container, ITemplate template)
		{
			if(container == null)
				throw new ArgumentNullException("container");

			if(template == null)
				return;

			template.InstantiateIn(container);
			this.View.Controls.Add(container);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		protected internal virtual void CreateChildControls(IEnumerable<ITreeNode<T>> items, int level)
		{
			if(items == null)
				throw new ArgumentNullException("items");

			if(level < 0 || level == int.MaxValue)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The level must be between 0 and {0}.", int.MaxValue - 1));

			// ReSharper disable PossibleMultipleEnumeration

			if(!items.Any())
				return;

			if(!this.IncludeLevel(level))
				return;

			if(level == 0 && this.View.HeaderTemplate != null)
				this.AddTemplate(this.CreateTreeNodeContainer(items.First()), this.View.HeaderTemplate);
			else if(this.View.LevelHeaderTemplate != null)
				this.AddTemplate(this.CreateTreeNodeContainer(items.First()), this.View.LevelHeaderTemplate);

			foreach(var item in items)
			{
				if(item.Equals(this.View.Current) && this.View.SelectedItemHeaderTemplate != null)
					this.AddTemplate(this.CreateTreeNodeContainer(item), this.View.SelectedItemHeaderTemplate);
				else if(this.View.SelectedAncestorHeaderTemplate != null && this.View.Current != null && this.View.Current.Ancestors.Contains(item))
					this.AddTemplate(this.CreateTreeNodeContainer(item), this.View.SelectedAncestorHeaderTemplate);
				else if(this.View.ItemHeaderTemplate != null)
					this.AddTemplate(this.CreateTreeNodeContainer(item), this.View.ItemHeaderTemplate);

				if(item.Equals(this.View.Current) && this.View.SelectedItemTemplate != null)
					this.AddTemplate(this.CreateTreeNodeContainer(item), this.View.SelectedItemTemplate);
				else if(this.View.SelectedAncestorTemplate != null && this.View.Current != null && this.View.Current.Ancestors.Contains(item))
					this.AddTemplate(this.CreateTreeNodeContainer(item), this.View.SelectedAncestorTemplate);
				else if(this.View.ItemTemplate != null)
					this.AddTemplate(this.CreateTreeNodeContainer(item), this.View.ItemTemplate);

				this.CreateChildControls(item.Children, level + 1);

				if(this.View.ItemFooterTemplate != null)
					this.AddTemplate(this.CreateTreeNodeContainer(item), this.View.ItemFooterTemplate);
			}

			if(level == 0 && this.View.FooterTemplate != null)
				this.AddTemplate(this.CreateTreeNodeContainer(items.Last()), this.View.FooterTemplate);
			else if(this.View.LevelFooterTemplate != null)
				this.AddTemplate(this.CreateTreeNodeContainer(items.Last()), this.View.LevelFooterTemplate);

			// ReSharper restore PossibleMultipleEnumeration
		}

		protected internal abstract Control CreateTreeNodeContainer(ITreeNode<T> treeNode);

		protected internal virtual bool IncludeLevel(int level)
		{
			if(!this.View.NumberOfLevels.HasValue)
				return true;

			if(this.View.NumberOfLevels.Value < 1)
				return false;

			return level < this.View.NumberOfLevels.Value;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewCreatingChildControls(CancelEventArgs e)
		{
			if(this.View.Root != null)
				this.CreateChildControls(this.View.IncludeRoot ? new[] {this.View.Root} : (IEnumerable<ITreeNode<T>>) this.View.Root.Children, 0);
		}

		protected internal virtual void OnViewDataBindingChildren(CancelEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			e.Cancel = !this.View.Visible;

			if(e.Cancel)
				return;

			this.EnsureChildControls = true;

			this.View.EnsureChildControlsAreCreated();
		}

		protected internal virtual void OnViewDataBound(EventArgs e)
		{
			this.ViewIsDataBound = true;
		}

		protected internal virtual void OnViewEnsuringChildControls(CancelEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			e.Cancel = !this.EnsureChildControls;

			this.EnsureChildControls = false;
		}

		protected internal virtual void OnViewPreRender(EventArgs e)
		{
			if(this.View.DataBindAutomatically && !this.ViewIsDataBound)
				this.View.DataBind();
		}

		#endregion
	}
}