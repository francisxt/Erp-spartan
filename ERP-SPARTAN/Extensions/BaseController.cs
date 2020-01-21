using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!string.IsNullOrEmpty(message)) TempData["notification"] = $@"Swal.fire('{title}','{message}','{type.ToString().ToLower()}')";
        }
    }
}
