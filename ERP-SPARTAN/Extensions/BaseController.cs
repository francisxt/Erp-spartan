using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERP_SPARTAN.Extensions
{
    /// <summary>
    /// this class is a extension of controller for implement sweetalert
    /// </summary>
    public abstract class BaseController  : Controller
    {
        public void BasicNotification(string message, NotificationType type , string title = "")
        {
             TempData["notification"] = $@"Swal.fire('{title}','{message}','{type.ToString().ToLower()}')";
        }

        public string GetUserLoggedId()
        {
            if (!User.Identity.IsAuthenticated) return null;
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
    /// <summary>
    /// Return 404
    /// </summary>
    public class NotFoundView : ViewResult
    {
        public NotFoundView(string name = "NotFound")
        {
            ViewName = name;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
