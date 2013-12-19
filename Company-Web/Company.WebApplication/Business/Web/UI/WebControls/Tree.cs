using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Company.Web.UI.Extensions;

namespace Company.WebApplication.Business.Web.UI.WebControls
{
	[ParseChildren(true)]
	public class Tree : Control, INamingContainer, INotifyPropertyChanged
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
		public virtual IHierarchyData Current
		{
			get { return (IHierarchyData) this.ViewState[_currentViewStateName]; }
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

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
		public virtual ITemplate FooterTemplate { get; set; }

		protected internal virtual bool HasPropertyChangedEvents
		{
			get { return this.PropertyChanged != null; }
		}

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
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

		protected internal virtual bool IsDataBound { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
		public virtual ITemplate ItemFooterTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
		public virtual ITemplate ItemHeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
		public virtual ITemplate ItemTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
		public virtual ITemplate LevelFooterTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
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
		public virtual IHierarchyData Root
		{
			get { return (IHierarchyData) this.ViewState[_rootViewStateName]; }
			set
			{
				this.ViewState[_rootViewStateName] = value;
				this.OnPropertyChanged(_rootViewStateName);
			}
		}

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
		public virtual ITemplate SelectedAncestorHeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
		public virtual ITemplate SelectedAncestorTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
		public virtual ITemplate SelectedItemHeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(HierarchyDataContainer))]
		public virtual ITemplate SelectedItemTemplate { get; set; }

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
			this.Controls.Add(container);
		}

		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected internal virtual void CreateChildControls(IEnumerable<IHierarchyData> items, int level)
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

			if(level == 0 && this.HeaderTemplate != null)
				this.AddTemplate(new HierarchyDataContainer(items.First()), this.HeaderTemplate);
			else if(this.LevelHeaderTemplate != null)
				this.AddTemplate(new HierarchyDataContainer(items.First()), this.LevelHeaderTemplate);

			foreach(var item in items)
			{
				if(item.Equals(this.Current) && this.SelectedItemHeaderTemplate != null)
					this.AddTemplate(new HierarchyDataContainer(item), this.SelectedItemHeaderTemplate);
				else if(this.SelectedAncestorHeaderTemplate != null && this.Current != null && this.Current.GetAncestors().Contains(item))
					this.AddTemplate(new HierarchyDataContainer(item), this.SelectedAncestorHeaderTemplate);
				else if(this.ItemHeaderTemplate != null)
					this.AddTemplate(new HierarchyDataContainer(item), this.ItemHeaderTemplate);

				if(item.Equals(this.Current) && this.SelectedItemTemplate != null)
					this.AddTemplate(new HierarchyDataContainer(item), this.SelectedItemTemplate);
				else if(this.SelectedAncestorTemplate != null && this.Current != null && this.Current.GetAncestors().Contains(item))
					this.AddTemplate(new HierarchyDataContainer(item), this.SelectedAncestorTemplate);
				else if(this.ItemTemplate != null)
					this.AddTemplate(new HierarchyDataContainer(item), this.ItemTemplate);

				this.CreateChildControls(item.GetChildren().OfType<IHierarchyData>().ToArray(), level + 1);

				if(this.ItemFooterTemplate != null)
					this.AddTemplate(new HierarchyDataContainer(item), this.ItemFooterTemplate);
			}

			if(level == 0 && this.FooterTemplate != null)
				this.AddTemplate(new HierarchyDataContainer(items.Last()), this.FooterTemplate);
			else if(this.LevelFooterTemplate != null)
				this.AddTemplate(new HierarchyDataContainer(items.Last()), this.LevelFooterTemplate);

			// ReSharper restore PossibleMultipleEnumeration
		}

		protected override void CreateChildControls()
		{
			if(this.Root != null)
				this.CreateChildControls(this.IncludeRoot ? new[] {this.Root} : this.Root.GetChildren().OfType<IHierarchyData>().ToArray(), 0);
		}

		public override void DataBind()
		{
			base.DataBind();

			this.IsDataBound = true;
		}

		protected override void DataBindChildren()
		{
			if(!this.Visible)
				return;

			base.EnsureChildControls();
			base.DataBindChildren();
		}

		protected override void EnsureChildControls() {}

		protected internal virtual bool IncludeLevel(int level)
		{
			if(!this.NumberOfLevels.HasValue)
				return true;

			if(this.NumberOfLevels.Value < 1)
				return false;

			return level < this.NumberOfLevels.Value;
		}

		#endregion

		#region Eventhandlers

		protected override void OnPreRender(EventArgs e)
		{
			if(this.DataBindAutomatically && !this.IsDataBound)
				this.DataBind();

			base.OnPreRender(e);
		}

		protected internal virtual void OnPropertyChanged(string propertyName)
		{
			if(this.HasPropertyChangedEvents)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}