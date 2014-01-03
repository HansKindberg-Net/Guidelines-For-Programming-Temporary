using System;
using Company.Collections.Generic.Traversing;
using Company.Web;
using StructureMap.Configuration.DSL;

namespace Company.IoC.StructureMap
{
	[CLSCompliant(false)]
	public abstract class Registry : global::StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		protected Registry()
		{
			Register(this);
		}

		#endregion

		#region Methods

		public static void Register(IRegistry registry)
		{
			if(registry == null)
				throw new ArgumentNullException("registry");
		}

		#endregion
	}
}