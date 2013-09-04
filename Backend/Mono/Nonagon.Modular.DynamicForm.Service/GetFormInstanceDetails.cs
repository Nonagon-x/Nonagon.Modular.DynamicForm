using ServiceStack.ServiceHost;
using Nonagon.Modular.DynamicForm.Operations;

namespace Nonagon.Modular.DynamicForm.Service
{
	[Route("/formInstance/{FormInstanceId}")]
	public class GetFormInstanceDetails : GetFormInstanceDetailsOperation.Param {}

	[Route("/formInstanceByRef/{Reference}")]
	public class GetFormInstanceDetailsByRef : GetFormInstanceDetailsOperation.Param {}
}

