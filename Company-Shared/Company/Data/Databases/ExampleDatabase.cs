using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Company.Data.Common;
using Company.Data.Entities;
using Company.Data.Repositories;

namespace Company.Data.Databases
{
	public class ExampleDatabase : IExampleRepository
	{
		#region Fields

		private readonly ConnectionStringSettings _connectionStringSettings;
		private readonly DbProviderFactory _databaseProviderFactory;
		private const string _exampleTableName = "tblExample";

		#endregion

		#region Constructors

		public ExampleDatabase(IDatabaseProviderFactoryRepository databaseProviderFactoryRepository, ConnectionStringSettings connectionStringSettings)
		{
			if(databaseProviderFactoryRepository == null)
				throw new ArgumentNullException("databaseProviderFactoryRepository");

			if(connectionStringSettings == null)
				throw new ArgumentNullException("connectionStringSettings");

			this._connectionStringSettings = connectionStringSettings;
			this._databaseProviderFactory = databaseProviderFactoryRepository.GetFactory(connectionStringSettings.ProviderName);
		}

		#endregion

		#region Properties

		protected internal virtual ConnectionStringSettings ConnectionStringSettings
		{
			get { return this._connectionStringSettings; }
		}

		protected internal virtual DbProviderFactory DatabaseProviderFactory
		{
			get { return this._databaseProviderFactory; }
		}

		protected internal virtual string ExampleTableName
		{
			get { return _exampleTableName; }
		}

		#endregion

		#region Methods

		protected internal virtual DbConnection CreateDatabaseConnection()
		{
			DbConnection databaseConnection = this.DatabaseProviderFactory.CreateConnection();
			// ReSharper disable PossibleNullReferenceException
			databaseConnection.ConnectionString = this.ConnectionStringSettings.ConnectionString;
			// ReSharper restore PossibleNullReferenceException
			return databaseConnection;
		}

		protected internal virtual DbParameter CreateDatabaseParameter(DbType type, string name, object value)
		{
			DbParameter parameter = this.DatabaseProviderFactory.CreateParameter();
			parameter.DbType = type;
			parameter.ParameterName = name;
			parameter.Value = value;

			return parameter;
		}

		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
		protected internal virtual string CreateSelectCommandText(IExampleItem queryFilter, out IEnumerable<DbParameter> parameters)
		{
			string whereClause = string.Empty;
			var whereClauses = this.CreateWhereClauses(queryFilter, out parameters).ToArray();

			if(whereClauses.Any())
				whereClause = " WHERE " + string.Join(" AND ", whereClauses);

			return string.Format(CultureInfo.InvariantCulture, "SELECT * FROM {0}{1};", _exampleTableName, whereClause);
		}

		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
		protected internal virtual IEnumerable<string> CreateWhereClauses(IExampleItem queryFilter, out IEnumerable<DbParameter> parameters)
		{
			var whereClauses = new List<string>();
			var parameterList = new List<DbParameter>();

			if(queryFilter != null)
			{
				if(queryFilter.Id.HasValue)
				{
					const string parameterName = "@Id";
					whereClauses.Add("Id = " + parameterName);
					parameterList.Add(this.CreateDatabaseParameter(DbType.Int32, parameterName, queryFilter.Id.Value));
				}

				if(!string.IsNullOrEmpty(queryFilter.Key))
				{
					const string parameterName = "@Key";
					whereClauses.Add("Key LIKE = " + parameterName);
					parameterList.Add(this.CreateDatabaseParameter(DbType.String, parameterName, queryFilter.Key));
				}

				if(!string.IsNullOrEmpty(queryFilter.Value))
				{
					const string parameterName = "@Value";
					whereClauses.Add("Value LIKE = " + parameterName);
					parameterList.Add(this.CreateDatabaseParameter(DbType.String, parameterName, queryFilter.Key));
				}
			}

			parameters = parameterList.ToArray();
			return whereClauses.ToArray();
		}

		public virtual bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public virtual IEnumerable<IExampleItem> Find(IExampleItem queryFilter)
		{
			IEnumerable<DbParameter> parameters;
			DbCommand databaseCommand = this.DatabaseProviderFactory.CreateCommand();
			// ReSharper disable PossibleNullReferenceException
			databaseCommand.CommandText = this.CreateSelectCommandText(queryFilter, out parameters);
			// ReSharper restore PossibleNullReferenceException
			databaseCommand.Connection = this.CreateDatabaseConnection();
			databaseCommand.Parameters.AddRange(parameters.ToArray());

			DbDataAdapter databaseDataAdapter = this.DatabaseProviderFactory.CreateDataAdapter();
			// ReSharper disable PossibleNullReferenceException
			databaseDataAdapter.SelectCommand = databaseCommand;
			// ReSharper restore PossibleNullReferenceException

			var exampleItems = new List<IExampleItem>();

			using(DataTable dataTable = new DataTable())
			{
				dataTable.Locale = CultureInfo.CurrentCulture;
				databaseDataAdapter.Fill(dataTable);

				// ReSharper disable LoopCanBeConvertedToQuery
				foreach(DataRow dataRow in dataTable.Rows)
				{
					exampleItems.Add(new ExampleItem((int) dataRow["Id"], (string) dataRow["Key"], (string) dataRow["Value"]));
				}
				// ReSharper restore LoopCanBeConvertedToQuery
			}

			return exampleItems.ToArray();
		}

		protected internal virtual bool QueryFilterIsEmpty(IExampleItem queryFilter)
		{
			if(queryFilter == null)
				return true;

			if(queryFilter.Id.HasValue)
				return false;

			if(!string.IsNullOrEmpty(queryFilter.Key))
				return false;

			if(!string.IsNullOrEmpty(queryFilter.Value))
				return false;

			return true;
		}

		public virtual void Save(IExampleItem exampleItem)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}