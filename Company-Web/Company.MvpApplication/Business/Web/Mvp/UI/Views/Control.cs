using System;
using System.ComponentModel;
using Company.MvpApplication.Business.Mvp.Views;
using WebFormsMvp.Web;

namespace Company.MvpApplication.Business.Web.Mvp.UI.Views
{
	public abstract class Control : System.Web.UI.Control, IView
	{
		#region Events

		public event EventHandler CreatedChildControls;
		public event EventHandler<CancelEventArgs> CreatingChildControls;
		public event EventHandler<CancelEventArgs> DataBindingChildren;
		public event EventHandler DataBound;
		public event EventHandler DataBoundChildren;
		public event EventHandler EnsuredChildControls;
		public event EventHandler<CancelEventArgs> EnsuringChildControls;
		public event EventHandler<CancelEventArgs> PreDataBinding;

		#endregion

		#region Properties

		protected internal virtual bool EnableAutomaticDataBinding
		{
			get { return false; }
		}

		protected internal virtual bool HasCreatedChildControlsEvents
		{
			get { return this.CreatedChildControls != null; }
		}

		protected internal virtual bool HasCreatingChildControlsEvents
		{
			get { return this.CreatingChildControls != null; }
		}

		protected internal virtual bool HasDataBindingChildrenEvents
		{
			get { return this.DataBindingChildren != null; }
		}

		protected internal virtual bool HasDataBoundChildrenEvents
		{
			get { return this.DataBoundChildren != null; }
		}

		protected internal virtual bool HasDataBoundEvents
		{
			get { return this.DataBound != null; }
		}

		protected internal virtual bool HasEnsuredChildControlsEvents
		{
			get { return this.EnsuredChildControls != null; }
		}

		protected internal virtual bool HasEnsuringChildControlsEvents
		{
			get { return this.EnsuringChildControls != null; }
		}

		protected internal virtual bool HasPreDataBindingEvents
		{
			get { return this.PreDataBinding != null; }
		}

		public virtual bool ThrowExceptionIfNoPresenterBound
		{
			get { return true; }
		}

		#endregion

		#region Methods

		protected override void CreateChildControls()
		{
			CancelEventArgs e = new CancelEventArgs();

			this.OnCreatingChildControls(e);

			if(e.Cancel)
				return;

			base.CreateChildControls();

			this.OnCreatedChildControls(EventArgs.Empty);
		}

		public override void DataBind()
		{
			CancelEventArgs e = new CancelEventArgs();

			this.OnPreDataBinding(e);

			if(e.Cancel)
				return;

			base.DataBind();

			this.OnDataBound(EventArgs.Empty);
		}

		protected override void DataBindChildren()
		{
			CancelEventArgs e = new CancelEventArgs();

			this.OnDataBindingChildren(e);

			if(e.Cancel)
				return;

			base.DataBindChildren();

			this.OnDataBoundChildren(EventArgs.Empty);
		}

		protected override void EnsureChildControls()
		{
			CancelEventArgs e = new CancelEventArgs();

			this.OnEnsuringChildControls(e);

			if(e.Cancel)
				return;

			base.EnsureChildControls();

			this.OnEnsuredChildControls(EventArgs.Empty);
		}

		public virtual void EnsureChildControlsAreCreated()
		{
			this.EnsureChildControls();
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnCreatedChildControls(EventArgs e)
		{
			if(this.HasCreatedChildControlsEvents)
				this.CreatedChildControls(this, e);
		}

		protected internal virtual void OnCreatingChildControls(CancelEventArgs e)
		{
			if(this.HasCreatingChildControlsEvents)
				this.CreatingChildControls(this, e);
		}

		protected internal virtual void OnDataBindingChildren(CancelEventArgs e)
		{
			if(this.HasDataBindingChildrenEvents)
				this.DataBindingChildren(this, e);
		}

		protected internal virtual void OnDataBound(EventArgs e)
		{
			if(this.HasDataBoundEvents)
				this.DataBound(this, e);
		}

		protected internal virtual void OnDataBoundChildren(EventArgs e)
		{
			if(this.HasDataBoundChildrenEvents)
				this.DataBoundChildren(this, e);
		}

		protected internal virtual void OnEnsuredChildControls(EventArgs e)
		{
			if(this.HasEnsuredChildControlsEvents)
				this.EnsuredChildControls(this, e);
		}

		protected internal virtual void OnEnsuringChildControls(CancelEventArgs e)
		{
			if(this.HasEnsuringChildControlsEvents)
				this.EnsuringChildControls(this, e);
		}

		protected override void OnInit(EventArgs e)
		{
			PageViewHost.Register(this, this.Context, this.EnableAutomaticDataBinding);

			base.OnInit(e);
		}

		protected internal virtual void OnPreDataBinding(CancelEventArgs e)
		{
			if(this.HasPreDataBindingEvents)
				this.PreDataBinding(this, e);
		}

		#endregion
	}

	public abstract class Control<TModel> : Control, IView<TModel>
	{
		#region Properties

		public virtual TModel Model { get; set; }

		#endregion
	}
}