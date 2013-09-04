using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Nonagon.Modular.DynamicForm.Operations;

namespace Nonagon.Modular.DynamicForm.Service
{
	[Authenticate]
	[Route("/forms")]
	public class GetForms : GetFormsOperation.Param {}
}

