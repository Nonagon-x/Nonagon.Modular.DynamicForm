using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

using ServiceStack.OrmLite;

using Nonagon.Modular.Params;
using System;

namespace Nonagon.Modular.DynamicForm.Operations
{
	/// <summary>
	/// Get all Forms operation.
	/// </summary>
	public class GetFormsOperation : DataModuleOperation<GetFormsOperation.Param, IEnumerable<IForm>>
	{
		/// <summary>
		/// Get all forms parameter.
		/// </summary>
		public class Param : ListOperationParam<Form> {}

		public override IEnumerable<IForm> Execute(Param param)
		{
			using(var dbConnection = DbConnectionFactory.OpenDbConnection())
			{
				var ev = OrmLiteConfig.DialectProvider.ExpressionVisitor<Form>();
				ev.Where(q => q.Status != FormStatus.Deleted);

				if(param != null) {

					if(param.Predicate != null) {

						ev.Where(param.Predicate);
					}

					ev.OrderByExpression = param.OrderBy;
					ev.Limit(param.Skip, param.Take);
				}

				var forms = dbConnection.Select<Form>(ev);
				var formVersions = dbConnection.Select<FormVersion> (
					"select a.formId `FormId`, max(b.version) `Version` from form a inner join formRevision b " +
					"on a.formId = b.formId " +
					"group by a.formId").ToList ().ToDictionary (f => f.FormId, f => f.Version);

				foreach(Form form in forms)
				{
					form.Revision = new FormRevision ();
					form.Revision.Version = formVersions[form.Id];
				}

				return forms.Select (f => (IForm)f);
			}
		}
	}

	internal class FormVersion
	{
		public Int64 FormId { get; set; }
		public Int32 Version { get; set; }
	}
}