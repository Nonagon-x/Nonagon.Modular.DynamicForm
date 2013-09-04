using System;
using System.Linq;
using System.Collections.Generic;

using ServiceStack.OrmLite;

namespace Nonagon.Modular.DynamicForm.Operations
{
	/// <summary>
	/// The operation to store the form.
	/// </summary>
	public class StoreFormOperation : DataModuleOperation<StoreFormOperation.Param, Form>
	{
		/// <summary>
		/// Store form parameter.
		/// </summary>
		public class Param
		{
			/// <summary>
			/// Gets or sets the form to be stored.
			/// </summary>
			/// <value>The form.</value>
			public Form Form { get; set; }
		}

		public override Form Execute(Param param)
		{
			if(param == null)
				throw new ArgumentException("param");

			if(param.Form == null)
				throw new ArgumentException("param.Form");

			if(param.Form.Revision == null)
				throw new ArgumentException("param.Form.Revision");

			Form form = param.Form;

			// If this is the new form.
			if(form.Id <= 0) {
				form.CreatedDate = DateTime.UtcNow;
				form.Reference = Guid.NewGuid().ToString();
				form.Status = FormStatus.Inactive;
			}

			form.LastUpdatedDate = DateTime.Now;

			using(var dbConnection = DbConnectionFactory.OpenDbConnection())
			{
				// Check if form instance exists.
				Boolean isInstanceExists = false;
				if (form.Id > 0 && form.Revision != null && form.Revision.Id > 0) {
					var rec = dbConnection.Select<FormInstance> (
						fi => fi.FormRevisionId == form.Revision.Id);

					if (rec != null && rec.Count > 0)
						isInstanceExists = true;
				}

				using(var dbTransaction = dbConnection.BeginTransaction())
				{
					// Insert or update Form table depending on if Id presented.
					if(form.Id <= 0)
					{
						dbConnection.Insert(form);
						form.Id = dbConnection.GetLastInsertId();
					}
					else
					{
						dbConnection.Update(form);
					}

					// Insert or update FormRevision table depending on if Id presented.
					// Or Insert if there is formInstance using it.
					form.Revision.FormId = form.Id;
					form.Revision.LastUpdatedDate = form.LastUpdatedDate;

					if(form.Revision.Id <= 0 || isInstanceExists)
					{
						form.Revision.CreatedDate = DateTime.Now;
						form.Status = FormStatus.Inactive;

						if (isInstanceExists)
							form.Revision.Version += 1;

						dbConnection.Insert(form.Revision);
						form.Revision.Id = dbConnection.GetLastInsertId();

						if (isInstanceExists) {
							dbConnection.Update<FormRevision> (
								new { Status = FormStatus.Inactive }, 
								p => p.Id != form.Revision.Id && p.FormId == form.Id);
						}
					}
					else
					{
						dbConnection.Update(form.Revision);
					}

					// Manipulate FormFields.
					if(form.Revision.Fields != null)
					{
						List<Int64> currentIds = new List<Int64>();

						foreach(var field in form.Revision.Fields)
						{
							field.FormRevisionId = form.Revision.Id;

							if(field.Id <= 0)
							{
								dbConnection.Insert(field);
								field.Id = dbConnection.GetLastInsertId();
							}
							else
							{
								dbConnection.Update(field);
							}

							currentIds.Add(field.Id);
						}

						// Delete all fields that are not exists in currentIds collection.
						SqlExpressionVisitor<FormField> ev = 
							OrmLiteConfig.DialectProvider.ExpressionVisitor<FormField>();

						ev.Where(rn=> !Sql.In(rn.Id, currentIds) && rn.FormRevisionId == form.Revision.Id);
						dbConnection.Delete(ev);
					}

					dbTransaction.Commit();
				}
			}

			return form;
		}
	}
}

