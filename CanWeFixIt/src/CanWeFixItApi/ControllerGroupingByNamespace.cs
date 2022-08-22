
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace CanWeFixItApi.GroupingConvention
{
    public class GroupingByNamespaceConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var apiVersion = controllerNamespace.Split(".").Last().ToLower();
            controller.ApiExplorer.GroupName = apiVersion;
        }
    }
}

