using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace TechChallenge.Api;

public class ApiExplorerGroupPerVersionConvetion : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        string groupName = controller.ControllerType.Namespace?.Split('.').Last().ToLower();
        controller.ApiExplorer.GroupName = groupName;
    }
}
