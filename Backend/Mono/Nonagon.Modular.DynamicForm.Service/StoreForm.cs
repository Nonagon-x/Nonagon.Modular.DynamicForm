using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Nonagon.Modular.DynamicForm.Operations;

namespace Nonagon.Modular.DynamicForm.Service
{
	[Authenticate]
	[Route("/form/save")]
	public class StoreForm : StoreFormOperation.Param {}
}