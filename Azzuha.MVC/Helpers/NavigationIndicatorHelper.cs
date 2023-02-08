using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azzuha.AdminPanel.Helpers
{
public static class NavigationIndicatorHelper
{
    public static string MakeActiveClass(this IUrlHelper urlHelper, string controller,string action)
    {
        try
        {
            string result = "active";
            string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
            string methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();
            if (string.IsNullOrEmpty(controllerName)) return null;
            if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                if (methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
                {
                    return result;
                }
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }

    }

        public static string MakeParentActiveClass(this IUrlHelper urlHelper, string controller, string action)
        {
            try
            {
                string result = "menu-open";
                string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                string methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();
                if (string.IsNullOrEmpty(controllerName)) return null;
                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    if (methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
                    {
                        return result;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        public static string IsSelectedParent(this IHtmlHelper htmlHelper, string controllers, string actions, string cssClass = "menu-open")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }
    }
}
